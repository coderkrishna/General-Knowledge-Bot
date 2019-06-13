// <copyright file="WelcomeAdaptiveCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using System.Collections.Generic;
    using System.IO;
    using GeneralKnowledgeBot.Properties;

    /// <summary>
    /// The class for the WelcomeAdaptiveCard.
    /// </summary>
    public class WelcomeAdaptiveCard
    {
        private static readonly string CardTemplate;

        /// <summary>
        /// Initializes static members of the <see cref="WelcomeAdaptiveCard"/> class.
        /// </summary>
        static WelcomeAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "WelcomeAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        /// <summary>
        /// Method that will produce the JSON string of the adaptive card.
        /// </summary>
        /// <param name="botDisplayName">The display name for the bot.</param>
        /// <returns>The JSON string for the welcome adaptive card.</returns>
        public static string GetCard(string botDisplayName)
        {
            var welcomeCardTitleText = Resource.WelcomeCardTitleText;
            var welcomeCardContentPart1 = string.Format(Resource.WelcomeCardContentPart1, botDisplayName);
            var welcomeCardContentPart2 = Resource.WelcomeCardContentPart2;
            var welcomeCardContentPart3 = Resource.WelcomeCardContentPart3;
            var bulletListItem1 = Resource.WelcomeCardBulletListItem1;
            var bulletListItem2 = Resource.WelcomeCardBulletListItem2;
            var bulletListItem3 = Resource.WelcomeCardBulletListItem3;
            var tourIntroText = Resource.TourIntroText;
            var takeATourButtonText = Resource.TakeATourButtonText;

            var variablesToValues = new Dictionary<string, string>()
            {
                { "welcomeCardTitleText", welcomeCardTitleText },
                { "welcomeCardContentPart1", welcomeCardContentPart1 },
                { "welcomeCardContentPart2", welcomeCardContentPart2 },
                { "welcomeCardContentPart3", welcomeCardContentPart3 },
                { "bulletListItem1", bulletListItem1 },
                { "bulletListItem2", bulletListItem2 },
                { "bulletListItem3", bulletListItem3 },
                { "tourIntroText", tourIntroText },
                { "takeATourButtonText", takeATourButtonText },
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