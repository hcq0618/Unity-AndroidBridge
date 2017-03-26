// Created by hcq
using UnityEngine;
using System.IO;

namespace UnityAndroidBridge
{
    public static class AndroidBitmap
    {
        static AndroidJavaClass GetBitmapClass()
        {
            return new AndroidJavaClass("android.graphics.Bitmap");
        }

        public static AndroidJavaObject CreateScaledBitmap(AndroidJavaObject srcBitmap, int dstWidth, int dstHeight, bool filter)
        {
            using (AndroidJavaClass bitmapClass = GetBitmapClass())
            {
                return bitmapClass.CallStatic<AndroidJavaObject>("createScaledBitmap", srcBitmap, dstWidth, dstHeight, filter);
            }
        }

        public static void Recycle(AndroidJavaObject bitmap)
        {
            bitmap.Call("recycle");
        }

        public static int GetWidth(AndroidJavaObject bitmap)
        {
            return bitmap.Call<int>("getWidth");
        }

        public static int GetHeight(AndroidJavaObject bitmap)
        {
            return bitmap.Call<int>("getHeight");
        }

        static AndroidJavaObject GetPNGCompressFormat()
        {
            using (AndroidJavaClass compressFormat = new AndroidJavaClass("android.graphics.Bitmap$CompressFormat"))
            {
                return compressFormat.GetStatic<AndroidJavaObject>("PNG");
            }
        }

        static bool CompressBitmap(AndroidJavaObject bitmap, AndroidJavaObject outputStream)
        {
            using (AndroidJavaObject pngCompressFormat = GetPNGCompressFormat())
            {
                return bitmap.Call<bool>("compress", pngCompressFormat, 100, outputStream);
            }
        }

        public static bool SaveToFile(AndroidJavaObject bitmap, string dirPath, string fileName)
        {
            if (bitmap == null || string.IsNullOrEmpty(dirPath) || string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string fullPath = dirPath + "/" + fileName;
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            using (AndroidJavaObject file = new AndroidJavaObject("java.io.File", dirPath, fileName))
            {
                using (AndroidJavaObject fos = new AndroidJavaObject("java.io.FileOutputStream", file))
                {
                    using (AndroidJavaObject bos = new AndroidJavaObject("java.io.BufferedOutputStream", fos))
                    {
                        CompressBitmap(bitmap, bos);

                        bos.Call("flush");
                        bos.Call("close");

                        return true;
                    }
                }
            }
        }

        public static byte[] Convert2Bytes(AndroidJavaObject bitmap)
        {
            if (bitmap == null)
            {
                return null;
            }

            byte[] result = null;

            //bitmap先转成byte[]
            using (AndroidJavaObject byteArrayOutputStream = new AndroidJavaObject("java.io.ByteArrayOutputStream"))
            {
                bool success = CompressBitmap(bitmap, byteArrayOutputStream);
                byteArrayOutputStream.Call("flush");

                Debug.Log("hcq Convert2Bytes " + success);
                if (success)
                {
                    result = byteArrayOutputStream.Call<byte[]>("toByteArray");
                }

                byteArrayOutputStream.Call("close");
            }

            return result;
        }
    }
}

