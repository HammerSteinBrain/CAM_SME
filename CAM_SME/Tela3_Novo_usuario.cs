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
using System.IO;
using SQLite;
using Android;
using LoginSQLite;

namespace CAM_SME
{
    [Activity(Label = "Sistema SCAM Mobile", Icon ="@drawable/logo")]
    public class Tela3_Novo_Usuario : Activity
    {
        Button btnCriarNovoUsuario;
        EditText txtNovoUsuario;
        EditText txtSenhaNovoUsuario;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Tela3_Novo_usuario);

            btnCriarNovoUsuario = FindViewById<Button>(Resource.Id.btnRegistrar);

            txtNovoUsuario = FindViewById<EditText>(Resource.Id.txtNovoUsuario);
            txtSenhaNovoUsuario = FindViewById<EditText>(Resource.Id.txtSenhaNovoUsuario);

            btnCriarNovoUsuario.Click += BtnCriarNovoUsuario_Click;

        }

        private void BtnCriarNovoUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                //define o caminho do banco de dados
                string dbPath = Path.Combine(System.Environment.GetFolderPath
                    (System.Environment.SpecialFolder.Personal),"Usuario.db3");
                //abre o banco de dados que foi apontado acima
                var db = new SQLiteConnection(dbPath);

                //Executa um create table 'if not existes' no banco de dados
                db.CreateTable<Login>();

                //criar instancia de login
                Login tbLogin = new Login();

                //atribui o nome e a senha informados
                tbLogin.usuario = txtNovoUsuario.Text;
                tbLogin.senha = txtSenhaNovoUsuario.Text;

                //inclui na tabela
                db.Insert(tbLogin);

                Toast.MakeText(this,"Registro Incluido com Sucesso...",ToastLength.Short).Show();

            }
            catch (Exception ex)
            {
                Toast.MakeText(this,ex.ToString(),ToastLength.Short).Show();
            }
        }
    }
}