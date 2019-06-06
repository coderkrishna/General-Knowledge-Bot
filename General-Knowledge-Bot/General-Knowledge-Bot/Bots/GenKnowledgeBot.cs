// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.3.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GeneralKnowledgeBot.Helpers.AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;

namespace GeneralKnowledgeBot.Bots
{
    public class GenKnowledgeBot : ActivityHandler
    {
        private readonly IConfiguration _configuration;

        public GenKnowledgeBot(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {turnContext.Activity.Text}"), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    var botDisplayName = _configuration["BotDisplayName"];
                    var welcomeCardAttachment = CreateWelcomeCardAttachment(botDisplayName);
                    await turnContext.SendActivityAsync(MessageFactory.Attachment(welcomeCardAttachment), cancellationToken);
                }
            }
        }

        private static Attachment CreateWelcomeCardAttachment(string botName)
        {
            var cardString = WelcomeMessageAdaptiveCard.GetCard(botName);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(cardString)
            };
            return adaptiveCardAttachment;
        }
    }
}