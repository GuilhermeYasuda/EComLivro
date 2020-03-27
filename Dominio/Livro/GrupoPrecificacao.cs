using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Livro
{
    public class GrupoPrecificacao : EntidadeDominio
    {
		private string _nome;

		public string Nome
		{
			get { return _nome; }
			set { _nome = value; }
		}

		private float _margemLucro;

		public float MargemLucro
		{
			get { return _margemLucro; }
			set { _margemLucro = value; }
		}

		public GrupoPrecificacao()
		{
			_nome = "";
			_margemLucro = (float)0.0;
		}

	}
}
