using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content.PM;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;
using CAM_SME.Resources.Model;
using Android.Graphics;
using Android.Provider;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using CameraAppDemo;

namespace CAM_SME
{
    [Activity(Label = "Sistema SCAM Mobile", Icon = "@drawable/Logo")]
    public class Tela4_CadastrarPatrimonio : Activity
    {
        //Camera variaveis------------------
        public static Java.IO.File _file;
        public static Java.IO.File _dir;
        public static Bitmap bitmap;

        private ImageView _imageView;
        //----------------------------------

        //Definindo variaveis de instancias globais
        EditText txtNomeItem;
        EditText txtPP;
        EditText txtDescricao;
        Button btnGravar;
        //-----------------------------------------

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

                SetContentView(Resource.Layout.Tela4_CadastrarPatrimonio);


                //instancias gerais
                txtNomeItem = FindViewById<EditText>(Resource.Id.txtNomeItem);
                txtPP = FindViewById<EditText>(Resource.Id.txtPP);
                txtDescricao = FindViewById<EditText>(Resource.Id.txtDescricao);

                //instancia btn gravar
                btnGravar = FindViewById<Button>(Resource.Id.btnGravar);
                btnGravar.Click += BtnGravar_Click; //evento click btn gravar


                //recursos e instancia do btn FOTOGRAFAR
                if (IsThereAnAppToTakePictures())//verifica se tem a api de tirar fotos
                {
                    CreateDirectoryForPictures();//criacao do diretorio no sd

                    Button btnFotografar = FindViewById<Button>(Resource.Id.btnFotografar);
                    _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
                    btnFotografar.Click += TakeAPicture;
                }
            }
            catch (Exception e)
            {

                Toast.MakeText(this,e.ToString(),ToastLength.Long).Show();
            }

        }


        private void BtnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                //define o caminho do banco de dados
                string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath
                    (System.Environment.SpecialFolder.Personal), "Patrimonio.db3");
                //abre o banco de dados que foi apontado acima
                var db = new SQLiteConnection(dbPath);

                //Executa um create table 'if not existes' no banco de dados
                db.CreateTable<Patrimonio>();

                //criar instancia de login
                Patrimonio tbPatrimonio = new Patrimonio();

                //Coleta os dados
                tbPatrimonio.PP = Convert.ToInt32(txtPP.Text);
                tbPatrimonio.Nome = txtNomeItem.Text;
                tbPatrimonio.Descricao = txtDescricao.Text;

                //inclui na tabela
                db.Insert(tbPatrimonio);

                Toast.MakeText(this, "Patrimonio Incluido com Sucesso...", ToastLength.Short).Show();

                //CLEAR
                txtPP.Text ="";
                txtNomeItem.Text ="";
                txtDescricao.Text="";

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        //chama a classe bitmap, depois de transformar a foto em bit map coloca-a no imageview
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(Tela4_CadastrarPatrimonio._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display
            // Loading the full sized image will consume to much memory 
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;
            Tela4_CadastrarPatrimonio.bitmap = Tela4_CadastrarPatrimonio._file.Path.LoadAndResizeBitmap(width, height);
            if (Tela4_CadastrarPatrimonio.bitmap != null)
            {
                _imageView.SetImageBitmap(Tela4_CadastrarPatrimonio.bitmap);
                Tela4_CadastrarPatrimonio.bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
        }



        //cria um diretorio para as imagens
        private void CreateDirectoryForPictures()
        {
            Tela4_CadastrarPatrimonio._dir = new Java.IO.File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "CameraAppDemo");
            if (!Tela4_CadastrarPatrimonio._dir.Exists())
            {
                Tela4_CadastrarPatrimonio._dir.Mkdirs();
            }
        }



        //função para testar se a activity de tirar foto funciona
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);


            Tela4_CadastrarPatrimonio._file = new Java.IO.File(Tela4_CadastrarPatrimonio._dir, String.Format(txtPP.Text+".jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(Tela4_CadastrarPatrimonio._file));

            StartActivityForResult(intent, 0);
        }

    }

}
