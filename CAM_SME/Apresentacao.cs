using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading;

namespace CAM_SME
{
    [Activity(Theme="@style/rodrigo.Theme",MainLauncher = true,NoHistory =true, Icon = "@drawable/Logo")]
    public class Apresentacao : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Thread.Sleep(2000);

            StartActivity(typeof(Tela1_Login));
        }
    }
}

