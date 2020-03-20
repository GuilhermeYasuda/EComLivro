using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Cliente
{
    public class Endereco : EntidadeDominio
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _destinatario;

        public string Destinatario
        {
            get { return _destinatario; }
            set { _destinatario = value; }
        }


        private TipoResidencia _tipoResidencia;

        public TipoResidencia TipoResidencia
        {
            get { return _tipoResidencia; }
            set { _tipoResidencia = value; }
        }


        private TipoLogradouro _tipoLogradouro;

        public TipoLogradouro TipoLogradouro
        {
            get { return _tipoLogradouro; }
            set { _tipoLogradouro = value; }
        }

        private string _rua;

        public string Rua
        {
            get { return _rua; }
            set { _rua = value; }
        }

        private string _numero;

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        private string _bairro;

        public string Bairro
        {
            get { return _bairro; }
            set { _bairro = value; }
        }

        private Cidade _cidade;

        public Cidade Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }

        private string _cep;

        public string CEP
        {
            get { return _cep; }
            set { _cep = value; }
        }

        private string _observacao;

        public string Observacao
        {
            get { return _observacao; }
            set { _observacao = value; }
        }


        public Endereco()
        {
            _nome = "";
            _destinatario = "";
            _tipoResidencia = new TipoResidencia();
            _tipoLogradouro = new TipoLogradouro();
            _rua = "";
            _numero = "";
            _bairro = "";
            _cidade = new Cidade();
            _cep = "";
            _observacao = "";
        }
    }
}
