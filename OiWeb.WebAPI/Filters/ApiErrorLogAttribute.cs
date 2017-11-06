﻿using System.Linq;
using System.Web.Http.Filters;

namespace OiWeb.WebAPI.Filters
{
    public class ApiErrorLogAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                //var entity = this.GetContextValues(filterContext);
                ///Business.SystemLog.Save(entity);
            }
        }

        //private Entity.SystemLog GetContextValues(HttpActionExecutedContext context)
        //{
        //    var errorMessage = context.Exception.Message;

        //    var originDescription = context.Exception.Source;

        //    var controllerName = context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.Name;

        //    var actionDescription = context.ActionContext.ActionDescriptor.ActionName;

        //    var stackTrace = string.Format("{0} // InnerException.Message = {1}", context.Exception.StackTrace, context.Exception.InnerException != null ? context.Exception.InnerException.Message : "Nenhuma InnerException");

        //    var targetSite = context.Exception.TargetSite.Name;

        //    var targetClass = context.Exception.TargetSite.DeclaringType == null ? string.Empty : context.Exception.TargetSite.DeclaringType.Name;

        //    var listParams = context.ActionContext.ActionArguments;

        //    var strParams = string.Empty;
        //    foreach (var value in listParams)
        //    {
        //        string valueForParams = string.Empty;
        //        if (value.Value != null && value.Value.GetType().BaseType == typeof(object))
        //            if (!(value.Value is string))
        //            {
        //                try
        //                {
        //                    valueForParams = value.Value.GetType()
        //                        .GetProperties()
        //                        .Aggregate(valueForParams,
        //                            (current, prop) =>
        //                                current + string.Format("{0}={1}; ", prop.Name, prop.GetValue(value.Value, null)));
        //                }
        //                finally
        //                {

        //                }
        //            }


        //        strParams = strParams + string.Format("param: {0} value: {1}; ", value.Key, string.IsNullOrEmpty(valueForParams) ? value.Value : valueForParams);
        //    }

        //    return new Entity.SystemLog(stackTrace, originDescription, actionDescription, controllerName, errorMessage,
        //        targetSite, targetClass, strParams);

        //}
    }
}