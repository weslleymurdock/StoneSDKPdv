using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using CommunityToolkit.Mvvm.Messaging;
using PoraoVendasApp.Messages;

namespace PoraoVendasApp;

[Activity(Label = "PrintActivity")]
[IntentFilter(new[] { Android.Content.Intent.ActionView },
              Categories = new[] { Android.Content.Intent.CategoryBrowsable, Android.Content.Intent.CategoryDefault },
              DataScheme = "poraovendasapp",
              DataHost = "print-response",
              AutoVerify = true
        )]
public class PrintActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        WeakReferenceMessenger.Default.Register<PrintMessage>(this, (r, m) =>
        {
            Android.Net.Uri.Builder uriBuilder = new Android.Net.Uri.Builder();
            uriBuilder.Authority("print");
            uriBuilder.Scheme("print-app");

            uriBuilder.AppendQueryParameter("SHOW_FEEDBACK_SCREEN", "200");
            uriBuilder.AppendQueryParameter("SCHEME_RETURN", "poraovendasapp");
            uriBuilder.AppendQueryParameter("PRINTABLE_CONTENT", "arquivoJSON");

            Intent i = new Intent(Intent.ActionView);
            i.AddFlags(ActivityFlags.NewTask);
            i.SetData(uriBuilder.Build());
            StartActivity(i);
        });
    }

    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent); 
        try
        {
            if (Intent!.Data != null)
            {
                WeakReferenceMessenger.Default.Send(new ReturnMessage(Intent!.DataString!));
            }
        }
        catch (Exception e)
        {
            WeakReferenceMessenger.Default.Send(new ReturnMessage(e.Message));
        }

      
    }
}