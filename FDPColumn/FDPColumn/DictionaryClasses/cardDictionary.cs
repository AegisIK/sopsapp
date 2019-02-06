using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class cardDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Acute Coronary Syndromes", 16},
            {"Bradycardia with a Pulse", 17},
            {"Narrow QRS Complex Tachycardia", 18},
            {"Wide Complex Tachycardia with a Pulse", 19},
            {"Ventricular fibrillation/pulseless VT", 20},
            {"Asystole/PEA", 21},
            {"Heart Failure/Pulmonary Edema/Cardiogenic Shock", 22},
            {"Left ventricular assist device", 23}
        };
    }
}
