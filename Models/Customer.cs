﻿using System.ComponentModel.DataAnnotations;

namespace SverigesFordonsFörening.Data
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string SocialSecurityNumber { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}