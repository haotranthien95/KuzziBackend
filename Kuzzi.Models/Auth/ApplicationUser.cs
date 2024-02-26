using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;



namespace Kuzzi.Models.Auth
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name {get;set;}

    }
}