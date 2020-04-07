using Dominio.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Venda
{
    public class Pedido : EntidadeDominio
    {
		private string _user;

		public string Usuario
		{
			get { return _user; }
			set { _user = value; }
		}

		private Endereco _endereco;

		public Endereco EnderecoEntrega
		{
			get { return _endereco; }
			set { _endereco = value; }
		}

		private float _frete;

		public float Frete
		{
			get { return _frete; }
			set { _frete = value; }
		}

		private float _total;

		public float Total
		{
			get { return _total; }
			set { _total = value; }
		}

		private List<PedidoDetalhe> _detalhes;

		public List<PedidoDetalhe> Detalhes
		{
			get { return _detalhes; }
			set { _detalhes = value; }
		}

		private List<CartaoCreditoPedido> _ccs;

		public List<CartaoCreditoPedido> CCs
		{
			get { return _ccs; }
			set { _ccs = value; }
		}

		private Cupom _cupom;

		public Cupom CupomPromocional
		{
			get { return _cupom; }
			set { _cupom = value; }
		}

		private List<Cupom> _cupons;

		public List<Cupom> CuponsTroca
		{
			get { return _cupons; }
			set { _cupons = value; }
		}

		private StatusPedido _status;

		public StatusPedido Status
		{
			get { return _status; }
			set { _status = value; }
		}


		public Pedido()
		{
			_user = "";
			_endereco = new Endereco();
			_frete = (float)0.0;
			_total = (float)0.0;
			_detalhes = new List<PedidoDetalhe>();
			_ccs = new List<CartaoCreditoPedido>();
			_cupom = new Cupom();
			_cupons = new List<Cupom>();
			_status = new StatusPedido();
		}
	}
}
