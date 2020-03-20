using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core
{
    /*
     * Interface para imposição de métodos para processamento de
     * operações do BD para as classes que implementarem esta interface
     */
    public interface IDAO
    {
        void Salvar(EntidadeDominio entidade);
        void Alterar(EntidadeDominio entidade);
        List<EntidadeDominio> Consultar(EntidadeDominio entidade);
        void Excluir(EntidadeDominio entidade);
    }
}
