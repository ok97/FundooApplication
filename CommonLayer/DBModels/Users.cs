using CommonLayer.DatabaseModel;
using CommonLayer.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Your FirstName should only contain Alphabets!")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Your LastName should only contain Alphabets!")]
        public string LastName { get; set; }

        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; }

        public List<Note> Notes { get; set; }
        public List<UserLabel> UserLabels { get; set; }
    }
}
