namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using GeneralKnowledgeBot.Models;
    using GeneralKnowledgeBot.Properties;
    using System.Collections.Generic;
    using System.IO;

    public class ResponseAdaptiveCard
    {
        public static string ResponseCardTemplate;

        static ResponseAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "ResponseAdaptiveCard.json");
            ResponseCardTemplate = File.ReadAllText(cardJsonFilePath);
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

            var cardBody = ResponseCardTemplate;
            foreach (var kvp in variablesToValues)
            {
                cardBody = cardBody.Replace($"%{kvp.Key}%", kvp.Value);
            }

            return cardBody;
        }
    }
}