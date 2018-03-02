
//Using MIT Lcense, this bot Uses BestMatchDialog from Garry Petty

using BestMatchDialog;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReseBot.Dialogs
{
    [Serializable]
    public class CommonResponsesDialog : BestMatchDialog<bool>
    {
        [BestMatch(new string[] { "Hej", "tjena", "Hallå", "Hejsan", "Hi",
            "Goddag", "Godmorgon"},
            threshold: 0.5, ignoreCase: true, ignoreNonAlphaNumericCharacters: true)]
        public async Task HandleGreeting(IDialogContext context, string messageText)
        {
            await context.PostAsync("Hej på dig! Gör en spontananökan genom att skriva 'Sök!' eller ställ en fråga.");
          
            context.Done(true);
        }

        [BestMatch(new string[] { "hejdå", "vi ses", "måste gå", "vi ses senare", "bye" },
            threshold: 0.5, ignoreCase: true, ignoreNonAlphaNumericCharacters: true)]
        public async Task HandleGoodbye(IDialogContext context, string messageText)
        {
            await context.PostAsync("Hej då!.");
            context.Done(true);
        }

        [BestMatch(new string[] { "Tack!", "tack!", "Tack", "tack", "super!", "Super!" },
            threshold: 0.5, ignoreCase: true, ignoreNonAlphaNumericCharacters: true)]
        public async Task HandleGreet(IDialogContext context, string messageText)
        {
            await context.PostAsync("Varsågod!.");
            context.Done(true);
        }

        [BestMatch(new string[] { "Lever du", "vem är du", "är du en robot" },
            threshold: 0.5, ignoreCase: true, ignoreNonAlphaNumericCharacters: false)]
        public async Task HandleBot(IDialogContext context, string messageText)
        {
            await context.PostAsync("Jag är en artificiell intelligens framtagen för att hjälpa andra! ", "Jag finns för att hjälpa andra, skapad av MW");
            context.Done(true);
        }
        public override async Task NoMatchHandler(IDialogContext context, string messageText)
        {
            context.Done(false);
        }
    }
}