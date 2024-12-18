

namespace RawWinFormWithADO.Infrastructure.Extensions
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   email.Contains("@") &&
                   email.Contains(".");
        }
    }
}
