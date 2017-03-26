// Created by hcq
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class JavaList
    {
        public static int Size(AndroidJavaObject list)
        {
            return list.Call<int>("size");
        }

        public static T Get<T>(AndroidJavaObject list, int index)
        {
            return list.Call<T>("get", index);
        }
    }
}

