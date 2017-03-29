using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidLocale
    {
        const string DEFAULT_COUNTRY = "US";
        const string DEFAULT_LANGUAGE = "en";

        public static string GetCountryCode()
        {
            using (AndroidJavaObject locale = GetLocale())
            {
                return locale == null ? DEFAULT_COUNTRY : locale.Call<string>("getCountry");
            }
        }

        public static string GetLanguageCode()
        {
            using (AndroidJavaObject locale = GetLocale())
            {
                return locale == null ? DEFAULT_LANGUAGE : locale.Call<string>("getLanguage");
            }
        }

        public static AndroidJavaObject GetLocale()
        {
            using (AndroidJavaObject resources = AndroidContext.GetResource())
            {
                using (AndroidJavaObject configuration = resources.Call<AndroidJavaObject>("getConfiguration"))
                {
                    return configuration.Get<AndroidJavaObject>("locale");
                }
            }
        }
    }
}

