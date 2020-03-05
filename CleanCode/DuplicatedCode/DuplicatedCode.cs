
using System;
using System.Security.AccessControl;
using System.Web.UI.WebControls;

namespace CleanCode.DuplicatedCode
{
    class DuplicatedCode
    {
        public class Time
        {

            public int Hours { get; private set; }
            public int Minutes { get; private set; }

            public Time(int hours, int minutes)
            {
                Hours = hours;
                Minutes = minutes;
            }

            public static Time Parse(string stringDateTime)
            {
                int time;

                if (!String.IsNullOrWhiteSpace(stringDateTime))
                {
                    if (Int32.TryParse(stringDateTime.Replace(":", ""), out time))
                    {
                        var hours = time / 100;
                        var minutes = time % 100;

                        return new Time(hours, minutes);
                    }
                    else
                    {
                        throw new ArgumentException("stringDateTime");
                    }
                }
                else
                    throw new ArgumentNullException("stringDateTime");
            }
        }


        public void AdmitGuest(string name, string admissionDateTime)
        {
            // Some logic 
            // ...

            var time = Time.Parse(admissionDateTime);

            // Some more logic 
            // ...
            if  (time.Hours < 10)
            {

            }
        }

        public void UpdateAdmission(int admissionId, string name, string admissionDateTime)
        {
            // Some logic 
            // ...

           var time = Time.Parse(admissionDateTime);

            // Some more logic 
            // ...
            if (time.Hours < 10)
            {

            }
        }
    }
}
