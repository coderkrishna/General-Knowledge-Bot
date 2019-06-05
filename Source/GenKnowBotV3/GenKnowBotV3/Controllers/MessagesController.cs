using System.Net;
using System.Net.Http;
namespace GenKnowBotV3
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            using (var connectorClient = new ConnectorClient(new Uri(activity.ServiceUrl)))
            {
                if (activity.Type == ActivityTypes.Message)
                {
                    await this.HandleMessageActivity(connectorClient, activity);
                }
                else
                {
                    await this.HandleSystemActivity(connectorClient, activity);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private async Task HandleMessageActivity(ConnectorClient connectorClient, Activity activity)
        {
            var handleMessageReply = activity.CreateReply("You hit the reply message activity");
            await connectorClient.Conversations.ReplyToActivityAsync(handleMessageReply);
        }

        private async Task HandleSystemActivity(ConnectorClient connectorClient, Activity activity)
        {
            var handleSystemReply = activity.CreateReply("System activity method hit");
            await connectorClient.Conversations.ReplyToActivityAsync(handleSystemReply);
        }
    }
}