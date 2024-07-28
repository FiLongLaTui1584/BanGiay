using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BanGiay.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Thông tin liên hệ là bắt buộc.")]
        [CustomValidation(typeof(ForgotPasswordViewModel), "ValidateContactInfo")]
        public string ContactInfo { get; set; }

        public static ValidationResult ValidateContactInfo(string contactInfo, ValidationContext context)
        {
            if (string.IsNullOrEmpty(contactInfo))
            {
                return new ValidationResult("Thông tin liên hệ là bắt buộc.");
            }

            bool isEmail = new EmailAddressAttribute().IsValid(contactInfo);
            bool isPhoneNumber = Regex.IsMatch(contactInfo, @"^[0-9]{10}$"); 

            if (!isEmail && !isPhoneNumber)
            {
                return new ValidationResult("Định dạng email hoặc số điện thoại không hợp lệ.");
            }

            return ValidationResult.Success;
        }
    }

    public class VerifyOtpViewModel
    {
        public string Otp { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Mật khẩu mới là bắt buộc.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }
    }

}
