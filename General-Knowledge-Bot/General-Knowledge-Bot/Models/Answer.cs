// <copyright file="Answer.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Models
{
    public class Answer
    {
        /// <summary>
        /// Gets or sets the questions
        /// </summary>
        public string[] questions { get; set; }

        /// <summary>
        /// Gets or sets the answer
        /// </summary>
        public string answer { get; set; }

        /// <summary>
        /// Gets or sets the score
        /// </summary>
        public double score { get; set; }

        /// <summary>
        /// Gets or sets the source
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// Gets or sets the context
        /// </summary>
        public Context context { get; set; }
    }
}