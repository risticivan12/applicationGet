#pragma checksum "C:\Users\Ivan\source\repos\ApplicationGet\ApplicationGet\Views\Student\ExamsPartialView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "308127df18beae9560ff3c248a2941469a6eb1c8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_ExamsPartialView), @"mvc.1.0.view", @"/Views/Student/ExamsPartialView.cshtml")]
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
#line 1 "C:\Users\Ivan\source\repos\ApplicationGet\ApplicationGet\Views\_ViewImports.cshtml"
using ApplicationGet;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Ivan\source\repos\ApplicationGet\ApplicationGet\Views\_ViewImports.cshtml"
using ApplicationGet.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Ivan\source\repos\ApplicationGet\ApplicationGet\Views\_ViewImports.cshtml"
using Data.DTO;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"308127df18beae9560ff3c248a2941469a6eb1c8", @"/Views/Student/ExamsPartialView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ac71734386163c78dcbab4d5e004aa3a91a3c7a", @"/Views/_ViewImports.cshtml")]
    public class Views_Student_ExamsPartialView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<PassedExam>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
            WriteLiteral(@"
<div class=""modal-dialog"">
    <div class=""modal-content"">
        <div class=""modal-header"">
            <h5 class=""modal-title"" id=""exampleModalLabel""  style=""text-align: center"">Položeni ispiti</h5>
            <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                <span aria-hidden=""true"">&times;</span>
            </button>
        </div>
        <div class=""modal-body"">
");
#nullable restore
#line 14 "C:\Users\Ivan\source\repos\ApplicationGet\ApplicationGet\Views\Student\ExamsPartialView.cshtml"
             foreach (PassedExam pe in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div style=\"text-align: left\">\r\n                    <h4>");
#nullable restore
#line 17 "C:\Users\Ivan\source\repos\ApplicationGet\ApplicationGet\Views\Student\ExamsPartialView.cshtml"
                   Write(pe.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 17 "C:\Users\Ivan\source\repos\ApplicationGet\ApplicationGet\Views\Student\ExamsPartialView.cshtml"
                               Write(pe.Mark);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                </div>\r\n");
#nullable restore
#line 19 "C:\Users\Ivan\source\repos\ApplicationGet\ApplicationGet\Views\Student\ExamsPartialView.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"modal-footer\">\r\n                <button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">Close</button>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<PassedExam>> Html { get; private set; }
    }
}
#pragma warning restore 1591
