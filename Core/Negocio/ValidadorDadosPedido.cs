using Core.Core;
using Core.DAO;
using Dominio;
using Dominio.Cliente;
using Dominio.Livro;
using Dominio.Venda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Negocio
{
    public class ValidadorDadosPedido : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            bool flgCupom = false;
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(Pedido))
            {
                Pedido pedido = (Pedido)entidade;
                List<EntidadeDominio> entidades = new List<EntidadeDominio>();

                // verifica se usuário está vazio ou nulo
                if (String.IsNullOrEmpty(pedido.Usuario))
                {
                    sb.Append("USUÁRIO É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se endereço foi selecionado
                if (pedido.EnderecoEntrega.ID == 0)
                {
                    sb.Append("UM ENDEREÇO DE ENTREGE DEVER SER SELECIONADO, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se frete foi informada
                if (pedido.Frete == (float)0.0)
                {
                    sb.Append("FRETE DEVE SER INFORMADO, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se total foi informado
                if (pedido.Total == (float)0.0)
                {
                    sb.Append("TOTAL DO PEDIDO DEVE SER INFORMADO, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                }

                foreach (PedidoDetalhe detalhe in pedido.Detalhes)
                {
                    if(detalhe.Livro.ID == 0)
                        sb.Append("O LIVRO DO CARRINHO DEVE SER INFORMADO, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                    if (string.IsNullOrEmpty(detalhe.Livro.Titulo))
                        sb.Append("O TÍTULO DO LIVRO DO CARRINHO DEVE SER INFORMADO, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                    if (detalhe.Quantidade == 0)
                        sb.Append("A QUANTIDADE DO LIVRO DO CARRINHO DEVE SER INFORMADO, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                    if (detalhe.ValorUnit == (float)0.0)
                        sb.Append("O VALOR UNITÁRIO DO LIVRO DO CARRINHO DEVE SER INFORMADO, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // lista para conter a lista de cupons de troca em ordem
                List<Cupom> cupom = pedido.CuponsTroca.OrderBy(c => c.ID).ToList();
                for (int i = 0; i < cupom.Count; i++)
                {
                    if (i != 0 && cupom[i].ID == cupom[i - 1].ID)
                    {
                        sb.Append("NÃO PODE SER APLICADO UM CUPOM DE TROCA DUAS VEZES NO MESMO PEDIDO! <br />");
                    }
                    if (cupom[i].ID != 0)
                    {
                        flgCupom = true;
                    }
                }

                foreach (CartaoCreditoPedido cc in pedido.CCs)
                {
                    if (cc.ValorCCPagto != (float)0.0)
                    {
                        if (cc.CC.ID == 0)
                        {
                            sb.Append("O CARTÃO DE CRÉDITO QUE SERÁ USADO DEVE SER INFORMADO, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                        }
                        if (cc.ValorCCPagto < 10 && !flgCupom)
                        {
                            sb.Append("VALOR MÍNIMO PERMITIDO PARA USO DO CARTÃO DE CRÉDITO É DE R$ 10,00! <br />");
                        }
                    }
                }
            }
            else
            {
                sb.Append("PEDIDO NÃO PODE SER VALIDADO, POIS ENTIDADE NÃO É PEDIDO! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
