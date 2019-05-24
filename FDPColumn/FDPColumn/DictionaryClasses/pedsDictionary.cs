using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class pedsDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Peds initial medical care", 71},
            {"Peds IMC – Circulation / perfusion / GCS", 72},
            {"Peds Secondary assessment/sedation/VS", 73},
            {"Special Healthcare needs", 75},
            {"Peds Airway Adjuncts", 76},
            {"Peds Respiratory: FBO;", 77},
            {"Peds Respiratory Arrest, SIDS, BRUE", 78},
            {"Peds Allergic Rx ; Anaphylactic Shock", 79},
            {"Peds Asthma", 80},
            {"Peds Croup/ Epiglottitis / RSV/ Bronchiolitis", 81},
            {"Peds cardiac SOPs", 83},
            {"Peds medical SOPs", 85},
            {"Peds Seizure – Sepsis & Septic Shock", 89},
            {"Peds ITC/Trauma score/Trauma SOPs/Abuse", 91}
        };
    }
}
