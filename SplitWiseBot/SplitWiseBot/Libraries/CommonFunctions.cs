using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SplitWiseBot.Libraries
{
    public class CommonFunctions
    {
        /// <summary>
        /// Check if any property of class is empty or not
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public static Dictionary<String, object> IsAnyNullOrEmpty<T>(object myObject)
        {

            Dictionary<String, object> _DicObject = new Dictionary<String, object>();
            try
            {
                Type fieldsType = typeof(T);
                FieldInfo[] ps = fieldsType.GetFields();
                foreach (FieldInfo pi in ps)
                {
                    if (pi.FieldType == typeof(string))
                    {
                        string value = (string)pi.GetValue(myObject);
                        if (string.IsNullOrEmpty(value))
                        {
                            _DicObject.Add(pi.Name, pi.Name + "cannot be empty");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _DicObject;
        }
        /// <summary>
        /// Check if email is in proper format
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Check if phone number is valid or not
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^([0-9]{10})$").Success;
        }


    }
    public class SimpleObjectDictionaryMapper<TObject>
    {
        public static TObject GetObject(IDictionary<string, object> d)
        {
            FieldInfo[] props = typeof(TObject).GetFields();
            TObject res = Activator.CreateInstance<TObject>();
            for (int i = 0; i < props.Length; i++)
            {
                if (d.ContainsKey(props[i].Name))
                {
                    props[i].SetValue(res, d[props[i].Name]);
                }
            }
            return res;
        }


        public static Dictionary<string, object> GetDictionary(TObject o)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            PropertyInfo[] props = typeof(TObject).GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                if (props[i].CanRead)
                {
                    res.Add(props[i].Name, props[i].GetValue(o, null));
                }
            }
            return res;
        }
    }
}
