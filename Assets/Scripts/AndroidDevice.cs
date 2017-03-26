// Created by hcq
using UnityEngine;
using System;
using System.IO;

namespace UnityAndroidBridge
{
    public static class AndroidDevice
    {
        //不用开wifi 获取mac地址 只有android 2.3以上才有该接口
        public static string GetMacAddressFromIp(string ip)
        {
            string macAdress = "";

            if (!string.IsNullOrEmpty(ip))
            {
                using (AndroidJavaClass inNetAddressClass = new AndroidJavaClass("java.net.InetAddress"))
                {
                    using (AndroidJavaObject inetAddress = inNetAddressClass.CallStatic<AndroidJavaObject>("getByName", ip))
                    {
                        using (AndroidJavaClass networkInterfaceClass = GetNetworkInterfaceClass())
                        {
                            using (AndroidJavaObject networkInterface = networkInterfaceClass.CallStatic<AndroidJavaObject>("getByInetAddress", inetAddress))
                            {
                                byte[] mac = networkInterface.Call<byte[]>("getHardwareAddress");
                                macAdress = Byte2hex(mac);
                            }
                        }
                    }
                }
            }

            return macAdress;
        }

        public static string GetMacAddress()
        {
            string macStr = "";
            using (AndroidJavaObject processObj = new AndroidJavaClass("java.lang.Runtime"))
            {
                using (AndroidJavaObject runtime = processObj.CallStatic<AndroidJavaObject>("getRuntime"))
                {
                    using (AndroidJavaObject process = runtime.Call<AndroidJavaObject>("exec", "cat /sys/class/net/wlan0/address"))
                    {
                        using (AndroidJavaObject inputStream = process.Call<AndroidJavaObject>("getInputStream"))
                        {
                            using (AndroidJavaObject inputStreamObj = new AndroidJavaObject("java.io.InputStreamReader", inputStream))
                            {
                                using (AndroidJavaObject lineReader = new AndroidJavaObject("java.io.LineNumberReader", inputStreamObj))
                                {
                                    macStr = lineReader.Call<string>("readLine").Trim();
                                }
                            }
                        }
                    }
                }
            }

            return macStr;
        }

        static string Byte2hex(byte[] byteArray)
        {
            string hex = BitConverter.ToString(byteArray);
            return hex.Replace("-", "");
        }

        static AndroidJavaClass GetNetworkInterfaceClass()
        {
            return new AndroidJavaClass("java.net.NetworkInterface");
        }

