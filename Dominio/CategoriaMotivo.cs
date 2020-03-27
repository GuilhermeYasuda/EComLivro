using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CategoriaMotivo : EntidadeDominio
    {
        // atributo usado para exclusão lógica do sistema
        private char _ativo;

        public char Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _descricao;

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public CategoriaMotivo()
        {
            _ativo = 'Z';
            _nome = "";
            _descricao = "";
        }
    }
}
