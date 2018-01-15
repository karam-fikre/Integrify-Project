using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MBotRangerCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using MBotRangerCore.Models.ImagesModels;

namespace MBotRangerCore.Models
{
    public class MBotRangerCoreContext : IdentityDbContext<ApplicationUser>
    {
        public MBotRangerCoreContext(DbContextOptions<MBotRangerCoreContext> options)
            : base(options)
        { }
  
        public DbSet<MBotRangerCore.Models.LoginViewModel> LoginViewModel { get; set; }
        public DbSet<ImagesModels.GalleryImage> GalleryImage { get; set; }
        
        
    }

    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY/MM/DD}")]
        public DateTime DateOfBirth { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

     

    }

}
