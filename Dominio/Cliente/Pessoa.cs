using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Cliente
{
    public class Pessoa : EntidadeDominio
    {
		private string _nome;

		public string Nome
		{
			get { return _nome; }
			set { _nome = value; }
		}

		private Telefone _telefone;

		public Telefone Telefone
		{
			get { return _telefone; }
			set { _telefone = value; }
		}

		private string _email;

		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}

		private List<Endereco> _enderecos;

		public List<Endereco> Enderecos
		{
			get { return _enderecos; }
			set { _enderecos = value; }
		}

		public Pessoa()
		{
			_nome = "";
			_telefone = new Telefone();
			_email = "";
			_enderecos = new List<Endereco>();
		}
	}
}
