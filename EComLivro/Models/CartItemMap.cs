using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace EComLivro.Models
{
    public class CartItemMap : EntityTypeConfiguration<CartItem>
    {
        public CartItemMap()
        {
            ToTable("tb_cart_item");        // tabela do BD que está sendo mapeada
            HasKey(c => c.item_id);    // indica a PK da tabela
        }
    }
}