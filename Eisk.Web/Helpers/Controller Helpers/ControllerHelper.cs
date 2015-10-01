using System;
using System.IO;
using System.Web.Mvc;

namespace Eisk.Helpers
{
    public class ControllerHelper
    {
        private readonly ControllerContext _controllerContext;
        private HtmlHelper _html;
        private UrlHelper _url;
        private ViewContext _viewContext;

        public ControllerHelper(Controller controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            _controllerContext = controller.ControllerContext;
        }

        public ViewContext ViewContext => _viewContext ?? (_viewContext = new ViewContext(
            _controllerContext,
            new InternalView(),
            _controllerContext.Controller.ViewData,
            _controllerContext.Controller.TempData,
            _controllerContext.HttpContext.Response.Output));

        public UrlHelper Url => _url ?? (_url = new UrlHelper(_controllerContext.RequestContext));

        public HtmlHelper Html => _html ??
                                  (_html =
                                      new HtmlHelper(ViewContext, new InternalViewDataContainer(ViewContext.ViewData)));

        private class InternalView : IView
        {
            public void Render(ViewContext viewContext, TextWriter writer)
            {
            }
        }

        private class InternalViewDataContainer : IViewDataContainer
        {
            public InternalViewDataContainer(ViewDataDictionary viewData)
            {
                ViewData = viewData;
            }

            public ViewDataDictionary ViewData { get; set; }
        }
    }
}