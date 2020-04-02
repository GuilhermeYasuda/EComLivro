using Dominio.Livro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EComLivro.Models
{
    public class CartItem
    {
        [Key]
        public string item_id { get; set; }

        public string cart_id { get; set; }

        public int quantidade { get; set; }

        public System.DateTime data_criada { get; set; }

        public int livro_id { get; set; }

        public string titulo_livro { get; set; }

        public float valor_venda { get; set; }

        //public virtual Livro Livro { get; set; }

    }
}