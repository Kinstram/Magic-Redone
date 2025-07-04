using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Redone.Simple
{
    public class SimplePageModel
    {
        public static void UpdateResult(SimplePageVM vM)
        {
            decimal existValue = GetValue(ModDicts.TimeExistDict, vM.TimeExist);
            decimal formValue = GetValue(ModDicts.FormDict, vM.Form);
            decimal methodValue = GetValue(ModDicts.MethodDict, vM.Method);
            decimal castValue = GetValue(ModDicts.TimeCastDict, vM.TimeCast);
            decimal size = vM.Size;
            Debug.WriteLine("Size back\t" + size);

            vM.Result = Math.Round(existValue * formValue * methodValue * castValue * size, 2);
            Debug.WriteLine("Result back\t" + vM.Result);
            Debug.WriteLine($"existValue: {existValue}, formValue: {formValue}, methodValue: {methodValue}, castValue: {castValue}, size: {size}");
        }

        public static decimal GetValue(Dictionary<string, decimal> dict, string key)
        {
            if (key != null && dict.TryGetValue(key, out decimal value))
            {
                return value;
            }
            else return 1m;
        }

        public static void PointCount(SimplePageVM vM)
        {
            vM.Total = vM.Points * vM.Result;
        }
    }
}
