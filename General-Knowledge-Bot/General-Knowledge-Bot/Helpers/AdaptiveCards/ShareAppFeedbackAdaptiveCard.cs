// <copyright file="ShareAppFeedbackAdaptiveCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using GeneralKnowledgeBot.Properties;

    /// <summary>
    /// The class for generating the share app feedback adaptive card.
    /// </summary>
    public static class ShareAppFeedbackAdaptiveCard
    {
        private static readonly string CardTemplate;

        /// <summary>
        /// Initializes static members of the <see cref="ShareAppFeedbackAdaptiveCard"/> class.
        /// </summary>
#pragma warning disable CA1810 // Initialize reference type static fields inline
        static ShareAppFeedbackAdaptiveCard()
#pragma warning restore CA1810 // Initialize reference type static fields inline
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
            var shareAppFeedbackFirstNamePlaceholder = Resource.ShareAppFeedbackFirstNamePlaceholder;

            var variablesToValues = new Dictionary<string, string>()
            {
                { "shareAppFeedbackAdaptiveCardTitle", shareAppFeedbackAdaptiveCardTitle },
                { "shareAppFeedbackAdaptiveCardContent", shareAppFeedbackAdaptiveCardContent },
                { "shareAppFeedbackInputPlaceholder", shareAppFeedbackInputPlaceholder },
                { "submitFeedbackButtonText", submitFeedbackButtonText },
                { "shareAppFeedbackEmailInputPlaceholder", shareAppFeedbackEmailInputPlaceholderText },
                { "shareAppFeedbackFirstNamePlaceholder", shareAppFeedbackFirstNamePlaceholder },
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