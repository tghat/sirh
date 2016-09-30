using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc.Html;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{

    public static class SelectExtensions
    {
        public static MvcHtmlString CustomEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            
            var items =
                values.Select(
                   value =>
                   new SelectListItem
                   {
                       Text = GetEnumDescription(value),
                       Value = value.ToString(),
                       Selected = value.Equals(metadata.Model)
                   });
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            return htmlHelper.DropDownListFor(expression, items, attributes);
        }

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

  
        public static IHtmlString DisplayEnumFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, string>> ex, Type enumType)
        {
            string value = (String)ModelMetadata.FromLambdaExpression(ex, html.ViewData).Model;
            var name = Enum.GetNames(enumType).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return null;
            }
            var field = enumType.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            string enumValue = customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
            return new HtmlString(html.Encode(enumValue));
        }
 
    }
}