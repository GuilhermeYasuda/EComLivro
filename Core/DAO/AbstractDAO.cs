using Core.Core;
using Core.Util;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DAO
{
    public abstract class AbstractDAO : IDAO
    {
        protected NpgsqlDataReader reader;
        protected NpgsqlConnection connection = Conexao.Connection;
        protected string table;
        protected string id_table;
        protected bool ctrlTransaction = true;
        protected NpgsqlCommand pst = new NpgsqlCommand();  // comentariado para não dar stackoverflow exception depois retirar
        protected NpgsqlParameter[] parameters;

        public AbstractDAO(NpgsqlConnection connection, string table, string id_table)
        {
            this.table = table;
            this.id_table = id_table;
            this.connection = connection;
        }

        public AbstractDAO(NpgsqlConnection connection, bool ctrlTransaction, string table, string id_table)
        {
            this.table = table;
            this.id_table = id_table;
            this.connection = connection;
            this.ctrlTransaction = ctrlTransaction;
        }

        protected AbstractDAO(string table, string id_table)
        {
            this.table = table;
            this.id_table = id_table;
        }

        public abstract void Salvar(EntidadeDominio entidade);

        public abstract void Alterar(EntidadeDominio entidade);

        public abstract List<EntidadeDominio> Consultar(EntidadeDominio entidade);

        /*
         * Método de exclusão genérico (exclusão do banco de dados)
         */
        public virtual void Excluir(EntidadeDominio entidade)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.CommandText = "DELETE FROM " + table + " WHERE " + id_table + " = :1";
                pst.Parameters.Clear();
                pst.Parameters.Add(new NpgsqlParameter("1", entidade.ID));
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                pst.ExecuteNonQuery();
                pst.CommandText = "COMMIT WORK";
                pst.ExecuteNonQuery();
                if (ctrlTransaction)
                    connection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
