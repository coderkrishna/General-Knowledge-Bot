// <copyright file="ShareAppFeedbackAdaptiveCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using System.Collections.Generic;
    using System.IO;
    using GeneralKnowledgeBot.Properties;

    /// <summary>
    /// The class for generating the share app feedback adaptive card.
    /// </summary>
    public class ShareAppFeedbackAdaptiveCard
    {
        private static readonly string CardTemplate;

        /// <summary>
        /// Initializes static members of the <see cref="ShareAppFeedbackAdaptiveCard"/> class.
        /// </summary>
        static ShareAppFeedbackAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "ShareAppFeedbackAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        /// <summary>
        /// Generates the JSON string for the adaptive card.
        /// </summary>
        /// <returns>A JSON string.</returns>
        public static string GetCard()
        {
            var shareAppFeedbackAdaptiveCardTitle = Resource.ShareAppFeedbackAdaptiveCardTitle;
            var shareAppFeedbackAdaptiveCardContent = Resource.ShareAppFeedbackAdaptiveCardContent;
            var shareAppFeedbackInputPlaceholder = Resource.ShareAppFeedbackInputPlaceholder;
            var submitFeedbackButtonText = Resource.SubmitFeedbackButtonText;
            var shareAppFeedbackEmailInputPlaceholderText = Resource.ShareAppFeedbackEmailInputPlaceholder;

            var variablesToValues = new Dictionary<string, string>()
            {
                { "shareAppFeedbackAdaptiveCardTitle", shareAppFeedbackAdaptiveCardTitle },
                { "shareAppFeedbackAdaptiveCardContent", shareAppFeedbackAdaptiveCardContent },
                { "shareAppFeedbackInputPlaceholder", shareAppFeedbackInputPlaceholder },
                { "submitFeedbackButtonText", submitFeedbackButtonText },
                { "shareAppFeedbackEmailInputPlaceholderText", shareAppFeedbackEmailInputPlaceholderText},
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