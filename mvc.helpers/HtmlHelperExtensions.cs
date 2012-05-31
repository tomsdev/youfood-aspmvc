﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace mvc.helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString EnumDropDownList<TEnumType>(this HtmlHelper htmlHelper, string name, TEnumType value)
        {
            var selectItems = GetSelectItemsForEnum(typeof(TEnumType), value);
            return htmlHelper.DropDownList(name, selectItems);
        }

        public static MvcHtmlString EnumDropDownListPlaceholder<TEnumType>(this HtmlHelper htmlHelper, string name, TEnumType value, string placeholderName = null)
        {
            var selectItems = GetSelectItemsForEnum(typeof(TEnumType), value);

            AddPlaceHolderToSelectItems(placeholderName, selectItems);
            return htmlHelper.DropDownList(name, selectItems, new { @class = "placeholder" });
        }

        public static MvcHtmlString GenerateHiddenFieldsForIncomingModel<TModel, TProperty>(
        this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression,
        object model)
        {
            var sb = new StringBuilder();

            foreach (PropertyInfo info in model.GetType().GetProperties())
            {
                if (info.CanRead)
                {
                    var o = info.GetValue(model, null);

                    if (o is DateTimeOffset || o is DateTime)
                        sb.Append("<input type='hidden' name='" + info.Name + "' value='" + string.Format("{0:dd MMM yyyy}", o) + "'/>");
                    else if (!(o is IList))
                        sb.Append("<input type='hidden' name='" + info.Name + "' value='" + o + "'/>");
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString EnumRadioButtonFor<TModel, TProperty>(
        this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression,
        bool includeNoneOption = true,
        bool isDisabled = false,
        string cssClass = null
    ) where TModel : class
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            var name = ExpressionHelper.GetExpressionText(expression);
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            ModelState currentValueInModelState;
            var couldGetValueFromModelState = htmlHelper.ViewData.ModelState.TryGetValue(fullName, out currentValueInModelState);
            object selectedValue = null;
            if (!couldGetValueFromModelState && htmlHelper.ViewData.Model != null)
            {
                selectedValue = expression.Compile()(htmlHelper.ViewData.Model);
            }

            var enumNames = GetSelectItemsForEnum(typeof(TProperty), selectedValue).Where(n => !string.IsNullOrEmpty(n.Value)).ToList();

            if (includeNoneOption)
            {
                if (!enumNames.Any(e => e.Value.ToLowerInvariant() == "none"))
                    enumNames.Add(new SelectListItem { Value = "None" });
            }

            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sb = new StringBuilder();
            sb.AppendFormat(
                "<ul class=\"radio-button-list{0}\">",
                string.IsNullOrEmpty(cssClass) ? string.Empty : " " + cssClass);
            foreach (var enumName in enumNames)
            {
                var id = string.Format(
                    "{0}_{1}_{2}",
                    htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix,
                    metaData.PropertyName,
                    enumName.Value
                    );

                if (id.StartsWith("-"))
                    id = id.Remove(0, 1);

                var value = enumName;
                if (includeNoneOption)
                {
                    if (enumNames.All(e => e.Value.ToLowerInvariant() != "none"))
                    enumNames.Add(new SelectListItem {Value = "None"});
                }

                //if (includeNoneOption && enumName == "None")
                //    value = string.Empty;

                sb.AppendFormat(
                    "<li>{2} <label for=\"{0}\">{1}</label></li>",
                    id,
                    HttpUtility.HtmlEncode(enumName),
                    isDisabled
                        ? htmlHelper.RadioButtonFor(expression, value, new { id, disabled = "disabled" }).ToHtmlString()
                        : htmlHelper.RadioButtonFor(expression, value, new { id }).ToHtmlString()
                    );
            }
            sb.Append("</ul>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString GuidanceNoteFor<TModel, TProperty>(
        this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression,
        string guidanceText)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            //var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sb = new StringBuilder();
            var name = ExpressionHelper.GetExpressionText(expression);
            var outterType = expression.Parameters[0].Type;
            var attributes = outterType.GetProperty(name).GetCustomAttributesData();
            var displayText = "";

            foreach (var attr in attributes)
            {
                if (attr.NamedArguments != null)
                    foreach (var namedArg in attr.NamedArguments.Where(namedArg => namedArg.MemberInfo.Name == "Name"))
                    {
                        displayText = (string)namedArg.TypedValue.Value;
                    }
            }

            if (String.IsNullOrEmpty(guidanceText))
                guidanceText = displayText;

            sb.Append("<div class=\"guidance-img\"></div>");
            sb.Append("<div class=\"guidance-box\">");
            sb.Append("    <table class=\"infobox\" width=\"250\" cellspacing=\"0\" cellpadding=\"0\">");
            sb.Append("    <tbody>");
            sb.Append("        <tr>");
            sb.Append("            <td class=\"left\" width=\"14\" rowspan=\"3\">");
            sb.Append("                <div></div>");
            sb.Append("            </td>");
            sb.Append("            <td class=\"top\" colspan=\"2\"><div></div>");
            sb.Append("            </td>");
            sb.Append("        </tr>");
            sb.Append("        <tr>");
            sb.Append("            <td class=\"info\" width=\"356\">");
            sb.Append("                <a href=\"javascript: void(0);\">close<div></div></a>");
            sb.AppendFormat("                <p>{0}</p>", guidanceText);
            sb.Append("            </td>");
            sb.Append("            <td class=\"right\" width=\"3\"><div></div>");
            sb.Append("            </td>");
            sb.Append("        </tr>");
            sb.Append("        <tr>");
            sb.Append("            <td class=\"bottom\" colspan=\"2\"><div></div>");
            sb.Append("            </td>");
            sb.Append("        </tr>");
            sb.Append("    </tbody>");
            sb.Append("</table>");
            sb.Append("</div>");

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes = null) where TModel : class
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            var name = ExpressionHelper.GetExpressionText(expression);
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            ModelState currentValueInModelState;
            var couldGetValueFromModelState = htmlHelper.ViewData.ModelState.TryGetValue(fullName, out currentValueInModelState);
            object selectedValue = null;
            if (!couldGetValueFromModelState &&
                htmlHelper.ViewData.Model != null)
            {
                selectedValue = expression.Compile()(htmlHelper.ViewData.Model);
            }

            var placeholderName = PlaceholderName(memberExpression);

            htmlAttributes = ApplyHtmlAttributes(htmlAttributes, placeholderName);

            var selectItems = GetSelectItemsForEnum(typeof(TProperty), selectedValue).ToList();
            AddPlaceHolderToSelectItems(placeholderName, selectItems);

            return htmlHelper.DropDownListFor(expression, selectItems, htmlAttributes);
        }

        public static MvcHtmlString PlaceholderDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes = null)
            where TModel : class
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            IList<SelectListItem> list = selectList.ToList();
            var placeholderName = PlaceholderName(memberExpression);
            AddPlaceHolderToSelectItems(placeholderName, list);

            htmlAttributes = ApplyHtmlAttributes(htmlAttributes, placeholderName);

            return htmlHelper.DropDownListFor(expression, list, string.IsNullOrEmpty(optionLabel) ? null : optionLabel, htmlAttributes);
        }

        public static IList<SelectListItem> GetSelectItemsForEnum(Type enumType, object selectedValue)
        {
            var selectItems = new List<SelectListItem>();

            if (enumType.IsGenericType &&
                enumType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                enumType = enumType.GetGenericArguments()[0];
                selectItems.Add(new SelectListItem { Value = string.Empty, Text = string.Empty });
            }

            var selectedValueName = selectedValue != null ? selectedValue.ToString() : null;
            var enumEntryNames = Enum.GetNames(enumType);
            var entries = enumEntryNames
                .Select(n => new
                {
                    Name = n,
                    DisplayAttribute = enumType
                        .GetField(n)
                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                        .OfType<DisplayAttribute>()
                        .SingleOrDefault() ?? new DisplayAttribute()
                })
                .Select(e => new
                {
                    Value = e.Name,
                    DisplayName = e.DisplayAttribute.Name ?? e.Name,
                    Order = e.DisplayAttribute.GetOrder() ?? 50
                })
                .OrderBy(e => e.Order)
                .ThenBy(e => e.DisplayName)
                .Select(e => new SelectListItem
                {
                    Value = e.Value,
                    Text = e.DisplayName,
                    Selected = e.Value == selectedValueName
                });

            selectItems.AddRange(entries);

            return selectItems;
        }

        public static IEnumerable<string> GetNamesForEnum(Type enumType, object selectedValue)
        {
            if (enumType.IsGenericType &&
               enumType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                enumType = enumType.GetGenericArguments()[0];
            }

            var enumEntryNames = Enum.GetNames(enumType);
            var entries = enumEntryNames
                .Select(n => new
                {
                    Name = n,
                    DisplayAttribute = enumType
                        .GetField(n)
                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                        .OfType<DisplayAttribute>()
                        .SingleOrDefault() ?? new DisplayAttribute()
                })
                .Select(e => new
                {
                    Value = e.Name,
                    DisplayName = e.DisplayAttribute.Name ?? e.Name,
                    Order = e.DisplayAttribute.GetOrder() ?? 50
                })
                .OrderBy(e => e.Order)
                .ThenBy(e => e.DisplayName)
                .Select(e => e.Value);
            return entries;
        }

        static string PlaceholderName(MemberExpression memberExpression)
        {
            var placeholderName = memberExpression.Member
                .GetCustomAttributes(typeof(DisplayAttribute), true)
                .Cast<DisplayAttribute>()
                .Select(a => a.Prompt)
                .FirstOrDefault();
            return placeholderName;
        }

        static void AddPlaceHolderToSelectItems(string placeholderName, IList<SelectListItem> selectList)
        {
            if (!selectList.Where(i => i.Text == string.Empty).Any())
                selectList.Insert(0, new SelectListItem { Selected = false, Text = placeholderName, Value = string.Empty });

            if (!selectList.Any() || selectList[0].Text != string.Empty) return;

            selectList[0].Value = "";
            selectList[0].Text = placeholderName;
        }

        static IDictionary<string, object> ApplyHtmlAttributes(IDictionary<string, object> htmlAttributes, string placeholderName)
        {
            if (!string.IsNullOrEmpty(placeholderName))
            {
                if (htmlAttributes == null)
                {
                    htmlAttributes = new Dictionary<string, object>();
                }

                if (!htmlAttributes.ContainsKey("class"))
                    htmlAttributes.Add("class", "placeholder");
                else
                {
                    htmlAttributes["class"] += " placeholder";
                }
            }
            return htmlAttributes;
        }
    }
}
