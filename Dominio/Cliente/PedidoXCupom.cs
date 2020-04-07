using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Cliente
{
    public class PedidoXCupom : EntidadeDominio
    {
        private Cupom _cupom;

        public Cupom Cupom
        {
            get { return _cupom; }
            set { _cupom = value; }
        }

        public PedidoXCupom()
        {
            _cupom = new Cupom();
        }

    }
}
