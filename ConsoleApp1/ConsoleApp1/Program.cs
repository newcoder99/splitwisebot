
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class Vishal
    {
        public string Id;
        public string Name;
        public string PhoneNumber;

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

    }

    class Program
    {

        static void Main(string[] args)
        {
            var config =
                new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", true, true)
                 .Build();
            var section0 = config.GetSection("section0").;

            //Dictionary<string, string> _s = section0.ToDictionary<string,string>(pair => pair.Key, pair =>pair.Value);

            Program p = new Program();
            Vishal v = new Vishal();
            v.Id = "1";
            v.Name = "hkj";
            v.PhoneNumber = "1234567890";

            bool i = Program.IsValidEmail("mailmail");

            IDictionary<string, object> v1 = new Dictionary<string, object>();
            //Program.IsAnyNullOrEmpty<Vishal>(v);


            v1.Add("Id", "1");
            v1.Add("Name", "2");
            v1.Add("PhoneNumber", "2");
            v = (Vishal)SimpleObjectDictionaryMapper<Vishal>.GetObject(v1);



        }
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^([0-9]{9})$").Success;
        }
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


        public static Dictionary<String, String> IsAnyNullOrEmpty<T>(object myObject)
        {

            Dictionary<String, String> _DicObject = new Dictionary<String, String>();
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

    }
}
