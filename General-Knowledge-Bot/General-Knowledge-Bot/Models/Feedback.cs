// <copyright file="Feedback.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Models
{
    /// <summary>
    /// The feedback data model.
    /// </summary>
    public class Feedback
    {
        /// <summary>
        /// Gets or sets the application feedback.
        /// </summary>
        public string AppFeedback { get; set; }

        /// <summary>
        /// Gets or sets the results relevancy.
        /// </summary>
        public string ResultsRelevancy { get; set; }

        /// <summary>
        /// Gets or sets the question for the expert being asked by the user.
        /// </summary>
        public string QuestionForExpert { get; set; }

        /// <summary>
        /// Gets or sets the first name of the person providing the feedback.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the person providing the feedback.
        /// </summary>
        public string EmailAddress { get; set; }
    }
}