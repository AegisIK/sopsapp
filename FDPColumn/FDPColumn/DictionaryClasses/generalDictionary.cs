using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class generalDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Introduction", 1},
            {"EMS Scopes of Practice", 2},
            {"General Patient Assessment/IMC", 3},
            {"Pain management / Drug alternatives", 5},
            {"OLMC Report/Handover Reports", 6},
            {"Withholding or Withdrawing Resuscitation", 7},
            {"Elderly patients", 9},
            {"Extremely obese patients", 10}
        };
    }
}
