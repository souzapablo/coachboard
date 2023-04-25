using System.ComponentModel;
using System.Reflection;

namespace CoachBoard.Core.Extensions;

public class EnumExtensions
{
    public static string GetDescription(Enum value)
    {
        var fi = value.GetType()
            .GetField(value.ToString());

        if (fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes &&
            attributes.Any())
        {
            return attributes.First().Description;
        }

        return value.ToString();
    }
}