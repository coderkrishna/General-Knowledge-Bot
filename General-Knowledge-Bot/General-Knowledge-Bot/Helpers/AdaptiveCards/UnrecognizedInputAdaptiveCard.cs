// <copyright file="UnrecognizedInputAdaptiveCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers.AdaptiveCards
{
    using System.Collections.Generic;
    using System.IO;
    using GeneralKnowledgeBot.Properties;

    public class UnrecognizedInputAdaptiveCard
    {
        private static readonly string CardTemplate;

        /// <summary>
        /// Initializes static members of the <see cref="UnrecognizedInputAdaptiveCard"/> class.
        /// </summary>
        static UnrecognizedInputAdaptiveCard()
        {
            var cardJsonFilePath = Path.Combine(".", "Helpers", "AdaptiveCards", "UnrecognizedInputAdaptiveCard.json");
            CardTemplate = File.ReadAllText(cardJsonFilePath);
        }

        public static string GetCard()
        {
            var unrecognizedInputCardTitleText = Resource.UnrecognizedInputCardTitleText;
            var unrecognizedInputCardContentPart1 = Resource.UnrecognizedInputCardContentPart1;
            var unrecognizedInputCardContentPart2 = Resource.UnrecognizedInputCardContentPart2;
            var welcomeMessageButtonText = Resource.UnrecognizedInputTakeATour;
            var imageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQYdkLqRmRTPbjIOcDZd5boysXZRnJh_MuWdpJ6JgjPBU52IFAX";

            var variablesToValues = new Dictionary<string, string>()
            {
                { "unrecognizedInputCardTitleText", unrecognizedInputCardTitleText },
                { "unrecognizedInputCardContentPart1", unrecognizedInputCardContentPart1 },
                { "unrecognizedInputCardContentPart2", unrecognizedInputCardContentPart2 },
                { "welcomeMessageButtonText", welcomeMessageButtonText },
                { "imageUrl", imageUrl }
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