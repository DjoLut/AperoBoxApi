
using System;
using System.ComponentModel.DataAnnotations;

namespace AperoBoxApi.DTO
{
    public class UtilisateurRoleDTO
    {
        [Required]
        public string IdRole {get; set;}
        [Required]
        public decimal IdUtilisateur {get; set;}

    }

}