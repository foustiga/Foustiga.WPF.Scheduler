using System;
using System.Linq;

namespace Foustiga.WPF.Scheduler.Foundation.Enums
{
    public static class EnumExtensions
    {
        public static string ToDisplay(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetMember(enumValue.ToString())[0]
                .GetCustomAttributes(false)
                .Select(a => a as EnumInfoAttribute)
                .FirstOrDefault();
            return displayAttribute?.Display ?? enumValue.ToString();
        }
        public static string ToTooltip(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetMember(enumValue.ToString())[0]
                .GetCustomAttributes(false)
                .Select(a => a as EnumInfoAttribute)
                .FirstOrDefault();
            return displayAttribute?.Tooltip ?? enumValue.ToString();
        }
    }
}
