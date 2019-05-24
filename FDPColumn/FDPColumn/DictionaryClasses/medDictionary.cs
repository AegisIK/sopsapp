using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class medDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Acute Abdominal/Flank Pain", 25},
            {"Dialysis/Chronic Renal Failure", 25},
            {"Alcohol Intoxication/Withdrawal", 26},
            {"Altered Mental Status/Syncope & Presyncope", 27},
            {"Drug Overdose/Poisoning", 28},
            {"Carbon monoxide (HBO)/Cyanide exposure", 30},
            {"Environmental emergencies: Cold related", 31},
            {"Environmental emergencies: Submersion", 32},
            {"Environmental emergencies: Heat related", 33},
            {"Glucose/Diabetes Emergencies", 34},
            {"Hypertension/Hypertensive crisis", 35},
            {"Psych/Behavioral Emerg/Agitated/Violent Pts", 36},
            {"Stroke – Assessment checklist", 38},
            {"Seizures", 40},
            {"Shock - Septic", 41},
            {"Shock – Hypovolemic", 42}
        };
    }
}
