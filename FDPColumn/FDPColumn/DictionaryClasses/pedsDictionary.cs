using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class pedsDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Peds initial medical care", 65},
            {"Peds IMC - GCS", 66},
            {"Peds Secondary assessment/sedation/VS", 67},
            {"Special Healthcare needs ", 69},
            {"Peds Airway Adjuncts", 70},
            {"Peds Respiratory: FBO; Arrest, SIDS, BRUE", 68},
            {"Peds Anaphylaxis / Asthma / Croup/ Epiglottitis / RSV", 73},
            {"Peds cardiac SOPs", 76},
            {"Peds medical SOPs", 78},
            {"Peds ITC/Trauma score/Trauma SOPs/Abuse", 86}
        };
    }
}
