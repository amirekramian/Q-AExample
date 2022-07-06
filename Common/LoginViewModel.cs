using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage ="لطفا{0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد!")]
        public string? UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]

        public bool RememberMe { get; set; }
    }
}