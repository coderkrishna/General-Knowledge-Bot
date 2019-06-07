namespace GeneralKnowledgeBot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Bot.Schema;
    using System.Threading.Tasks;
    using System.Threading;
    using Microsoft.Bot.Builder;
    using GeneralKnowledgeBot.Helpers.AdaptiveCards;
    using Newtonsoft.Json;
    using GeneralKnowledgeBot.Models;

    public static class GenKBot
    {
        public static async Task SendUserWelcomeMessage(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken, string botDisplayName)
        {
            var welcomeCardAttachment = CreateWelcomeCardAttachment(botDisplayName);
            await turnContext.SendActivityAsync(MessageFactory.Attachment(welcomeCardAttachment), cancellationToken);
        }

        public static async Task SendAnswerMessage(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken, string answer, string question)
        {
            var responseCardAttachment = CreateResponseCardAttachment(question, answer);
            await turnContext.SendActivityAsync(MessageFactory.Attachment(responseCardAttachment), cancellationToken);
        }

        private static Attachment CreateResponseCardAttachment(string question, string answer)
        {
            var responseCardString = ResponseAdaptiveCard.GetCard(question, answer);
            var responseAdaptiveCard = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(responseCardString)
            };

            return responseAdaptiveCard;
        }

        private static Attachment CreateWelcomeCardAttachment(string botName)
        {
            var cardString = WelcomeMessageAdaptiveCard.GetCard(botName);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(cardString)
            };

            return adaptiveCardAttachment;
        }
    }
}