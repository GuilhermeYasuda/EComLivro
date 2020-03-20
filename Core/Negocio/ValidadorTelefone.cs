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
    public class ValidadorTelefone : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(Telefone))
            {
                Telefone telefone = (Telefone)entidade;

                // verifica se tipo do telefone foi selecionado
                if (telefone.TipoTelefone.ID == 0)
                {
                    sb.Append("TIPO DO TELEFONE É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se DDD do telefone está vazio ou nulo
                if (String.IsNullOrEmpty(telefone.DDD))
                {
                    sb.Append("DDD DO TELEFONE É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se telefone está vazio ou nulo
                if (String.IsNullOrEmpty(telefone.NumeroTelefone))
                {
                    sb.Append("NÚMERO DO TELEFONE É UM CAMPO OBRIGATÓRIO! <br />");
                }
                else
                {
                    // remove caracteres indesejados
                    telefone.NumeroTelefone = telefone.NumeroTelefone.Replace(".", "");
                    telefone.NumeroTelefone = telefone.NumeroTelefone.Replace("-", "");
                    telefone.NumeroTelefone = telefone.NumeroTelefone.Replace(" ", "");
                    
                    // expressão regular para verificar se contém 8 ou 9 dígitos numéricos
                    Regex Rgx = new Regex(@"^\d{8,9}$");

                    // Verifica tamanho do número 8 ou 9 dígitos
                    if (!Rgx.IsMatch(telefone.NumeroTelefone))
                    {
                        sb.Append("NÚMERO DO TELEFONE INCORRETO! OBRIGATÓRIO 8 OU 9 DÍGITOS NUMÉRICOS! <br />");
                    }
                }
            }
            else
            {
                sb.Append("TELEFONE NÃO PODE SER VALIDADO, POIS ENTIDADE NÃO É TELEFONE! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
