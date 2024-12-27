using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TesseractOCR_GUI.Interop
{
    public class TesseractWrapper
    {
        [DllImport("TesseractApiTest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr getEnglishText(string imagePath);

        [DllImport("TesseractApiTest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr getChineseText(string imagePath);

        [DllImport("TesseractApiTest.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void freeMemory(IntPtr ptr);

        public string GetEnglishTextNative(string imagePath)
        {
            IntPtr resultPtr = getEnglishText(imagePath);

            try
            {
                if (resultPtr == IntPtr.Zero)
                {
                    return "识别失败";
                }

                string? result = Marshal.PtrToStringUTF8(resultPtr);

                if (string.IsNullOrEmpty(result))
                {
                    return "识别失败";
                }

                return result;
            }
            finally
            {
                if (resultPtr != IntPtr.Zero)
                {
                    freeMemory(resultPtr);
                }
            }
        }

        public string GetChineseTextNative(string imagePath)
        {
            IntPtr resultPtr = getChineseText(imagePath);
            if (resultPtr == IntPtr.Zero)
            {
                freeMemory(resultPtr);
                return "识别失败";
            }

            string? result = Marshal.PtrToStringUTF8(resultPtr);

            if (string.IsNullOrEmpty(result))
            {
                freeMemory(resultPtr);
                return "识别失败";
            }
            freeMemory(resultPtr);
            return result;
        }

    }
}
