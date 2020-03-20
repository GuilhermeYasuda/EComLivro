using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Cliente
{
    public class ClientePF : PessoaFisica
    {
        private List<CartaoCredito> _cartoes;

        public List<CartaoCredito> CartoesCredito
        {
            get { return _cartoes; }
            set { _cartoes = value; }
        }

        public ClientePF()
        {
            _cartoes = new List<CartaoCredito>();
        }

    }
}
