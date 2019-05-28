using ninja.Models.validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ninja.Models
{
    public class InvoiceViewModels
    {
        [Required]
        public long Id { get; set; }

        [Required,CustomTypeValidator]
        public string Type { get; set; }
        public int CountItems { get;  set; }
    }
}