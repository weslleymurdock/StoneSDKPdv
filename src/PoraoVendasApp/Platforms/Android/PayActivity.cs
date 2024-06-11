using Android.App;
using Android.Content;
using Android.OS;
using CommunityToolkit.Mvvm.Messaging;
using StoneSdkApp.Messages;

namespace StoneSdkApp;

[Activity(Label = "PayActivity")]
[IntentFilter(new[] { Android.Content.Intent.ActionView },
              Categories = new[] { Android.Content.Intent.CategoryBrowsable, Android.Content.Intent.CategoryDefault },
              DataScheme = "poraovendasapp",
              DataHost = "pay-response",
              AutoVerify = true
        )]
public class PayActivity : Activity
{
   
}