using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SignUpCode.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        [Column("Passenger_Name")]
        public string Passenger_Name { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match. Please re-enter.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.Password)]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class registrationDB : DbContext
    {
        public registrationDB() : base("TMUsersLogin")
        {

        }

        public DbSet<User> Users { get; set; }

    }



}

