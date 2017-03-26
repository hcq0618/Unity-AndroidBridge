// Created by hcq
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidSettings
    {
        public static AndroidJavaClass GetSettingsSystemClass()
        {
            return new AndroidJavaClass("android.provider.Settings$System");
        }

        static AndroidJavaClass GetSettingsGobalClass()
        {
            return new AndroidJavaClass("android.provider.Settings$Global");
        }

        static AndroidJavaObject GetContentResolver(AndroidJavaObject context)
        {
            return context.Call<AndroidJavaObject>("getContentResolver");
        }

        #region default

        public static void PutInt(AndroidJavaObject context, string name, int value)
        {
            PutIntSystem(context, name, value);
        }

        public static int GetInt(AndroidJavaObject context, string name, int def)
        {
            return GetIntSystem(context, name, def);
        }

        public static void PutString(AndroidJavaObject context, string name, string value)
        {
            PutStringSystem(context, name, value);
        }

        public static string GetString(AndroidJavaObject context, string name)
        {
            return GetStringSystem(context, name);
        }

        #endregion

        #region system

        public static bool PutStringSystem(AndroidJavaObject context, string key, string value)
        {
            using (AndroidJavaClass systemSetting = GetSettingsSystemClass())
            {
                using (AndroidJavaObject cr = GetContentResolver(context))
                {
                    return systemSetting.CallStatic<bool>("putString", cr, key, value);
                }
            }
        }

        public static string GetStringSystem(AndroidJavaObject context, string key)
        {
            using (AndroidJavaClass systemSetting = GetSettingsSystemClass())
            {
                using (AndroidJavaObject cr = GetContentResolver(context))
                {
                    return systemSetting.CallStatic<string>("getString", cr, key);
                }
            }
        }

        public static bool PutIntSystem(AndroidJavaObject context, string key, int value)
        {
            using (AndroidJavaClass systemSetting = GetSettingsSystemClass())
            {
                using (AndroidJavaObject cr = GetContentResolver(context))
                {
                    return systemSetting.CallStatic<bool>("putInt", cr, key, value);
                }
            }
        }

        public static int GetIntSystem(AndroidJavaObject context, string key, int def)
        {
            using (AndroidJavaClass systemSetting = GetSettingsSystemClass())
            {
                using (AndroidJavaObject cr = GetContentResolver(context))
                {
                    return systemSetting.CallStatic<int>("getInt", cr, key, def);
                }
            }
        }

        #endregion

        #region global

        public static bool PutStringGlobal(AndroidJavaObject context, string key, string value)
        {
            using (AndroidJavaClass globalSetting = GetSettingsGobalClass())
            {
                using (AndroidJavaObject cr = GetContentResolver(context))
                {
                    return globalSetting.CallStatic<bool>("putString", cr, key, value);
                }
            }
        }

        public static string GetStringGlobal(AndroidJavaObject context, string key)
        {
            using (AndroidJavaClass globalSetting = GetSettingsGobalClass())
            {
                using (AndroidJavaObject cr = GetContentResolver(context))
                {
                    return globalSetting.CallStatic<string>("getString", cr, key);
                }
            }
        }

        public static bool PutIntGlobal(AndroidJavaObject context, string key, int value)
        {
            using (AndroidJavaClass globalSetting = GetSettingsGobalClass())
            {
                using (AndroidJavaObject cr = GetContentResolver(context))
                {
                    return globalSetting.CallStatic<bool>("putInt", cr, key, value);
                }
            }
        }

        public static int GetIntGlobal(AndroidJavaObject context, string key, int def)
        {
            using (AndroidJavaClass globalSetting = GetSettingsGobalClass())
            {
                using (AndroidJavaObject cr = GetContentResolver(context))
                {
                    return globalSetting.CallStatic<int>("getInt", cr, key, def);
                }
            }
        }

        #endregion
    }
}

