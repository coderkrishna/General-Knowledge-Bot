// <copyright file="GenKnowledgeBot.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Bots
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using GeneralKnowledgeBot.Models;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>
    /// The class for all the bot interactions.
    /// </summary>
    public class GenKnowledgeBot : ActivityHandler
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<GenKnowledgeBot> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenKnowledgeBot"/> class.
        /// </summary>
        /// <param name="configuration">The configuration - accessing appsettings.json file.</param>
        /// <param name="logger">The logging mechanism.</param>
        public GenKnowledgeBot(IConfiguration configuration, ILogger<GenKnowledgeBot> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        /// <summary>
        /// The method that gets invoked each time there is a message that is coming in.
        /// </summary>
        /// <param name="turnContext">The current turn.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var isQuery = turnContext.Activity.Text.EndsWith('?') || turnContext.Activity.Text.EndsWith('.');
            if (isQuery)
            {
                var uri = this.configuration["KbHost"] + this.configuration["Service"] + "/knowledgebases/" + this.configuration["KbID"] + "/generateAnswer";
                var question = turnContext.Activity.Text;
                var rankerType = this.configuration["RankerType"];

                this.logger.LogInformation("Calling QnA Maker");

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(uri);
                    request.Content = new StringContent("{'question': '" + question + "', 'RankerType': '" + rankerType + "'}", Encoding.UTF8, "application/json");
                    request.Headers.Add("Authorization", "EndpointKey " + this.configuration["EndpointKey"]);

                    var response = await client.SendAsync(request);
                    var responseText = await response.Content.ReadAsStringAsync();

                    var responseModel = JsonConvert.DeserializeObject<Response>(responseText);

                    if (responseModel != null)
                    {
                        // TODO: Ensuring to have all of this logic properly encapsulated.
                        await GenKBot.SendAnswerMessage(turnContext, cancellationToken, responseModel.answers[0].answer, question);
                    }
                    else
                    {
                        await turnContext.SendActivityAsync(MessageFactory.Text("No QnA Maker answers were found."), cancellationToken);
                    }
                }
            }
            else if (turnContext.Activity.Text == "Take a tour")
            {
                await GenKBot.SendTourCarouselCard(turnContext, cancellationToken);
            }
            else
            {
                await GenKBot.SendUnrecognizedInputMessage(turnContext, cancellationToken);
            }
        }

        /// <summary>
        /// The method that gets called when the bot is first opened after installation.
        /// </summary>
        /// <param name="membersAdded">The account that has been eiter added or interacting with the bot.</param>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await GenKBot.SendUserWelcomeMessage(turnContext, cancellationToken);
                }
                else
                {
                    var botDisplayName = this.configuration["BotDisplayName"];
                    await GenKBot.SendProactiveWelcomeMessage(turnContext, cancellationToken, botDisplayName);
                }
            }
        }

        /// <summary>
        /// Method which fires at the time there is a conversation update.
        /// </summary>
        /// <param name="turnContext">The turn context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        protected override async Task OnConversationUpdateActivityAsync(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var eventType = turnContext.Activity.ChannelData["eventType"].ToString();
            this.logger.LogInformation($"Event has been found: {eventType}");

            if (eventType == "teamMemberAdded")
            {
                var membersAdded = turnContext.Activity.MembersAdded;
                await this.OnMembersAddedAsync(membersAdded, turnContext, cancellationToken);
            }
        }
    }
}