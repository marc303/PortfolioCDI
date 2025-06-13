using BiblioPortCartier.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace BiblioPortCartier.Helpers
{
    public static class UsersHelper
    {
        public static dynamic Cast(object obj, Type type)
        {
            return Convert.ChangeType(obj, type);
        }

        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class 
        {
            tempData[key] = JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object obj;
            tempData.TryGetValue(key, out obj);
            return obj == null ? null : JsonConvert.DeserializeObject<T>((string)obj);
        }

        public static int GenerateUserCode()
        {
            Random code = new Random();
            return code.Next(100000, 1000000);
        }

        public static string CreateUserCode(int code, string roleName)
        {
            switch (roleName)
            {
                case "Admin":
                    return "ADM-" + code.ToString();
                case "Employee":
                    return "EMP-" + code.ToString();
                case "Member":
                default:
                    return "MEM-" + code.ToString();
            }
        }
    }
}
