using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;

namespace ReseBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                activity.Locale = "sv";
                //Simulates when bot is typing so User can see it.
                var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                Activity typingBack = activity.CreateReply();
                typingBack.Type = ActivityTypes.Typing;
                await connector.Conversations.ReplyToActivityAsync(typingBack);
            

            //if (activity.Text == "Get_Started_Fb")
            //{
            //    await Conversation.SendAsync(activity, () => new Dialogs.ApplyCard());
            //}

          
                //Starts main dialoghandler
                await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
            }


            
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            
            {
                IConversationUpdateActivity iConversationUpdated = message as IConversationUpdateActivity;
                
                ConnectorClient connector = new ConnectorClient(new System.Uri(message.ServiceUrl));
                

                foreach (var member in iConversationUpdated.MembersAdded ?? System.Array.Empty<ChannelAccount>())
                {
                    // if the bot is added, then
                    if (member.Id == iConversationUpdated.Recipient.Id)
                    {
                       
                    }
                }

                
            }

                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                //
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}