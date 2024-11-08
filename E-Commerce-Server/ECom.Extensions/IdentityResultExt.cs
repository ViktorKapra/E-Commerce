using Microsoft.AspNetCore.Identity;
using System.Text;

namespace ECom.Extensions
{
    public static class IdentityResultExt
    {
        public static string GetErrorsDescriptions(this IdentityResult result, string defaultError)
        {
            var errors = new StringBuilder();
            foreach (var error in result.Errors)
            {
                errors.AppendLine(error.Description);
            }
            if (errors.Length == 0)
            {
                errors.Append(defaultError);
            }
            return errors.ToString();
        }
    }
}
