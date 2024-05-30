using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Maui;
namespace PoraoVendasApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(lifecycle =>
                { 
#if ANDROID
					lifecycle.AddAndroid(android => {
						android.OnCreate((activity, bundle) =>
						{
							var action = activity.Intent?.Action;
							var data = activity.Intent?.Data?.ToString();

							if (action == Android.Content.Intent.ActionView && data is not null)
							{
								activity.Finish();
								System.Threading.Tasks.Task.Run(() => HandleAppLink(data));
							}
						});
					});
#endif
                });
            
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            
            return builder.Build();
        }
        static void HandleAppLink(string url)
        {
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri))
            {
                App.Current?.SendOnAppLinkRequestReceived(uri);
            }
        }
    }
}
