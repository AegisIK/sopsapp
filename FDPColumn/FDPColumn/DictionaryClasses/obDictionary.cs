using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class obDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Childbirth", 66},
            {"Newborn and post-partum care", 67},
            {"Delivery complications", 68},
            {"Newborn resuscitation", 69},
            {"OB complications", 70}
        };
    }
}
