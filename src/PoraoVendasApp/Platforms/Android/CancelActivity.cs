using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using CommunityToolkit.Mvvm.Messaging;
using PoraoVendasApp.Messages;

namespace PoraoVendasApp;
[Activity(Label = "CancelActivity", Exported = true)]
[IntentFilter(new[] { Android.Content.Intent.ActionView },
              Categories = new[] { Android.Content.Intent.CategoryBrowsable, Android.Content.Intent.CategoryDefault },
              DataScheme = "poraovendasapp",
              DataHost = "cancel-response",
              AutoVerify = true
        )]
public class CancelActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        StrongReferenceMessenger.Default.Register<CancelMessage>(this, (r, m) =>
        {
            Android.Net.Uri.Builder uriBuilder = new Android.Net.Uri.Builder();
            uriBuilder.Authority("cancel");
            uriBuilder.Scheme("cancel-app");

            uriBuilder.AppendQueryParameter("returnscheme", "poraovendasapp");
            uriBuilder.AppendQueryParameter("atk", "atk");
            uriBuilder.AppendQueryParameter("amount", "amount");
            uriBuilder.AppendQueryParameter("editable_amount", "false"); //true/false
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
                StrongReferenceMessenger.Default.Send(new ReturnMessage(Intent!.DataString!));
            }
        }
        catch (Exception e)
        {
            StrongReferenceMessenger.Default.Send(new ReturnMessage(e.Message));
        }


    }
}
