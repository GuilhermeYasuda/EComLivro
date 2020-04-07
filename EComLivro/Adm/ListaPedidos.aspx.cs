using Dominio.Cliente;
using Dominio.Venda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Adm
{
    public partial class ListaPedidos : ViewGenerico
    {
        Pedido pedido = new Pedido();
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //dropIdDocumento.DataSource = TipoDocumentoDatatable(commands["CONSULTAR"].execute(new TipoDocumento()).Entidades.Cast<TipoDocumento>().ToList());
                //dropIdDocumento.DataBind();

                ConstruirTabela();
            }
        }


        private void ConstruirTabela()
        {
            int evade = 0;

            string GRID = "<TABLE class='table table-bordered' id='GridViewGeral' width='100%' cellspacing='0'>{0}<TBODY>{1}</TBODY></TABLE>";
            string tituloColunas = "<THEAD><tr>" +
                "<th>ID</th>" +
                "<th>Usuário</th>" +
                "<th>Entrega em:</th>" +
                "<th>Item(ns)</th>" +
                "<th>Cartão(ões)</th>" +
                "<th>Cupom Promo</th>" +
                "<th>Cupom(ns) Troca</th>" +
                "<th>Status</th>" +
                "<th>Frete</th>" +
                "<th>Total</th>" +
                "<th>Data Ent/Atual</th>" +
                "<th>Operações</th>" +
                "</tr></THEAD>";
            tituloColunas += "<TFOOT><tr>" +
                "<th>ID</th>" +
                "<th>Usuário</th>" +
                "<th>Entrega em:</th>" +
                "<th>Item(ns)</th>" +
                "<th>Cartão(ões)</th>" +
                "<th>Cupom Promo</th>" +
                "<th>Cupom(ns) Troca</th>" +
                "<th>Status</th>" +
                "<th>Frete</th>" +
                "<th>Total</th>" +
                "<th>Data Ent/Atual</th>" +
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
                "<td>{8}</td>" +
                "<td>{9}</td>" +
                "<td>{10}</td>" +
                "<td style='text-align-last: center;'>" +
                    "<a class='btn btn-warning' href='CadastroCliente.aspx?idClientePF={0}' title='Editar'>" +
                        "<div class='fas fa-edit'></div></a>" +
                    "<a class='btn btn-danger' href='CadastroCliente.aspx?delIdClientePF={0}' title='Apagar'>" +
                        "<div class='fas fa-trash-alt'></div></a>" +
                "</td></tr>";

            entidades = commands["CONSULTAR"].execute(pedido).Entidades;
            try
            {
                evade = entidades.Count;
            }
            catch
            {
                evade = 0;
            }

            StringBuilder conteudo = new StringBuilder();

            // lista para conter todos clientes retornados do BD
            List<Pedido> pedidos = new List<Pedido>();
            foreach (Pedido pedido in entidades)
            {
                pedidos.Add(pedido);
            }

            foreach (var pedido in pedidos)
            {
                // para pesquisar os itens que o pedido tem

                pedido.EnderecoEntrega = commands["CONSULTAR"].execute(new Endereco() { ID = pedido.EnderecoEntrega.ID }).Entidades.Cast<Endereco>().ElementAt(0);

                // passa ID de pedido e consulta
                foreach (PedidoDetalhe detalhe in
                    commands["CONSULTAR"].execute(new PedidoDetalhe { IdPedido = pedido.ID }).Entidades)
                {
                    // Passa itens para o pedido
                    pedido.Detalhes.Add(detalhe);
                }

                foreach (CartaoCreditoPedido cc in
                    commands["CONSULTAR"].execute(new CartaoCreditoPedido { IdPedido = pedido.ID }).Entidades)
                {
                    // Passa ccs para o pedido
                    pedido.CCs.Add(cc);
                }

                // passa ID pedido e consulta 
                foreach (PedidoXCupom pedidoXCupom in
                    commands["CONSULTAR"].execute(new PedidoXCupom { ID = pedido.ID }).Entidades)
                {
                    // Passa cupom promo para o pedido
                    pedido.CupomPromocional = pedidoXCupom.Cupom;
                }

                // passa ID pedido e consulta 
                foreach (Cupom cupom in
                    commands["CONSULTAR"].execute(new Cupom { IdPedido = pedido.ID }).Entidades)
                {
                    // Passa cupom troca para o pedido
                    pedido.CuponsTroca.Add(cupom);
                }

                conteudo.AppendFormat(linha,
                pedido.ID,
                pedido.Usuario,
                EnderecoToString(pedido.EnderecoEntrega),
                DetalhesToString(pedido.Detalhes),
                CartoesToString(pedido.CCs),
                CupomPromoToString(pedido.CupomPromocional),
                CupomTrocaToString(pedido.CuponsTroca),
                "ID Status: " + pedido.Status.ID + " - " + pedido.Status.Nome,
                "R$ " + pedido.Frete.ToString("N2"),
                "R$ " + pedido.Total.ToString("N2"),
                pedido.DataCadastro.ToString()
                );
            }
            string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
            divTable.InnerHtml = tabelafinal;
            pedido.ID = 0;

            // Rodapé da tabela informativo de quando foi a última vez que foi atualizada a lista
            lblRodaPeTabela.InnerText = "Lista atualizada em " + DateTime.Now.ToString();
        }

        public string EnderecoToString(Endereco endereco)
        {
            string retorno = "";
            retorno += "ID: " + endereco.ID + ", " +
                endereco.TipoLogradouro.Nome + " " +
                endereco.Rua + ", " +
                endereco.Numero + ", " +
                endereco.Bairro + ", " +
                endereco.Cidade.Nome + " - " +
                endereco.Cidade.Estado.Sigla + ", " +
                "CEP: " + endereco.CEP + "<br />";

            return retorno;
        }

        public string DetalhesToString(List<PedidoDetalhe> detalhes)
        {
            string retorno = "";
            foreach (PedidoDetalhe detalhe in detalhes)
            {
                if (detalhe.ID != 0)
                    retorno += "ID: " + detalhe.ID + ", " +
                        "ID Livro: " + detalhe.Livro.ID + " - " +
                        detalhe.Livro.Titulo + ", " +
                        "Qtde: " + detalhe.Quantidade + ", " +
                        "Vlr Unit: " + detalhe.ValorUnit + "<br /> ";
            }

            return retorno;
        }


        public string CartoesToString(List<CartaoCreditoPedido> ccs)
        {
            string retorno = "";
            foreach (CartaoCreditoPedido cc in ccs)
            {
                if (cc.ID != 0)
                    retorno += "ID: " + cc.ID + ", " +
                        cc.CC.NomeImpresso + ", " +
                        cc.CC.NumeroCC + ", " +
                        cc.CC.Bandeira.Nome + ", " +
                        cc.CC.CodigoSeguranca + ", " +
                        "Vlr Pgto: " + cc.ValorCCPagto + "<br /> ";
            }

            return retorno;
        }

        public string CupomPromoToString(Cupom cupom)
        {
            string retorno = "";
            retorno += "ID: " + cupom.ID + ", " +
                cupom.CodigoCupom + " - " +
                cupom.ValorCupom * 100 + "%, " +
                cupom.Tipo.Nome;

            return retorno;
        }

        public string CupomTrocaToString(List<Cupom> cupons)
        {
            string retorno = "";
            foreach (Cupom cupom in cupons)
                retorno += "ID: " + cupom.ID + ", " +
                cupom.CodigoCupom + " - " +
                "R$ " + cupom.ValorCupom.ToString("N2") + ", " +
                cupom.Tipo.Nome + "</br> ";

            return retorno;
        }
    }
}