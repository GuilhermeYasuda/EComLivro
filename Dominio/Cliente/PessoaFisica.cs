using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Cliente
{
    public class PessoaFisica : Pessoa
    {
		private string _cpf;

		public string CPF
		{
			get { return _cpf; }
			set { _cpf = value; }
		}

		private char _genero;

		public char Genero
		{
			get { return _genero; }
			set { _genero = value; }
		}

		private DateTime ? _dtNascimento;

		public DateTime ? DataNascimento
		{
			get { return _dtNascimento; }
			set { _dtNascimento = value; }
		}

		public PessoaFisica()
		{
			_cpf = "";
			_genero = '\0';
			_dtNascimento = null ;
		}

	}
}
