// Created by hcq
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidAudioManager
    {
        static int GetStreamMusic(AndroidJavaObject audioManager)
        {
            return audioManager.GetStatic<int>("STREAM_MUSIC");
        }

        public static AndroidJavaObject GetAudioManager()
        {
            using (AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context"))
            {
                string audioService = contextClass.GetStatic<string>("AUDIO_SERVICE");

                using (AndroidJavaObject context = AndroidContext.GetApplicationContext())
                {
                    return context.Call<AndroidJavaObject>("getSystemService", audioService);
                }
            }
        }

        public static int GetMaxVolume(AndroidJavaObject audioManager)
        {
            return audioManager.Call<int>("getStreamMaxVolume", GetStreamMusic(audioManager));
        }

        public static int GetCurrentVolume(AndroidJavaObject audioManager)
        {
            return audioManager.Call<int>("getStreamVolume", GetStreamMusic(audioManager));
        }

        public static void SetCurrentVolume(AndroidJavaObject audioManager, int volume)
        {
            audioManager.Call("setStreamVolume", GetStreamMusic(audioManager), volume, 0);
        }
    }
}

