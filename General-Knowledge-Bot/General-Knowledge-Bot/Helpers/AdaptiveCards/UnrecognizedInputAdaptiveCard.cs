namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using GeneralKnowledgeBot.Properties;
    using System.IO;
    using System.Collections.Generic;

    public class UnrecognizedInputAdaptiveCard
    {
        public static string CardTemplate;

        static UnrecognizedInputAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "UnrecognizedInputAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        public static string GetCard()
        {
            var unrecognizedInputCardTitleText = Resource.UnrecognizedInputCardTitleText;
            var unrecognizedInputCardContentPart1 = Resource.UnrecognizedInputCardContentPart1;
            var unrecognizedInputCardContentPart2 = Resource.UnrecognizedInputCardContentPart2;
            var welcomeMessageButtonText = Resource.WelcomeMessageButtonText;

            var variablesToValues = new Dictionary<string, string>()
            {
                { "unrecognizedInputCardTitleText", unrecognizedInputCardTitleText },
                { "unrecognizedInputCardContentPart1", unrecognizedInputCardContentPart1 },
                { "unrecognizedInputCardContentPart2", unrecognizedInputCardContentPart2 },
                { "welcomeMessageButtonText", welcomeMessageButtonText }
            };

            var cardBody = CardTemplate;
            foreach (var kvp in variablesToValues)
            {
                cardBody = cardBody.Replace($"%{kvp.Key}%", kvp.Value);
            }

            return cardBody;
        }
    }
}