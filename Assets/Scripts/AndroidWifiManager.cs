// Created by hcq
//<uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidWifiManager
    {
        public static AndroidJavaObject GetWifiManager(AndroidJavaObject context)
        {
            using (AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context"))
            {
                string wifiService = contextClass.GetStatic<string>("WIFI_SERVICE");
                return context.Call<AndroidJavaObject>("getSystemService", wifiService);
            }
        }

        public static AndroidJavaObject GetWifiInfo(AndroidJavaObject wifiManager)
        {
            return wifiManager.Call<AndroidJavaObject>("getConnectionInfo");
        }

        public static int GetRssi(AndroidJavaObject wifiInfo)
        {
            return wifiInfo.Call<int>("getRssi");
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

