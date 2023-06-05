using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SimpleViewProjector.Elastic.Extensions
{
    internal static class EnumExtension
    {
        public static string GetDescription<TEnum>(this int value) => 
            typeof(TEnum)
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description
                ?? value.ToString();
    }
}
