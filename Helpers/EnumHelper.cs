using System;
using System.ComponentModel.DataAnnotations;

namespace Daytona.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDisplayName(this Enum type)
        {
            var fi = type.GetType().GetField(type.ToString());
            if (fi == null)
                return string.Empty;

            var attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes[0].Name;
        }
    }
}