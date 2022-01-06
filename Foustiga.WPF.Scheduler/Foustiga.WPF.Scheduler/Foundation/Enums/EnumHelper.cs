using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Foustiga.WPF.Scheduler.Foundation.Enums
{

        /*
          Enum declared with Attributes Display and Tooltip.
          Display is the value shown in the combobox/texbox/label...
          Tooltip is the text in tooltip when cursor is on a specific enum value.
         * 
         * 
         * Each enum must be declared as:
             public enum Partner_Profile_Enum
             {
                [EnumInfo(Display = "None", Tooltip = "Select a profile")]
                None = 0,

                [EnumInfo(Display = "Company", Tooltip = "Company or Company-related")]
                Company,

                [EnumInfo(Display = "Individual", Tooltip = "Physical person")]
                Individual
             }

        For a localized enum:
              public enum ROE_Update_DurationUnit
                {
                [EnumInfo(ResourceType = typeof(Resources.Resources), //where Resources.Resources is the Resources file to look into.
                    Display = "Enum_ROE_Update_DurationUnit_Months", //and here is the ResourceKey
                    Tooltip = "Enum_ROE_Update_DurationUnit_Months_Tooltip")]
                Month,

                [EnumInfo(Display = "Enum_ROE_Update_DurationUnit_Years", ResourceType = typeof(Resources.Resources))]
                Year,
                }


         */

        public class EnumInfoAttribute : Attribute //Attributes allowed to be part of Enums
        {

            //public string DisplayResource { get; set; }


            public Type ResourceType { get; set; }

            private string _display;
            public string Display
            {
                get
                {
                    if (ResourceType == null) return _display;//No resource, no localization.
                    return Localization.GetResourceEntry(
                        ResourceType: ResourceType,
                        ResourceKey: _display);
                }
                set { _display = value; }
            }



            private string _tooltip;
            public string Tooltip
            {
                get
                {
                    if (ResourceType == null) return _tooltip;//No resource, no localization.
                    return Localization.GetResourceEntry(
                        ResourceType: ResourceType,
                        ResourceKey: _tooltip);
                }
                set { _tooltip = value; }
            }
        }

        public class EnumContentConverter
        {
            private object _enumValue;
            public EnumContentConverter(object enumValue)
            {
                if (enumValue != null)
                {
                    if (!enumValue.GetType().IsEnum)
                    {
                        throw new ArgumentException("enumValue must be an Enumeration type");
                    }
                    else
                    {
                        _enumValue = enumValue;
                    }
                }
            }

            public string Display
            {
                get
                {
                    var fieldInfo = _enumValue.GetType().GetField(_enumValue.ToString());
                    var array = fieldInfo.GetCustomAttributes(false);
                    foreach (var attrib in array)
                    {
                        if (attrib is EnumInfoAttribute)
                        {
                            EnumInfoAttribute enumInfoAttrib = attrib as EnumInfoAttribute;
                            return enumInfoAttrib.Display;
                        }
                    }

                    //if we get here than there was no attrib, just pretty up the output by spacing on case
                    // per http://stackoverflow.com/questions/155303/net-how-can-you-split-a-caps-delimited-string-into-an-array
                    string name = Enum.GetName(_enumValue.GetType(), _enumValue);
                    return Regex.Replace(name, "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ");
                }

            }

            public string Tooltip
            {
                get
                {
                    var fieldInfo = _enumValue.GetType().GetField(_enumValue.ToString());
                    var array = fieldInfo.GetCustomAttributes(false);
                    foreach (var attrib in array)
                    {
                        if (attrib is EnumInfoAttribute)
                        {
                            EnumInfoAttribute enumInfoAttrib = attrib as EnumInfoAttribute;
                            return enumInfoAttrib.Tooltip;
                        }
                    }

                    //if we get here than there was no attrib, just pretty up the output by spacing on case
                    // per http://stackoverflow.com/questions/155303/net-how-can-you-split-a-caps-delimited-string-into-an-array
                    string name = Enum.GetName(_enumValue.GetType(), _enumValue);
                    return Regex.Replace(name, "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ");
                }

            }

        }
    }

