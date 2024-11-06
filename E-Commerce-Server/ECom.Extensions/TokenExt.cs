using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ECom.Extensions
{
    public static class TokenExt
    {
        public static string EncodeToken(this string token) => WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        public static string DecodeToken(this string token) => Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
    }
}
