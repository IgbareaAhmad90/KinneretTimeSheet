using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinneretCollegeTimeSheet.Models.RegistrationModels
{
    public class ReportModel
    {
        public int Id { get; set; }

        [Range(1, 12)]
        [DisplayName("חודש")]
        public int month { get; set; }


        [Required]
        [DisplayName("סוג פגישה")]
        public string type { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayName("תאריך פגישה")]
        public DateTime date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayName("שעת התחלה")]
        public DateTime timeStart { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayName("שעת סיום")]
        public DateTime timeEnd { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("תיעוד המפגש")]
        public String discription { get; set; }
    }
}
