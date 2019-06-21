// <copyright file="AskAnExpertAdaptiveCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using System.Collections.Generic;
    using System.IO;
    using GeneralKnowledgeBot.Properties;

    /// <summary>
    /// The class responsible for the generation of the ask an expert adaptive card.
    /// </summary>
    public class AskAnExpertAdaptiveCard
    {
        private static readonly string CardTemplate;

        /// <summary>
        /// Initializes static members of the <see cref="AskAnExpertAdaptiveCard"/> class.
        /// </summary>
        static AskAnExpertAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "AskAnExpertAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        /// <summary>
        /// Method that will return the JSON string of the adaptive card.
        /// </summary>
        /// <returns>JSON string.</returns>
        public static string GetCard()
        {
            var askAnExpertAdaptiveCardTitle = Resource.AskAnExpertAdaptiveCardTitle;
            var askAnExpertAdaptiveCardContent = Resource.AskAnExpertAdaptiveCardContent;
            var askAnExpertAdaptiveCardFirstNamePlaceholder = Resource.AskAnExpertAdaptiveCardFirstNamePlaceholder;
            var askAnExpertAdaptiveCardEmailAddressPlaceholder = Resource.AskAnExpertAdaptiveCardEmailAddressPlaceholder;
            var askAnExpertAdaptiveCardQuestionPlaceholder = Resource.AskAnExpertAdaptiveCardQuestionPlaceholder;
            var submitFeedbackButtonText = Resource.SubmitFeedbackButtonText;

            var variablesToValues = new Dictionary<string, string>()
            {
                { "askAnExpertAdaptiveCardTitle", askAnExpertAdaptiveCardTitle },
                { "askAnExpertAdaptiveCardContent", askAnExpertAdaptiveCardContent },
                { "askAnExpertAdaptiveCardFirstNamePlaceholder", askAnExpertAdaptiveCardFirstNamePlaceholder },
                { "askAnExpertAdaptiveCardEmailAddressPlaceholder", askAnExpertAdaptiveCardEmailAddressPlaceholder },
                { "askAnExpertAdaptiveCardQuestionPlaceholder", askAnExpertAdaptiveCardQuestionPlaceholder },
                { "submitFeedbackButtonText", submitFeedbackButtonText },
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