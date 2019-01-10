using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class obDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Childbirth", 60},
            {"Newborn and post-partum care", 61},
            {"Delivery complications", 62},
            {"Newborn resuscitation", 63},
            {"OB complications", 64}
        };
    }
}
