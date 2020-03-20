using Core.Core;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Negocio
{
    class ComplementoDtCadastro : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            if (entidade != null)
            {
                DateTime data = DateTime.Now;
                entidade.DataCadastro = data;
            }
            else
            {
                return "Entidade: " + entidade.GetType().Name + " nula!";
            }

            return null;
        }
    }
}
