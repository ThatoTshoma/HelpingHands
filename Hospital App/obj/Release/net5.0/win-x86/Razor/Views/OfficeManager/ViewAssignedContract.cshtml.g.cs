#pragma checksum "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5f52fb72a372e46a2e4ca8dea98ea0d6b4c2bb40"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_OfficeManager_ViewAssignedContract), @"mvc.1.0.view", @"/Views/OfficeManager/ViewAssignedContract.cshtml")]
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
#line 1 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\_ViewImports.cshtml"
using Hospital_App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\_ViewImports.cshtml"
using Hospital_App.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f52fb72a372e46a2e4ca8dea98ea0d6b4c2bb40", @"/Views/OfficeManager/ViewAssignedContract.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3a8c863e2a636f978ad2ebfc09c57a9f12d8c8ff", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_OfficeManager_ViewAssignedContract : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Contract>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml"
  
    ViewData["Title"] = "List Of Assigned Contract";
    Layout = "~/Views/Shared/_OfficeManagerLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""row"">
    <ol class=""breadcrumb"">
        <li>
            <a href=""#"">
                <em class=""fa fa-home""></em>
            </a>
        </li>
        <li class=""active"">Contract</li>
        <li class=""active"">Assigned Contracts</li>
    </ol>
</div>
<h2><b>");
#nullable restore
#line 17 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml"
  Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b></h2>
<hr />



<table class=""table table-bordered"">
    <thead>
        <tr>
            <th>Contract Date</th>
            <th>Nurse Name</th>
            <th>Patient Name</th>
            <th>Address Line 1</th>
            <th>Address Line 2</th>
            <th>Suburb</th>
            <th>Status</th>


        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 37 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml"
         foreach (var contract in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 40 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml"
               Write(contract.ContractDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 41 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml"
               Write(contract.Nurse.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 42 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml"
               Write(contract.Patient.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 43 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml"
               Write(contract.AddressLine1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 44 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml"
               Write(contract.AddressLine2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 45 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml"
               Write(contract.Suburb.SuburbName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 46 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml"
               Write(contract.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n\r\n         \r\n\r\n            </tr>\r\n");
#nullable restore
#line 52 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\ViewAssignedContract.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Contract>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
