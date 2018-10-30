using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class respDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Airway obstruction", 11},
            {"Drug Assisted Intubation", 12},
            {"Allergic Reaction/Anaphylactic Shock", 13},
            {"Asthma/COPD", 14},
            {"Pts w/ tracheostomy (adult or peds)", 15}
        };
    }
}
