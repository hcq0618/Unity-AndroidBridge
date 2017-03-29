// hcq 2017/3/29
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidContext
    {
        public static AndroidJavaObject GetApplicationContext(AndroidJavaObject activity = null)
        {
            if (activity == null)
            {
                using (activity = GetActivity())
                {
                    return activity.Call<AndroidJavaObject>("getApplicationContext");
                }
            }
            else
            {
                return activity.Call<AndroidJavaObject>("getApplicationContext");
            }
        }

        public static AndroidJavaObject GetActivity()
        {
            return AndroidActivity.Get();
        }

        public static AndroidJavaObject GetResource()
        {
            using (AndroidJavaObject context = GetApplicationContext())
            {
                return context.Call<AndroidJavaObject>("getResources");
            }
        }

        public static AndroidJavaObject GetContentResolver()
        {
            using (AndroidJavaObject context = GetApplicationContext())
            {
                return context.Call<AndroidJavaObject>("getContentResolver");
            }
        }
    }
}
