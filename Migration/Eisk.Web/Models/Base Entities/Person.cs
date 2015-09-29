using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eisk.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [StringLength(25)]
        [RegularExpression("^(Mr.|Dr.|Mrs.|Ms.)(, (?!\\1)(Mr.|Dr.|Mrs.|Ms.))*$",
            ErrorMessage = "Allowed values: Mr. , Dr., Mrs. , Ms.")]
        [Display(Name = "Title of Courtesy")]
        //[DisplayFormat(ApplyFormatInEditMode = true, NullDisplayText = "No note provided yet.")]
        public string TitleOfCourtesy { get; set; }

        [Required(ErrorMessage = "First name required.")]
        [StringLength(10)]
        [Display(Name = "First Name")]
        //[UIHint("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name required.")]
        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        public Address Address { get; set; }

        [Required(ErrorMessage = "Phone required")]
        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(4)]
        public string Extension { get; set; }

        [DataType(DataType.MultilineText)]
        [Column(TypeName = "ntext")]
        [UIHint("TextArea")]
        public string Notes { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }
    }
}