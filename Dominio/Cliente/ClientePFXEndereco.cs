using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Cliente
{
    public class ClientePFXEndereco : EntidadeDominio
    {
        private Endereco _endereco;

        public Endereco Endereco
        {
            get { return _endereco; }
            set { _endereco = value; }
        }

        public ClientePFXEndereco()
        {
            _endereco = new Endereco();
        }

    }
}
