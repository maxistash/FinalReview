using FinalReview.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalReview.Infrastructure
    // Creates the urls as we select a tags
{
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTag : TagHelper
    {
        private IUrlHelperFactory urlInfo;
        public PaginationTag(IUrlHelperFactory tagfactory)
        {
            urlInfo = tagfactory;
        }

        public PageNumberingInfo PageInfo {get; set;}

        //our own dictionary of key values that we create
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();
                        
        [HtmlAttributeNotBoundAttribute]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public bool PageClassesEnabled { get; set; } = false;
        public string PageAction { get; set; }
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder finishedTag = new TagBuilder("div");
            IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);
            // dynamically creates the a tags for the page number
            for (int i=1; i <= PageInfo.Pages; i++)
            {

                TagBuilder individualTag = new TagBuilder("a");
                KeyValuePairs["pageNum"] = i;
                individualTag.Attributes["href"] = urlHelp.Action(PageAction, KeyValuePairs);                
                
                
                //if (PageClassesEnabled)
                //{
                //    individualTag.AddCssClass(PageClass);
                //    individualTag.AddCssClass(i == PageInfo.currentPage ? PageClassSelected : PageClassNormal);
                //}
                individualTag.InnerHtml.Append(i.ToString());
                finishedTag.InnerHtml.AppendHtml(individualTag);
            }

            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
