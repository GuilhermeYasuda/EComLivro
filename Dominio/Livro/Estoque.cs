using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Livro
{
    public class Estoque : EntidadeDominio
    {
		private Livro _livro;

		public Livro Livro
		{
			get { return _livro; }
			set { _livro = value; }
		}

		private int _qtde;

		public int Qtde
		{
			get { return _qtde; }
			set { _qtde = value; }
		}

		// UNITÁRIO
		private float _custo;

		public float ValorCusto
		{
			get { return _custo; }
			set { _custo = value; }
		}

		private float _venda;

		public float ValorVenda
		{
			get { return _venda; }
			set { _venda = value; }
		}


		private Fornecedor _fornecedor;

		public Fornecedor Fornecedor
		{
			get { return _fornecedor; }
			set { _fornecedor = value; }
		}

		public Estoque()
		{
			_livro = new Livro();
			_qtde = 0;
			_custo = (float)0.0;
			_venda = (float)0.0;
			_fornecedor = new Fornecedor();
		}
	}
}
