using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AperoBoxApi.DTO
{
    public class BoxDTO
    {
        public decimal Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nom { get; set; }
        [Required]
        [Range(0, 999.99)]
        public decimal PrixUnitaireHtva { get; set; }
        [Required]
        [Range(0, 99.99)]
        public decimal Tva { get; set; }
        [Range(0, 100)]
        public decimal? Promotion { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        public string Photo { get; set; }
        public byte Affichable { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreation { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<CommentaireDTO> Commentaire { get; set; }
        public virtual ICollection<LigneCommandeDTO> LigneCommande { get; set; }
        public virtual ICollection<LigneProduitDTO> LigneProduit { get; set; }
    }
}