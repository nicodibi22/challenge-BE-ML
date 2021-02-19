using challenge_servicios.servicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_servicios.implementaciones
{
    public class SimpleArrayStringMerge : StrategyStringMerger
    {
        public string[] Merge(string[] stringsOne, string[] stringsTwo)
        {
            if (stringsOne.Length != stringsTwo.Length)
                throw new ArgumentException();
            string[] result = new string[stringsOne.Length];

            for (int index = 0; index < stringsOne.Length; index++)
            {
                if (stringsOne[index].Equals(stringsTwo[index]))
                    result[index] = stringsOne[index];
                else
                    result[index] = string.Empty;
            }
            return result;
        }
    }
}
