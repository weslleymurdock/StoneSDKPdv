using CommunityToolkit.Mvvm.Messaging;
using PoraoVendasApp.Messages;

namespace PoraoVendasApp
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<ReturnMessage>(this, (r, m) =>
            {
                OnMessageReceived(m.Value);
            });
        }

        private void OnMessageReceived(string value)
        {
            statusLbl.Text = value;
        }

        private void OnPayBtn(object sender, EventArgs e)
        {
            WeakReferenceMessenger.Default.Send("pay");

            SemanticScreenReader.Announce(PayBtn.Text);
        }

        private void OnCancelBtn(object sender, EventArgs e)
        {

            WeakReferenceMessenger.Default.Send("cancel");

            SemanticScreenReader.Announce(CancelBtn.Text);
        }

        private void OnPrintBtn(object sender, EventArgs e)
        {
            WeakReferenceMessenger.Default.Send("print");

            SemanticScreenReader.Announce(PrintBtn.Text);
        }
    }

}
