using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Eisk.Helpers
{
    public static class ControllerExtensions
    {
        //validation related extension

        public static IList<ValidationResult> Validate(this Controller controller, object @object)
        {
            var validationResults = Validate(@object);

            foreach (var validationResult in validationResults)
            {
                if (validationResult.MemberNames.Any())
                {
                    foreach (var memberName in validationResult.MemberNames)
                        controller.ModelState.AddModelError(memberName, validationResult.ErrorMessage);
                }
                else
                    controller.ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
            }

            return validationResults;
        }

        public static IList<ValidationResult> Validate(object @object)
        {
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(@object, null, null);
            Validator.TryValidateObject(@object, validationContext, results, true);
            (@object as IValidatableObject)?.Validate(validationContext);
            return results;
        }

        //messsage extensions

        public static void ShowMessage(this Controller controller, string message,
            MessageType messageType = MessageType.Info, bool showAfterRedirect = true)
        {
            var messageTypeKey = messageType.ToString();
            if (showAfterRedirect)
            {
                controller.TempData[messageTypeKey] = message;
            }
            else
            {
                controller.ViewData[messageTypeKey] = message;
            }
        }

        public static void ShowModelStateErrors(this Controller controller, bool showAfterRedirect = false)
        {
            foreach (var error in controller.GetModelErrors())
            {
                controller.ShowMessage(error.ErrorMessage, MessageType.Danger, showAfterRedirect);
            }
        }

        //error related extensions

        public static ModelErrorCollection GetModelErrors(this Controller controller)
        {
            var errors = new ModelErrorCollection();

            foreach (var modelState in controller.ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.Add(error);
                }
            }

            return errors;
        }

        public static bool IsErrorAvalilableIn(this Controller controller, string errorMessage)
        {
            return IsErrorAvalilableIn(controller.GetModelErrors(), errorMessage);
        }

        private static bool IsErrorAvalilableIn(ModelErrorCollection errors, string errorMessage)
        {
            return errors.Any(error => error.ErrorMessage == errorMessage);
        }
    }
}