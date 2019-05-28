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
        [System.ComponentModel.DisplayName("Invoice")]
        public long InvoiceId { get; set; }

        public long Id { get; set; }

        [Required]
        [System.ComponentModel.DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        [System.ComponentModel.DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

        [Required]
        [System.ComponentModel.DisplayName("Total Price")]
        public double TotalPrice { get; set; }

        [Required]
        [System.ComponentModel.DisplayName("Total Price (With Taxes)")]
        public double TotalPriceWithTaxes { get; set; }
    }
}