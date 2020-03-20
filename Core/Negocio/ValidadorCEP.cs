using Core.Core;
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
    public class ValidadorCEP : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(Endereco))
            {
                string cep = ((Endereco)entidade).CEP;

                // remove caracteres indesejados
                cep = cep.Replace(".", "");
                cep = cep.Replace("-", "");
                cep = cep.Replace(" ", "");

                // verifica se CEP está vazio ou nulo
                if (cep.Equals(null) || cep.Equals("") || cep.Equals(String.Empty))
                {
                    sb.Append("CEP É UM CAMPO OBRIGATÓRIO! <br />");
                }
                else
                {
                    // expressão regular para verificar se contém 8 dígitos numéricos
                    Regex Rgx = new Regex(@"^\d{8}$");

                    // Verifica tamanho do CEP 8 dígitos
                    if (!Rgx.IsMatch(cep))
                    {
                        sb.Append("CEP INCORRETO! OBRIGATÓRIO 8 DÍGITOS NUMÉRICOS! <br />");
                    }
                }
            }
            else
            {
                sb.Append("CEP NÃO PODE SER VALIDADO, POIS ENTIDADE NÃO É ENDEREÇO! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
