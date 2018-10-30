using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class traumaDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Initial trauma care (ITC)/GCS/RTS", 39},
            {"Triage & transport criteria (table)", 41},
            {"Cardiac Arrest due to Trauma", 42},
            {"Conducted electrical weapon (Taser)", 42},
            {"Burns", 43},
            {"Chest trauma", 45},
            {"Eye emergencies / Facial trauma", 46},
            {"Head trauma", 47},
            {"Musculoskeletal trauma", 48},
            {"Spine trauma/Equipment removal guidelines", 49},
            {"Multiple Patient Incidents", 51},
            {"START & JumpSTART", 52},
            {"Stroke – Transport algorithm", 35},
            {"Seizures", 37},
            {"Shock differential – Hypovolemic / Septic", 38}
        };
    }
}
