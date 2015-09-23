using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Eisk;
using System.IO;
using Eisk.Models;
public static class HtmlHelperExtensions
{
    #region Message Helpers 

    public static IHtmlString RenderMessages(this HtmlHelper htmlHelper)
    {
        var messages = String.Empty;
        foreach (var messageType in Enum.GetNames(typeof(MessageType)))
        {
            var message = htmlHelper.ViewContext.ViewData.ContainsKey(messageType)
                            ? htmlHelper.ViewContext.ViewData[messageType]
                            : htmlHelper.ViewContext.TempData.ContainsKey(messageType)
                                ? htmlHelper.ViewContext.TempData[messageType]
                                : null;
            if (message != null)
            {
                MessageType messageTypeEnum;
                Enum.TryParse<MessageType>(messageType, out messageTypeEnum);
                MessageViewModel messageViewModel = new MessageViewModel { MessageType = messageTypeEnum, Message = message.ToString() };
                messages += RenderRazorViewToString(htmlHelper.ViewContext, "_Message", messageViewModel);
            }
        }

        return MvcHtmlString.Create(messages);
    }

    public static string RenderRazorViewToString(ControllerContext controllerContext, string viewName, Object model)
    {
        controllerContext.Controller.ViewData.Model = model;

        using (var sw = new StringWriter())
        {
            var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
            var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
            ViewResult.View.Render(ViewContext, sw);
            ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
            return sw.GetStringBuilder().ToString();
        }
    }

    #endregion

    #region Wrapper Panels  

    #region Primitive Controls

    public static IHtmlString EditorCalenderFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
    {
        Func<TModel, TProperty> deleg = expression.Compile();
        var result = deleg(htmlHelper.ViewData.Model);

        var value = result.ToString() == DateTime.MinValue.ToString() ? string.Empty : $"{result:M/dd/yyyy}";

        return htmlHelper.TextBoxFor(expression, new { @class = "datepicker text", Value = value });
    }

    public static IHtmlString RequiredSymbolFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string symbol = "* ")
    {
        ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

        if (modelMetadata.IsRequired)
        {
            return MvcHtmlString.Create(symbol);
        }

        return MvcHtmlString.Create(string.Empty);
    }

    #endregion

    #region Panel Controls 

    public static IHtmlString EditorCalenderPanelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
    {
        return BuildEditorPanelFor(htmlHelper, expression, htmlHelper.EditorCalenderFor(expression));
    }

    public static IHtmlString EditorPanelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
    {
        return BuildEditorPanelFor(htmlHelper, expression, htmlHelper.EditorFor(expression));
    }

    public static IHtmlString EditorDropdownPanelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel)
    {
        return BuildEditorPanelFor(htmlHelper, expression, htmlHelper.DropDownListFor(expression, selectList, optionLabel));
    }

    #endregion

    #region Panel Builders

    static IHtmlString BuildEditorPanelFor<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IHtmlString editorForString)
    {
        IHtmlString labelForString = htmlHelper.LabelFor(expression);
        IHtmlString requiredSymbolFor = htmlHelper.RequiredSymbolFor(expression);
        IHtmlString validationMessageForString = htmlHelper.ValidationMessageFor(expression);

        IHtmlString outout = MvcHtmlString.Create(
            labelForString.ToHtmlString() + ":" + requiredSymbolFor.ToHtmlString() + "<br class=\"control-label\" />" +
            editorForString.ToHtmlString() + "<br class=\"form-control\" />" +
            validationMessageForString.ToHtmlString() + "<br class=\"control-label\" />");

        return outout;
    }

    #endregion

    #endregion
}
