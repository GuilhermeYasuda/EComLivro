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
    public class ValidadorCidade : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(Cidade))
            {
                Cidade cidade = (Cidade)entidade;

                // verifica se uma cidade foi selecionada
                if (cidade.ID == 0)
                {
                    sb.Append("UMA CIDADE DEVE SER SELECIONADA, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                }

            }
            else
            {
                sb.Append("CIDADE NÃO PODE SER VALIDADA, POIS ENTIDADE NÃO É CIDADE! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
