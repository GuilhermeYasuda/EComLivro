using Core.Aplicacao;
using Core.DAO;
using Dominio;
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
    public partial class ListaEstoque : ViewGenerico
    {
        private Estoque estoque = new Estoque();

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropAtivo.DataSource = AtivoDatatable();
                dropAtivo.DataValueField = "ID";
                dropAtivo.DataTextField = "Name";
                dropAtivo.DataBind();

                dropIdCategoriaMotivo.DataSource = CategoriaMotivoDatatable(commands["CONSULTAR"].execute(new CategoriaMotivo()).Entidades.Cast<CategoriaMotivo>().ToList());
                dropIdCategoriaMotivo.DataValueField = "ID";
                dropIdCategoriaMotivo.DataTextField = "Name";
                dropIdCategoriaMotivo.DataBind();

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

        public static DataTable CategoriaMotivoDatatable(List<CategoriaMotivo> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione uma Categoria de Motivo de Ativação/Inativação";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                CategoriaMotivo categoria = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = categoria.ID;
                dr[1] = categoria.Ativo + " - " + categoria.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }

        private void ConstruirTabela()
        {
            int evade = 0;

            string GRID = "<TABLE class='table table-bordered' id='GridViewGeral' width='100%' cellspacing='0'>{0}<TBODY>{1}</TBODY></TABLE>";
            string tituloColunas = "<THEAD><tr>" +
                "<th>ID do Livro</th>" +
                "<th>Título</th>" +
                "<th>Quantidade</th>" +
                "<th>Custo Unit.</th>" +
                "<th>Valor Unit.</th>" +
                "<th>Editora: Cidade</th>" +
                "<th>Status do Livro</th>" +
                "<th>Data Entrada</th>" +
                "<th>Operações</th>" +
                "</tr></THEAD>";
            tituloColunas += "<TFOOT><tr>" +
                "<th>ID do Livro</th>" +
                "<th>Título</th>" +
                "<th>Quantidade</th>" +
                "<th>Custo Unit.</th>" +
                "<th>Valor Unit.</th>" +
                "<th>Editora: Cidade</th>" +
                "<th>Status do Livro</th>" +
                "<th>Data Entrada</th>" +
                "<th>Operações</th>" +
                "</tr></TFOOT>";
            string linha = "<tr>" +
                "<td>{0}</td>" +
                "<td>{1}</td>" +
                "<td>{2}</td>" +
                "<td>{3}</td>" +
                "<td>{4}</td>" +
                "<td>{5}</td>" +
                "<td>{6}</td>" +
                "<td>{7}</td>" +
                "<td style='text-align-last: center;'>" +
                    "<a class='btn btn-primary' href='EstoqueLivro.aspx?idLivro={0}' title='Dar Entrada'>" +
                        "<div class='fas fa-plus'></div></a>" +
                    "<a class='btn btn-danger' href='EstoqueLivro.aspx?delIdLivro={0}' title='Apagar'>" +
                        "<div class='fas fa-trash-alt'></div></a>" +
                "</td></tr>";

            if (Convert.ToInt32(dropIdCategoriaMotivo.SelectedValue) >= 0)
                estoque.Livro.CategoriaMotivo.ID = Convert.ToInt32(dropIdCategoriaMotivo.SelectedValue);

            if (Convert.ToInt32(dropAtivo.SelectedIndex) > 0)
                estoque.Livro.CategoriaMotivo.Ativo = dropAtivo.SelectedValue.First();

            entidades = commands["CONSULTAR"].execute(estoque).Entidades;
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
                estoque = (Estoque)entidades.ElementAt(i);
                    conteudo.AppendFormat(linha,
                    estoque.Livro.ID,
                    estoque.Livro.Titulo,
                    estoque.Qtde,
                    estoque.ValorCusto,
                    estoque.ValorVenda,
                    estoque.Fornecedor.Nome + ": " + estoque.Fornecedor.Cidade.Nome,
                    estoque.Livro.CategoriaMotivo.Ativo + " - " + estoque.Livro.CategoriaMotivo.Nome,
                    estoque.DataCadastro
                    );

            }
            string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
            divTable.InnerHtml = tabelafinal;
            estoque.ID = estoque.Livro.ID = 0;

            // Rodapé da tabela informativo de quando foi a última vez que foi atualizada a lista
            lblRodaPeTabela.InnerText = "Lista atualizada em " + DateTime.Now.ToString();
        }

        protected void DropAtivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropAtivo.SelectedIndex != 0)
            {
                dropIdCategoriaMotivo.DataSource = CategoriaMotivoDatatable(commands["CONSULTAR"].execute(new CategoriaMotivo() { Ativo = dropAtivo.SelectedValue.First() }).Entidades.Cast<CategoriaMotivo>().ToList());
                dropIdCategoriaMotivo.DataValueField = "ID";
                dropIdCategoriaMotivo.DataTextField = "Name";
                dropIdCategoriaMotivo.DataBind();
                dropIdCategoriaMotivo.Enabled = true;
            }
            else
            {
                dropIdCategoriaMotivo.DataSource = CategoriaMotivoDatatable(commands["CONSULTAR"].execute(new CategoriaMotivo()).Entidades.Cast<CategoriaMotivo>().ToList());
                dropIdCategoriaMotivo.DataValueField = "ID";
                dropIdCategoriaMotivo.DataTextField = "Name";
                dropIdCategoriaMotivo.DataBind();
                dropIdCategoriaMotivo.Enabled = false;
            }

            ConstruirTabela();
        }

        protected void DropIdCategoriaMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConstruirTabela();
        }

    }
}