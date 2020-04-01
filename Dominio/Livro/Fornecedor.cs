using Dominio.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Livro
{
    public class Fornecedor : EntidadeDominio
    {
		private string _nome;

		public string Nome
		{
			get { return _nome; }
			set { _nome = value; }
		}

		private Cidade _cidade;

		public Cidade Cidade
		{
			get { return _cidade; }
			set { _cidade = value; }
		}


		public Fornecedor()
		{
			_nome = "";
			_cidade = new Cidade();
		}
	}
}
