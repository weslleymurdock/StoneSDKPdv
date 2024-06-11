namespace StoneSdkApp
{
    public partial class App : Application
    {

        public static string StartupData { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        public void HandleAppLink(string data)
        {
            StartupData = data;
        }
        protected override async void OnAppLinkRequestReceived(Uri uri)
        {
            base.OnAppLinkRequestReceived(uri);

            // Show an alert to test that the app link was received.
            await Dispatcher.DispatchAsync(async () =>
            {
                await Windows[0].Page!.DisplayAlert("App link received", uri.ToString(), "OK");
            });

            Console.WriteLine("App link: " + uri.ToString());
        }

    }
}
