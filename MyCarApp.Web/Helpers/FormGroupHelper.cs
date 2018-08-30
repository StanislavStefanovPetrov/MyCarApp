using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text.Encodings.Web;

namespace MyCarApp.Web.Helpers
{
    public static class FormGroupHelper
    {
        public static IHtmlContent FormEditGroupFor<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            using (var writer = new StringWriter())
            {
                var label = htmlHelper.LabelFor(expression, new { @class = "control-label col-sm-2" });
                var editor = htmlHelper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control" } });
                var validationMessage = htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" });

                writer.Write("<div class=\"form-group\">");
                label.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("<div class=\"col-sm-10\">");
                editor.WriteTo(writer, HtmlEncoder.Default);
                validationMessage.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</div></div>");

                return new HtmlString(writer.ToString());
            }
        }

        public static IHtmlContent FormEditGroupWithCustomAttibutesFor<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IDictionary<string, object> htmlAttributes = null)
        {
            if (htmlAttributes == null)
            {
                htmlAttributes =
                    new Dictionary<string, object>();
            }

            using (var writer = new StringWriter())
            {
                var lableAtt = htmlAttributes.ContainsKey("LabelFor") ? htmlAttributes["LabelFor"] : new { };
                var label = htmlHelper.LabelFor(expression, lableAtt);
               
                var editorAtt = htmlAttributes.ContainsKey("EditorFor") ? htmlAttributes["EditorFor"] : new { };
                var editor = htmlHelper.EditorFor(expression, new { htmlAttributes = editorAtt });

                var validationAtt = htmlAttributes.ContainsKey("ValidationMessageFor") ? htmlAttributes["ValidationMessageFor"] : new { };

                var validationMessage = htmlHelper.ValidationMessageFor(expression, null, validationAtt);

                writer.Write("<div class=\"form-group\">");
                label.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("<div class=\"col-sm-10\">");
                editor.WriteTo(writer, HtmlEncoder.Default);
                validationMessage.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</div></div>");

                return new HtmlString(writer.ToString());
            }
        }

        public static IHtmlContent FormDropDownListGroupWithCustomAttibutesFor<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression,IEnumerable<SelectListItem> selectLists, string optioanlLable, bool disableLabel, IDictionary<string, object> htmlAttributes = null)
        {
            if (htmlAttributes == null)
            {
                htmlAttributes =
                    new Dictionary<string, object>();
            }

            using (var writer = new StringWriter())
            {
                var lableAtt = htmlAttributes.ContainsKey("LabelFor") ? htmlAttributes["LabelFor"] : new { };
                var label = htmlHelper.LabelFor(expression, lableAtt);

                var dropDownListAtt = htmlAttributes.ContainsKey("DropDownList") ? htmlAttributes["DropDownList"] : new { };
                var dropDownList = htmlHelper.DropDownListFor(expression, selectLists,  optioanlLable,dropDownListAtt);

               
                var validationAtt = htmlAttributes.ContainsKey("ValidationMessageFor") ? htmlAttributes["ValidationMessageFor"] : new { };
                var validationMessage = htmlHelper.ValidationMessageFor(expression, null, validationAtt);

                writer.Write("<div class=\"form-group\">");
                label.WriteTo(writer, HtmlEncoder.Default);
                dropDownList.WriteTo(writer, HtmlEncoder.Default);
                validationMessage.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</div>");

                var result = writer.ToString();

                if (disableLabel && !string.IsNullOrWhiteSpace(optioanlLable))
                {
                    result = DisableOptionLable(result, optioanlLable, disableLabel);
                }

                return new HtmlString(result);
            }
        }

        private static string DisableOptionLable(string disabledOption, string optionLabel, bool disableLabel)
        {
            int index = disabledOption.IndexOf(optionLabel);
            disabledOption = disabledOption.Insert(index - 1, " disabled");

            return disabledOption;
        }
    }
}
