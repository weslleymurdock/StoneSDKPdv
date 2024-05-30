using CommunityToolkit.Mvvm.Messaging;
using PoraoVendasApp.Messages;

namespace PoraoVendasApp
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            StrongReferenceMessenger.Default.Register<ReturnMessage>(this, (r, m) =>
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
            StrongReferenceMessenger.Default.Send(new PayMessage());

            SemanticScreenReader.Announce(PayBtn.Text);
        }

        private void OnCancelBtn(object sender, EventArgs e)
        {

            StrongReferenceMessenger.Default.Send(new CancelMessage("cancel"));

            SemanticScreenReader.Announce(CancelBtn.Text);
        }

        private void OnPrintBtn(object sender, EventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new PrintMessage("print"));

            SemanticScreenReader.Announce(PrintBtn.Text);
        }
    }

}
