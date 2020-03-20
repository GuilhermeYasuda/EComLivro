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
    public class ValidadorClienteCC : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(ClientePFXCC))
            {
                ClientePFXCC clientePFXCC = (ClientePFXCC)entidade;

                // verifica se cliente foi selecionado
                if (clientePFXCC.ID == 0)
                {
                    sb.Append("ID CLIENTE INFORMADO INCORRETO! <br />");
                }

                // verifica se cc está vazio ou nulo
                if(clientePFXCC.CC == null)
                {
                    sb.Append("CARTÃO DE CRÉDITO É UM CAMPO OBRIGATÓRIO! <br />");
                } else
                {
                    ValidadorCartaoCredito valCC = new ValidadorCartaoCredito();
                    String msg = valCC.processar(clientePFXCC.CC);
                    if (msg != null)
                    {
                        sb.Append(msg);
                    }
                }

            }
            else
            {
                sb.Append("CLIENTE PESSOA FÍSICA X CARTÃO NÃO PODE SER VALIDADA, POIS ENTIDADE NÃO É CLIENTE PESSOA FÍSICA X CARTÃO! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