        //获取本地IP 只有android 2.3以上才有该接口
        public static string GetIpAddress()
        {
            //org.apache.http.conn.util.InetAddressUtils
            using (AndroidJavaClass inetAddressUtils = new AndroidJavaClass("org.apache.http.conn.util.InetAddressUtils"))
            {

                using (AndroidJavaClass networkInterfaceClass = GetNetworkInterfaceClass())
                {
                    using (AndroidJavaObject networkInterfaces = networkInterfaceClass.CallStatic<AndroidJavaObject>("getNetworkInterfaces"))
                    {
                        while (networkInterfaces.Call<bool>("hasMoreElements"))
                        {
                            using (AndroidJavaObject intf = networkInterfaces.Call<AndroidJavaObject>("nextElement"))
                            {
                                using (AndroidJavaObject enumIpAddr = intf.Call<AndroidJavaObject>("getInetAddresses"))
                                {
                                    while (enumIpAddr.Call<bool>("hasMoreElements"))
                                    {
                                        using (AndroidJavaObject inetAddress = enumIpAddr.Call<AndroidJavaObject>("nextElement"))
                                        {
                                            string address = inetAddress.Call<string>("getHostAddress");
                                            //http://blog.csdn.net/olevin/article/details/16985627
                                            if (!inetAddress.Call<bool>("isLoopbackAddress") && inetAddressUtils.CallStatic<bool>("isIPv4Address", address) && !inetAddress.Call<bool>("isLinkLocalAddress"))
                                            {
                                                return address;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return "";
        }

        public static AndroidJavaObject GetTelephonyManager(AndroidJavaObject context)
        {
            using (AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context"))
            {
                string connectivityServiceName = contextClass.GetStatic<string>("TELEPHONY_SERVICE");
                return context.Call<AndroidJavaObject>("getSystemService", connectivityServiceName);
            }
        }

        public static int GetBatteryLevel()
        {
            string CapacityString = File.ReadAllText("/sys/class/power_supply/battery/capacity");
            return int.Parse(CapacityString);
        }

        //获取sd卡可用空间
        public static long GetSDCardAvaliableBytes()
        {
            string path = GetSDCardPath();

            using (AndroidJavaObject statFs = new AndroidJavaObject("android.os.StatFs", path))
            {
                //mounted
                long size;
                if (AndroidBuild.GetBuildVersionSDKInt() >= 18)
                {
                    size = statFs.Call<long>("getAvailableBlocksLong") * statFs.Call<long>("getBlockSizeLong");
                }
                else
                {
                    size = statFs.Call<long>("getAvailableBlocks") * statFs.Call<long>("getBlockSize");
                }

                return size;
            }
        }

        public static string GetSDCardAvaliable(AndroidJavaObject context)
        {
            return FormatFileSize(context, GetSDCardAvaliableBytes());
        }

        //获取sd卡总大小
        public static long GetSDCardTotalBytes()
        {
            string path = GetSDCardPath();

            using (AndroidJavaObject statFs = new AndroidJavaObject("android.os.StatFs", path))
            {
                //mounted
                long size;
                if (AndroidBuild.GetBuildVersionSDKInt() >= 18)
                {
                    size = statFs.Call<long>("getBlockCountLong") * statFs.Call<long>("getBlockSizeLong");
                }
                else
                {
                    size = statFs.Call<long>("getBlockCount") * statFs.Call<long>("getBlockSize");
                }

                return size;
            }
        }

        public static string GetSDCardTotal(AndroidJavaObject context)
        {
            return FormatFileSize(context, GetSDCardTotalBytes());
        }

        //获取sd卡缓存路径
        public static string GetExternalCacheDir()
        {
            using (AndroidJavaObject activity = GetActivity())
            {
                using (AndroidJavaObject cacheDir = activity.Call<AndroidJavaObject>("getExternalCacheDir"))
                {
                    string path = cacheDir.Call<string>("getPath");
                    return path;
                }
            }
        }

        static AndroidJavaObject GetActivity()
        {
            using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                return unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }

        public static long GetMaxMemory()
        {
            using (AndroidJavaClass runtime = new AndroidJavaClass("java.lang.Runtime"))
            {
                using (AndroidJavaObject run = runtime.CallStatic<AndroidJavaObject>("getRuntime"))
                {
                    long maxMemory = run.Call<long>("maxMemory");
                    return maxMemory;
                }
            }
        }

        static AndroidJavaClass GetEnvironmentClass()
        {
            return new AndroidJavaClass("android.os.Environment");
        }

        public static bool IsExistSDCard()
        {
            using (AndroidJavaClass environment = GetEnvironmentClass())
            {
                string state = environment.CallStatic<string>("getExternalStorageState");
                return "mounted".Equals(state);
            }
        }

        public static string GetSDCardPath()
        {
            using (AndroidJavaClass environment = GetEnvironmentClass())
            {
                using (AndroidJavaObject directory = environment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"))
                {
                    return directory.Call<string>("getPath");
                }
            }
        }

        static string FormatFileSize(AndroidJavaObject context, long sizeBytes)
        {
            using (AndroidJavaClass formatter = new AndroidJavaClass("android.text.format.Formatter"))
            {
                return formatter.CallStatic<string>("formatFileSize", context, sizeBytes);
            }
        }
    }
}

