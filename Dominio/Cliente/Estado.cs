using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Cliente
{
    public class Estado : EntidadeDominio
    {
        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _sigla;

        public string Sigla
        {
            get { return _sigla; }
            set { _sigla = value; }
        }

        private Pais _pais;

        public Pais Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        public Estado()
        {
            _nome = "";
            _sigla = "";
            _pais = new Pais();
        }
    }
}
