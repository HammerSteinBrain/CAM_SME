using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CAM_SME
{
    [Activity(Label = "Sistema SCAM Mobile", MainLauncher = false, Icon = "@drawable/Logo")]
    public class Tela6_EditarPatrimonio : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Tela6_EditarPatrimonios);


        }
    }
}