using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class medDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Acute Abdominal/Flank Pain", 24},
            {"Dialysis/Chronic Renal Failure", 24},
            {"Alcohol Intoxication/Withdrawal", 25},
            {"Altered Mental Status/Syncope & Presyncope", 26},
            {"Drug Overdose/Poisoning", 27},
            {"Carbon monoxide (HBO)/Cyanide exposure", 28},
            {"Environmental emergencies: Cold related", 29},
            {"Environmental emergencies: Submersion", 30},
            {"Environmental emergencies: Heat related", 31},
            {"Glucose/Diabetes Emergencies", 32},
            {"Hypertension/Hypertensive crisis", 33},
            {"Psych/Behavioral Emerg/Agitated/Violent Pts", 34},
            {"Stroke – Transport algorithm", 35},
            {"Seizures", 37},
            {"Shock differential – Hypovolemic / Septic", 38}
        };
    }
}
