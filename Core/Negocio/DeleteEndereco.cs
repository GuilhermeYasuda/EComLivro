using Core.Core;
using Core.DAO;
using Dominio;
using Dominio.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Negocio
{
    public class DeleteEndereco : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(Endereco))
            {
                Endereco endereco = (Endereco)entidade;

                if(endereco.ID == 0)
                {
                    return "PARÂMETRO PARA EXCLUSÃO DA ENTIDADE INCORRETO(ID)! <br />";
                } else
                {
                    // necessário para excluir na tabela n-n antes de fazer a exclusão na tabela dos cartões
                    ClientePFXEnderecoDAO clienteXEnderecoDAO = new ClientePFXEnderecoDAO("id_endereco");
                    clienteXEnderecoDAO.Excluir(endereco);
                }

            }
            else
            {
                sb.Append("CLIENTE PESSOA FÍSICA NÃO PODE SER VALIDADA, POIS ENTIDADE NÃO É CLIENTE PESSOA FÍSICA! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }
    }
}
