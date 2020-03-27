using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Livro
{
    public class Livro : EntidadeDominio
    {
		private List<Autor> _autores;

		public List<Autor> Autores
		{
			get { return _autores; }
			set { _autores = value; }
		}

		private List<Categoria> _categorias;

		public List<Categoria> Categorias
		{
			get { return _categorias; }
			set { _categorias = value; }
		}

		private string _ano;

		public string Ano
		{
			get { return _ano; }
			set { _ano = value; }
		}

		private string _titulo;

		public string Titulo
		{
			get { return _titulo; }
			set { _titulo = value; }
		}

		private Editora _editora;

		public Editora Editora
		{
			get { return _editora; }
			set { _editora = value; }
		}

		private string _edicao;

		public string Edicao
		{
			get { return _edicao; }
			set { _edicao = value; }
		}

		private string _isbn;

		public string ISBN
		{
			get { return _isbn; }
			set { _isbn = value; }
		}

		private string _nPaginas;

		public string NumeroPaginas
		{
			get { return _nPaginas; }
			set { _nPaginas = value; }
		}

		private string _sinopse;

		public string Sinopse
		{
			get { return _sinopse; }
			set { _sinopse = value; }
		}

		private Dimensoes _dimensoes;

		public Dimensoes Dimensoes
		{
			get { return _dimensoes; }
			set { _dimensoes = value; }
		}

		private GrupoPrecificacao _grupoPrecificacao;

		public GrupoPrecificacao GrupoPrecificacao
		{
			get { return _grupoPrecificacao; }
			set { _grupoPrecificacao = value; }
		}

		private string _codigoBarras;

		public string CodigoBarras
		{
			get { return _codigoBarras; }
			set { _codigoBarras = value; }
		}

		// atributo usado para exclusão lógica do sistema
		private CategoriaMotivo _categoriaMotivo;

		public CategoriaMotivo CategoriaMotivo
		{
			get { return _categoriaMotivo; }
			set { _categoriaMotivo = value; }
		}

		// atributo usado para identificar motivo da ativação/inativação
		private string _motivo;

		public string Motivo
		{
			get { return _motivo; }
			set { _motivo = value; }
		}


		public Livro()
		{
			_autores = new List<Autor>();
			_categorias = new List<Categoria>();
			_ano = "";
			_titulo = "";
			_editora = new Editora();
			_edicao = "";
			_isbn = "";
			_nPaginas = "";
			_sinopse = "";
			_dimensoes = new Dimensoes();
			_grupoPrecificacao = new GrupoPrecificacao();
			_codigoBarras = "";
			_categoriaMotivo = new CategoriaMotivo();
			_motivo = "";
		}
	}
}
