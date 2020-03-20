using Core.Aplicacao;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core
{
    /*
     * Interface para imposição de métodos para processamento da
     * operação específica que virá do Command
     */
    public interface IFachada
    {
        Resultado salvar(EntidadeDominio entidade);
        Resultado alterar(EntidadeDominio entidade);
        Resultado excluir(EntidadeDominio entidade);
        Resultado consultar(EntidadeDominio entidade);
    }
}
