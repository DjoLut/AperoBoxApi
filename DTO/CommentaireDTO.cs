using System;

namespace AperoBoxApi.DTO
{
    public partial class CommentaireDTO
    {
        public decimal Id { get; set; }
        public decimal Texte { get; set; }
        public DateTime DateCreation { get; set; }
        public decimal Utilisateur { get; set; }
        public decimal Box { get; set; }
    }
}