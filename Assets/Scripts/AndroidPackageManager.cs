// Created by hcq
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidPackageManager
    {
        public static AndroidJavaObject GetPackageManager(AndroidJavaObject context)
        {
            return context.Call<AndroidJavaObject>("getPackageManager");
        }

        public static string GetPackageName(AndroidJavaObject context)
        {
            return context.Call<string>("getPackageName");
        }

        public static AndroidJavaObject GetApplicationInfo(AndroidJavaObject context, int flags)
        {
            using (AndroidJavaObject pm = GetPackageManager(context))
            {
                return pm.Call<AndroidJavaObject>("getApplicationInfo", GetPackageName(context), flags);
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

        public static string GetMetaData(AndroidJavaObject context, string name)
        {
            using (AndroidJavaObject appInfo = GetApplicationInfo(context, GetMetadataFlag()))
            {
                using (AndroidJavaObject metaData = appInfo.Get<AndroidJavaObject>("metaData"))
                {
                    return metaData.Call<string>("getString", name, "");
                }
            }
        }


        public static int GetMetaDataInt(AndroidJavaObject context, string name)
        {
            using (AndroidJavaObject appInfo = GetApplicationInfo(context, GetMetadataFlag()))
            {
                using (AndroidJavaObject metaData = appInfo.Get<AndroidJavaObject>("metaData"))
                {
                    return metaData.Call<int>("getInt", name, -1);
                }
            }
        }

        public static bool GetMetaDataBoolean(AndroidJavaObject context, string name)
        {
            return GetMetaDataBoolean(context, name, false);
        }

        public static bool GetMetaDataBoolean(AndroidJavaObject context, string name, bool defaultValue)
        {
            using (AndroidJavaObject appInfo = GetApplicationInfo(context, GetMetadataFlag()))
            {
                using (AndroidJavaObject metaData = appInfo.Get<AndroidJavaObject>("metaData"))
                {
                    return metaData.Call<bool>("getBoolean", name, defaultValue);
                }
            }
        }

        public static AndroidJavaObject GetPackageInfo(AndroidJavaObject context)
        {
            using (AndroidJavaObject packageManager = GetPackageManager(context))
            {
                return packageManager.Call<AndroidJavaObject>("getPackageInfo", GetPackageName(context), 0);
            }
        }

        public static string GetVersionName(AndroidJavaObject context)
        {
            using (AndroidJavaObject packageInfo = GetPackageInfo(context))
            {
                return packageInfo.Get<string>("versionName");
            }
        }

        public static int GetVersionCode(AndroidJavaObject context)
        {
            using (AndroidJavaObject packageInfo = GetPackageInfo(context))
            {
                return packageInfo.Get<int>("versionCode");
            }
        }

        public static string GetAppName(AndroidJavaObject context)
        {
            using (AndroidJavaObject packageManager = GetPackageManager(context))
            {
                using (AndroidJavaObject applicationInfo = GetApplicationInfo(context, 0))
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

