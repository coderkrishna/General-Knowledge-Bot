// <copyright file="Context.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the Context navigation property.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Gets or sets a value indicating whether isContextOnly.
        /// </summary>
        [JsonProperty("isContextOnly")]
        public bool IsContextOnly { get; set; }
    }
}