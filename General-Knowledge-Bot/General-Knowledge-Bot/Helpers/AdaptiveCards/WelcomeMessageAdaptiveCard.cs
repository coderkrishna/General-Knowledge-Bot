namespace General_Knowledge_Bot.Helpers.AdaptiveCards
{
    using General_Knowledge_Bot.Properties;
    using System.Collections.Generic;
    using System.IO;

    public class WelcomeMessageAdaptiveCard
    {
        public static string CardTemplate;

        static WelcomeMessageAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "WelcomeMessageAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        public static string GetCard()
        {
            var welcomeCardTitleText = Resource.WelcomeCardTitleText;
            var welcomeCardContentPart1 = Resource.WelcomeCardContentPart1;
            var welcomeCardContentPart2 = Resource.WelcomeCardContentPart2;

            var variablesToValues = new Dictionary<string, string>()
            {
                { "welcomeCardTitleText", welcomeCardTitleText },
                { "welcomeCardContentPart1", welcomeCardContentPart1 },
                { "welcomeCardContentPart2", welcomeCardContentPart2 }
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