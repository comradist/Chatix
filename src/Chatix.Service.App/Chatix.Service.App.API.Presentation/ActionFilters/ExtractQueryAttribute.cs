// using Microsoft.Net.Http.Headers;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
// using Microsoft.AspNetCore.Mvc.Formatters;
// using System.Web;
// using System.Text;

// namespace Chatix.Service.App.API.Presentation.ActionFilters;

// public class ExtractQueryAttribute : IActionFilter
// {
//     public void OnActionExecuting(ActionExecutingContext context)
//     {
//         var dataFromQuery = context.HttpContext.Request.QueryString.ToString();
//         var queryParts = HttpUtility.ParseQueryString(dataFromQuery);
//         var filterStringBuilder = new StringBuilder();

//         foreach (var key in queryParts.AllKeys)
//         {
//             if (key != "pageNumber" && key != "pageSize" && key != "orderBy" &&
//                 key != "PageNumber" && key != "PageSize" && key != "OrderBy")
//             {
//                 filterStringBuilder.Append($"{key}={queryParts[key]}&");
//             }
//         }

//         var filterString = filterStringBuilder.ToString().TrimEnd('&');

//         context.HttpContext.Items.Add("filterAndSearchTerm", filterString);

//     }

//     public void OnActionExecuted(ActionExecutedContext context)
//     {

//     }
// }