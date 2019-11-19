namespace BreakPoint.Data.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username required!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be min 2 and maximum 20 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email required!")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid Email.")]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Salt { get; set; }

        [MaxLength(50)]
        public string Nickname { get; set; }

        public string Location { get; set; }

        public string Gender { get; set; }

        public string DanceStyle { get; set; }

        public int Skilled { get; set; } // likes

        public virtual ICollection<User> Followers { get; set; } // Every user has other users as followers

        public virtual ICollection<TokenManager> Tokens { get; set;}
    }
}
