using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UserService.Controllers
{
  [ApiController]
    [Route("api/")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.Count == 0)
                return Problem();

            if (errors.All(e => e.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }

            // Handle single error (most cases)
            var firstError = errors[0];

            return firstError.Type switch
            {
                ErrorType.NotFound => NotFound(firstError.Description),
                ErrorType.Conflict => Conflict(firstError.Description),
                ErrorType.Validation => ValidationProblem(errors),
                ErrorType.Unauthorized => Unauthorized(firstError.Description),
                ErrorType.Forbidden => Forbid(firstError.Description),
                _ => Problem(statusCode: 500, title: firstError.Description),
            };
        }

        private IActionResult ValidationProblem(List<Error> errors)
        {
            var modelState = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem(modelState);
        }
    }
}