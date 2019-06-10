// <copyright file="GenKBot.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GeneralKnowledgeBot.Helpers.AdaptiveCards;
    using GeneralKnowledgeBot.Properties;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using Newtonsoft.Json;

    /// <summary>
    /// This class allows for the separation of logic
    /// </summary>
    public static class GenKBot
    {
        /// <summary>
        /// Having the method to send the welcome messge for the user
        /// </summary>
        /// <param name="turnContext">The turn context</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <param name="botDisplayName">The bot display name (what name will show up in Teams)</param>
        /// <returns>A unit of execution that is tracked</returns>
        public static async Task SendProactiveWelcomeMessage(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken, string botDisplayName)
        {
            var welcomeCardAttachment = CreateWelcomeCardAttachment(turnContext, botDisplayName);
            await turnContext.SendActivityAsync(MessageFactory.Attachment(welcomeCardAttachment), cancellationToken);
        }

        public static async Task SendUserWelcomeMessage(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken, string botDisplayName)
        {
            var welcomeCardAttachment = CreateWelcomeCardAttachment(turnContext, botDisplayName);
            await turnContext.SendActivityAsync(MessageFactory.Attachment(welcomeCardAttachment), cancellationToken);
        }

        public static async Task SendAnswerMessage(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken, string answer, string question)
        {
            var responseCardAttachment = CreateResponseCardAttachment(question, answer);
            await turnContext.SendActivityAsync(MessageFactory.Attachment(responseCardAttachment), cancellationToken);
        }

        public static async Task SendUnrecognizedInputMessage(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var unrecognizedCardAttachment = CreateUnrecognizedInputCardAttachment();
            await turnContext.SendActivityAsync(MessageFactory.Attachment(unrecognizedCardAttachment), cancellationToken);
        }

        private static Attachment CreateUnrecognizedInputCardAttachment()
        {
            var unrecognizedInputCardString = UnrecognizedInputAdaptiveCard.GetCard();
            var unrecognizedInputCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(unrecognizedInputCardString)
            };

            return unrecognizedInputCardAttachment;
        }

        private static Attachment CreateResponseCardAttachment(string question, string answer)
        {
            var responseCardString = ResponseAdaptiveCard.GetCard(question, answer);
            var responseCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(responseCardString)
            };

            return responseCardAttachment;
        }

        private static Attachment CreateWelcomeCardAttachment(ITurnContext<IConversationUpdateActivity> turnContext, string botName)
        {
            var welcomeHeroCard = new HeroCard()
            {
                Title = Resource.WelcomeCardTitleText,
                Text = $"I am {botName} and I am a QnAMaker bot that can query a simple knowledge base to return answers to questions that you ask. " +
                       "If you want to know what I do, proceed to click on the button that reads <i>Take a tour</i>",
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Title = Resource.WelcomeCardBulletListItem1,
                        DisplayText = Resource.WelcomeCardBulletListItem1,
                        Type = ActionTypes.MessageBack,
                        Text = Resource.WelcomeCardBulletListItem1
                    }
                }
            };

            return welcomeHeroCard.ToAttachment();
        }

        private static Attachment CreateWelcomeCardAttachment(ITurnContext<IMessageActivity> turnContext, string botName)
        {
            var welcomeHeroCard = new HeroCard()
            {
                Title = Resource.WelcomeCardTitleText,
                Text = $"I am {botName} and I am a QnAMaker bot that can query a simple knowledge base to return answers to questions that you ask." +
                       "Apart from that, I can do the following actions: <ul><li>Take a tour</li><li>Provide feedback</li><li>Ask a human</li></ul>",
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Title = Resource.WelcomeCardBulletListItem1,
                        DisplayText = Resource.WelcomeCardBulletListItem1,
                        Type = ActionTypes.MessageBack,
                        Text = Resource.WelcomeCardBulletListItem1
                    },
                    new CardAction()
                    {
                        Title = Resource.WelcomeCardBulletListItem2,
                        DisplayText = Resource.WelcomeCardBulletListItem2,
                        Type = ActionTypes.MessageBack,
                        Text = Resource.WelcomeCardBulletListItem2
                    },
                    new CardAction()
                    {
                        Title = Resource.WelcomeCardBulletListItem3,
                        DisplayText = Resource.WelcomeCardBulletListItem3,
                        Type = ActionTypes.MessageBack,
                        Text = Resource.WelcomeCardBulletListItem3
                    }
                }
            };

            return welcomeHeroCard.ToAttachment();
        }
    }
}