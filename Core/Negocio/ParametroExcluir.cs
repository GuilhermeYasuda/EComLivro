using Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Core.Negocio
{
    public class ParametroExcluir : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            try
            {
                if (entidade.ID == 0)
                    return "PARÂMETRO PARA EXCLUSÃO DA ENTIDADE INCORRETO(ID)! <br />";
                return null;
            }
            catch
            {
                return "PARÂMETRO PARA EXCLUSÃO NO FORMATO INCORRETO! <br />";
            }
        }
    }
}
