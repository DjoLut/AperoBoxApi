
using System;
using System.ComponentModel.DataAnnotations;

namespace AperoBoxApi.DTO
{
    public class UtilisateurRoleDTO
    {
        [Required]
        public string IdRole {get; set;}
        [Required]
        public int IdUtilisateur {get; set;}
        [Required]
        public RoleDTO Role {get; set;}

    }

}