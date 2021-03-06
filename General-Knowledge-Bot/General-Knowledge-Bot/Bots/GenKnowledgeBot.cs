﻿// <copyright file="GenKnowledgeBot.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot.Bots
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GeneralKnowledgeBot.Models;
    using Microsoft.ApplicationInsights;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Connector;
    using Microsoft.Bot.Schema;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    /// <summary>
    /// The class for all the bot interactions.
    /// </summary>
    public class GenKnowledgeBot : ActivityHandler
    {
        private readonly IConfiguration configuration;
        private readonly TelemetryClient telemetryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenKnowledgeBot"/> class.
        /// </summary>
        /// <param name="configuration">The configuration - accessing appsettings.json file.</param>
        /// <param name="telemetryClient">ApplicationInsights DI.</param>
        public GenKnowledgeBot(
            IConfiguration configuration,
            TelemetryClient telemetryClient)
        {
            this.configuration = configuration;
            this.telemetryClient = telemetryClient;
        }

        /// <summary>
        /// The method that gets invoked each time there is a message that is coming in.
        /// </summary>
        /// <param name="turnContext">The current turn.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            if (turnContext.Activity.Value != null && turnContext.Activity.Text == null)
            {
                string feedbackType;
                var obj = JsonConvert.DeserializeObject<Feedback>(turnContext.Activity.Value.ToString());
                if (obj.AppFeedback != null)
                {
                    feedbackType = "App Feedback";
                    await GenKBot.BroadcastTeamMessage(
                        turnContext,
                        this.configuration["MicrosoftAppId"],
                        this.configuration["MicrosoftAppPassword"],
                        this.configuration["ChannelId"],
                        feedbackType,
                        obj.AppFeedback,
                        obj.FirstName,
                        obj.EmailAddress,
                        cancellationToken).ConfigureAwait(false);

                    await GenKBot.UpdatePostFeedbackActivity(
                        turnContext,
                        this.configuration["MicrosoftAppId"],
                        this.configuration["MicrosoftAppPassword"],
                        cancellationToken).ConfigureAwait(false);
                }
                else if (obj.ResultsRelevancy != null)
                {
                    feedbackType = "Results Feedback";
                    await GenKBot.BroadcastTeamMessage(
                        turnContext,
                        this.configuration["MicrosoftAppId"],
                        this.configuration["MicrosoftAppPassword"],
                        this.configuration["ChannelId"],
                        feedbackType,
                        obj.ResultsRelevancy,
                        obj.FirstName,
                        obj.EmailAddress,
                        cancellationToken).ConfigureAwait(false);

                    await GenKBot.UpdatePostFeedbackActivity(
                        turnContext,
                        this.configuration["MicrosoftAppId"],
                        this.configuration["MicrosoftAppPassword"],
                        cancellationToken).ConfigureAwait(false);
                }
                else if (obj.QuestionForExpert != null)
                {
                    await GenKBot.BroadcastTeamMessage(
                        turnContext,
                        this.configuration["MicrosoftAppId"],
                        this.configuration["MicrosoftAppPassword"],
                        this.configuration["ChannelId"],
                        "Ask an Expert",
                        obj.QuestionForExpert,
                        obj.FirstName,
                        obj.EmailAddress,
                        cancellationToken).ConfigureAwait(false);

                    await GenKBot.UpdatePostFeedbackActivity(
                        turnContext,
                        this.configuration["MicrosoftAppId"],
                        this.configuration["MicrosoftAppPassword"],
                        cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text("Turns out I'm getting some good information...I may not be able to do anything with it right now")).ConfigureAwait(false);
                }
            }
            else
            {
                var isQuery = turnContext.Activity.Text.Trim().EndsWith('?') || turnContext.Activity.Text.Trim().EndsWith('.');
                if (isQuery)
                {
                    var uri = this.configuration["KbHost"] + this.configuration["Service"] + "/knowledgebases/" + this.configuration["KbID"] + "/generateAnswer";
                    var question = turnContext.Activity.Text;
                    var rankerType = this.configuration["RankerType"];
                    var endpointKey = this.configuration["EndpointKey"];

                    this.telemetryClient.TrackTrace("Calling QnAMaker");

                    await GenKBot.GetAnswerFromQnAResource(uri, question, endpointKey, rankerType, turnContext, cancellationToken).ConfigureAwait(false);
                }
                else if (turnContext.Activity.Text.Trim() == "Take a tour")
                {
                    await GenKBot.SendTourCarouselCard(turnContext, cancellationToken).ConfigureAwait(false);
                }
                else if (turnContext.Activity.Text.Trim() == "Take a team tour")
                {
                    await GenKBot.SendTeamTourCarouselCard(turnContext, cancellationToken).ConfigureAwait(false);
                }
                else if (turnContext.Activity.Text.Trim() == "Ask an expert")
                {
                    await GenKBot.SendAskAnExpertCard(turnContext, cancellationToken).ConfigureAwait(false);
                }
                else if (turnContext.Activity.Text.Trim() == "Share app feedback")
                {
                    await GenKBot.SendShareAppFeedbackCard(turnContext, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    await GenKBot.SendUnrecognizedInputMessage(turnContext, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// The method that gets called when the bot is first opened after installation.
        /// </summary>
        /// <param name="membersAdded">The account that has been eiter added or interacting with the bot.</param>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        protected override async Task OnMembersAddedAsync(
            IList<ChannelAccount> membersAdded,
            ITurnContext<IConversationUpdateActivity> turnContext,
            CancellationToken cancellationToken)
        {
            if (membersAdded is null)
            {
                throw new ArgumentNullException(nameof(membersAdded));
            }

            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            var teamId = turnContext.Activity.ChannelData["team"]["id"].ToString();
            var tenantId = turnContext.Activity.ChannelData["tenant"]["id"].ToString();
            var botDisplayName = this.configuration["BotDisplayName"];
            var teamName = turnContext.Activity.ChannelData["team"]["name"].ToString();

            this.telemetryClient.TrackTrace("Team members are being added");

            using (var connectorClient = new ConnectorClient(
                new Uri(turnContext.Activity?.ServiceUrl),
                this.configuration["MicrosoftAppId"],
                this.configuration["MicrosoftAppPassword"]))
            {
                foreach (var member in membersAdded)
                {
                    if (member.Id != turnContext.Activity.Recipient.Id)
                    {
                        this.telemetryClient.TrackTrace($"Welcoming the user: {member.Id}");
                        await GenKBot.SendUserWelcomeMessage(
                            connectorClient,
                            member.Id,
                            teamId,
                            tenantId,
                            turnContext.Activity.Recipient.Id,
                            botDisplayName,
                            cancellationToken).ConfigureAwait(false);
                    }
                    else
                    {
                        this.telemetryClient.TrackTrace($"Welcoming the team: {teamId}");
                        await GenKBot.SendTeamWelcomeMessage(connectorClient, teamName, teamId, botDisplayName, cancellationToken).ConfigureAwait(false);
                    }
                }
            }
        }

        /// <summary>
        /// Method which fires at the time there is a conversation update.
        /// </summary>
        /// <param name="turnContext">The turn context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        protected override async Task OnConversationUpdateActivityAsync(
            ITurnContext<IConversationUpdateActivity> turnContext,
            CancellationToken cancellationToken)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            var eventType = turnContext.Activity.ChannelData["eventType"].ToString();
            this.telemetryClient.TrackTrace($"Event has been found: {eventType}");

            if (eventType == "teamMemberAdded")
            {
                var membersAdded = turnContext.Activity.MembersAdded;
                await this.OnMembersAddedAsync(membersAdded, turnContext, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}