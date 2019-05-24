using System;
using System.Collections.Generic;
using System.Text;

namespace FDPColumn.DictionaryClasses
{
    public static class traumaDictionary
    {
        public static IDictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            {"Initial trauma care (ITC)/GCS/RTS", 43},
            {"Triage & transport criteria (table)", 45},
            {"Cardiac Arrest due to Trauma", 46},
            {"Conducted electrical weapon (Taser)", 46},
            {"Burns", 47},
            {"Chest trauma", 49},
            {"Eye emergencies / Facial trauma", 50},
            {"Head trauma", 51},
            {"Musculoskeletal trauma", 53},
            {"Spine trauma/Equipment removal guidelines", 54},
            {"Multiple Patient Incidents", 56},
            {"START & JumpSTART", 57},
            {"Hazardous Materials Incidents", 58},
            {"Chemical Agents", 59},
            {"Chempack Requests", 60},
            {"Active Shooter Response", 61},
            {"Widespread disease outbreak", 63},
            {"Abuse and Maltreatment: Domestic/Sexual/Elder", 64},
            {"Trauma in pregnancy", 65}
        };
    }
}
