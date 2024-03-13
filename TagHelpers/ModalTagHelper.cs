using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using HtmlAgilityPack;


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

            var modalContent = await output.GetChildContentAsync();


            var doc = new HtmlDocument();
            doc.LoadHtml(  modalContent.GetContent());


            var modalBodyContent = doc.DocumentNode.SelectSingleNode("//div[@class='modal-body-content']");
            string bodyContent = modalBodyContent.InnerHtml;

            var modalfooterContent = doc.DocumentNode.SelectSingleNode("//div[@class='modal-footer-content']");
            string footerContent = (modalfooterContent != null) ? $@"<div class=""modal-footer"">{modalfooterContent.InnerHtml}</div>":"" ;

            var IdTitleModal = TitleModal.Replace(" ", "-").ToLower();

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "modal fade");
            output.Attributes.SetAttribute("id", IdModal);
            output.Attributes.SetAttribute("tabindex", -1);

            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("aria-labelledby", $"{IdModal}label");
            output.Attributes.SetAttribute("aria-hidden", true);

            output.Content.SetHtmlContent(
            $@"<div class=""modal-dialog"">
                    <div class=""modal-content"">
                        <div class=""modal-header"">
                            <h1 class=""modal-title fs-5"" id=""{IdTitleModal}id"">{TitleModal}</h1>
                            <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                        </div>
                        <div class=""modal-body"">
                       {bodyContent}
                        </div>
                       {footerContent}
                    </div>
                </div>"
            );
        }

    }

}