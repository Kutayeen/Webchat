using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Webchat.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "UserName must be between 5-50 characters", MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(50,ErrorMessage = "UserMail must be between 5-50 characters", MinimumLength = 5)]
        public string UserMail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "UserPassword must be between 5-50 characters", MinimumLength = 5)]
        public string UserPassword { get; set; }
        [Required]
        public DateTime? UserCreated { get; set; }
        public byte[] UserPhoto { get; set; }
        public bool IsAdmin { get; set; }

    }
}
