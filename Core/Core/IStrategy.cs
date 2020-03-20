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
     * validações e verificações das classe que implementarem esta interface
     */
    public interface IStrategy
    {
        string processar(EntidadeDominio entidade);
    }
}
