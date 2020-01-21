using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AperoBoxApi.DTO
{
    public class CommandeDTO
    {
        public decimal Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreation { get; set; }
        public decimal Utilisateur { get; set; }
        public decimal? Promotion { get; set; }
        public decimal Adresse { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<LigneCommandeDTO> LigneCommande { get; set; }
    }
}