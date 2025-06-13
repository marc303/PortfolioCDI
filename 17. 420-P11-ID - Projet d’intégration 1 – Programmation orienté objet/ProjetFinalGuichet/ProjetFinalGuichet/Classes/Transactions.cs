using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetFinalGuichet.Classes
{
    public class Transactions
    {
        public int Id { get; set; }
        public string CompteDe { get; set; }
        public string CompteVers { get; set; }
        public int Type { get; set; }
        public string NumeroFacture { get; set; }
        public decimal Montant { get; set; }
        public string Description { get; set; }
        public DateTime DateTransaction { get; set; }
        public TypeTransaction typeTransaction { get; set; }


        private SqlDataAdapter da;

        private DataSet dsGuichet = new DataSet();

        private DataRow dr;

        public void fillDataAdapter(SqlConnection conn)
        {
            da = new SqlDataAdapter("spSelectTypeTransaction", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsGuichet, SchemaType.Mapped, "TypeTransaction");
            da.Fill(dsGuichet, "TypeTransaction");
        }

        public TypeTransaction getTypeTransaction(int typeId)
        {
            TypeTransaction typeTrans = new TypeTransaction();

            dr = dsGuichet.Tables["TypeTransaction"].Rows.Find(typeId);

            typeTrans.Id = (int)dr["Id"];
            typeTrans.Nom = dr["Nom"].ToString();

            return typeTrans;
        }
    }
}