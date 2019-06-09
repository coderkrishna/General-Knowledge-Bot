// <copyright file="ResponseAdaptiveCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using System.Collections.Generic;
    using System.IO;
    using GeneralKnowledgeBot.Models;
    using GeneralKnowledgeBot.Properties;

    public class ResponseAdaptiveCard
    {
        private static string cardTemplate;

        static ResponseAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "ResponseAdaptiveCard.json");
            cardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        public static string GetCard(string question, string answer)
        {
            var questionLineText = string.Format(Resource.QuestionLineText, question);
            var responseCardTitleText = Resource.ResponseCardTitleText;
            var answerLineText = string.Format(Resource.AnswerLineText, answer);

            var variablesToValues = new Dictionary<string, string>()
            {
                { "responseCardTitleText", responseCardTitleText },
                { "questionLineText", questionLineText },
                { "answerLineText", answerLineText }
            };

            var cardBody = cardTemplate;
            foreach (var kvp in variablesToValues)
            {
                cardBody = cardBody.Replace($"%{kvp.Key}%", kvp.Value);
            }

            return cardBody;
        }
    }
}