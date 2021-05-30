using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.webui.EmailService;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    public class AccountController:Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailsender;
        private ICartService _cartService;
        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager,IEmailSender emailsender,
        ICartService cartService)
        {
            this._cartService=cartService;
            this._userManager=userManager;
            this._signInManager=signInManager;
            this._emailsender=emailsender;
            
        }
        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModels model){
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user=await _userManager.FindByNameAsync(model.UserName);
            if (user==null)
            {
                ModelState.AddModelError("","Bu kullanıcı adı ile kayıt yok");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                 ModelState.AddModelError("","Lütfen hesabınızı onaylayın.");
                return View(model);
            }
            var result=await _signInManager.PasswordSignInAsync(user,model.Password,true,false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }
             ModelState.AddModelError("","Girilen kullanıcı adı veya parola yanlış");
            return View();

        }

        public async Task<IActionResult> LogoutAsync(){
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public IActionResult Register(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterModel model){
            if(!ModelState.IsValid){
                return View(model);
            }
            var user=new User(){
                FirstName=model.FirstName,
                LastName=model.LastName,
                UserName=model.UserName,
                Email=model.Email
            };
            var result=await _userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user,"müşteri");
                //token üretme
                var code=await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //token ile karşılaştırma yapmak için url oluşturulur
                var url=Url.Action("ConfirmEmail","Account",new{
                    userId=user.Id,
                    token=code
                });
                //kullanıcıya email gönderme
                await _emailsender.SendEmailAsync(model.Email,"Hesabınızı Onaylayınız",$"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:5001{url}'>tıklayınız</a>");
                return RedirectToAction("Login","Account");
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if (userId==null || token==null)
            {
                TempData["message"]="Geçersiz Token";
                return View();
            }
            var user=await _userManager.FindByIdAsync(userId);
            if (user!=null)
            {
                var result=await _userManager.ConfirmEmailAsync(user,token);
                if (result.Succeeded)
                {
                    //Kullanıcı hesabını onayladığı anda sepeti oluşacak.
                    _cartService.InitializeCart(user.Id);
                    TempData["message"]="Hesabınız Onaylandı";
                    return View();
                }
            }

            
            TempData["message"]="Hesabınız Onaylanmadı.";
            return View();
            

        }

        public IActionResult ForgotPassword(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email){
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }
            var user=await _userManager.FindByEmailAsync(Email);
            if (user==null)
            {
                return View();
                
            }
            var code=await _userManager.GeneratePasswordResetTokenAsync(user);
            
                //PasswordReset sayfasına url gider.
            var url=Url.Action("PasswordReset","Account",new{
                    userId=user.Id,
                    token=code
            });
                //kullanıcıya email gönderme
            await _emailsender.SendEmailAsync(Email,"Şifre Sıfırlama",$"Lütfen şifrenizi sıfırlamak için linke <a href='https://localhost:5001{url}'>tıklayınız</a>");
            return View();
        }


        public async Task<IActionResult> PasswordReset(string userId,string token){
            if (userId==null || token==null)
            {
                return RedirectToAction("Login","Account");
            }
            var user=await _userManager.FindByIdAsync(userId);
            var model=new ResetPasswordModel{Token=token,Email=user.Email};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PasswordReset(ResetPasswordModel model){
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user=await _userManager.FindByEmailAsync(model.Email);
            if (user==null)
            {
                return RedirectToAction("Index","Home");
            }
            var result=await _userManager.ResetPasswordAsync(user,model.Token,model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login","Account");
            }
            return View();

        }

        public IActionResult AccessDenied(){
            return View();
        }

        





    }
}