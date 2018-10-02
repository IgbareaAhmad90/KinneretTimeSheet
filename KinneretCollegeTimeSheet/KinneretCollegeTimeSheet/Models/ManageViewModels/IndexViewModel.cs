using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinneretCollegeTimeSheet.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [DisplayName("שם משתמש")]
        public string Username { get; set; }

        [DisplayName("אישור חשבון")]
        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("אימייל")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "מספר פלפון")]
        public string PhoneNumber { get; set; }
        [Display(Name = "הודעת מצב")]
        public string StatusMessage { get; set; }

    }
}
