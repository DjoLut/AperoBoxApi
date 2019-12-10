using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AperoBoxApi.DTO
{
    public class ProduitDTO
    {
        public decimal Id { get; set; }
        [Required]
        [Range(0, 999.99)]
        public decimal PrixUnitaireHtva { get; set; }
        [Required]
        [Range(0, 99.99)]
        public decimal Tva { get; set; }
        [Required]
        [StringLength(100)]
        public string Nom { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DatePeremption { get; set; }
        [Required]
        public byte Alcool { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<LigneCommandeDTO> LigneCommande { get; set; }
        public virtual ICollection<LigneProduitDTO> LigneProduit { get; set; }
    }
}