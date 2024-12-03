using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Portfolio.Web.Models;

namespace Portfolio.Web.Controllers;
public abstract class BaseController : Controller
{
    public void RemoveValidate(string key, bool contain = false)
    {
        if (contain == false)
        {
            foreach (var modelStateKey in ViewData.ModelState.Keys)
            {
                if (key.ToLower() != modelStateKey.ToLower())
                {
                    ModelState.Remove(modelStateKey);
                }
            }
        }
        else
        {
            foreach (var modelStateKey in ViewData.ModelState.Keys)
            {
                if (modelStateKey.StartsWith(key))
                {
                    ModelState.Remove(modelStateKey);
                }
            }
        }

    }

    protected virtual IActionResult MessageViewHelper(/*IResult result*/)
    {
        MessageModel model = new MessageModel();
        //model.Result = (Result)result;
        return PartialView("~/Views/Shared/_SiteMessageJs.cshtml", model);
    }
}

public static class ControllerPDF
{
    public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewName, TModel model, bool partial = false)
    {
        if (string.IsNullOrEmpty(viewName))
        {
            viewName = controller.ControllerContext.ActionDescriptor.ActionName;
        }
        controller.ViewData.Model = model;
        using (var writer = new StringWriter())
        {
            IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);
            if (viewResult.Success == false)
            {
                return $"A view with the name {viewName} could not be found";
            }
            ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, writer, new HtmlHelperOptions());
            await viewResult.View.RenderAsync(viewContext);
            return writer.GetStringBuilder().ToString();
        }
    }
}

