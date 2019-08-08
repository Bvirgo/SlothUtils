using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SlothUtils
{
    public static class ParseUtils
    {
        #region String & String ParseTool
     

        public static byte[] String2ByteDefault(string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        public static string Byte2StringDefault(byte[] byt)
        {
            return Encoding.Default.GetString(byt);
        }

        public static byte[] String2ByteUTF8(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string Byte2StringUTF8(byte[] byt)
        {
            if (null == byt)
            {
                return string.Empty;
            }
            return Encoding.UTF8.GetString(byt);
        }

        /// <summary>
        /// Eg:"5.4|3.2|2.3" --> List[5.4,3.2,2.3]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float[] String2FloatArray(string value)
        {
            string[] strArray = String2StringArray(value);
            float[] array = new float[strArray.Length];

            for (int i = 0; i < strArray.Length; i++)
            {
                float tmp = float.Parse(strArray[i]);

                array[i] = tmp;
            }

            return array;
        }

        /// <summary>
        /// Eg:"true|false" --> List[true,false]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool[] String2BoolArray(string value)
        {
            string[] strArray = String2StringArray(value);
            bool[] array = new bool[strArray.Length];

            for (int i = 0; i < strArray.Length; i++)
            {
                bool tmp = bool.Parse(strArray[i]);

                array[i] = tmp;
            }

            return array;
        }

        /// <summary>
        /// Eg:"2,3" -> <2,3>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2 String2Vector2(string value)
        {
            try
            {
                string[] values = value.Split(',');
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);

                return new Vector2(x, y);
            }
            catch (Exception e)
            {
                throw new Exception("ParseVector2: Don't convert value to Vector2 value:" + value + "\n" + e.ToString()); // throw  
            }
        }

        /// <summary>
        /// Eg:"2,3|2,5|3,6" --> List[<2,3>,<2,5>,<3,6>]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2[] String2Vector2Array(string value)
        {
            string[] strArray = String2StringArray(value);
            Vector2[] array = new Vector2[strArray.Length];

            for (int i = 0; i < strArray.Length; i++)
            {
                string[] values = strArray[i].Split(',');
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);

                array[i] = new Vector2(x, y);
            }

            return array;
        }

        /// <summary>
        /// Eg:"(2,3,4)" --><2,3,4>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3 String2Vector3(string value)
        {
            try
            {
                value = value.Replace(" ","");
                value = value.Replace("(","");
                value = value.Replace(")","");
                string[] values = value.Split(',');
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                float z = float.Parse(values[2]);

                return new Vector3(x, y, z);
            }
            catch (Exception e)
            {
                throw new Exception("ParseVector3: Don't convert value to Vector3 value:" + value + "\n" + e.ToString()); // throw  
            }
        }

        /// <summary>
        /// Eg:"2,3,4 |1,2,4" --> List[<2,3,4>,<1,2,4>]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3[] String2Vector3Array(string value)
        {
            string[] strArray = String2StringArray(value);
            Vector3[] array = new Vector3[strArray.Length];

            for (int i = 0; i < strArray.Length; i++)
            {
                string[] values = strArray[i].Split(',');
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                float z = float.Parse(values[2]);

                array[i] = new Vector3(x, y, z);
            }

            return array;
        }

        /// <summary>
        /// Eg:"12,34,225" -->Color(12,34,225)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color String2Color(string value)
        {
            try
            {
                string[] values = value.Split(',');
                float r = float.Parse(values[0]);
                float g = float.Parse(values[1]);
                float b = float.Parse(values[2]);
                float a = 1;

                if (values.Length > 3)
                {
                    a = float.Parse(values[3]);
                }

                return new Color(r, g, b, a);
            }
            catch (Exception e)
            {
                throw new Exception("ParseColor: Don't convert value to Color value:" + value + "\n" + e.ToString()); // throw  
            }
        }
        /// <summary>
        /// Eg:"1,2,3,4" -> <1,2,3,4>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public Vector4 StringToVector4(string str)
        {
            //InfoTips.LogInfo (str);
            str = str.Substring(1, str.Length - 2);
            string[] nums = str.Split(",".ToCharArray(), 4);
            return new Vector4(float.Parse(nums[0]), float.Parse(nums[1]), float.Parse(nums[2]), float.Parse(nums[3]));
        }

        /// <summary>
        /// Eg:"1,2,3,4" --> Quanernion<1,2,3,4>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public Quaternion StringToQuaternion(string str)
        {
            //InfoTips.LogInfo (str);
            str = str.Substring(1, str.Length - 2);
            string[] nums = str.Split(",".ToCharArray(), 4);
            return new Quaternion(float.Parse(nums[0]), float.Parse(nums[1]), float.Parse(nums[2]), float.Parse(nums[3]));
        }

        static public Color StringToColor4(string str)
        {
            //InfoTips.LogInfo (str);
            str = str.Substring(5, str.Length - 6);
            string[] nums = str.Split(",".ToCharArray(), 4);
            return new Color(float.Parse(nums[0]), float.Parse(nums[1]), float.Parse(nums[2]), float.Parse(nums[3]));
        }

        static string[] c_NullStringArray = new string[0];

        public static string[] String2StringArray(string value)
        {
            if (value != null
                    && value != ""
                    && value != "null"
                    && value != "Null"
                    && value != "NULL"
                    && value != "None")
            {
                return value.Split('|');
            }
            else
            {
                return c_NullStringArray;
            }

        }

        /// <summary>
        /// Eg:"1 | 2" -->List[1,2]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int[] String2IntArray(string value)
        {
            int[] intArray = null;
            if (!string.IsNullOrEmpty(value))
            {
                string[] strs = value.Split('|');
                intArray = Array.ConvertAll(strs, s => int.Parse(s));
                return intArray;
            }
            else
            {
                return new int[0];
            }
        }
        #endregion

        #region Double & Float
        public static double TryParam(string _strValue)
        {
            double d = 0;
            if (string.IsNullOrEmpty(_strValue))
            {
                return d;
            }
            double.TryParse(_strValue, out d);
            return d;
        }
        #endregion

        #region String Extention
        public static Vector3 ToVector3(this string _str)
        {
            return String2Vector3(_str);
        }

        public static Vector2 ToVector2(this string _str)
        {
            return String2Vector2(_str);
        }

        public static Color ToColor(this string _str)
        {
            return String2Color(_str);
        }

        public static int ToInt(this string _str)
        {
            _str = _str.Trim();
            Regex regex = new Regex(@"^\d+$");
            int n = int.MinValue;
            if(regex.IsMatch(_str))
            {
                n = int.Parse(_str);
            }
            return n;
        }

        public static float ToFloat(this string _str)
        {
            _str = _str.Trim();
            float f;
            if (float.TryParse(_str,out f))
            {
                return f;
            }
            return float.MinValue;
        }

        #endregion

       
    }
}
