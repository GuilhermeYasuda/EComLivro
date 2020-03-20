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
    public class ValidadorEndereco : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(Endereco))
            {
                Endereco endereco = (Endereco)entidade;

                // verifica se nome está vazio ou nulo
                if (String.IsNullOrEmpty(endereco.Nome))
                {
                    sb.Append("NOME DO ENDEREÇO É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se destinatário está vazio ou nulo
                if (String.IsNullOrEmpty(endereco.Destinatario))
                {
                    sb.Append("NOME DO ENDEREÇO É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se tipo da residência foi selecionado
                if (endereco.TipoResidencia.ID == 0)
                {
                    sb.Append("TIPO DA RESIDÊNCIA É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se tipo do logradouro foi selecionado
                if (endereco.TipoLogradouro.ID == 0)
                {
                    sb.Append("TIPO DO LOGRADOURO É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se rua está vazio ou nulo
                if (String.IsNullOrEmpty(endereco.Rua))
                {
                    sb.Append("LOGRADOURO É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se número está vazio ou nulo
                if (String.IsNullOrEmpty(endereco.Numero))
                {
                    sb.Append("NÚMERO DO ENDEREÇO É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se bairro está vazio ou nulo
                if (String.IsNullOrEmpty(endereco.Bairro))
                {
                    sb.Append("BAIRRO É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se cidade foi selecionada
                if (endereco.Cidade == null)
                {
                    sb.Append("CIDADE É UM CAMPO OBRIGATÓRIO! <br />");
                }
                else
                {
                    ValidadorCidade valCidade = new ValidadorCidade();
                    String msg = valCidade.processar(endereco.Cidade);
                    if (msg != null)
                    {
                        sb.Append(msg);
                    }
                }

                // verifica se CEP está vazio ou nulo
                if (String.IsNullOrEmpty(endereco.CEP))
                {
                    sb.Append("CEP É UM CAMPO OBRIGATÓRIO! <br />");
                }
                else
                {
                    ValidadorCEP valCEP = new ValidadorCEP();
                    String msg = valCEP.processar(endereco);
                    if (msg != null)
                    {
                        sb.Append(msg);
                    }
                }
            }
            else
            {
                sb.Append("ENDEREÇO NÃO PODE SER VALIDADO, POIS ENTIDADE NÃO É ENDEREÇO! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
