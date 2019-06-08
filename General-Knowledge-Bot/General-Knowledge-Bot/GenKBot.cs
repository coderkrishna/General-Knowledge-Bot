namespace GeneralKnowledgeBot
{
    using GeneralKnowledgeBot.Helpers.AdaptiveCards;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using Newtonsoft.Json;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

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

        private static Attachment CreateWelcomeCardAttachment(string botName)
        {
            var welcomeCardString = WelcomeMessageAdaptiveCard.GetCard(botName);
            var welcomeCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(welcomeCardString)
            };

            return welcomeCardAttachment;
        }
    }
}