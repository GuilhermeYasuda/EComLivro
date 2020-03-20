using Core.Aplicacao;
using Core.DAO;
using Dominio;
using Dominio.Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro
{
    public partial class ListaCliente : ViewGenerico
    {
        private ClientePFDAO clienteDAO = new ClientePFDAO();
        private Dominio.Cliente.ClientePF clientePF = new Dominio.Cliente.ClientePF();
        private Resultado resultado = new Resultado();
        private List<EntidadeDominio> res = new List<EntidadeDominio>();

        protected override void Page_Load(object sender, EventArgs e)
        {
            Session["tipo_sel"] = null;
            if (!IsPostBack)
            {
                //dropIdDocumento.DataSource = TipoDocumentoDatatable(commands["CONSULTAR"].execute(new TipoDocumento()).Entidades.Cast<TipoDocumento>().ToList());
                //dropIdDocumento.DataBind();

                ConstruirTabela();
            }
        }

        public static DataTable TipoDocumentoDatatable(List<TipoTelefone> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione um Tipo de Telefone";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                TipoTelefone tipo = input.ElementAt(i);
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
                "<th>ID</th>" +
                "<th>Nome</th>" +
                "<th>CPF</th>" +
                "<th>Data de Nascimento</th>" +
                "<th>Telefone</th>" +
                "<th>E-mail</th>" +
                "<th>Enderecos</th>" +
                "<th>Cartões de Credito</th>" +
                "<th>Data de Cadastro</th>" +
                "<th>Operações</th>" +
                "</tr></THEAD>";
            tituloColunas += "<TFOOT><tr>" +
                "<th>ID</th>" +
                "<th>Nome</th>" +
                "<th>CPF</th>" +
                "<th>Data de Nascimento</th>" +
                "<th>Telefone</th>" +
                "<th>E-mail</th>" +
                "<th>Enderecos</th>" +
                "<th>Cartões de Credito</th>" +
                "<th>Data de Cadastro</th>" +
                "<th>Operações</th>" +
                "</tr></TFOOT>";
            string linha = "<tr>" +
            //linha += "{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td style='text-align-last: center;'><a class='btn fas fa-edit' style='background-color: #ddd; border-color: #999; color: #111' href='Produtor.aspx?idProdutor={0}' title='Editar'></a><a class='btn fas fa-trash-alt' style='background-color: #ddd; border-color: #999; color: #111' href='Produtor.aspx?delIdProdutor={0}' title='Apagar'></a></td></tr>";
                "<td>{0}</td>" +
                "<td>{1}</td>" +
                "<td>{2}</td>" +
                "<td>{3}</td>" +
                "<td>{4}</td>" +
                "<td>{5}</td>" +
                "<td>{6}</td>" +
                "<td>{7}</td>" +
                "<td>{8}</td>" +
                "<td style='text-align-last: center;'>" +
                    "<a class='btn btn-warning' href='CadastroCliente.aspx?idClientePF={0}' title='Editar'>" +
                        "<div class='fas fa-edit'></div></a>" +
                    "<a class='btn btn-danger' href='CadastroCliente.aspx?delIdClientePF={0}' title='Apagar'>" +
                        "<div class='fas fa-trash-alt'></div></a>" +
                "</td></tr>";

            //if (int.Parse(dropIdDocumento.SelectedValue)>= 0)
            //    produtor.TipoDocumento.ID = int.Parse(dropIdDocumento.SelectedValue);

            res = commands["CONSULTAR"].execute(clientePF).Entidades;
            try
            {
                evade = res.Count;
            }
            catch
            {
                evade = 0;
            }

            StringBuilder conteudo = new StringBuilder();

            ClientePF clienteAux = new ClientePF();
            clienteAux.ID = 0;

            for (int i = 0; i < evade; i++)
            {
                clientePF = (Dominio.Cliente.ClientePF)res.ElementAt(i);
                if (clientePF.ID != clienteAux.ID)
                {
                    conteudo.AppendFormat(linha,
                    clientePF.ID,
                    clientePF.Nome,
                    clientePF.CPF,
                    clientePF.DataNascimento,
                    "(" + clientePF.Telefone.DDD + ")" + clientePF.Telefone.NumeroTelefone,
                    clientePF.Email,
                    EnderecosToString(clientePF),
                    CartoesToString(clientePF),
                    clientePF.DataCadastro
                    );

                    clienteAux.ID = clientePF.ID;
                }
            }
            string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
            divTable.InnerHtml = tabelafinal;
            clientePF.ID = 0;

            // Rodapé da tabela informativo de quando foi a última vez que foi atualizada a lista
            lblRodaPeTabela.InnerText = "Lista atualizada em " + DateTime.Now.ToString();
        }

        public string EnderecosToString(ClientePF cliente)
        {
            string retorno = "";
            foreach (Endereco endereco in cliente.Enderecos)
            {
                retorno += "ID: " + endereco.ID + ", " +
                    endereco.TipoLogradouro.Nome + " " +
                    endereco.Rua + ", " +
                    endereco.Numero + ", " +
                    endereco.Bairro + ", " +
                    endereco.Cidade.Nome + " - " +
                    endereco.Cidade.Estado.Sigla + ", " +
                    "CEP: " + endereco.CEP + "<br />" +
                    "<a class='btn btn-warning' href='CadastroEndereco.aspx?idEndereco=" + endereco.ID + 
                        "' title='Alterar Endereço'>" +
                        "<div class='fas fa-edit'></div></a>" +
                    "<a class='btn btn-danger' href='CadastroEndereco.aspx?delIdEndereco=" + endereco.ID + 
                        "' title='Apagar Endereço'>" +
                        "<div class='fas fa-trash-alt'></div></a><br />";
            }

            retorno += "<a class='btn btn-success' href='CadastroEndereco.aspx?idClientePF=" + cliente.ID + 
                            "' title='Novo Endereço'>" +
                            "<div class='fas fa-fw fa-plus'></div></a>";


            return retorno;
        }

        public string CartoesToString(ClientePF cliente)
        {
            string retorno = "";
            foreach (CartaoCredito cc in cliente.CartoesCredito)
            {
                if (cc.ID != 0)
                    retorno += "ID: " + cc.ID + ", " +
                        cc.NomeImpresso + ", " +
                        cc.NumeroCC + ", " +
                        cc.Bandeira.Nome + ", " +
                        cc.CodigoSeguranca + "<br /> " +
                    "<a class='btn btn-warning' href='CadastroCC.aspx?idCC=" + cc.ID + 
                        "' title='Alterar Cartão de Crédito'>" +
                        "<div class='fas fa-edit'></div></a>" +
                    "<a class='btn btn-danger' href='CadastroCC.aspx?delIdCC=" + cc.ID + 
                        "' title='Apagar Cartão de Crédito'>" +
                        "<div class='fas fa-trash-alt'></div></a><br />";
            }

            retorno += "<a class='btn btn-success' href='CadastroCC.aspx?idClientePF=" + cliente.ID + 
                            "' title='Novo Cartão de Crédito'>" +
                            "<div class='fas fa-fw fa-plus'></div></a>";

            return retorno;
        }
    }
}