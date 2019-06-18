// <copyright file="Cards.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers
{
    using System;
    using System.Collections.Generic;
    using GeneralKnowledgeBot.Helpers.AdaptiveCards;
    using GeneralKnowledgeBot.Properties;
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
        public static HeroCard AskAQuestionCard()
        {
            var heroCard = new HeroCard()
            {
                Title = Resource.AskAQuestionCarouselCardTitle,
                Text = Resource.AskAQuestionCarouselContent,
                Images = new List<CardImage>()
                {
                    new CardImage("https://s3-eu-west-1.amazonaws.com/stm-stmvalidation/uploads/20160923123038/q-and-a-icon-21627-storre-300x270.png"),
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Type = ActionTypes.MessageBack,
                        DisplayText = Resource.AskAQuestionCarouselButtonText,
                        Title = Resource.AskAQuestionCarouselButtonText,
                        Text = "How many teams are in the NFL?",
                    },
                },
            };

            return heroCard;
        }

        /// <summary>
        /// Ensuring to generate the adaptive card for asking a human.
        /// </summary>
        /// <returns>Hero card gets returned.</returns>
        public static HeroCard AskAnExpertCard()
        {
            var askAHumanCard = new HeroCard()
            {
                Title = Resource.AskAnExpertCarouselTitle,
                Text = Resource.AskAnExpertCarouselContent,
                Images = new List<CardImage>()
                {
                    new CardImage("https://banner2.kisspng.com/20180423/eqq/kisspng-programmer-computer-programming-clip-art-bachelor-clipart-5add7deb3384b2.449284371524465131211.jpg"),
                },
            };

            return askAHumanCard;
        }

        /// <summary>
        /// Ensuring to generate a card for giving feedback.
        /// </summary>
        /// <returns>Hero card gets returned.</returns>
        public static HeroCard ShareFeedbackCard()
        {
            var shareAppFeedbackCard = new HeroCard()
            {
                Title = Resource.ShareAppFeedbackCarouselTitle,
                Text = Resource.ShareAppFeedbackCarouselContent,
                Images = new List<CardImage>()
                {
                    new CardImage("https://banner2.kisspng.com/20180423/eqq/kisspng-programmer-computer-programming-clip-art-bachelor-clipart-5add7deb3384b2.449284371524465131211.jpg"),
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Type = ActionTypes.MessageBack,
                        DisplayText = Resource.ShareAppFeedbackCarouselButtonText,
                        Title = Resource.ShareAppFeedbackCarouselButtonText,
                        Text = Resource.ShareAppFeedbackCarouselButtonText,
                    },
                },
            };

            return shareAppFeedbackCard;
        }

        /// <summary>
        /// Generates the welcome adaptive card.
        /// </summary>
        /// <param name="botDisplayName">The bot display name.</param>
        /// <returns>The adaptive card attachment.</returns>
        public static Attachment CreateWelcomeUserCardAttachment(string botDisplayName)
        {
            var welcomeCardString = WelcomeUserAdaptiveCard.GetCard(botDisplayName);
            var welcomeCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(welcomeCardString),
            };

            return welcomeCardAttachment;
        }

        /// <summary>
        /// The method that would generate the attachment for the team.
        /// </summary>
        /// <param name="botDisplayName">The bot display name.</param>
        /// <param name="teamName">The team name.</param>
        /// <returns>The attachment to be appended to the welcome message for the team.</returns>
        public static Attachment CreateWelcomeTeamCardAttachment(string botDisplayName, string teamName)
        {
            var welcomeTeamCardAttachmentString = WelcomeTeamAdaptiveCard.GetCard(botDisplayName, teamName);
            var welcomeTeamCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(welcomeTeamCardAttachmentString),
            };

            return welcomeTeamCardAttachment;
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

        /// <summary>
        /// Generates the adaptive card for the ability to share the app feedback.
        /// </summary>
        /// <returns>The adaptive card attachment.</returns>
        public static Attachment CreateShareAppFeedbackAttachment()
        {
            var shareAppFeedbackCardString = ShareAppFeedbackAdaptiveCard.GetCard();
            var shareAppFeedbackAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(shareAppFeedbackCardString),
            };

            return shareAppFeedbackAttachment;
        }

        /// <summary>
        /// Generates the Self Assign Case card in the Team welcome tour.
        /// </summary>
        /// <returns>A hero card is returned.</returns>
        public static HeroCard SelfAssignCaseCard()
        {
            var selfAssignHeroCard = new HeroCard()
            {
                Title = "Self Assign Case",
                Text = "Anytime that a user wants additional help, you can assign the incoming case to yourself only!",
            };

            return selfAssignHeroCard;
        }

        /// <summary>
        /// Generates a hero card for describing the chat with questioner in the Team welcome tour.
        /// </summary>
        /// <returns>A hero card is returned.</returns>
        public static HeroCard ChatWithQuestioner()
        {
            var chatWithQuestionerHeroCard = new HeroCard()
            {
                Title = "Chat With Questioner",
                Text = "At the time a user wants the help of a SME, a notification will popup and you can have the ability to chat with the questioner directly.",
            };

            return chatWithQuestionerHeroCard;
        }
    }
}