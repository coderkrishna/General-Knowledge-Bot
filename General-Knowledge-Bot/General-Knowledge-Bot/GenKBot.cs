// <copyright file="GenKBot.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GeneralKnowledgeBot.Helpers;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Connector;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// Implements the core logic of the General Knowledge Bot.
    /// </summary>
    public static class GenKBot
    {
        /// <summary>
        /// Having the method to send the welcome messge for the user.
        /// </summary>
        /// <param name="turnContext">The turn context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="botDisplayName">The bot display name (what name will show up in Teams).</param>
        /// <returns>A unit of execution that is tracked.</returns>
        public static async Task SendTeamWelcomeMessage(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken, string botDisplayName)
        {
            var welcomeCardAttachment = Cards.CreateWelcomeUserCardAttachment(botDisplayName);
            await turnContext.SendActivityAsync(MessageFactory.Attachment(welcomeCardAttachment), cancellationToken);
        }

        /// <summary>
        /// Method to send the proactive welcome message to the user.
        /// </summary>
        /// <param name="connectorClient">The turn connector client - make sure to have the MicrosoftAppId and MicrosoftAppPassword fields filled in.</param>
        /// <param name="memberAddedId">The id of the newly added member.</param>
        /// <param name="teamId">The team id.</param>
        /// <param name="tenantId">The tenant id.</param>
        /// <param name="botId">The bot id.</param>
        /// <param name="botDisplayName">The bot display name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        public static async Task SendUserWelcomeMessage(ConnectorClient connectorClient, string memberAddedId, string teamId, string tenantId, string botId, string botDisplayName, CancellationToken cancellationToken)
        {
            var allMembers = await connectorClient.Conversations.GetConversationMembersAsync(teamId, cancellationToken);

            ChannelAccount userThatJustJoined = null;
            foreach (var m in allMembers)
            {
                // both values are 29: values
                if (m.Id == memberAddedId)
                {
                    userThatJustJoined = m;
                    break;
                }
            }

            if (userThatJustJoined != null)
            {
                var welcomeUserAdaptiveCardAttachment = Cards.CreateWelcomeUserCardAttachment(botDisplayName);
                await NotifyUser(connectorClient, userThatJustJoined, welcomeUserAdaptiveCardAttachment, botId, tenantId, cancellationToken);
            }
        }

        /// <summary>
        /// Method that will send the tour carousel.
        /// </summary>
        /// <param name="turnContext">The turn context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution that is tracked.</returns>
        public static async Task SendTourCarouselCard(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = turnContext.Activity.CreateReply();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = new List<Attachment>()
            {
                Cards.AskAQuestionCard().ToAttachment(),
                Cards.AskAnExpertCard().ToAttachment(),
                Cards.ShareFeedbackCard().ToAttachment(),
            };

            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        /// <summary>
        /// Method that will generate the adaptive card that renders the answer.
        /// </summary>
        /// <param name="turnContext">The turn context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="answer">The answer that is retrieved from the KB.</param>
        /// <param name="question">The question that the user has asked the bot.</param>
        /// <returns>A unit of execution that is tracked.</returns>
        public static async Task SendAnswerMessage(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken, string answer, string question)
        {
            var responseCardAttachment = Cards.CreateResponseCardAttachment(question, answer);
            await turnContext.SendActivityAsync(MessageFactory.Attachment(responseCardAttachment), cancellationToken);
        }

        /// <summary>
        /// Method that will generate the adaptive card which renders when there is an unrecognized input.
        /// </summary>
        /// <param name="turnContext">The turn context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution that is tracked.</returns>
        public static async Task SendUnrecognizedInputMessage(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var unrecognizedCardAttachment = Cards.CreateUnrecognizedInputCardAttachment();
            await turnContext.SendActivityAsync(MessageFactory.Attachment(unrecognizedCardAttachment), cancellationToken);
        }

        /// <summary>
        /// Method that fires to welcome a user.
        /// </summary>
        /// <param name="connectorClient">The connector client.</param>
        /// <param name="userThatJustJoined">The newly added member.</param>
        /// <param name="botId">The bot id.</param>
        /// <param name="tenantId">The tenant id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        private static async Task<bool> NotifyUser(ConnectorClient connectorClient, ChannelAccount userThatJustJoined, Attachment attachmentToSend, string botId, string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                // Ensuring that the conversation exists.
                var bot = new ChannelAccount { Id = botId };
                var conversationParameters = new ConversationParameters()
                {
                    Bot = bot,
                    Members = new List<ChannelAccount>()
                    {
                        userThatJustJoined,
                    },
                    TenantId = tenantId,
                };

                var response = await connectorClient.Conversations.CreateConversationAsync(conversationParameters, cancellationToken);
                var conversationId = response.Id;

                var activity = new Activity()
                {
                    Type = ActivityTypes.Message,
                    Attachments = new List<Attachment>()
                    {
                        attachmentToSend,
                    },
                };

                await connectorClient.Conversations.SendToConversationAsync(conversationId, activity);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}