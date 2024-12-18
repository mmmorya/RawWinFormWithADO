﻿
namespace RawWinFormWithADO.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
