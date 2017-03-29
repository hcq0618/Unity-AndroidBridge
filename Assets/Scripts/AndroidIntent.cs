// Created by hcq
using UnityEngine;
using System;

namespace UnityAndroidBridge
{
    public class AndroidIntent : IDisposable
    {
        readonly AndroidJavaObject intent;

        public AndroidIntent()
        {
            intent = new AndroidJavaObject("android.content.Intent");
        }

        public AndroidIntent(string actionName)
        {
            intent = new AndroidJavaObject("android.content.Intent", GetStringField(actionName));
        }

        public AndroidIntent SetAction(string action)
        {
            intent.Call<AndroidJavaObject>("setAction", action);
            return this;
        }

        public AndroidIntent AddCategory(string categoryName)
        {
            intent.Call<AndroidJavaObject>("addCategory", GetStringField(categoryName));

            return this;
        }

        public AndroidIntent SetComponent(string pkgName, string className)
        {
            using (AndroidJavaObject componentName = new AndroidJavaObject("android.content.ComponentName", pkgName, className))
            {
                intent.Call<AndroidJavaObject>("setComponent", componentName);
                return this;
            }
        }

        public AndroidIntent SetFlags(int flags)
        {
            intent.Call<AndroidJavaObject>("setFlags", flags);
            return this;
        }

        public AndroidJavaObject GetIntent()
        {
            return intent;
        }

        static AndroidJavaClass GetIntentClass()
        {
            return new AndroidJavaClass("android.content.Intent");
        }

        public static string GetStringField(string fieldName)
        {
            using (AndroidJavaClass intentClass = GetIntentClass())
            {
                return intentClass.GetStatic<string>(fieldName);
            }
        }

        public static int GetIntField(string fieldName)
        {
            using (AndroidJavaClass intentClass = GetIntentClass())
            {
                return intentClass.GetStatic<int>(fieldName);
            }
        }

        public AndroidIntent PutExtra(string name, bool value)
        {
            intent.Call<AndroidJavaObject>("putExtra", name, value);
            return this;
        }

        public AndroidIntent PutExtra(string name, int value)
        {
            intent.Call<AndroidJavaObject>("putExtra", name, value);
            return this;
        }

        public AndroidIntent PutExtra(string name, string value)
        {
            intent.Call<AndroidJavaObject>("putExtra", name, value);
            return this;
        }

        public void Dispose()
        {
            intent.Dispose();
        }
    }
}

