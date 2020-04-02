using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace EComLivro.Models
{
    public class LivroMap : EntityTypeConfiguration<Livro>
    {
        public LivroMap()
        {
            ToTable("tb_livro");        // tabela do BD que está sendo mapeada
            HasKey(c => c.id_livro);    // indica a PK da tabela
        }
    }
}