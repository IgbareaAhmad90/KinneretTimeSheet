using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KinneretCollegeTimeSheet.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public override DateTimeOffset? LockoutEnd { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if two factor authentication is enabled for this
        //     user.
        public override bool TwoFactorEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if a user has confirmed their telephone address.
        public override bool PhoneNumberConfirmed { get; set; }
        //
        // Summary:
        //     Gets or sets a telephone number for the user.
        [DisplayName("מס' טלפון")]

        public override string PhoneNumber { get; set; }
        //
        // Summary:
        //     A random value that must change whenever a user is persisted to the store
        public override string ConcurrencyStamp { get; set; }
        //
        // Summary:
        //     A random value that must change whenever a users credentials change (password
        //     changed, login removed)
        public override string SecurityStamp { get; set; }
        //
        // Summary:
        //     Gets or sets a salted and hashed representation of the password for this user.
        public override string PasswordHash { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if a user has confirmed their email address.
        public override bool EmailConfirmed { get; set; }
        //
        // Summary:
        //     Gets or sets the normalized email address for this user.
        public override string NormalizedEmail { get; set; }
        //
        // Summary:
        //     Gets or sets the email address for this user.
        [DisplayName("אימייל")]
        public override string Email { get; set; }
        //
        // Summary:
        //     Gets or sets the normalized user name for this user.
        public override string NormalizedUserName { get; set; }
        //
        // Summary:
        //     Gets or sets the user name for this user.
        
        public override string UserName { get; set; }

        //
        // Summary:
        //     Gets or sets a flag indicating if the user could be locked out.
        public override bool LockoutEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets the number of failed login attempts for the current user.
        public override int AccessFailedCount { get; set; }

        [Required]
        [DisplayName("ת.ז")]
        [StringLength(150)]
        public string CertificateID { get; set; }

        public  ICollection<UserCourse> UserCourses { get; set; }

        public  ICollection<Models.RoleViewModel> UserRoles { get; set; }


    }
}
