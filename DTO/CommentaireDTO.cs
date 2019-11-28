using System;

namespace AperoBoxApi.DTO
{
    public partial class CommentaireDTO
    {
        public decimal id { get; set; }
        public decimal texte { get; set; }
        public DateTime dateCreation { get; set; }
        public decimal utilisateur { get; set; }
        public decimal box { get; set; }
    }
}