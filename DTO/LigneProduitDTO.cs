using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AperoBoxApi.DTO
{
    public class LigneProduitDTO
    {
        public decimal Id { get; set; }
        public decimal? Quantite { get; set; }
        public decimal? Box { get; set; }
        public decimal? Produit { get; set; }
    }
}