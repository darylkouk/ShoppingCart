#pragma checksum "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "445a27e0e80de6d72ce9da3e3cea6f860e644b13"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_PurchaseHistory), @"mvc.1.0.view", @"/Views/Account/PurchaseHistory.cshtml")]
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
#line 1 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\_ViewImports.cshtml"
using ShoppingCart;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\_ViewImports.cshtml"
using ShoppingCart.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"445a27e0e80de6d72ce9da3e3cea6f860e644b13", @"/Views/Account/PurchaseHistory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a65a8198b297c3ea7f3bc4f7f8cdac67c148a265", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_PurchaseHistory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
  
    ViewData["Title"] = "PurchaseHistory";
    Layout = "~/Views/Shared/_Layout.cshtml";
    //List<PurchaseDetails> history = (List<PurchaseDetails>)ViewData["purchase history"]; //this is useless now following the accountcontrollers
    List<string> totalproductname = (List<string>)ViewData["productnames"];
    List<string> totalactivationcode = (List<string>)ViewData["activationcodes"];
    List<DateTime> totalcreateddate = (List<DateTime>)ViewData["createddates"];
    string username = (string)ViewData["username"];
    username = username.Substring(0, 1).ToUpper() + username.Substring(1);
    bool odd = false;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"PHWelcome\">Welcome to your purchase history, ");
#nullable restore
#line 14 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
                                                    Write(username);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 15 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
  
    if (totalactivationcode.Count() == 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"PHFrameNoPurchase\"><p>You don\'t have any past purchase yet ");
#nullable restore
#line 18 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
                                                                          Write(username);

#line default
#line hidden
#nullable disable
            WriteLiteral(", why dont you click <a href=\"/Home/Gallery\">here</a> to buy some.</p></div>\r\n");
#nullable restore
#line 19 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
    }



    for (int i = 0; i < totalactivationcode.Count(); i++)
    {
        odd = !odd;
        string[] activationcodes = totalactivationcode[i].Split(" ");
        string src = "/Images/" + totalproductname[i] + ".jpg";

        string framecolour = (odd) ? "PHFrameodd" : "PHFrameeven";

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div");
            BeginWriteAttribute("class", " class=", 1254, "", 1273, 1);
#nullable restore
#line 30 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
WriteAttributeValue("", 1261, framecolour, 1261, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" align=\"justify\">\r\n\r\n            <div class=\"PHPic\" align=\"left\"> <img");
            BeginWriteAttribute("src", " src=\"", 1343, "\"", 1353, 1);
#nullable restore
#line 32 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
WriteAttributeValue("", 1349, src, 1349, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\'300\' height=\"300\" style=\"object-fit:fill\"></div>\r\n            <div class=\"PHDesc\" align=\"right\">\r\n                <div class=\"PHName\">");
#nullable restore
#line 34 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
                               Write(totalproductname[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </div>\r\n                <div class=\"PHDate\">");
#nullable restore
#line 35 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
                               Write(totalcreateddate[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </div>\r\n                <div class=\"PHCode\">\r\n                    Activation code<br />\r\n");
#nullable restore
#line 38 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
                     if (activationcodes.Length > 1)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            WriteLiteral("<select id=\"dropdown\">\r\n");
#nullable restore
#line 41 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
                            foreach (string y in activationcodes)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            WriteLiteral("<option value=\"\">\r\n");
#nullable restore
#line 44 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
                               Write(y);

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            WriteLiteral("</option>\r\n");
#nullable restore
#line 46 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            WriteLiteral("</select>\r\n");
#nullable restore
#line 48 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
                    }
                    else
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
                   Write(totalactivationcode[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            WriteLiteral("\r\n");
#nullable restore
#line 53 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n            <br />\r\n        </div>\r\n");
#nullable restore
#line 58 "C:\Users\Daryl\source\repos\darylkouk\ShoppingCart\Views\Account\PurchaseHistory.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
