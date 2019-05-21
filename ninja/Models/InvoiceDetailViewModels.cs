using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ninja.Models
{
    public class InvoiceDetailViewModels
    {

        public double Taxes { get { return 1.21; } }

        [Required]
        public long InvoiceId { get; set; }

        public long Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public double TotalPriceWithTaxes { get; set; }
    }
}