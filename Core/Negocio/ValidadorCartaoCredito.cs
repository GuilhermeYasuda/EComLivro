using Core.Core;
using Dominio;
using Dominio.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Negocio
{
    public class ValidadorCartaoCredito : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(CartaoCredito))
            {
                CartaoCredito cc = (CartaoCredito)entidade;

                // verifica se nome está vazio ou nulo
                if (String.IsNullOrEmpty(cc.NomeImpresso))
                {
                    sb.Append("NOME IMPRESSO É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se número está vazio ou nulo
                if (String.IsNullOrEmpty(cc.NumeroCC))
                {
                    sb.Append("NÚMERO DO CARTÃO É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se bandeira foi selecionado
                if (cc.Bandeira.ID == 0)
                {
                    sb.Append("BANDEIRA É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se código está vazio ou nulo
                if (String.IsNullOrEmpty(cc.CodigoSeguranca))
                {
                    sb.Append("CÓDIGO DE SEGURANÇA É UM CAMPO OBRIGATÓRIO! <br />");
                }
            }
            else
            {
                sb.Append("CARTÃO DE CRÉDITO NÃO PODE SER VALIDADA, POIS ENTIDADE NÃO É CARTÃO DE CRÉDITO! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
