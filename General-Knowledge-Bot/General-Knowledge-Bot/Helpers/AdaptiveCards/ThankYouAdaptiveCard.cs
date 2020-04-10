// <copyright file="ThankYouAdaptiveCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using GeneralKnowledgeBot.Properties;

    /// <summary>
    /// The class responsible for having the Thank You adaptive card generated.
    /// </summary>
    public static class ThankYouAdaptiveCard
    {
        private static readonly string CardTemplate;

        /// <summary>
        /// Initializes static members of the <see cref="ThankYouAdaptiveCard"/> class.
        /// </summary>
#pragma warning disable CA1810 // Initialize reference type static fields inline
        static ThankYouAdaptiveCard()
#pragma warning restore CA1810 // Initialize reference type static fields inline
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "ThankYouAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        /// <summary>
        /// Method that would return the JSON string of the Thank You adaptive card.
        /// </summary>
        /// <returns>The JSON string for the adaptive card.</returns>
        public static string GetCard()
        {
            var imageUrl = "https://qulture.rocks/wp-content/uploads/2019/02/feedback.png";
            var thankYouAdaptiveCardTitleText = Resource.ThankYouAdaptiveCardTitleText;
            var thankYouAdaptiveCardContent = Resource.ThankYouAdaptiveCardContent;

            var variablesToValues = new Dictionary<string, string>()
            {
                { "thankYouAdaptiveCardTitleText", thankYouAdaptiveCardTitleText },
                { "thankYouAdaptiveCardContent", thankYouAdaptiveCardContent },
                { "imageUrl", imageUrl },
            };

            var cardBody = CardTemplate;
            foreach (var kvp in variablesToValues)
            {
                cardBody = cardBody.Replace($"%{kvp.Key}%", kvp.Value, StringComparison.InvariantCultureIgnoreCase);
            }

            return cardBody;
        }
    }
}