using DataAccessLayer.Model.Enums;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
   
    public class ExceptionHandlerFilter : ExceptionFilterAttribute
    {
        private readonly ILoggerRepository _logger;
        public ExceptionHandlerFilter(ILoggerRepository logger) {
            _logger = logger;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            
            // base.OnException(actionExecutedContext);
            var aData = new Dictionary<string, object>.ValueCollection(actionExecutedContext.ActionContext.ActionArguments);
            string responseContent = null;
            

            if (actionExecutedContext.Exception is BusinessException)
            {
                var businessException = actionExecutedContext.Exception as BusinessException;
                var errorMessage = new System.Web.Http.HttpError(businessException.Message);
                responseContent= errorMessage.Message;
                if (businessException.ErrorCode == 501)
                {
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.NotImplemented, errorMessage);
                }
            } else
            {
                var errorMessage = new HttpError("Exception occured, please contact us");
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            // Save it to LogRepo
            var logEntity = new LogEntity
            {
                Guid = new Guid(),
                RecordType = LogRecordType.Exception,
                RequestUri = actionExecutedContext.Request.RequestUri.ToString(),
                RequestMethod = actionExecutedContext.Request.Method.ToString(),
                RequestBody = JsonConvert.SerializeObject(aData),
                StatusCode = (int)actionExecutedContext.Response.StatusCode,
                ResponseContent = responseContent,
                Source = actionExecutedContext.Exception.Source,
                Message = actionExecutedContext.Exception.Message,
                StackTrace = actionExecutedContext.Exception.StackTrace,
                CreatedAt = DateTime.Now
            };
            
            _logger.SaveLogAsync(logEntity);

        }
        
    }
}
