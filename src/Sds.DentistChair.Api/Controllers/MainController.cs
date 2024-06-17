using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sds.DentistChair.Api.Dtos;
using Sds.DentistChair.Domain.Notifier;
using System.Net;

namespace Sds.DentistChair.Api.Controllers;

[ApiController]
public abstract class MainController(INotifierMessage notifierMessage)
    : ControllerBase
{
    protected ActionResult CustomResponse(HttpStatusCode httpStatusCode, object data = null)
    {
        if (!notifierMessage.IsValid())
        {
            var messages = notifierMessage.GetMessages().ToArray();
            var resultFail = new CustomResponse
            {
                Success = false,
                Status = (int)httpStatusCode,
                Data = null,
                Messages = messages
            };

            return httpStatusCode switch
            {
                HttpStatusCode.BadRequest => BadRequest(resultFail),
                HttpStatusCode.UnprocessableEntity => UnprocessableEntity(resultFail),
                HttpStatusCode.NotFound => NotFound(resultFail),
                _ => BadRequest(resultFail),
            };
        }

        var resultSuccess = new CustomResponse
        {
            Success = true,
            Status = (int)httpStatusCode,
            Data = data,
            Messages = null
        };

        return httpStatusCode switch
        {
            HttpStatusCode.Created => Created(string.Empty, resultSuccess),
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.OK => Ok(resultSuccess),
            _ => Ok(),
        };
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        notifierMessage.AddRange(erros.Select(e => e.ErrorMessage).ToList());
        return CustomResponse(HttpStatusCode.BadRequest);
    }
}
