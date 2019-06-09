// <copyright file="WelcomeMessageAdaptiveCard.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using System.Collections.Generic;
    using System.IO;
    using GeneralKnowledgeBot.Properties;

    public class WelcomeMessageAdaptiveCard
    {
        public static readonly string CardTemplate;

        static WelcomeMessageAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "WelcomeMessageAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        public static string GetCard(string botName)
        {
            var welcomeCardTitleText = Resource.WelcomeCardTitleText;
            var welcomeCardContentPart1 = string.Format(Resource.WelcomeCardContentPart1, botName);
            var welcomeCardContentPart2 = Resource.WelcomeCardContentPart2;
            var bulletListItem1 = Resource.BulletListItem1;
            var bulletListItem2 = Resource.BulletListItem2;
            var bulletListItem3 = Resource.BulletListItem3;

            var variablesToValues = new Dictionary<string, string>()
            {
                { "welcomeCardTitleText", welcomeCardTitleText },
                { "welcomeCardContentPart1", welcomeCardContentPart1 },
                { "welcomeCardContentPart2", welcomeCardContentPart2 },
                { "bulletListItem1", bulletListItem1 },
                { "bulletListItem2", bulletListItem2 },
                { "bulletListItem3", bulletListItem3 }
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