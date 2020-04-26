/*
*  FILE          : CreateRoleViewModel.cs
*  PROJECT		 : PROG2030 - Network Application Architecture
*  PROGRAMMER    : Kyle Horsley 2437, Randy Lefebvre 2256, Bence Karner 5307, Lucas Roes 6742
*  DESCRIPTION   : This file contains the CreateRoleViewModel class, which is used to define the 
*                  properties of a users role
*/

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace CollegeBookStore.Models
{
    public class CreateRoleViewModel : Model
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
