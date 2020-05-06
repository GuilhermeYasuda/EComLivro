using Dominio.Venda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Analise
{
    public class Analise : EntidadeDominio
    {
		private Pedido _pedido;

		public Pedido Pedido
		{
			get { return _pedido; }
			set { _pedido = value; }
		}

		private DateTime ? _dtFim;

		public DateTime ? DataFim
		{
			get { return _dtFim; }
			set { _dtFim = value; }
		}

		public Analise()
		{
			_pedido = new Pedido();
			_dtFim = null;
		}
	}
}
