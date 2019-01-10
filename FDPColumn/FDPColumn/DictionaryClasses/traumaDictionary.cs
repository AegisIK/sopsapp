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
            {"Hazardous Materials Incidents", 53},
            {"Chemical Agents", 54},
            {"Active Shooter Response", 55},
            {"Widespread disease outbreak", 57},
            {"Abuse and Maltreatment: Domestic/Sexual/Elder", 58 },
            {"Trauma in pregnancy", 59}
        };
    }
}
