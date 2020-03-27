using Core.Core;
using Dominio;
using Dominio.Cliente;
using Dominio.Livro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Negocio
{
    public class ValidadorAtivacaoInativacaoLivro : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(Livro))
            {
                Livro livro = (Livro)entidade;

                // verifica se um livro foi selecionado
                if (livro.ID == 0)
                {
                    sb.Append("UM LIVRO DEVE SER SELECIONADA, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                }

                if(livro.CategoriaMotivo.ID == 0)
                {
                    sb.Append("UMA CATEGORIA DE ATIVAÇÃO/INATIVAÇÃO DEVE SER SELECIONADA, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                }

                // verifica se nome está vazio ou nulo
                if (String.IsNullOrEmpty(livro.Motivo))
                {
                    sb.Append("MOTIVO DEVE SER INFORMADO, POIS É UM CAMPO OBRIGATÓRIO! <br />");
                }

            }
            else
            {
                sb.Append("LIVRO NÃO PODE SER VALIDADO, POIS ENTIDADE NÃO É LIVRO! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
