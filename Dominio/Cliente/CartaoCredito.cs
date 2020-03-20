using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Cliente
{
    public class CartaoCredito : EntidadeDominio
    {
		private string _numeroCC;

		public string NumeroCC
		{
			get { return _numeroCC; }
			set { _numeroCC = value; }
		}

		private string _nome;

		public string NomeImpresso
		{
			get { return _nome; }
			set { _nome = value; }
		}

		private Bandeira _bandeira;

		public Bandeira Bandeira
		{
			get { return _bandeira; }
			set { _bandeira = value; }
		}

		private string _codigo;

		public string CodigoSeguranca
		{
			get { return _codigo; }
			set { _codigo = value; }
		}

		public CartaoCredito()
		{
			_numeroCC = "";
			_nome = "";
			_bandeira = new Bandeira();
			_codigo = "";
		}

	}
}
