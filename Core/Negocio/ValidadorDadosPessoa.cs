using Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Dominio.Cliente;

namespace Core.Negocio
{
    class ValidadorDadosPessoa : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(Pessoa))
            {
                Pessoa pessoa = (Pessoa)entidade;

                // verifica se nome está vazio ou nulo
                if(String.IsNullOrEmpty(pessoa.Nome))
                {
                    sb.Append("NOME É UM CAMPO OBRIGATÓRIO! <br />");
                }
                else
                {
                    // verifica se nome é maior que 100
                    if (pessoa.Nome.Length > 100)
                    {
                        sb.Append("NOME TEM QUE TER TAMANHO DE NO MÁXIMO 100 LETRAS! <br />");
                    }
                }

                // vefica se telefone está vazio ou nulo
                if(pessoa.Telefone == null)
                {
                    sb.Append("TELEFONE É UM CAMPO OBRIGATÓRIO! <br />");
                }
                else
                {
                    ValidadorTelefone valTelefone = new ValidadorTelefone();
                    String msg = valTelefone.processar(pessoa.Telefone);
                    if (msg != null)
                    {
                        sb.Append(msg);
                    }
                }

                // verifica se email está vazio ou nulo
                if (String.IsNullOrEmpty(pessoa.Email))
                {
                    sb.Append("E-MAIL É UM CAMPO OBRIGATÓRIO! <br />");
                }
                else
                {
                    ValidadorEmail valEmail = new ValidadorEmail();
                    String msg = valEmail.processar(pessoa);
                    if (msg != null)
                    {
                        sb.Append(msg);
                    }
                }

                // verifica se endereço está vazio ou nulo
                if(pessoa.Enderecos == null)
                {
                    sb.Append("ENDEREÇO É UM CAMPO OBRIGATÓRIO! <br />");
                }
                else
                {
                    ValidadorEndereco valEndereco = new ValidadorEndereco();
                    foreach (var endereco in pessoa.Enderecos)
                    {
                        String msg = valEndereco.processar(endereco);
                        if (msg != null)
                        {
                            sb.Append(msg);
                        }
                    }
                }

            }
            else
            {
                sb.Append("PESSOA NÃO PODE SER VALIDADA, POIS ENTIDADE NÃO É PESSOA! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
