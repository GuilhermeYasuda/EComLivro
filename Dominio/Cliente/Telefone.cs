using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Cliente
{
    public class Telefone : EntidadeDominio
    {
		private TipoTelefone _tipoTelefone;

		public TipoTelefone TipoTelefone
		{
			get { return _tipoTelefone; }
			set { _tipoTelefone = value; }
		}

		private string _ddd;

		public string DDD
		{
			get { return _ddd; }
			set { _ddd = value; }
		}

		private string _numero;

		public string NumeroTelefone
		{
			get { return _numero; }
			set { _numero = value; }
		}

		public Telefone()
		{
			_tipoTelefone = new TipoTelefone();
			_ddd = "";
			_numero = "";
		}
	}
}
