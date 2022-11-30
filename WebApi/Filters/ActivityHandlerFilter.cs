using DataAccessLayer.Model.Enums;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class ActivityLoggingFilter : ActionFilterAttribute
    {
        private readonly ILoggerRepository _logger;

        public ActivityLoggingFilter(ILoggerRepository logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // base.OnActionExecuted(actionExecutedContext);
            // request successfully 
            if (actionExecutedContext.Exception == null)
            {
                var postedData = new Dictionary<string, object>.ValueCollection(actionExecutedContext.ActionContext.ActionArguments);

                // response
                var objectContent = actionExecutedContext.Response.Content as ObjectContent;
                
                string responseContent = null;
                if (objectContent != null)
                {
                    responseContent = JsonConvert.SerializeObject(objectContent.Value);
                }

                var logEntity = new LogEntity
                {
                    Guid = Guid.NewGuid(),
                    RecordType = LogRecordType.Activity,
                    RequestUri = actionExecutedContext.Request.RequestUri.ToString(),
                    RequestMethod = actionExecutedContext.Request.Method.ToString(),
                    RequestBody = JsonConvert.SerializeObject(postedData),
                    StatusCode = (int)actionExecutedContext.Response.StatusCode,
                    ResponseContent = responseContent,
                    Source = "",
                    Message = "",
                    StackTrace = "",
                    CreatedAt = DateTime.Now
                };

                _logger.SaveLogAsync(logEntity);
            
            };
        
            
        }
    }
}
