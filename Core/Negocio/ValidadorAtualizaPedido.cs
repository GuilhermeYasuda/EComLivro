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
    public class ValidadorAtualizaPedido : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(Pedido))
            {
                Pedido pedido = (Pedido)entidade;

                // aqui o ID é igual a 2 por que no front já seta o valor do novo status 
                if (pedido.Status.ID == 2)
                {

                    foreach (CartaoCreditoPedido cc in pedido.CCs)
                    {
                        //if (cc.CC.NumeroCC.Trim().Equals("123456789"))
                        if (String.Equals(cc.CC.NumeroCC, "123456789", StringComparison.InvariantCulture))
                        {
                            pedido.Status.ID = 3;
                        }
                        else if (String.Equals(cc.CC.NumeroCC, "4701280572311522", StringComparison.InvariantCulture))
                        {
                            pedido.Status.ID = 3;
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
