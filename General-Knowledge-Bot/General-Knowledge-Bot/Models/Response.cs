// <copyright file="Response.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Models
{
    /// <summary>
    /// This class will be responsible to model the response that is returned
    /// after querying the QnAMaker KB
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the answers that are returned from the KB
        /// </summary>
        public Answer[] answers { get; set; }
    }
}