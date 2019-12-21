using System;
using System.Collections.Generic;
using static System.Math;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjemTimeTableGeneration1
{
    class SplitToPeriod
    {
       
        //private static int n { get; set; } = 0;
        public static List<OngoingClass>[] SplitToTimeTablePeriod()
        {
            int hallCount = Halls.ListOfHalls().Count;
           
            var OngoingClassesy = GenerateTimetable.GetHallAvailabilityReturnOngoingCourses();
            #region Was Used to mark/set the Periods for the Dynamic Tables, because of so many Bugs(The School system has just 46 periods so....)
            //n = OngoingClassesy.Count / hallCount;
            //if (OngoingClassesy.Count % hallCount > 0)
            //{
            //    n += 1;
            //}
            #endregion
            int counter = 0;
            List<OngoingClass>[] period = new List<OngoingClass>[46];


            int i = 0, sum = 0;
            int count12 = 0;
            do
            {
               //period = new List<OngoingClass>[n];

                // int sum = 0;

                period[counter] = new List<OngoingClass>();

                while (period[counter].Count <= (hallCount/2) - 1)
                {





                    #region Code That Ensures the Halls Doesnt Clash (Commented... Tiny Big Bug)
                    bool checkVal = period[counter].Exists(x => x.Location == OngoingClassesy[i].Location );
                    if (checkVal == true)
                    {

                        // period[counter].Remove(OngoingClassesy[i]);
                        try
                        {

                            period[counter + 1] = new List<OngoingClass>
                            {
                                //period[counter].Remove(OngoingClassesy[i]);
                                OngoingClassesy[i]
                            };

                            //while (!(n > hallCount * 10 * 5))
                            //{
                            //    n += 1;
                            //}
                        }
                        catch (Exception)
                        {

                            return period;
                        }




                    }
                    else
                    {
                       

                        period[counter].Add(OngoingClassesy[i]);

                    }
                    #endregion


                    count12++;
                        if (count12 == hallCount - 1)
                        {
                       
                                sum += i + 1;
                            i = sum;
                            count12 = 0;

                        
                       
                            //i = 0;
                        }
                    i++;

                   if (i >=  OngoingClassesy.Count)
                   {

                        i -= i;
                        //break;


                   }
                    sum = 0;
                    //period[counter].Add(OngoingClassesy[i]);

                    
                }
                counter++;
            } while (counter < period.Length);

        return period;
    }
    }
}
