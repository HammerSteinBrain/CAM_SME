using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace CAM_SME.Resources.Model
{
    public class Patrimonio
    {
        [PrimaryKey]
        public int PP { get; set; }

        [MaxLength(25)]
        public string Nome { get; set; }

        [MaxLength(50)]
        public string Descricao { get; set; }

        public override string ToString()//override no metodo ToString para forçar retorno de string
        {
            return this.PP+" "+this.Nome+" "+this.Descricao;
        }
    }
}