// <copyright file="IncomingExpertEnquiryAdaptiveCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using System.Collections.Generic;
    using System.IO;
    using GeneralKnowledgeBot.Properties;

    /// <summary>
    /// The class responsible generating the adaptive card representing asking an expert.
    /// </summary>
    public class IncomingExpertEnquiryAdaptiveCard
    {
        private static readonly string CardTemplate;

        /// <summary>
        /// Initializes static members of the <see cref="IncomingExpertEnquiryAdaptiveCard"/> class.
        /// </summary>
        static IncomingExpertEnquiryAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "IncomingExpertEnquiryAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        /// <summary>
        /// Method that would return the JSON string.
        /// </summary>
        /// <param name="feedbackType">The feedback type - in this case, it's Ask an Expert</param>
        /// <param name="questionForExpert">The question being asked to the SME team.</param>
        /// <param name="personName">The person asking the question.</param>
        /// <param name="personEmail">The email of the person who asked the question.</param>
        /// <returns>The card JSON string.</returns>
        public static string GetCard(string feedbackType, string questionForExpert, string personName, string personEmail)
        {
            var incomingExpertEnquiryTitleText = feedbackType;
            var incomingExpertEnquirySubtitleText = $"{personName} has asked a question, it's important to take a look at their inquiry!";
            var incomingExpertEnquiryQuestionText = $"Question: {questionForExpert}";
            var personUpn = personEmail;
            var incomingExpertEnquiryChatWithPersonButtonText = string.Format(Resource.IncomingAppFeedbackChatWithPersonButtonText, personName);

            var variablesToValues = new Dictionary<string, string>()
            {
                { "incomingExpertEnquiryTitleText", incomingExpertEnquiryTitleText },
                { "incomingExpertEnquirySubtitleText", incomingExpertEnquirySubtitleText },
                { "incomingExpertEnquiryQuestionText", incomingExpertEnquiryQuestionText },
                { "personUpn", personUpn },
                { "incomingExpertEnquiryChatWithPersonButtonText", incomingExpertEnquiryChatWithPersonButtonText },
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