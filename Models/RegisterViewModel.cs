using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanGiay.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Tên user là bắt buộc")]
        public string TenTK { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại không hợp lệ, Phải gồm 10 số trở lên và không chứa ký tự đặc biệt")]
        [Remote("IsPhoneNumberAvailable", "Home", HttpMethod = "POST", ErrorMessage = "Số điện thoại đã tồn tại")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string diaChi { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [Display(Name = "Ngày sinh")]
        public DateTime ngaySinh { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Remote("IsEmailAvailable", "Home", HttpMethod = "POST", ErrorMessage = "Email đã tồn tại")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password là bắt buộc")]
        public string Pass { get; set; }
    }
}