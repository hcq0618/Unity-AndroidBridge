// Created by hcq
//<uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidWifiManager
    {
        public static AndroidJavaObject GetWifiManager()
        {
            using (AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context"))
            {
                string wifiService = contextClass.GetStatic<string>("WIFI_SERVICE");

                using (AndroidJavaObject context = AndroidContext.GetApplicationContext())
                {
                    return context.Call<AndroidJavaObject>("getSystemService", wifiService);
                }
            }
        }

        public static AndroidJavaObject GetWifiInfo()
        {
            return GetWifiManager().Call<AndroidJavaObject>("getConnectionInfo");
        }

        public static int GetRssi()
        {
            return GetWifiInfo().Call<int>("getRssi");
        }

        //        rssi    int: The power of the signal measured in RSSI.
        //        numLevels   int: The number of levels to consider in the calculated level.
        //        returns int A level of the signal, given in the range of 0 to numLevels-1 (both inclusive).
        public static int CalculateSignalLevel(int rssi, int numLevels)
        {
            using (AndroidJavaClass wifiManagerClass = new AndroidJavaClass("android.net.wifi.WifiManager"))
            {
                return wifiManagerClass.CallStatic<int>("calculateSignalLevel", rssi, numLevels);
            }
        }
    }
}

