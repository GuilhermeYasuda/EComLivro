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
    public class ValidadorClienteEndereco : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(ClientePFXEndereco))
            {
                ClientePFXEndereco clientePFXEndereco = (ClientePFXEndereco)entidade;

                // verifica se cliente foi selecionado
                if (clientePFXEndereco.ID == 0)
                {
                    sb.Append("ID CLIENTE INFORMADO INCORRETO! <br />");
                }

                // verifica se cc está vazio ou nulo
                if(clientePFXEndereco.Endereco == null)
                {
                    sb.Append("ENDEREÇO É UM CAMPO OBRIGATÓRIO! <br />");
                } else
                {
                    ValidadorEndereco valEndereco = new ValidadorEndereco();
                    String msg = valEndereco.processar(clientePFXEndereco.Endereco);
                    if (msg != null)
                    {
                        sb.Append(msg);
                    }
                }

            }
            else
            {
                sb.Append("CLIENTE PESSOA FÍSICA X ENDEREÇO NÃO PODE SER VALIDADA, POIS ENTIDADE NÃO É CLIENTE PESSOA FÍSICA X ENDEREÇO! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
