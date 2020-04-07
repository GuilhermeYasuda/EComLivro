using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Venda
{
	public class StatusPedido : EntidadeDominio
	{
		private string _nome;

		public string Nome
		{
			get { return _nome; }
			set { _nome = value; }
		}

		public StatusPedido()
		{
			_nome = "";
		}
	}
}
