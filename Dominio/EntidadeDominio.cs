using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EntidadeDominio
    {
        // ID da entidade domínio
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        // atributo para conter a data de cadastro no sistemas
        private DateTime ? _dtCadastro;

        public DateTime ? DataCadastro
        {
            get { return _dtCadastro; }
            set { _dtCadastro = value; }
        }

        public EntidadeDominio()
        {
            _ID = 0;
            //_dtCadastro = DateTime.Now;
            _dtCadastro = null;
        }

    }
}
