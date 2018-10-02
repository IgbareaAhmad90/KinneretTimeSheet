using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinneretCollegeTimeSheet.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה נוכחית")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "הסיסמה צריכה להיות לפחות באורך 6 ", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה חדשה")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "אמת סיסמה חדשה")]
        [Compare("NewPassword", ErrorMessage = "הסיסמה אינה זהה לסיסמה שהכנסת! בבקשה הכנס סיסמה זהה!!.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
