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
    public class ValidadorDadosPessoaFisica : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(PessoaFisica))
            {
                PessoaFisica pessoa = (PessoaFisica)entidade;

                // verifica se CPF está vazio ou nulo
                if (String.IsNullOrEmpty(pessoa.CPF))
                {
                    sb.Append("CPF É UM CAMPO OBRIGATÓRIO! <br />");
                }
                else
                {
                    ValidadorCPF valCPF = new ValidadorCPF();
                    String msg = valCPF.processar(pessoa);
                    if (msg != null)
                    {
                        sb.Append(msg);
                    }
                }

                // vefica se gênero está vazio ou nulo ou se não foi selecionado
                if (pessoa.Genero == '\0' || pessoa.Genero == '0')
                {
                    sb.Append("GÊNERO É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se email está vazio ou nulo
                if (pessoa.DataNascimento == null)
                {
                    sb.Append("DATA DE NASCIMENTO É UM CAMPO OBRIGATÓRIO! <br />");
                }

            }
            else
            {
                sb.Append("PESSOA FÍSICA NÃO PODE SER VALIDADA, POIS ENTIDADE NÃO É PESSOA FÍSICA! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
