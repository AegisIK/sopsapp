using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class cardDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Acute Coronary Syndromes/STEMI", 17},
            {"Bradycardia with a Pulse", 18},
            {"Narrow QRS Complex Tachycardia", 19},
            {"Wide Complex Tachycardia with a Pulse", 20},
            {"Cardiac Arrest Management (Adult & Peds)", 21},
            {"Heart Failure/Pulmonary Edema/Cardiogenic Shock", 23},
            {"Left ventricular assist device", 24}
        };
    }
}
