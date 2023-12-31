#pragma checksum "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "17f54707143eda3e8ee2a8b1d6e8eee6fc123ef6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_OfficeManager_CareContract), @"mvc.1.0.view", @"/Views/OfficeManager/CareContract.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"17f54707143eda3e8ee2a8b1d6e8eee6fc123ef6", @"/Views/OfficeManager/CareContract.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3a8c863e2a636f978ad2ebfc09c57a9f12d8c8ff", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_OfficeManager_CareContract : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Hospital_App.Models.Contract>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
  
    ViewBag.Title = "Contract Information";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>Contract Information</h2>\r\n\r\n");
#nullable restore
#line 9 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
 using (Html.BeginForm("GetContractInformation", "Contract", FormMethod.Post))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group\">\r\n        <label for=\"StartDate\">Start Date:</label>\r\n        ");
#nullable restore
#line 13 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
   Write(Html.TextBox("startDate", null, new { @class = "form-control datepicker" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        <label for=\"EndDate\">End Date:</label>\r\n        ");
#nullable restore
#line 17 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
   Write(Html.TextBox("endDate", null, new { @class = "form-control datepicker" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        <label for=\"Status\">Status:</label>\r\n        ");
#nullable restore
#line 21 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
   Write(Html.TextBox("status", null, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <button type=\"submit\" class=\"btn btn-primary\">Search</button>\r\n");
#nullable restore
#line 24 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<table class=""table table-striped"">
    <thead>
        <tr>
            <th>Contract Date</th>
            <th>Address Line 1</th>
            <th>Address Line 2</th>
            <th>Suburb Name</th>
            <th>Start Date</th>
            <th>End Date</th>
            <th>Wound Description</th>
            <th>Status</th>
            <th>Nurse</th>
            <th>Patient</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 42 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
         foreach (var contract in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 45 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
               Write(contract.AddressLine1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 46 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
               Write(contract.AddressLine2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 47 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
               Write(contract.Suburb);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 48 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
               Write(contract.StartDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 49 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
               Write(contract.EndDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 50 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
               Write(contract.WoundDesc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 51 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
               Write(contract.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 52 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
               Write(contract.Nurse);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 53 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
               Write(contract.Patient);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 55 "D:\Modules\4th Year\ONP400\Assignments\Hospital App\Hospital App\Views\OfficeManager\CareContract.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js""></script>
    <link href=""https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css"" rel=""stylesheet"" />
    <script>
        $('.datepicker').datepicker({
            format: 'yyyy-mm-dd',
            autoclose: true,
            todayHighlight: true
        });
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Hospital_App.Models.Contract>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
