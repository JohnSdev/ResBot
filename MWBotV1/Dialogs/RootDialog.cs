using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System.Web;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//Resbot

namespace ReseBot.Dialogs
{

    [LuisModel("", "")] //Add yout Luis model KEY and subscription key here
    [Serializable]
    public class RootDialog : LuisDialog<object>

    {
        //[Template(TemplateUsage.Navigation, "Vad vill du ändra? {*}")] Test only

        // CONSTANTS        
        // Entity
        public string startstation = "";
        public string endstation = ""; 

        // Intents
        public const string Intent_None = "None";

        public RootDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"],
            ConfigurationManager.AppSettings["LuisAPIKey"],
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        { }



        [LuisIntent("Deleteuser")]
        public async Task Reminder(IDialogContext context, LuisResult result)
        {
            
            context.Reset();
            context.ConversationData.Clear();
            context.UserData.Clear();
            context.PrivateConversationData.Clear();
            await context.FlushAsync(CancellationToken.None);
        }

        [LuisIntent("SL")]
        public async Task slBot(IDialogContext context, LuisResult result)
        {
            //Gets the quesry and splits/formats the string to get start and stop sites. 
            var valuesEntity = result.Query.ToString();              
            string s = "";
            s = valuesEntity.ToString();

            string[] words = s.Split(' ');
            string placeA = words[1];
            string placeB = words[2];

            this.startstation = placeA;
            this.endstation = placeB;

            //First API Call to get Site ID from name
            //Item 1 is Name and Item2 is the SiteID Needed for the next API call.
            var start = Sl.slPlace(placeA);

            var end = Sl.slPlace(placeB);

            //2ns API call to get route from A to B
            var route = Sl.slRoute(start.Item2.ToString(), end.Item2.ToString(), end.Item1.ToString());
            //To Json object/class
            var routedata = (JObject)JsonConvert.DeserializeObject(route.Item1);
            var lista = new List<JToken>();
            var lista2 = new List<string>();
            lista.Add(routedata["Trip"][0]["LegList"]["Leg"]);

            //Define parameters from result
            Console.WriteLine(routedata);
            var newjson = JsonConvert.DeserializeObject<RootObject>(route.Item1);

            string endStation = newjson.Trip[0].LegList.Leg[0].Destination.name.ToString();

            string endStationTime = newjson.Trip[0].LegList.Leg[0].Destination.time.ToString();

            string startStation = newjson.Trip[0].LegList.Leg[0].Origin.name.ToString();

            string startVehicle = newjson.Trip[0].LegList.Leg[0].name.ToString();

            string startTime = newjson.Trip[0].LegList.Leg[0].Origin.time.ToString();

            await context.PostAsync(startTime + " " + startVehicle + " > " + startStation);

            await this.SlButtons(context, result, endStation, endStationTime);

        }

        //Same as SL above, but prits a detailed route.
        [LuisIntent("Details")]
        public async Task Detalis(IDialogContext context, LuisResult result)
        {


            string placeA = this.startstation;
            string placeB = this.endstation;

            //Item 1 is Name and Item2 is the SiteID Needed for the next API call.
            var start = Sl.slPlace(placeA);

            var end = Sl.slPlace(placeB);

            var route = Sl.slRoute(start.Item2.ToString(), end.Item2.ToString(), end.Item1.ToString());
            var routedata = (JObject)JsonConvert.DeserializeObject(route.Item1);
            var lista = new List<JToken>();
            var lista2 = new List<string>();
            lista.Add(routedata["Trip"][0]["LegList"]["Leg"]);

            Console.WriteLine(routedata);
            var newjson = JsonConvert.DeserializeObject<RootObject>(route.Item1);
            string endStation = newjson.Trip[0].LegList.Leg[0].Destination.name.ToString();
            string endStationTime = newjson.Trip[0].LegList.Leg[0].Destination.time.ToString();

       

            try
            {

                JToken byte1 = Sl.formatter(lista[0][0]);
                await context.PostAsync(byte1.ToString());
                JToken byte2 = Sl.formatter(lista[0][1]);
                await context.PostAsync(byte2.ToString());
                JToken byte3 = Sl.formatter(lista[0][2]);
                await context.PostAsync(byte3.ToString());
                JToken byte4 = Sl.formatter(lista[0][3]);
                await context.PostAsync(byte4.ToString());
                JToken byte5 = Sl.formatter(lista[0][4]);
                await context.PostAsync(byte5.ToString());
                JToken byte6 = Sl.formatter(lista[0][5]);
                await context.PostAsync(byte6.ToString());
                JToken byte7 = Sl.formatter(lista[0][6]);
                await context.PostAsync(byte7.ToString());

            }
            catch (Exception)
            {

            }
            await this.SlButtons(context, result, endStation, endStationTime);

        }

        //Prints Facebook qick reply
        public async Task SlButtons(IDialogContext context, LuisResult result, string endStation, string endStationTime)
        {

            var reply = context.MakeMessage();
            reply.Text = $"Frame vid { endStation} kl { endStationTime}";

            if (reply.ChannelId.Equals("facebook", StringComparison.InvariantCultureIgnoreCase))
            {
                var channelData = JObject.FromObject(new
                {
                    quick_replies = new dynamic[]
                    {
                        new
                        {
                            content_type = "text",
                            title = "Nästa",
                            payload = "Nästa",
                            image_url = "https://cdn3.iconfinder.com/data/icons/developperss/PNG/Blue%20Ball.png"
                        },
                        new
                        {
                            content_type = "text",
                            title = "Detaljer",
                            payload = "Details",
                            image_url = "https://cdn3.iconfinder.com/data/icons/developperss/PNG/Green%20Ball.png"
                        }
                        
                   
                    }
                });
                reply.ChannelData = channelData;
            }

            await context.PostAsync(reply);
            //context.Wait(this.OnColorPicked);

        }


        //Chat responses for simple conversation 
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            var dialoge = new CommonResponsesDialog();
            dialoge.InitialMessage = result.Query;
            context.Call(dialoge, AfterCommonResponseHandled);
        }

        private async Task AfterCommonResponseHandled(IDialogContext context, IAwaitable<bool> result)
        {
            var messageHandled = await result;

            if (!messageHandled)
            {
                await context.PostAsync("Förlåt men jag förstår inte, kan du formulera om dig? ");
            }

            context.Wait(MessageReceived);
        }
    }
}















