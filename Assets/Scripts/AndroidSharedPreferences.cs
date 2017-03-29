//  Created by hcq on 07/20/2016.
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class AndroidSharedPreferences
    {
        #region sp

        public static AndroidJavaObject GetSharedPreferences(string path)
        {
            using (AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context"))
            {
                int contextModePrivate = contextClass.GetStatic<int>("MODE_PRIVATE");

                using (AndroidJavaObject context = AndroidContext.GetApplicationContext())
                {
                    return context.Call<AndroidJavaObject>("getSharedPreferences", path, contextModePrivate);
                }
            }
        }

        public static AndroidJavaObject Edit(AndroidJavaObject sharedPreferences)
        {
            return sharedPreferences.Call<AndroidJavaObject>("edit");
        }

        public static void Submit(AndroidJavaObject sharedPreferencesEditor)
        {
            //api 9
            if (AndroidBuild.GetBuildVersionSDKInt() > AndroidBuild.GetGingerBreadInt())
            {
                sharedPreferencesEditor.Call("apply");
            }
            else
            {
                sharedPreferencesEditor.Call("commit");
            }
        }

        public static long GetLong(string path, string key, long def)
        {
            using (AndroidJavaObject sharedPreferences = GetSharedPreferences(path))
            {
                return sharedPreferences.Call<long>("getLong", key, def);
            }
        }

        public static long GetLong(string path, string key)
        {
            return GetLong(path, key, 0);
        }

        public static AndroidJavaObject PutLong(AndroidJavaObject editor, string key, long value)
        {
            editor = editor.Call<AndroidJavaObject>("putLong", key, value);
            return editor;
        }

        public static void SaveLong(string path, string key, long value)
        {
            using (AndroidJavaObject sharedPreferences = GetSharedPreferences(path))
            {
                using (AndroidJavaObject editor = Edit(sharedPreferences))
                {
                    using (AndroidJavaObject e = PutLong(editor, key, value))
                    {
                        Submit(e);
                    }
                }
            }
        }

        public static bool GetBoolean(string path, string key, bool def)
        {
            using (AndroidJavaObject sharedPreferences = GetSharedPreferences(path))
            {
                return sharedPreferences.Call<bool>("getBoolean", key, def);
            }
        }

        public static bool GetBoolean(string path, string key)
        {
            return GetBoolean(path, key, false);
        }

        public static AndroidJavaObject PutBoolean(AndroidJavaObject editor, string key, bool value)
        {
            editor = editor.Call<AndroidJavaObject>("putBoolean", key, value);
            return editor;
        }

        public static void SaveBoolean(string path, string key, bool value)
        {
            using (AndroidJavaObject sharedPreferences = GetSharedPreferences(path))
            {
                using (AndroidJavaObject editor = Edit(sharedPreferences))
                {
                    using (AndroidJavaObject e = PutBoolean(editor, key, value))
                    {
                        Submit(e);
                    }
                }
            }
        }

        #endregion

        #region PlayerPrefs

        //On Android data is stored (persisted) on the device. The data is saved in SharerPreferences. C#/JavaScript,
        //	Android Java and Native code can all access the PlayerPrefs data.
        //	The PlayerPrefs data is physically stored in /data/data/pkg-name/shared_prefs/pkg-name.xml.
        public static int GetInt(string key, int def)
        {
            return PlayerPrefs.GetInt(key, def);
        }

        public static int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public static void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public static float GetFloat(string key, float def)
        {
            return PlayerPrefs.GetFloat(key, def);
        }

        public static float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public static void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public static string GetString(string key, string def)
        {
            return PlayerPrefs.GetString(key, def);
        }

        public static string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public static void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        #endregion
    }
}
