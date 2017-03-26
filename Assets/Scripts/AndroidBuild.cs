//  Created by hcq on 07/20/2016.
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidBuild
    {
        static AndroidJavaClass GetBuildClass()
        {
            return new AndroidJavaClass("android.os.Build");
        }

        static AndroidJavaClass GetVersionCodeClass()
        {
            return new AndroidJavaClass("android.os.Build$VERSION_CODES");
        }

        public static int GetBuildVersionSDKInt()
        {
            using (AndroidJavaClass buildVersionClass = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                return buildVersionClass.GetStatic<int>("SDK_INT");
            }
        }

        public static int GetGingerBreadInt()
        {
            using (AndroidJavaClass versionCodeClass = GetVersionCodeClass())
            {
                return versionCodeClass.GetStatic<int>("GINGERBREAD");
            }
        }

        public static int GetLollipopInt()
        {
            using (AndroidJavaClass versionCodeClass = GetVersionCodeClass())
            {
                return versionCodeClass.GetStatic<int>("LOLLIPOP");
            }
        }

        public static string GetBuildModel()
        {
            return SystemInfo.deviceModel;
        }

        public static string GetBuildId()
        {
            using (AndroidJavaClass buildClass = GetBuildClass())
            {
                return buildClass.GetStatic<string>("ID");
            }
        }

        public static string GetBuildBrand()
        {
            using (AndroidJavaClass buildClass = GetBuildClass())
            {
                return buildClass.GetStatic<string>("BRAND");
            }
        }

        public static string GetBuildDevice()
        {
            using (AndroidJavaClass buildClass = GetBuildClass())
            {
                return buildClass.GetStatic<string>("DEVICE");
            }
        }

        public static string GetBuildDisplay()
        {
            using (AndroidJavaClass buildClass = GetBuildClass())
            {
                return buildClass.GetStatic<string>("DISPLAY");
            }
        }
    }
}
