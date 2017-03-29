// Created by hcq
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidPackageManager
    {
        public static AndroidJavaObject GetPackageManager()
        {
            using (AndroidJavaObject context = AndroidContext.GetApplicationContext())
            {
                return context.Call<AndroidJavaObject>("getPackageManager");
            }
        }

        public static string GetPackageName()
        {
            using (AndroidJavaObject context = AndroidContext.GetApplicationContext())
            {
                return context.Call<string>("getPackageName");
            }
        }

        public static AndroidJavaObject GetApplicationInfo(int flags)
        {
            using (AndroidJavaObject pm = GetPackageManager())
            {
                return pm.Call<AndroidJavaObject>("getApplicationInfo", GetPackageName(), flags);
            }
        }

        public static int GetPackageManagerFlags(string flagsFieldName)
        {
            using (AndroidJavaClass packageManagerClass = new AndroidJavaClass("android.content.pm.PackageManager"))
            {
                return packageManagerClass.GetStatic<int>(flagsFieldName);
            }
        }

        public static int GetMetadataFlag()
        {
            return GetPackageManagerFlags("GET_META_DATA");
        }

        public static string GetMetaData(string name)
        {
            using (AndroidJavaObject appInfo = GetApplicationInfo(GetMetadataFlag()))
            {
                using (AndroidJavaObject metaData = appInfo.Get<AndroidJavaObject>("metaData"))
                {
                    return metaData.Call<string>("getString", name, "");
                }
            }
        }


        public static int GetMetaDataInt(string name)
        {
            using (AndroidJavaObject appInfo = GetApplicationInfo(GetMetadataFlag()))
            {
                using (AndroidJavaObject metaData = appInfo.Get<AndroidJavaObject>("metaData"))
                {
                    return metaData.Call<int>("getInt", name, -1);
                }
            }
        }

        public static bool GetMetaDataBoolean(string name)
        {
            return GetMetaDataBoolean(name, false);
        }

        public static bool GetMetaDataBoolean(string name, bool defaultValue)
        {
            using (AndroidJavaObject appInfo = GetApplicationInfo(GetMetadataFlag()))
            {
                using (AndroidJavaObject metaData = appInfo.Get<AndroidJavaObject>("metaData"))
                {
                    return metaData.Call<bool>("getBoolean", name, defaultValue);
                }
            }
        }

        public static AndroidJavaObject GetPackageInfo()
        {
            using (AndroidJavaObject packageManager = GetPackageManager())
            {
                return packageManager.Call<AndroidJavaObject>("getPackageInfo", GetPackageName(), 0);
            }
        }

        public static string GetVersionName()
        {
            using (AndroidJavaObject packageInfo = GetPackageInfo())
            {
                return packageInfo.Get<string>("versionName");
            }
        }

        public static int GetVersionCode()
        {
            using (AndroidJavaObject packageInfo = GetPackageInfo())
            {
                return packageInfo.Get<int>("versionCode");
            }
        }

        public static string GetAppName()
        {
            using (AndroidJavaObject packageManager = GetPackageManager())
            {
                using (AndroidJavaObject applicationInfo = GetApplicationInfo(0))
                {
                    using (AndroidJavaObject label = packageManager.Call<AndroidJavaObject>("getApplicationLabel", applicationInfo))
                    {
                        return label.Call<string>("toString");
                    }
                }
            }
        }
    }
}

