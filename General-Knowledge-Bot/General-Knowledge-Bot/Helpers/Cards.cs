﻿// <copyright file="Cards.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Helpers
{
    using System.Collections.Generic;
    using GeneralKnowledgeBot.Properties;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// The Cards class for the tour carousel
    /// </summary>
    public static class Cards
    {
        /// <summary>
        /// Method that will generate the first card in the carousel tour
        /// </summary>
        /// <returns>The functionality card</returns>
        public static HeroCard FunctionalityCard()
        {
            var heroCard = new HeroCard()
            {
                Title = "Give Questions, Get Answers",
                Subtitle = "Functionality",
                Text = "This is the most basic functionality that I have - you as the user give me a question, and I as the bot will provide you the answer. If you want to know more about my internals, find me on GitHub 😀",
                Images = new List<CardImage>()
                {
                    new CardImage("https://s3-eu-west-1.amazonaws.com/stm-stmvalidation/uploads/20160923123038/q-and-a-icon-21627-storre-300x270.png")
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Type = ActionTypes.OpenUrl,
                        Title = "Find me on GitHub",
                        Value = "https://github.com/coderkrishna/General-Knowledge-Bot"
                    }
                }
            };

            return heroCard;
        }

        public static HeroCard AskAHumanCard()
        {
            var askAHumanCard = new HeroCard()
            {
                Title = "Ask A Human",
                Subtitle = "Escalation",
                Text = "If there is a chance that I cannot retrieve the answer to your question, I will let you know! I will also provision the option for you to escalate this, and ask a human - at which point I consult a SME (Subject Matter Expert) and hopefully with a little human ingenuity - we get the question answered",
                Images = new List<CardImage>()
                {
                    new CardImage("https://banner2.kisspng.com/20180423/eqq/kisspng-programmer-computer-programming-clip-art-bachelor-clipart-5add7deb3384b2.449284371524465131211.jpg")
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Type = ActionTypes.OpenUrl,
                        Title = "Ask A Human",
                        Value = "https://www.google.com"
                    }
                }
            };

            return askAHumanCard;
        }

        public static HeroCard GiveFeedbackCard()
        {
            var askAHumanCard = new HeroCard()
            {
                Title = "Ask A Human",
                Subtitle = "Escalation",
                Text = "If there is a chance that I cannot retrieve the answer to your question, I will let you know! I will also provision the option for you to escalate this, and ask a human - at which point I consult a SME (Subject Matter Expert) and hopefully with a little human ingenuity - we get the question answered",
                Images = new List<CardImage>()
                {
                    new CardImage("https://banner2.kisspng.com/20180423/eqq/kisspng-programmer-computer-programming-clip-art-bachelor-clipart-5add7deb3384b2.449284371524465131211.jpg")
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Type = ActionTypes.OpenUrl,
                        Title = "Ask A Human",
                        Value = "https://www.google.com"
                    }
                }
            };

            return askAHumanCard;
        }

        public static HeroCard WelcomeCard(string botName)
        {
            var welcomeCard = new HeroCard()
            {
                Title = Resource.WelcomeCardTitleText,
                Text = $"I am {botName} and I am a QnAMaker bot that can query a simple knowledge base to return answers to questions that you ask. " +
                       "If you want to know what I do, proceed to click on the button that reads <i>Take a tour</i>",
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Title = Resource.WelcomeCardBulletListItem1,
                        DisplayText = Resource.WelcomeCardBulletListItem1,
                        Type = ActionTypes.MessageBack,
                        Text = Resource.WelcomeCardBulletListItem1
                    }
                }
            };

            return welcomeCard;
        }
    }
}