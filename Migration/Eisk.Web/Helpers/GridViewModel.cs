using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Eisk.Helpers;

namespace Eisk.Models
{
    public class GridViewModel
    {
        ControllerHelper ControllerHelper { get; }
        string SingleItemDeleteAction { get; }
        public string GridBindingAction { get; set; }
        public int TotalRecords { get; }

        public GridViewModel(Controller controller, int totalRecords, string gridBindingAction = "GridData", string singleItemDeleteAction = "Delete")
        {
            ControllerHelper = new ControllerHelper(controller);
            TotalRecords = totalRecords;
            GridBindingAction = gridBindingAction;
            SingleItemDeleteAction = singleItemDeleteAction;
        }

        public IHtmlString GridDatabind => ControllerHelper.Html.Raw(ControllerHelper.Url.Action(GridBindingAction));

        public IHtmlString SingleItemDelete => ControllerHelper.Html.Raw(ControllerHelper.Url.Action(SingleItemDeleteAction, new { id = "__ID__" }));


        public int TotalPages => (TotalRecords - 1) / ItemsPerPage + 1;

        public int CurrentPage => Start / ItemsPerPage + 1;

        List<int> _itemsPerPageOptions;
        public List<int> ItemsPerPageOptions => _itemsPerPageOptions ?? (_itemsPerPageOptions = new List<int> {10, 20, 50, 100});

        public RouteValueDictionary CurrentRouteValues
        {
            get
            {
                RouteValueDictionary routeDictionary = new RouteValueDictionary
                {
                    [nameof(Start)] = Start,
                    [nameof(ItemsPerPage)] = ItemsPerPage,
                    [nameof(OrderBy)] = OrderBy,
                    [nameof(Desc)] = Desc
                };


                return routeDictionary;
            }
        }

        public int Start => GetIntRequestObject(nameof(Start));

        public string OrderBy
        {
            get
            {
                object requestValue = GetRequestObject(nameof(OrderBy));
                if (requestValue != null)
                    return requestValue.ToString();

                return "Id";
            }
        }

        public bool Desc
        {
            get
            {
                bool value = false;
                object requestValue = GetRequestObject(nameof(Desc));
                if (requestValue != null)
                    bool.TryParse(requestValue.ToString(), out value);
                return value;
            }
        }

        public int ItemsPerPage => GetIntRequestObject(nameof(ItemsPerPage), 10);

        protected object GetRequestObject(string key)
        {
            return ControllerHelper.ViewContext.HttpContext.Request[key];
        }

        protected int GetIntRequestObject(string key, int defaultValue = 0)
        {
            int value = defaultValue;
            object requestValue = GetRequestObject(key);
            if (requestValue != null)
                int.TryParse(requestValue.ToString(), out value);
            return value;
        }
    }
}