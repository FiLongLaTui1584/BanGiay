using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace BanGiay.Models
{
    public class EditUserViewModel
    {
        public int maUser { get; set; }

        [Required]
        public string TenTK { get; set; }

        [Required]
        public string SDT { get; set; }

        public string diaChi { get; set; }

        public DateTime? ngaySinh { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Pass { get; set; }
    }
}