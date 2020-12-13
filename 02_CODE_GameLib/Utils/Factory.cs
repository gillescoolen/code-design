using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CODE_GameLib.Utilities
{
    public class Factory<T> where T : class
    {
        private readonly string nameSpace;

        public Factory(string nameSpace = "Models")
        {
            this.nameSpace = nameSpace;
        }

        public T Create(string name)
        {
            var currentType = typeof(Factory<T>);
            var currentTypeNamespace = currentType.Namespace;
            var type = Type.GetType($"{currentTypeNamespace?.Split(".")[0]}.{nameSpace}.{ConvertString(name)}");
            return type == null ? null : (T)Activator.CreateInstance(type)!;
        }

        private static string ConvertString(string name)
        {
            var textInfo = new CultureInfo("en-US", false);
            return Regex.Replace(textInfo.TextInfo.ToTitleCase(name), @"\s+", "");
        }
    }
}