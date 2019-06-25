﻿// <copyright file="IncomingAppFeedbackAdaptiveCard.cs" company="Microsoft">
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

        /// <summary>
        /// Initializes static members of the <see cref="IncomingAppFeedbackAdaptiveCard"/> class.
        /// </summary>
        static IncomingAppFeedbackAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "IncomingAppFeedbackAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        /// <summary>
        /// Method that returns the JSON string for the adaptive card.
        /// </summary>
        /// <param name="feedbackType">The feedback type.</param>
        /// <param name="appFeedback">The detailed feedback about the application.</param>
        /// <param name="personName">The name of the person that is giving the feedback.</param>
        /// <param name="personUpn">The email address of the person providing feedback - will be used in the deep link.</param>
        /// <returns>The JSON string.</returns>
        public static string GetCard(string feedbackType, string appFeedback, string personName, string personUpn)
        {
            var incomingAppFeedbackTitleText = feedbackType;
            var incomingAppFeedbackSubHeaderText = string.Format(Resource.IncomingAppFeedbackSubHeaderText, personName, feedbackType);
            var incomingAppFeedbackSubjectLine = string.Format(Resource.IncomingAppFeedbackSubjectLine, feedbackType);
            var incomingAppFeedbackDetailsText = string.Format(Resource.IncomingAppFeedbackDetailsText, appFeedback, feedbackType);
            var incomingAppFeedbackChatWithPersonButtonText = string.Format(Resource.IncomingAppFeedbackChatWithPersonButtonText, personName);

            var variablesToValues = new Dictionary<string, string>()
            {
                { "incomingAppFeedbackTitleText", incomingAppFeedbackTitleText },
                { "incomingAppFeedbackSubHeaderText", incomingAppFeedbackSubHeaderText },
                { "incomingAppFeedbackSubjectLine", incomingAppFeedbackSubjectLine },
                { "incomingAppFeedbackDetailsText", incomingAppFeedbackDetailsText },
                { "incomingAppFeedbackChatWithPersonButtonText", incomingAppFeedbackChatWithPersonButtonText },
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