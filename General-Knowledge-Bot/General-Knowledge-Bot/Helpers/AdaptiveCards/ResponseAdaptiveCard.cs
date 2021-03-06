﻿// <copyright file="ResponseAdaptiveCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using GeneralKnowledgeBot.Properties;

    /// <summary>
    /// The class to build the adaptive card for the response.
    /// </summary>
    public static class ResponseAdaptiveCard
    {
        private static readonly string CardTemplate;

        /// <summary>
        /// Initializes static members of the <see cref="ResponseAdaptiveCard"/> class.
        /// </summary>
#pragma warning disable CA1810 // Initialize reference type static fields inline
        static ResponseAdaptiveCard()
#pragma warning restore CA1810 // Initialize reference type static fields inline
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "ResponseAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        /// <summary>
        /// Creates the necessary JSON string for the adaptive card to be shown in the response.
        /// </summary>
        /// <param name="question">The question that is asked from the user to the bot.</param>
        /// <param name="answer">The answer that is returned after the query against QnAMaker is made.</param>
        /// <returns>A JSON string.</returns>
        public static string GetCard(string question, string answer)
        {
            var questionLineText = string.Format(CultureInfo.InvariantCulture, Resource.QuestionLineText, question);
            var responseCardTitleText = Resource.ResponseCardTitleText;
            var answerLineText = string.Format(CultureInfo.InvariantCulture, Resource.AnswerLineText, answer);
            var viewFullArticleButtonText = Resource.ViewFullArticleButtonText;
            var viewRelatedArticlesButtonText = Resource.ViewRelatedArticlesButtonText;
            var giveFeedbackButtonText = Resource.GiveFeedbackButtonText;
            var feedbackQuestionText = Resource.FeedbackQuestionText;
            var submitFeedbackButtonText = Resource.SubmitFeedbackButtonText;

            var variablesToValues = new Dictionary<string, string>()
            {
                { "responseCardTitleText", responseCardTitleText },
                { "questionLineText", questionLineText },
                { "answerLineText", answerLineText },
                { "viewFullArticleButtonText", viewFullArticleButtonText },
                { "viewRelatedArticlesButtonText", viewRelatedArticlesButtonText },
                { "giveFeedbackButtonText", giveFeedbackButtonText },
                { "feedbackQuestionText", feedbackQuestionText },
                { "submitFeedbackButtonText", submitFeedbackButtonText },
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