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
        public string ItemId { get; set; }

        public string CartId { get; set; }

        public int Quantidade { get; set; }

        public System.DateTime DataCriada { get; set; }

        public int LivroId { get; set; }

        public virtual Livro Livro { get; set; }

    }
}