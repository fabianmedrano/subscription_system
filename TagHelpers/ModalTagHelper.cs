using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace subscription_system.TagHelpers
{

    [HtmlTargetElement("modal")]
    public class ModalTagHelper : TagHelper
    {
   
        public string IdModal { get; set; } = "";
        public string TitleModal { get; set; } = "";
        public string Size { get; set; } = "md";
        
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var IdTitleModal = TitleModal.Replace(" ", "-").ToLower();
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "modal fade");
            output.Attributes.SetAttribute("id", IdModal);
            output.Attributes.SetAttribute("tabindex", -1);

            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("aria-labelledby", $"{IdModal}-label");
            output.Attributes.SetAttribute("aria-hidden", true);



            var modalContent = await output.GetChildContentAsync();
           
            output.Content.SetHtmlContent( 
                $@"
                  <div class=""modal-dialog modal-{Size} modal-dialog-centered"" role=""document"">
                    <div class=""modal-content"">
                      <div class=""modal-header"">
                        <h5 class=""modal-title"" id=""{IdTitleModal}-id"">{TitleModal}</h5>
                        <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                          <span aria-hidden=""true"">&times;</span>
                        </button>
                      </div>
                      <div class=""modal-body"">
                        {modalContent}
                      </div>
                      <div class=""modal-footer"">
                        <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Close</button>
                        <button type=""button"" class=""btn btn-primary"">Save changes</button>
                      </div>
                    </div>
                  </div>
                "
                );
        }

    }

}