using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS; 
using CommunityToolkit.Mvvm.Messaging;
using PoraoVendasApp.Messages;

namespace PoraoVendasApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", LaunchMode = LaunchMode.SingleTop, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
              Categories = new[] { Android.Content.Intent.CategoryBrowsable, Android.Content.Intent.CategoryDefault },
              DataScheme = "poraovendasapp",
              DataHost = "pay-response",
              AutoVerify = true
        )]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StrongReferenceMessenger.Default.Register<PayMessage>(this, (r, m) =>
            {
                Android.Net.Uri.Builder uriBuilder = new Android.Net.Uri.Builder();
                uriBuilder.Authority("pay");
                uriBuilder.Scheme("payment-app");

                uriBuilder.AppendQueryParameter("amount", "200");
                uriBuilder.AppendQueryParameter("editable_amount", "0"); // 1 = true and 0 = false
                uriBuilder.AppendQueryParameter("transaction_type", "credit"); //DEBIT, CREDIT, VOUCHER, INSTANT_PAYMENT, PIX
                uriBuilder.AppendQueryParameter("installment_type", "merchant"); //MERCHANT, ISSUER, NONE
                uriBuilder.AppendQueryParameter("return_scheme", "poraovendasapp");

                String order_id = "";
                if (order_id != null)
                {
                    uriBuilder.AppendQueryParameter("order_id", order_id);
                }

                String installment_count = "2";
                if (installment_count != null)
                {
                    uriBuilder.AppendQueryParameter("installment_count", installment_count); // 2 to 99
                }

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
}
