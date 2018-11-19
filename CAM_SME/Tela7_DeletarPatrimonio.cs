using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CAM_SME.Resources.Model;

namespace CAM_SME
{
    [Activity(Label = "Sistema SCAM Mobile", MainLauncher = false, Icon = "@drawable/Logo")]
    public class Tela7_DeletarPatrimonio : Activity
    {
        //Variaveis globais--------------------------------------
        EditText txtBuscarPP;
        TextView txtPP_view;
        TextView txtNome_view;
        TextView txtDescricao_view;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Tela7_DeletarPatrimonio);

            //Instancia edit text placa patrimonial
            txtBuscarPP = FindViewById<EditText>(Resource.Id.txtBuscarPP);

            //instancia btn Buscar placa patrimonial
            Button btnBuscar = FindViewById<Button>(Resource.Id.btnBuscarPatrimonio);
            btnBuscar.Click += BtnBuscar_Click;

            //instancia btn DELETAR
            Button btnDeletar = FindViewById<Button>(Resource.Id.btnDeletar);
            btnDeletar.Click += BtnDeletar_Click;   
        }

        private void BtnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath
                    (System.Environment.SpecialFolder.Personal), "Patrimonio.db3");
                //path (caminho do banco no sistema) procura o banco "Patrimonio.db3"

                var db = new SQLiteConnection(dbPath);//inicia conexão

                db.Table<Patrimonio>().Delete(x => (x.PP.Equals(txtBuscarPP.Text)));
                //funcao que deleta x.PP == txtBuscarPP.text

                txtPP_view.Text = "";
                txtNome_view.Text = "";
                txtDescricao_view.Text = "";

                Toast.MakeText(this,"Deletado com sucesso!", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath
                    (System.Environment.SpecialFolder.Personal), "Patrimonio.db3");
                //path (caminho do banco no sistema) procura o banco "Patrimonio.db3"

                var db = new SQLiteConnection(dbPath);//inicia conexão
                var dados = db.Table<Patrimonio>(); //Chama a tabela

                //verifica se o usuario/senha existem
                var Placa = dados.Where(x => (x.PP.Equals(txtBuscarPP.Text))).FirstOrDefault();
                //FirstOrDeafault faz ele retornar o primeiro elemento da sequencia, se n tiver nada na 
                //tabela login ele retorna null
                //x contem x.PP (q esta na tabela Patrimonio) .equals(txtBuscarPP.Text) tem um override
                //la no BD patrimonio pra q isso seja possivel


                //se não for nulo
                if (Placa != null)
                {
                    Toast.MakeText(this, txtBuscarPP.Text + " Localizado com sucesso", ToastLength.Short).Show();

                    String Patrimonio = Placa.ToString();//String contento o resultado da query

                    String[] Patrimonio_splited = Patrimonio.Split(' ');

                    //instancia e carrega a pp no txt buscar PP
                    txtPP_view = FindViewById<TextView>(Resource.Id.txtPP_view);
                    txtPP_view.Text = txtPP_view.Text + Patrimonio_splited[0];


                    txtNome_view = FindViewById<TextView>(Resource.Id.txtNome_view);
                    txtNome_view.Text = txtNome_view.Text + Patrimonio_splited[1];

                    txtDescricao_view = FindViewById<TextView>(Resource.Id.txtDescricao_view);
                    for (int i = 2; i < Patrimonio_splited.Length; i++)
                    {
                        txtDescricao_view.Text = txtDescricao_view.Text + Patrimonio_splited[i] + " ";
                    }
                }
                else
                {
                    Toast.MakeText(this, "Placa Patrimonial não localizada ;(", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }
    }
}