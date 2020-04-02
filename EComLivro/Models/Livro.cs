using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EComLivro.Models
{
    public class Livro
    {
        [Key]
        public int id_livro { get; set; }
        public string ano_livro { get; set; }
        public string titulo_livro { get; set; }
        public int editora_fk { get; set; }
        public string edicao_livro { get; set; }
        public string isbn { get; set; }
        public string numero_paginas { get; set; }
        public string sinopse { get; set; }
        public int dimensoes_fk { get; set; }
        public int grupo_preco_fk { get; set; }
        public string codigo_barras_livro { get; set; }
        public int categoria_motivo_fk { get; set; }
        public string motivo { get; set; }
        public DateTime dt_cadastro_livro { get; set; }
    }
}