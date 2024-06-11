using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS; 
using CommunityToolkit.Mvvm.Messaging;
using StoneSdkApp.Messages;

namespace StoneSdkApp;

[Activity(Label = "PrintActivity", Exported = true, LaunchMode = LaunchMode.SingleTop)]
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

        StrongReferenceMessenger.Default.Register<PrintMessage>(this, (r, m) =>
        {
            Android.Net.Uri.Builder uriBuilder = new Android.Net.Uri.Builder();
            uriBuilder.Authority("print");
            uriBuilder.Scheme("print-app");

            uriBuilder.AppendQueryParameter("SHOW_FEEDBACK_SCREEN", "200");
            uriBuilder.AppendQueryParameter("SCHEME_RETURN", "poraovendasapp");
            uriBuilder.AppendQueryParameter("PRINTABLE_CONTENT", "{}");

            Intent i = new Intent(Intent.ActionView);
            i.AddFlags(ActivityFlags.NewTask);
            i.SetData(uriBuilder.Build());
            StartActivity(i);
        });
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
    {
        base.OnActivityResult(requestCode, resultCode, data);
        try
        {
            if (data != null)
            {
                StrongReferenceMessenger.Default.Send(new ReturnMessage(data!.DataString!));
            }
        }
        catch (Exception e)
        {
            StrongReferenceMessenger.Default.Send(new ReturnMessage(e.Message));
        }
    }

    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);
        try
        {
            if (intent != null)
            {
                StrongReferenceMessenger.Default.Send(new ReturnMessage(intent!.DataString!));
            }
        }
        catch (Exception e)
        {
            StrongReferenceMessenger.Default.Send(new ReturnMessage(e.Message));
        }

    }
}