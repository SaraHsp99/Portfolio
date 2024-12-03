using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web.ViewComponents;

public class HeaderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("Header");
    }
}

public class FooterViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("Footer");
    }
}