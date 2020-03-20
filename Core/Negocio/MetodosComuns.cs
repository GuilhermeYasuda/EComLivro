using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Negocio
{
    public class MetodosComuns
    {
        public static string removeCaracteres(string codigo)
        {
            return Regex.Replace(codigo, "[^0-9]+", "");
        }
    }
}
