﻿using System.ComponentModel.DataAnnotations;

namespace StarterProject.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}