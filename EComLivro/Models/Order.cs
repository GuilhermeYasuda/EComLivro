using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EComLivro.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public System.DateTime OrderDate { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public decimal Total { get; set; }
    }
}