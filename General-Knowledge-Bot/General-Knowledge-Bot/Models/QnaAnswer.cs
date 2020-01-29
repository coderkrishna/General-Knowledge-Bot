// <copyright file="QnaAnswer.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class models an answer from QnA Maker.
    /// </summary>
    public class QnaAnswer
    {
        /// <summary>
        /// Gets or sets the questions.
        /// </summary>
        [JsonProperty("questions")]
        public string[] Questions { get; set; }

        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        [JsonProperty("answer")]
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        [JsonProperty("score")]
        public double Score { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        [JsonProperty("context")]
        public Context Context { get; set; }
    }
}