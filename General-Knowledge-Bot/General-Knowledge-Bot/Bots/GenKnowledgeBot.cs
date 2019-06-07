// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.3.0

namespace GeneralKnowledgeBot.Bots
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using GeneralKnowledgeBot.Helpers.AdaptiveCards;
    using GeneralKnowledgeBot.Models;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class GenKnowledgeBot : ActivityHandler
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GenKnowledgeBot> _logger;

        public GenKnowledgeBot(IConfiguration configuration, ILogger<GenKnowledgeBot> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Calling QnA Maker");

            var uri = _configuration["KbHost"] + _configuration["Service"] + "/knowledgebases/" + _configuration["KbID"] + "/generateAnswer";
            var question = turnContext.Activity.Text;

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent("{'question': '" + question + "'}", Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", "EndpointKey " + _configuration["EndpointKey"]);

                var response = await client.SendAsync(request);
                var responseText = await response.Content.ReadAsStringAsync();

                var responseModel = JsonConvert.DeserializeObject<Response>(responseText);

                if (responseModel != null)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(responseModel.answers[0].answer), cancellationToken);
                }
                else
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text("No QnA Maker answers were found."), cancellationToken);
                }
            }
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