// Created by hcq
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class JavaMap
    {
        public static int Size(AndroidJavaObject map)
        {
            return map.Call<int>("size");
        }

        public static V Get<K, V>(AndroidJavaObject map, K key)
        {
            return map.Call<V>("get", key);
        }

    }
}

