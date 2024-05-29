using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using CommunityToolkit.Mvvm.Messaging;
using PoraoVendasApp.Messages;

namespace PoraoVendasApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
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
            
            WeakReferenceMessenger.Default.Register<PayMessage>(this, (r, m) =>
            {
                OnNewIntent(Intent);
            });

        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
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

            try
            {
                if (Intent!.Data != null)
                {
                    WeakReferenceMessenger.Default.Send(new ReturnMessage(Intent!.DataString!)) ;
                }
            }
            catch (Exception e)
            {
                WeakReferenceMessenger.Default.Send(new ReturnMessage(e.Message));
            }

            Intent i = new Intent(Intent.ActionView);
            i.AddFlags(ActivityFlags.NewTask);
            i.SetData(uriBuilder.Build());
            StartActivity(i);
        }

    }
}
