﻿// <copyright file="GenKBot.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GeneralKnowledgeBot.Helpers;
    using GeneralKnowledgeBot.Helpers.AdaptiveCards;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using Newtonsoft.Json;

    /// <summary>
    /// This class allows for the separation of logic.
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
        public static async Task SendProactiveWelcomeMessage(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken, string botDisplayName)
        {
            var welcomeCardAttachment = Cards.CreateWelcomeCardAttachment(botDisplayName);
            await turnContext.SendActivityAsync(MessageFactory.Attachment(welcomeCardAttachment), cancellationToken);
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
                Cards.FunctionalityCard().ToAttachment(),
                Cards.AskAHumanCard().ToAttachment(),
                Cards.GiveFeedbackCard().ToAttachment(),
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
    }
}