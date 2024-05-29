namespace PoraoVendasApp
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

    }
}
