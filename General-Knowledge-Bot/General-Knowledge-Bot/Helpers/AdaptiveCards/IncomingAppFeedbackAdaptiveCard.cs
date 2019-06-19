// <copyright file="IncomingAppFeedbackAdaptiveCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using System.Collections.Generic;
    using System.IO;
    using GeneralKnowledgeBot.Properties;

    /// <summary>
    /// This class will be generating the adaptive card to be sent to the General channel of
    /// the SME team relating to the application feedback.
    /// </summary>
    public class IncomingAppFeedbackAdaptiveCard
    {
        private static readonly string CardTemplate;

        static IncomingAppFeedbackAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "IncomingAppFeedbackAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        /// <summary>
        /// Method that returns the JSON string for the adaptive card.
        /// </summary>
        /// <param name="appFeedback">The detailed feedback about the application.</param>
        /// <param name="personName">The name of the person that is giving the feedback.</param>
        /// <param name="personUpn">The email address of the person providing feedback - will be used in the deep link.</param>
        /// <returns>The JSON string.</returns>
        public static string GetCard(string appFeedback, string personName, string personUpn)
        {
            var incomingAppFeedbackTitleText = Resource.IncomingAppFeedbackTitleText;
            var incomingAppFeedbackSubHeaderText = string.Format(Resource.IncomingAppFeedbackSubHeaderText, personName);
            var incomingAppFeedbackSubjectLine = Resource.IncomingAppFeedbackSubjectLine;
            var incomingAppFeedbackDetailsText = string.Format(Resource.IncomingAppFeedbackDetailsText, appFeedback);

            var variablesToValues = new Dictionary<string, string>()
            {
                { "incomingAppFeedbackTitleText", incomingAppFeedbackTitleText },
                { "incomingAppFeedbackSubHeaderText", incomingAppFeedbackSubHeaderText },
                { "incomingAppFeedbackSubjectLine", incomingAppFeedbackSubjectLine },
                { "incomingAppFeedbackDetailsText", incomingAppFeedbackDetailsText },
                { "personUpn", personUpn },
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