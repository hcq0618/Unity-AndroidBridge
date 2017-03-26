using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidLocale
    {
        const string DEFAULT_COUNTRY = "US";
        const string DEFAULT_LANGUAGE = "en";

        public static string GetCountryCode(AndroidJavaObject context)
        {
            using (AndroidJavaObject locale = GetLocale(context))
            {
                return locale == null ? DEFAULT_COUNTRY : locale.Call<string>("getCountry");
            }
        }

        public static string GetLanguageCode(AndroidJavaObject context)
        {
            using (AndroidJavaObject locale = GetLocale(context))
            {
                return locale == null ? DEFAULT_LANGUAGE : locale.Call<string>("getLanguage");
            }
        }

        static AndroidJavaObject GetLocale(AndroidJavaObject context)
        {
            if (context == null)
            {
                return null;
            }

            using (AndroidJavaObject resources = context.Call<AndroidJavaObject>("getResources"))
            {
                if (resources == null)
                {
                    return null;
                }

                using (AndroidJavaObject configuration = resources.Call<AndroidJavaObject>("getConfiguration"))
                {
                    if (configuration == null)
                    {
                        return null;
                    }

                    return configuration.Get<AndroidJavaObject>("locale");
                }
            }

        }
    }
}

