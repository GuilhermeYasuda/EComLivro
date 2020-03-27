using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Livro
{
    public class Autor : EntidadeDominio
    {
		private string _nome;

		public string Nome
		{
			get { return _nome; }
			set { _nome = value; }
		}

		public Autor()
		{
			_nome = "";
		}
	}
}
