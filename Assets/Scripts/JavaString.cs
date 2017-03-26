//  Created by hcq on 07/20/2016.
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class JavaString
    {
        public static AndroidJavaClass GetStringClass()
        {
            return new AndroidJavaClass("java.lang.String");
        }

        public static string Format(string format, params string[] args)
        {
            using (AndroidJavaClass stringClass = GetStringClass())
            {
                return stringClass.CallStatic<string>("format", format, args);
            }
        }
    }
}

