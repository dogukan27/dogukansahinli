using System.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using shopapp.business.Abstract;
using shopapp.business.Concreate;
using shopapp.data.Abstract;
using shopapp.data.Concrete.EfCore;
using shopapp.webui.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using shopapp.webui.EmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.HttpOverrides;

namespace shopapp.webui
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            this._configuration=configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //User identity işlemleri
            services.AddDbContext<ApplicationContext>(options=>options.UseSqlite("Data Source=shopDb"));
            services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            //Identity Ayarları
            services.Configure<IdentityOptions>(options=>{
                //şifre işlemleri

                //Şifrede mutlaka sayı olacak
                options.Password.RequireDigit=true;
                //Şifrede mutlaka küçük harf olacak
                options.Password.RequireLowercase=true;
                //Şifrede mutlaka büyük harf olacak
                options.Password.RequireUppercase=true;
                //Şifre minimum girilen karekter kadar olur.
                options.Password.RequiredLength=6;
                //Şifrede . - gibi alfanümerik ifade olacak
                options.Password.RequireNonAlphanumeric=true;


                //Kullanıcı kilitleme işlemleri(Lockout)

                //5 girişten sonra hesabı kitler
                options.Lockout.MaxFailedAccessAttempts=5;
                //Kitlenen hesap şu kadar sonra yeniden girebilir gibi
                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);
                //Lockout işlemlerinin aktif olması için
                options.Lockout.AllowedForNewUsers=true;

                //Hesap işlemleri

                //Her e mailden sadece bir hesap açılabilir
                options.User.RequireUniqueEmail=true;
                //Kullanıcı üye olduktan sonra onay maili ile hesabı aktif olur
                //Şimdilik false olsun sonra true yapacaz bu onay kodunu yazarken
                options.SignIn.RequireConfirmedEmail=true;
                //Telefon için de onay mesajı ile aktif olur hesap
                //Şimdilik false olsun sonra true yapacaz bu onay kodunu yazarken
                options.SignIn.RequireConfirmedPhoneNumber=false;
            });

            //Tarayıcı çerez ayarları
            services.ConfigureApplicationCookie(options =>{
                //Çerezlerin süresi bittiğinde tarayıcı hesaptan çıkış yapar
                //Çıkış yapınca giriş yapma ve çıkış yapma sayfalarını
                //tanıtmamız lazım o yüzden
                options.LoginPath="/account/login";
                options.LogoutPath="/account/logout";
                //Kullanıcıların yetkisinin olmadığı sayfaya girdiğinde
                //karşısına çıkacak sayfanın yolu
                options.AccessDeniedPath="/account/accessdenied";
                //Çerezlerin süresini 20 dakika yapar bu sürede işlem
                //yapılmazsa tarayıcı çerezleri siler
                options.SlidingExpiration=true;
                //Kullanıcının sitede kaç gün tanınacağını ayarlar
                //sonra geri giriş yapması lazım
                options.ExpireTimeSpan=TimeSpan.FromDays(30);

                //Çerezlerin yapılandırılması
                options.Cookie=new CookieBuilder{
                    //Sadece http ile gelen sayfalar bilgileri alır
                    //javascript falan alamaz bu önemli
                    HttpOnly=true,
                    //Cookie ye isim verme
                    Name=".ShopApp.Security.Cookie",
                    SameSite=SameSiteMode.Strict
                };



            });

            //Business katmanları yönlendirme
            services.AddScoped<IProductService,ProductManager>();
            services.AddScoped<ICategoryService,CategoryManager>();
            services.AddScoped<ICartService,CartManager>();

            //Email katmanları yönlendirme
            services.AddScoped<IEmailSender,SmtpEmailSender>(i=>
                new SmtpEmailSender(_configuration["EmailSender:Host"],
                _configuration.GetValue<int>("EmailSender:Port"),
                _configuration.GetValue<bool>("EmailSender:EnableSSl"),
                _configuration["EmailSender:UserName"],
                _configuration["EmailSender:Password"]
                )
            );

            //data katmanları yönlendirme
            services.AddScoped<ICategoryRepository,EfCoreCategoryRepository>();
            services.AddScoped<IProductRepository,EfCoreProductRepository>();
            services.AddScoped<ICartRepository,EfCoreCartRepository>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IConfiguration configuration,UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            app.UseStaticFiles(); // wwwroot için
            app.UseAuthentication(); //User Identity işlemleri için
            

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"node_modules")),
                    RequestPath="/modules"                
            });

            if (env.IsDevelopment())
            {
                SeedDatabase.Seed();
                app.UseDeveloperExceptionPage();
            }
            //Admin kullanıcısının eklenmesi
            SeedIdentity.Seed(userManager,roleManager,configuration).Wait();
             
            app.UseRouting();
            app.UseAuthorization(); //Admin sayfalarına erişim düzenleme

            app.UseEndpoints(endpoints =>
            {

                 endpoints.MapControllerRoute(
                    name: "product",
                    pattern:"urunler/",
                    defaults:new {controller="Product",action="List"}
                );

                endpoints.MapControllerRoute(
                    name: "rolelist",
                    pattern:"admin/role/list",
                    defaults:new {controller="Admin",action="RoleList"}
                );

                endpoints.MapControllerRoute(
                    name: "rolecreate",
                    pattern:"admin/role/create",
                    defaults:new {controller="Admin",action="RoleCreate"}
                );
                endpoints.MapControllerRoute(
                    name: "userlist",
                    pattern:"admin/user/list",
                    defaults:new {controller="Admin",action="UserList"}
                );

                 endpoints.MapControllerRoute(
                    name: "useredit",
                    pattern:"admin/user/edit/{id?}",
                    defaults:new {controller="Admin",action="UserEdit"}
                );

                 endpoints.MapControllerRoute(
                    name: "userdelete",
                    pattern:"admin/user/delete/{id?}",
                    defaults:new {controller="Admin",action="UserDelete"}
                );

                

                 endpoints.MapControllerRoute(
                    name: "roleedit",
                    pattern:"admin/role/edit/{id?}",
                    defaults:new {controller="Admin",action="RoleEdit"}
                );

                endpoints.MapControllerRoute(
                    name: "cart",
                    pattern:"cart/item",
                    defaults:new {controller="Cart",action="Item"}
                );

                 endpoints.MapControllerRoute(
                    name: "chechkout",
                    pattern:"cart/checkout",
                    defaults:new {controller="Cart",action="Checkout"}
                );





                // kategorileri listeleme
                 endpoints.MapControllerRoute(
                    name: "categorylist",
                    pattern:"admin/categories",
                    defaults:new {controller="Admin",action="CategoryList"}
                );

                // Kategori ekleme
                 endpoints.MapControllerRoute(
                    name: "categorycreate",
                    pattern:"admin/categories/create",
                    defaults:new {controller="Admin",action="CategoryCreate"}
                );

                // Kategori düzenleme
                 endpoints.MapControllerRoute(
                    name: "categoryedit",
                    pattern:"admin/categories/edit/{id?}",
                    defaults:new {controller="Admin",action="CategoryEdit"}
                );

                // Kategori silme
                 endpoints.MapControllerRoute(
                    name: "categoryedit",
                    pattern:"admin/categories/delete/{id?}",
                    defaults:new {controller="Admin",action="CategoryDelete"}
                );

                 // Kategoriden ürün silme
                 endpoints.MapControllerRoute(
                    name: "categorydenurunsilme",
                    pattern:"admin/categories/productdelete",
                    defaults:new {controller="Admin",action="CategoryProductDelete"}
                );

                // üründen kategori silme
                 endpoints.MapControllerRoute(
                    name: "urundenkategorisilme",
                    pattern:"admin/categories/categorydelete",
                    defaults:new {controller="Admin",action="ProductCategoryDelete"}
                );


                  endpoints.MapControllerRoute(
                    name: "home",
                    pattern:"home",
                    defaults:new {controller="Home",action="index"}
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
