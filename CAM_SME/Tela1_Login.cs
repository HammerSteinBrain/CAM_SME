using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;
using System;
using LoginSQLite;

namespace CAM_SME
{
    [Activity(Label = "Sistema SCAM Mobile", MainLauncher = false, Icon = "@drawable/Logo")]
    public class Tela1_Login : Activity
    {
        //VARIAVEIS GLOBAIS-----------------------------
        EditText txtUsuario;
        EditText txtSenha;

        //----------------------------------------------

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Tela1_Login);

            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            Button btnCriar = FindViewById<Button>(Resource.Id.btnRegistrar);

            //instancias declaradas la em cima em variaveis globais
            txtUsuario = FindViewById<EditText>(Resource.Id.txtUsuario);
            txtSenha = FindViewById<EditText>(Resource.Id.txtSenha);

            btnLogin.Click += BtnLogin_Click;

            btnCriar.Click += BtnCriar_Click;

            CriarBancoDeDados();//Cria o banco de dados usuario.db3

            CriarBD_Patrimonio();//Cria o banco de dados Patrimonio.db3

        }

        private void BtnCriar_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(Tela3_Novo_Usuario));
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath
                    (System.Environment.SpecialFolder.Personal), "Usuario.db3");
                //path (caminho do banco no sistema) procura o banco "Usuario.db3"

                var db = new SQLiteConnection(dbPath);//inicia conexão
                var dados = db.Table<Login>(); //Chama a tabela

                //verifica se o usuario/senha existem
                var login = dados.Where(x => x.usuario == txtUsuario.Text && x.senha == txtSenha.Text).FirstOrDefault();
                //FirstOrDeafault faz ele retornar o primeiro elemento da sequencia, se n tiver nada na 
                //tabela login ele retorna null

                //se não for nulo
                if (login != null)
                {
                    Toast.MakeText(this, "Login realizado com sucesso!", ToastLength.Short).Show();

                    var atividade2 = new Intent(this, typeof(Tela2));
                    //declara a intent de enviar a var atividade2 e abrir a tela LoginActivity
                    //mas não executa a intent

                    //pega os dados digitados em txtUsuario
                    atividade2.PutExtra("nome", FindViewById<EditText>(Resource.Id.txtUsuario).Text);
                    StartActivity(atividade2);//executa a intent enviando atividade2 para a tela
                    //loginActivity e abre essa tela
                }
                else
                {
                    Toast.MakeText(this, "Nome de usuario e/ou senha invalido", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void CriarBancoDeDados()
        {
            try
            {
                //combina duas cadeias de caracteres em um caminho
                string dbPath = Path.Combine(System.Environment.GetFolderPath
                    (System.Environment.SpecialFolder.Personal), "Usuario.db3");//Cria o BD con o nome Usuario.db3

                //Cria o banco de dados se ele n existir
                var db = new SQLiteConnection(dbPath);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }

        }

        private void CriarBD_Patrimonio()
        {
            try
            {
                //combina duas cadeias de caracteres em um caminho
                string dbPath = Path.Combine(System.Environment.GetFolderPath
                    (System.Environment.SpecialFolder.Personal), "Patrimonio.db3");//Cria o BD con o nome Patrimonio.db3

                //Cria o banco de dados se ele n existir
                var db = new SQLiteConnection(dbPath);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }

        }
    }
}

