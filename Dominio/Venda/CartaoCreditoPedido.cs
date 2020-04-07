using Dominio.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Venda
{
    public class CartaoCreditoPedido : EntidadeDominio
    {
		private int _idPedido;

		public int IdPedido
		{
			get { return _idPedido; }
			set { _idPedido = value; }
		}

		private CartaoCredito _cc;

		public CartaoCredito CC
		{
			get { return _cc; }
			set { _cc = value; }
		}

		private float _valor;

		public float ValorCCPagto
		{
			get { return _valor; }
			set { _valor = value; }
		}

		public CartaoCreditoPedido()
		{
			_idPedido = 0;
			_cc = new CartaoCredito();
			_valor = (float)0.0;
		}

	}
}
