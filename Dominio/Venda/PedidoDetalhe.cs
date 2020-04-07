using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Venda
{
    public class PedidoDetalhe : EntidadeDominio
	{
		private int _idPedido;

		public int IdPedido
		{
			get { return _idPedido; }
			set { _idPedido = value; }
		}

		private Dominio.Livro.Livro _livro;

		public Dominio.Livro.Livro Livro
		{
			get { return _livro; }
			set { _livro = value; }
		}

		private int _qtde;

		public int Quantidade
		{
			get { return _qtde; }
			set { _qtde = value; }
		}

		private float _valor;

		public float ValorUnit
		{
			get { return _valor; }
			set { _valor = value; }
		}

		public PedidoDetalhe()
		{
			_idPedido = 0;
			_livro = new Dominio.Livro.Livro();
			_qtde = 0;
			_valor = (float)0.0;
		}
	}
}
