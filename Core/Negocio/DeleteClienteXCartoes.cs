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
    public class DeleteClienteXCartoes : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(ClientePF))
            {
                ClientePF pessoa = (ClientePF)entidade;

                if(pessoa.ID == 0)
                {
                    return "PARÂMETRO PARA EXCLUSÃO DA ENTIDADE INCORRETO(ID)! <br />";
                } else
                {
                    ClientePFXCartaoDAO clienteXCartaoDAO = new ClientePFXCartaoDAO();
                    clienteXCartaoDAO.Excluir(pessoa);
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
