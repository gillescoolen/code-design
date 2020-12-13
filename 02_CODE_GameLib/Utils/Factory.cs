using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CODE_GameLib.Utilities
{
    public class Factory<T> where T : class
    {
        private readonly string Namespace;

        public Factory(string nameSpace = "Models")
        {
            this.Namespace = nameSpace;
        }

        public T Create(string name)
        {
            var currentType = typeof(Factory<T>);
            var currentTypeNamespace = currentType.Namespace;
            var type = Type.GetType($"{currentTypeNamespace?.Split(".")[0]}.{Namespace}.{ConvertString(name)}");
            return type == null ? null : (T)Activator.CreateInstance(type)!;
        }

        private static string ConvertString(string name)
        {
            var info = new CultureInfo("en-US", false);
            return Regex.Replace(info.TextInfo.ToTitleCase(name), @"\s+", "");
        }
    }
}