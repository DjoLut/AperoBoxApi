using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AperoBoxApi.DTO
{
    public class RoleDTO
    {
        [Required]
        public string Nom {get; set;}

    }

}