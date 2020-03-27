using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Livro
{
    public class Dimensoes : EntidadeDominio
    {
		private int _altura;

		public int Altura
		{
			get { return _altura; }
			set { _altura = value; }
		}

		private int _largura;

		public int Largura
		{
			get { return _largura; }
			set { _largura = value; }
		}

		private int _profundidade;

		public int Profundidade
		{
			get { return _profundidade; }
			set { _profundidade = value; }
		}

		private float _peso;

		public float Peso
		{
			get { return _peso; }
			set { _peso = value; }
		}

		public Dimensoes()
		{
			_altura = 0;
			_largura = 0;
			_profundidade = 0;
			_peso = (float)0.0;
		}
	}
}
