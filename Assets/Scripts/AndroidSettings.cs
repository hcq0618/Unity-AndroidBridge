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

        #region default

        public static void PutInt(string name, int value)
        {
            PutIntSystem(name, value);
        }

        public static int GetInt(string name, int def)
        {
            return GetIntSystem(name, def);
        }

        public static void PutString(string name, string value)
        {
            PutStringSystem(name, value);
        }

        public static string GetString(string name)
        {
            return GetStringSystem(name);
        }

        #endregion

        #region system

        public static bool PutStringSystem(string key, string value)
        {
            using (AndroidJavaClass systemSetting = GetSettingsSystemClass())
            {
                using (AndroidJavaObject cr = AndroidContext.GetContentResolver())
                {
                    return systemSetting.CallStatic<bool>("putString", cr, key, value);
                }
            }
        }

        public static string GetStringSystem(string key)
        {
            using (AndroidJavaClass systemSetting = GetSettingsSystemClass())
            {
                using (AndroidJavaObject cr = AndroidContext.GetContentResolver())
                {
                    return systemSetting.CallStatic<string>("getString", cr, key);
                }
            }
        }

        public static bool PutIntSystem(string key, int value)
        {
            using (AndroidJavaClass systemSetting = GetSettingsSystemClass())
            {
                using (AndroidJavaObject cr = AndroidContext.GetContentResolver())
                {
                    return systemSetting.CallStatic<bool>("putInt", cr, key, value);
                }
            }
        }

        public static int GetIntSystem(string key, int def)
        {
            using (AndroidJavaClass systemSetting = GetSettingsSystemClass())
            {
                using (AndroidJavaObject cr = AndroidContext.GetContentResolver())
                {
                    return systemSetting.CallStatic<int>("getInt", cr, key, def);
                }
            }
        }

        #endregion

        #region global

        public static bool PutStringGlobal(string key, string value)
        {
            using (AndroidJavaClass globalSetting = GetSettingsGobalClass())
            {
                using (AndroidJavaObject cr = AndroidContext.GetContentResolver())
                {
                    return globalSetting.CallStatic<bool>("putString", cr, key, value);
                }
            }
        }

        public static string GetStringGlobal(string key)
        {
            using (AndroidJavaClass globalSetting = GetSettingsGobalClass())
            {
                using (AndroidJavaObject cr = AndroidContext.GetContentResolver())
                {
                    return globalSetting.CallStatic<string>("getString", cr, key);
                }
            }
        }

        public static bool PutIntGlobal(string key, int value)
        {
            using (AndroidJavaClass globalSetting = GetSettingsGobalClass())
            {
                using (AndroidJavaObject cr = AndroidContext.GetContentResolver())
                {
                    return globalSetting.CallStatic<bool>("putInt", cr, key, value);
                }
            }
        }

        public static int GetIntGlobal(string key, int def)
        {
            using (AndroidJavaClass globalSetting = GetSettingsGobalClass())
            {
                using (AndroidJavaObject cr = AndroidContext.GetContentResolver())
                {
                    return globalSetting.CallStatic<int>("getInt", cr, key, def);
                }
            }
        }

        #endregion
    }
}

