using Core.Aplicacao;
using Dominio.Livro;
using Dominio.Venda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Adm
{
    public partial class ReentradaEstoqueTroca : ViewGenerico
    {
        Resultado resultado = new Resultado();
        protected override void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSim_Click(object sender, EventArgs e)
        {
            // passa ID de pedido e consulta os itens inseridos no pedido
            foreach (PedidoDetalhe detalhe in
                commands["CONSULTAR"].execute(new PedidoDetalhe() { IdPedido = Convert.ToInt32(Session["idPedido"]) }).Entidades.Cast<PedidoDetalhe>())
            {
                // Reentrada no estoque
                Estoque estoque = commands["CONSULTAR"].execute(new Estoque() { Livro = new Livro() { ID = detalhe.Livro.ID } } ).Entidades.Cast<Estoque>().ElementAt(0);
                estoque.Qtde = detalhe.Quantidade;
                resultado = commands["ALTERAR"].execute(estoque);
            }

            Response.Redirect("./GeraCupomTroca.aspx");
        }

        protected void btnNao_Click(object sender, EventArgs e)
        {
            Response.Redirect("./GeraCupomTroca.aspx");
        }
    }
}