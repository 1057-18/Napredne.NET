#pragma checksum "/Users/jovanbubonja/Desktop/Employment WebApp/EmploymentWebApp/Views/Home/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ea7686468b937a2914e774e77539b35a1862b8d5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "/Users/jovanbubonja/Desktop/Employment WebApp/EmploymentWebApp/Views/_ViewImports.cshtml"
using EmploymentWebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/jovanbubonja/Desktop/Employment WebApp/EmploymentWebApp/Views/_ViewImports.cshtml"
using EmploymentWebApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea7686468b937a2914e774e77539b35a1862b8d5", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"55ea11830f4608089ede7055ca2e8753b34332a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/jovanbubonja/Desktop/Employment WebApp/EmploymentWebApp/Views/Home/Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <div class=""row"" style=""padding-top: 15px"">
        <div class=""col"" style=""height: 280px; width: 300px"">
            <canvas id=""doughnutChart""></canvas>
        </div>
        <div class=""col"">
            <label style=""font-size: 1.5rem; padding-top: 130px; padding-left: 110px"">Total employee count : ");
#nullable restore
#line 11 "/Users/jovanbubonja/Desktop/Employment WebApp/EmploymentWebApp/Views/Home/Index.cshtml"
                                                                                                        Write(ViewBag.NumberOfEmployees);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n        </div>\r\n    </div>\r\n    <div class=\"row\" style=\"padding-top: 45px\">\r\n        <div class=\"col\">\r\n            <label style=\"font-size: 1.5rem; padding-top: 116px; padding-left: 60px\">Year payment sum : ");
#nullable restore
#line 16 "/Users/jovanbubonja/Desktop/Employment WebApp/EmploymentWebApp/Views/Home/Index.cshtml"
                                                                                                   Write(ViewBag.YearPaymentSum);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n        </div>\r\n        <div class=\"col\" style=\"height: 280px; width: 300px\">\r\n            <canvas id=\"lineChart\"></canvas>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
