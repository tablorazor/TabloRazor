using System.Reflection;

namespace Tabler.Docs.Components.TypeBrowsers
{
    internal class PropertyView
    {
        public PropertyView(PropertyInfo propertyInfo)
        {
            PropertyInfo = PropertyInfo;
            Name = propertyInfo.Name;
            TypeName = propertyInfo.PropertyType.GetFriendlyName();
            IsComponentParameter = propertyInfo.IsComponentParameter();

        }

        public PropertyInfo PropertyInfo { get; private set; }
        public string Name { get; private set; }
        public string TypeName { get; private set; }
        public bool IsComponentParameter { get; private set; }
    }
}
