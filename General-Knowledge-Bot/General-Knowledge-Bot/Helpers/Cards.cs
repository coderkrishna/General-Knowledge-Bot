// <copyright file="Cards.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers
{
    using System.Collections.Generic;
    using GeneralKnowledgeBot.Helpers.AdaptiveCards;
    using Microsoft.Bot.Schema;
    using Newtonsoft.Json;

    /// <summary>
    /// The Cards class for the tour carousel.
    /// </summary>
    public static class Cards
    {
        /// <summary>
        /// Method that will generate the first card in the carousel tour.
        /// </summary>
        /// <returns>Hero card gets returned.</returns>
        public static HeroCard FunctionalityCard()
        {
            var heroCard = new HeroCard()
            {
                Title = "Give Questions, Get Answers",
                Subtitle = "Functionality",
                Text = "This is the most basic functionality that I have - you as the user give me a question, and I as the bot will provide you the answer. If you want to know more about my internals, find me on GitHub 😀",
                Images = new List<CardImage>()
                {
                    new CardImage("https://s3-eu-west-1.amazonaws.com/stm-stmvalidation/uploads/20160923123038/q-and-a-icon-21627-storre-300x270.png"),
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Type = ActionTypes.OpenUrl,
                        Title = "Find me on GitHub",
                        Value = "https://github.com/coderkrishna/General-Knowledge-Bot",
                    },
                },
            };

            return heroCard;
        }

        /// <summary>
        /// Ensuring to generate the adaptive card for asking a human.
        /// </summary>
        /// <returns>Hero card gets returned.</returns>
        public static HeroCard AskAHumanCard()
        {
            var askAHumanCard = new HeroCard()
            {
                Title = "Ask A Human",
                Subtitle = "Escalation",
                Text = "If there is a chance that I cannot retrieve the answer to your question, I will let you know! I will also provision the option for you to escalate this, and ask a human - at which point I consult a SME (Subject Matter Expert) and hopefully with a little human ingenuity - we get the question answered",
                Images = new List<CardImage>()
                {
                    new CardImage("https://banner2.kisspng.com/20180423/eqq/kisspng-programmer-computer-programming-clip-art-bachelor-clipart-5add7deb3384b2.449284371524465131211.jpg"),
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Type = ActionTypes.OpenUrl,
                        Title = "Ask A Human",
                        Value = "https://www.google.com",
                    },
                },
            };

            return askAHumanCard;
        }

        /// <summary>
        /// Ensuring to generate a card for giving feedback.
        /// </summary>
        /// <returns>Hero card gets returned.</returns>
        public static HeroCard GiveFeedbackCard()
        {
            var askAHumanCard = new HeroCard()
            {
                Title = "Give Feedback",
                Subtitle = "Feedback",
                Text = "If there is anything that you want to see improve - we welcome your comments and questions",
                Images = new List<CardImage>()
                {
                    new CardImage("https://banner2.kisspng.com/20180423/eqq/kisspng-programmer-computer-programming-clip-art-bachelor-clipart-5add7deb3384b2.449284371524465131211.jpg"),
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Type = ActionTypes.OpenUrl,
                        Title = "Give Feedback",
                        Value = "https://www.google.com",
                    },
                },
            };

            return askAHumanCard;
        }

        /// <summary>
        /// Generates the welcome card which is an adaptive card.
        /// </summary>
        /// <param name="botDisplayName">The bot display name.</param>
        /// <returns>The adaptive card attachment.</returns>
        public static Attachment CreateWelcomeCardAttachment(string botDisplayName)
        {
            var welcomeCardString = WelcomeAdaptiveCard.GetCard(botDisplayName);
            var welcomeCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(welcomeCardString),
            };

            return welcomeCardAttachment;
        }

        /// <summary>
        /// Generates the adaptive card for the unrecognized input scenario.
        /// </summary>
        /// <returns>The adaptive card attachment.</returns>
        public static Attachment CreateUnrecognizedInputCardAttachment()
        {
            var unrecognizedInputCardString = UnrecognizedInputAdaptiveCard.GetCard();
            var unrecognizedInputCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(unrecognizedInputCardString),
            };

            return unrecognizedInputCardAttachment;
        }

        /// <summary>
        /// Generates the adaptive card for the response that is retrieved when the bot is asked a question by the user.
        /// </summary>
        /// <param name="question">The question that the user asks the bot.</param>
        /// <param name="answer">The response that the bot retrieves after querying the knowledge base.</param>
        /// <returns>The adaptive card attachment.</returns>
        public static Attachment CreateResponseCardAttachment(string question, string answer)
        {
            var responseCardString = ResponseAdaptiveCard.GetCard(question, answer);
            var responseCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(responseCardString),
            };

            return responseCardAttachment;
        }
    }
}