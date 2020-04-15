using Core.Core;
using Core.DAO;
using Dominio;
using Dominio.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Negocio
{
    public class ValidadorExistenciaEmail : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(ClientePF))
            {

                // Valida existência do e-mail
                ClientePF clienteAux = new ClientePF();
                clienteAux.Email = ((ClientePF)entidade).Email;
                List<EntidadeDominio> entidades = new ClientePFDAO().Consultar(clienteAux);
                if (entidades.Count > 0)
                {
                    sb.Append("E-MAIL JÁ CADASTRADO! <br />");
                }

            }
            else
            {
                sb.Append("E-MAIL NÃO PODE SER VALIDADO, POIS ENTIDADE NÃO É CLIENTE PF! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
