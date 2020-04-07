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
    public class ValidadorStatusPedido : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(Pedido))
            {
                Pedido pedido = (Pedido)entidade;
                
                if(pedido.Status.ID == 0)
                {
                    sb.Append("STATUS DO PEDIDO DEVE SER INFORMADO, POIS É UM CAMPO OBRIGATÓRIO! <br />");
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
