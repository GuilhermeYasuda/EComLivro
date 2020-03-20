using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplicacao
{
    public class Resultado 
    {
        /*
         * Classe utilizada para retorno de resultado, podendo retornar como resultado uma 
         * lista de EntidadesDominio (Consulta) ou uma mensagem de erro (Strategys).
         * Sendo utilizada pelas DAOs e Strategys.
         */
        private string _msg;
        private List<EntidadeDominio> _entidades;

        public List<EntidadeDominio> Entidades
        {
            get { return _entidades; }
            set { _entidades = value; }
        }

        public string Msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

        public Resultado()
        {
            Msg = "";
            Entidades = new List<EntidadeDominio>();
        }
    }
}