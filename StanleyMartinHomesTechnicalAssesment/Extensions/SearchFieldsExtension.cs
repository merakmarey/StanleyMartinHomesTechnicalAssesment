using System.Reflection;

namespace StanleyMartinHomesTechnicalAssesment.Extensions
{
    public static class SearchFieldsExtension
    {
        public static bool ContainsValue(this object _object, string value)
        {
            // returns if either object or search value are null
            if (_object is null || String.IsNullOrEmpty(value)) return false;

            // Get all properties of the anonymous object
            var properties = _object.GetType().GetProperties();

            // Check if any property contains the specified string value

            return properties.Any(prop =>
            {
                var propValue = prop.GetValue(_object) as string;
                return propValue != null && propValue.Contains(value, StringComparison.OrdinalIgnoreCase);
            });
        }
    }
}
