// hcq 2017/3/29
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidActivity
    {
        public static AndroidJavaObject Get()
        {
            using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                return unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }

        //在android ui线程中执行
        public static void RunOnAndroidUiThread(AndroidJavaRunnable runnable)
        {
            using (AndroidJavaObject activity = Get())
            {
                activity.Call("runOnUiThread", runnable);
            }
        }

        public static void StartActivity(AndroidIntent intent)
        {
            using (AndroidJavaObject activity = Get())
            {
                activity.Call("startActivity", intent.GetIntent());
                intent.Dispose();
            }
        }

        public static AndroidJavaObject GetApplication()
        {
            using (AndroidJavaObject activity = Get())
            {
                return activity.Call<AndroidJavaObject>("getApplication");
            }
        }
    }
}
