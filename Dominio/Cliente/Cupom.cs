using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Cliente
{
    public class Cupom : EntidadeDominio
    {
		private int _idPedido;

		public int IdPedido
		{
			get { return _idPedido; }
			set { _idPedido = value; }
		}

		private int _idCliente;

		public int IdCliente
		{
			get { return _idCliente; }
			set { _idCliente = value; }
		}

		private string _codigo;

		public string CodigoCupom
		{
			get { return _codigo; }
			set { _codigo = value; }
		}

		private TipoCupom _tipo;

		public TipoCupom Tipo
		{
			get { return _tipo; }
			set { _tipo = value; }
		}

		// atributo usado para exclusão lógica do sistema
		private char _status;

		public char Status
		{
			get { return _status; }
			set { _status = value; }
		}

		private float _valor;

		public float ValorCupom
		{
			get { return _valor; }
			set { _valor = value; }
		}

		public Cupom()
		{
			_idPedido = 0;
			_idCliente = 0;
			_codigo = "";
			_tipo = new TipoCupom();
			_status = 'Z';
			_valor = (float)0.0;
		}

	}
}
