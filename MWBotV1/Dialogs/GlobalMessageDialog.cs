
namespace ReseBot.Dialogs
{
    using Microsoft.Bot.Builder.Dialogs;
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class GlobalMessageDialog : IDialog<object>
    {
        private readonly string _messageToSend;

        public GlobalMessageDialog(string message)
        {
            _messageToSend = message;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync(_messageToSend);
            context.Done<object>(null);
        }
    }
}