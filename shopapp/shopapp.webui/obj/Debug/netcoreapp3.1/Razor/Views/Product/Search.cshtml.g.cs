#pragma checksum "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\Product\Search.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "18820ce7978d35fcae58a3494370ed2a77729c94"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_Search), @"mvc.1.0.view", @"/Views/Product/Search.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\_ViewImports.cshtml"
using shopapp.entity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\_ViewImports.cshtml"
using shopapp.webui.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\_ViewImports.cshtml"
using shopapp.webui.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\_ViewImports.cshtml"
using shopapp.webui.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"18820ce7978d35fcae58a3494370ed2a77729c94", @"/Views/Product/Search.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9c89e762eb59a9c757fde69f5825913f0bc12611", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_Search : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProductViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            DefineSection("Categories", async() => {
                WriteLiteral("\r\n    ");
#nullable restore
#line 5 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\Product\Search.cshtml"
Write(await Component.InvokeAsync("Categories"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral("\r\n");
#nullable restore
#line 8 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\Product\Search.cshtml"
 if(Model.Products.Count == 0)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\Product\Search.cshtml"
Write(await Html.PartialAsync("_noproduct"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\Product\Search.cshtml"
                                          
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"row\">                  \r\n");
#nullable restore
#line 15 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\Product\Search.cshtml"
         foreach (var product in Model.Products)
        {    

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-md-4\">\r\n                ");
#nullable restore
#line 18 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\Product\Search.cshtml"
           Write(await Html.PartialAsync("_product", product));

#line default
#line hidden
#nullable disable
            WriteLiteral("   \r\n        </div>       \r\n");
#nullable restore
#line 20 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\Product\Search.cshtml"
        }   

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n");
#nullable restore
#line 22 "C:\Users\Doğukan\Desktop\web project\shopapp\shopapp.webui\Views\Product\Search.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProductViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
