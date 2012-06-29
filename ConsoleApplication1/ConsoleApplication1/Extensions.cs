using System;

namespace ConsoleApplication1
{
    public static class Extensions
    {
        public static string GetHubName(this Type type)
        {
            string typeName = type.Name;
            return char.ToLower(typeName[0]) + typeName.Substring(1);
        }
    }
}