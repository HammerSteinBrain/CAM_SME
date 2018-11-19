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
using Android;

namespace CAM_SME
{
    [Activity(Label = "Sistema SCAM Mobile", Icon = "@drawable/Logo")]
    public class Tela2_Menu : Activity
    {
        TextView txtTextoLogin;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Tela2_Menu);
            txtTextoLogin = FindViewById<TextView>(Resource.Id.txtTextoLogin);

            //pega os dados obtidos na primeira atividade e exibe no TextField
            //GetStringExtra - retorna os dados extendidos la da intent atividade2
            FindViewById<TextView>(Resource.Id.txtTextoLogin).Text =
                txtTextoLogin.Text+" : "+Intent.GetStringExtra("nome") ?? "Erro ao obter os dados";

            //instancia btn Cadastrar
            Button btnCadastrar = FindViewById<Button>(Resource.Id.btnCadastrarPatrimonio);
            btnCadastrar.Click += btnCadastra_Click;

            //instancia btn Visualizar Patrimonios
            Button btnVisualizarPatrimonios = FindViewById<Button>(Resource.Id.btnVisualizarPatrimonios);
            btnVisualizarPatrimonios.Click += BtnVisualizarPatrimonios_Click;

            //instancia btn Editar Patrimonios
            Button btnEditarPatrimonios = FindViewById<Button>(Resource.Id.btnEditarPatrimonios);
            btnEditarPatrimonios.Click += BtnEditarPatrimonios_Click;

            //instancia btn Deletar Patrimonios
            Button btnDeletarPatrimonios = FindViewById<Button>(Resource.Id.btnDeletarPatrimonios);
            btnDeletarPatrimonios.Click += BtnDeletarPatrimonios_Click;
        }

        private void BtnDeletarPatrimonios_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Tela7_DeletarPatrimonio));
        }

        private void BtnEditarPatrimonios_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Tela6_EditarPatrimonio));
        }

        private void BtnVisualizarPatrimonios_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Tela5_VisualizarPatrimonio));
        }

        private void btnCadastra_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Tela4_CadastrarPatrimonio));
        }
    }
}