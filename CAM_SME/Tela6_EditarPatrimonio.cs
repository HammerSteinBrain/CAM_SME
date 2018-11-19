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
using SQLite;
using CAM_SME.Resources.Model;

namespace CAM_SME
{
    [Activity(Label = "Sistema SCAM Mobile", MainLauncher = false, Icon = "@drawable/Logo")]
    public class Tela6_EditarPatrimonio : Activity
    {
        //Variaveis globais--------------------------------------
        EditText txtBuscarPP;
        EditText txtEditPP;
        EditText txtEditNome;
        EditText txtEditDescricao;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Tela6_EditarPatrimonios);

            //Instancia edit text pp patrimonial
            txtBuscarPP = FindViewById<EditText>(Resource.Id.txtBuscarPP);

            //instancia btn Buscar
            Button btnBuscar = FindViewById<Button>(Resource.Id.btnBuscarPatrimonio);
            btnBuscar.Click += BtnBuscar_Click;

            //instancia btn Buscar
            Button btnEditPatrimonio = FindViewById<Button>(Resource.Id.btnEditPatrimonio);
            btnEditPatrimonio.Click += BtnEditPatrimonio_Click;
        }

        private void BtnEditPatrimonio_Click(object sender, EventArgs e)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath
                    (System.Environment.SpecialFolder.Personal), "Patrimonio.db3");
                //path (caminho do banco no sistema) procura o banco "Patrimonio.db3"

                var db = new SQLiteConnection(dbPath);//inicia conexão

                Patrimonio p = new Patrimonio()
                {
                    PP = Convert.ToInt32(txtEditPP.Text),
                    Nome = txtEditNome.Text,
                    Descricao = txtEditDescricao.Text
                };
                db.Update(p);
                Toast.MakeText(this, "Patrimonio atualizado com sucesso!", ToastLength.Long).Show();
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
                /*FirstOrDeafault faz ele retornar o primeiro elemento da sequencia, se n tiver nada na 
                tabela login ele retorna null
                x contem x.PP (q esta na tabela Patrimonio) .equals(txtBuscarPP.Text) tem um override
                la no BD Model/Patrimonio pra q isso seja possivel
                */


                //se não for nulo
                if (Placa != null)
                {
                    Toast.MakeText(this, txtBuscarPP.Text + " Localizado com sucesso", ToastLength.Short).Show();

                    String Patrimonio = Placa.ToString();//String contento o resultado da query

                    String[] Patrimonio_splited = Patrimonio.Split(' ');


                    /*
                     * Essas instancias acontecem quando a função btn_buscar é ativada
                     *Então para que elas sejam carregadas na memoria RAM é necessario
                     * que o usuario busque primeiro uma Placa Patrimonial e depois faça
                     * a alteração dela!
                     * Se a função alterar PP for ativada antes dará erro, pq na memoria
                     * não tem carregada as instancias dos txt abaixo
                     */
                    
                    //instancia e Carrega a pp la no txt buscar PP
                    txtEditPP = FindViewById<EditText>(Resource.Id.txtEditPP);
                    txtEditPP.Text = txtEditPP.Text + Patrimonio_splited[0];

                    //instancia txt edit nome
                    txtEditNome = FindViewById<EditText>(Resource.Id.txtEditNome);
                    txtEditNome.Text = txtEditNome.Text + Patrimonio_splited[1];

                    //instancia txt editar descricao
                    txtEditDescricao = FindViewById<EditText>(Resource.Id.txtEditDescricao);
                    txtEditDescricao.Text = txtEditDescricao.Text + Patrimonio_splited[2];
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