using Core.Aplicacao;
using Core.DAO;
using Dominio;
using Dominio.Cliente;
using Dominio.Livro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Adm
{
    public partial class ListaCupom : ViewGenerico
    {
        private Cupom cupom = new Cupom();

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropAtivo.DataSource = AtivoDatatable();
                dropAtivo.DataValueField = "ID";
                dropAtivo.DataTextField = "Name";
                dropAtivo.DataBind();

                dropIdTipoCupom.DataSource = TipoCupomDatatable(commands["CONSULTAR"].execute(new TipoCupom()).Entidades.Cast<TipoCupom>().ToList());
                dropIdTipoCupom.DataValueField = "ID";
                dropIdTipoCupom.DataTextField = "Name";
                dropIdTipoCupom.DataBind();

                ConstruirTabela();
            }
        }

        public static DataTable AtivoDatatable()
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(char)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 'Z';
            dr[1] = "Selecione um Status";
            data.Rows.Add(dr);

            dr = data.NewRow();
            dr[0] = 'A';
            dr[1] = "A - Ativo";
            data.Rows.Add(dr);

            dr = data.NewRow();
            dr[0] = 'I';
            dr[1] = "I - Inativo";
            data.Rows.Add(dr);

            return data;
        }

        public static DataTable TipoCupomDatatable(List<TipoCupom> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione um Tipo de Cupom";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                TipoCupom tipo = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = tipo.ID;
                dr[1] = tipo.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }

        private void ConstruirTabela()
        {
            int evade = 0;

            string GRID = "<TABLE class='table table-bordered' id='GridViewGeral' width='100%' cellspacing='0'>{0}<TBODY>{1}</TBODY></TABLE>";
            string tituloColunas = "<THEAD><tr>" +
                "<th>ID do Cupom</th>" +
                "<th>ID do Pedido</th>" +
                "<th>ID do Cliente</th>" +
                "<th>Código</th>" +
                "<th>Tipo</th>" +
                "<th>Status</th>" +
                "<th>Valor</th>" +
                //"<th>Operações</th>" +
                "</tr></THEAD>";
            tituloColunas += "<TFOOT><tr>" +
                "<th>ID do Cupom</th>" +
                "<th>ID do Pedido</th>" +
                "<th>ID do Cliente</th>" +
                "<th>Código</th>" +
                "<th>Tipo</th>" +
                "<th>Status</th>" +
                "<th>Valor</th>" +
                //"<th>Operações</th>" +
                "</tr></TFOOT>";
            string linha = "<tr>" +
                "<td>{0}</td>" +
                "<td>{1}</td>" +
                "<td>{2}</td>" +
                "<td>{3}</td>" +
                "<td>{4}</td>" +
                "<td>{5}</td>" +
                "<td>{6}</td>";
                //"<td style='text-align-last: center;'>" +
                //    "<a class='btn btn-primary' href='EstoqueLivro.aspx?idLivro={0}' title='Dar Entrada'>" +
                //        "<div class='fas fa-plus'></div></a>" +
                //    "<a class='btn btn-danger' href='EstoqueLivro.aspx?delIdLivro={0}' title='Apagar'>" +
                //        "<div class='fas fa-trash-alt'></div></a>" +
                //"</td></tr>";

            if (Convert.ToInt32(dropIdTipoCupom.SelectedValue) >= 0)
                cupom.Tipo.ID = Convert.ToInt32(dropIdTipoCupom.SelectedValue);

            if (Convert.ToInt32(dropAtivo.SelectedIndex) > 0)
                cupom.Status = dropAtivo.SelectedValue.First();

            entidades = commands["CONSULTAR"].execute(cupom).Entidades;
            try
            {
                evade = entidades.Count;
            }
            catch
            {
                evade = 0;
            }

            StringBuilder conteudo = new StringBuilder();

            for (int i = 0; i < evade; i++)
            {
                cupom = (Cupom)entidades.ElementAt(i);
                if(cupom.Tipo.ID == 1)
                {
                    conteudo.AppendFormat(linha,
                    cupom.ID,
                    cupom.IdPedido,
                    cupom.IdCliente,
                    cupom.CodigoCupom,
                    "ID: " + cupom.Tipo.ID + " - " + cupom.Tipo.Nome,
                    cupom.Status,
                    "R$ " + cupom.ValorCupom.ToString("N2")
                    );
                }
                else
                {
                    conteudo.AppendFormat(linha,
                    cupom.ID,
                    cupom.IdPedido,
                    cupom.IdCliente,
                    cupom.CodigoCupom,
                    "ID: " + cupom.Tipo.ID + " - " + cupom.Tipo.Nome,
                    cupom.Status,
                    cupom.ValorCupom * 100 + "%"
                    );
                }

            }
            string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
            divTable.InnerHtml = tabelafinal;
            cupom.ID = 0;

            // Rodapé da tabela informativo de quando foi a última vez que foi atualizada a lista
            lblRodaPeTabela.InnerText = "Lista atualizada em " + DateTime.Now.ToString();
        }

        protected void DropAtivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConstruirTabela();
        }

        protected void dropIdTipoCupom_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConstruirTabela();
        }

    }
}