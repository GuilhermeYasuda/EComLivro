using Core.Core;
using Core.DAO;
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
    public class ValidadorDadosEstoque : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(Estoque))
            {
                Estoque estoque = (Estoque)entidade;
                List<EntidadeDominio> entidades = new List<EntidadeDominio>();

                // verifica se livro foi selecionado
                if (estoque.Livro.ID == 0)
                {
                    sb.Append("ID DO LIVRO É UM CAMPO OBRIGATÓRIO! <br />");
                }
                else
                {
                    Estoque estoqueAux = new Estoque();
                    estoqueAux.Livro.ID = estoque.Livro.ID;
                    EstoqueDAO estoqueDAO = new EstoqueDAO();
                    entidades = estoqueDAO.Consultar(estoqueAux);
                }

                // verifica se quantidade foi informada e se é maior que 0
                if (estoque.Qtde <= 0)
                {
                    sb.Append("QUANTIDADE DE ENTRADA EM ESTOQUE DEVE SER MAIOR QUE ZERO! <br />");
                }
                else
                {
                    if (entidades.Count > 0)
                    {
                        estoque.Qtde += ((Estoque)entidades.ElementAt(0)).Qtde;
                    }
                }

                // verifica se quantidade foi informada e se é maior que 0
                if (estoque.ValorCusto <= 0)
                {
                    sb.Append("VALOR DE CUSTO DEVE SER MAIOR QUE ZERO! <br />");
                }
                else
                {
                    if (entidades.Count > 0)
                    {
                        // verifica se valor de custo que tem no BD é maior que o valor informado
                        if(estoque.ValorCusto < ((Estoque)entidades.ElementAt(0)).ValorCusto)
                        {
                            // se for maior, atribui o valor vindo do BD no valor de custo
                            estoque.ValorCusto = ((Estoque)entidades.ElementAt(0)).ValorCusto;
                        }
                    }

                    entidades = new List<EntidadeDominio>();
                    LivroDAO livroDAO = new LivroDAO();
                    entidades = livroDAO.Consultar(estoque.Livro);

                    if(entidades.Count > 0)
                    {
                        // aplicação da margem de lucro do grupo de precificação
                        estoque.ValorVenda = estoque.ValorCusto + (estoque.ValorCusto * ((Livro)entidades.ElementAt(0)).GrupoPrecificacao.MargemLucro);
                    }
                    else
                    {
                        sb.Append("ERRO NA BUSCA DO GRUPO DE PRECIFICAÇÃO DO LIVRO! <br />");
                    }
                }

                if(estoque.Fornecedor.ID == 0)
                {
                    sb.Append("UM FORNECEDOR DEVE SER SELECIONADO,POIS É UM CAMPO OBRIGATÓRIO! <br />");
                }
            }
            else
            {
                sb.Append("ESTOQUE NÃO PODE SER VALIDADO, POIS ENTIDADE NÃO É ESTOQUE! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
