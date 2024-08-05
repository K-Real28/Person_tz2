using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person_tz2
{
    [Table("Persons")]
    public class Person
    {
        [Key]        
        [StringLength(20)]
        public string PersonalId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[А-Яа-я]+$")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Должны быть только кириллические буквы")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Должны быть только кириллические буквы")]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string PhoneNumber { get; set; }
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(PersonalId) &&
                   PersonalId.Length <= 20 &&
                   !string.IsNullOrEmpty(LastName) &&
                   !string.IsNullOrEmpty(FirstName) &&
                   !string.IsNullOrEmpty(MiddleName) &&
                   !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(PhoneNumber) &&
                   Email.Contains("@") &&
                   Email.Contains(".") &&
                   DateTime.TryParseExact(BirthDate.ToString("dd.MM.yyyy"), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
