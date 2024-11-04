using Microsoft.AspNetCore.Razor.TagHelpers;

namespace subscription_system.TagHelpers {
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("input", Attributes ="asp-mask")]
    public class InputMaskTagHelper : TagHelper {


        public string AspMask { set; get; } = "";
        public override void Process(TagHelperContext context, TagHelperOutput output) {
            if (output.TagName == "input") output.Attributes.SetAttribute("Data-mask", AspMask);

            if (AspMask == "currency") {
                output.Attributes.SetAttribute("data-prefix", "$");
                output.Attributes.SetAttribute("data-group-separator", ",");
                output.Attributes.SetAttribute("data-digits", "2");
                output.Attributes.SetAttribute("data-right-align", "false");
            }
        }
    }
}
