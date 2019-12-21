using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

using System.Text;
using System.Threading.Tasks;

namespace OjemTimeTableGeneration1
{
    /// <summary>
    /// This is a class That Assign Courses to the Hall Based on thier Sizes, and ensures to prevent clashes.
    /// Adding the merged course into a list of ongoing Courses
    /// </summary>
    class GenerateTimetable
    {
        public static List<OngoingClass> GetHallAvailabilityReturnOngoingCourses()
        {
            //List of ALL Courses

            //var listOfCourses = Courses.SendList ; OLD CODE
            var listOfCourses = ImportCourses.ListOfCourses();
            // return listOfCourses;
            //List of All Halls
            var listOfHalls = Halls.ListOfHalls();
            var onGoingClass = OngoingClass.OngoingCourses;


            #region First randomize the List of Courses
            //This is Creation of a Random List of Courses
            List<ImportCourses> randomCourseList = new List<ImportCourses>();
            //Call the Random Object
                Random random = new Random();
            //varable Random index
                     int randomIndex = 0;

            //loop
            while (listOfCourses.Count > 0)
            {
                randomIndex = random.Next(0, listOfCourses.Count);
                randomCourseList.Add(listOfCourses[randomIndex]);
                listOfCourses.RemoveAt(randomIndex);

            }
            #endregion
           
            #region Looping thru the Randomized Courses and Adding them to the Ongoing Courses


                //i have no idea why i wrote this, or what it does
                int checkHallAvailability = listOfHalls.FindIndex(i => i.Availability == true);
            //truthfully --- signed Ojo Emmanuel

            for (int i = 0; i < randomCourseList.Count; i++)
            {
                //loop thru a count of the random list
                
                foreach (var hall in listOfHalls  )
                {
                    //loop thru the available halls
                    
                    foreach (var course in randomCourseList)
                    {
                        if (course.Status.ToUpper() == "C")
                        {
                            course.Size = "BIG";
                        }
                        else if (course.Status.ToUpper() == "E")
                        {
                            course.Size = "SMALL";
                        }
                        else if (course.Status.ToUpper() == "R")
                        {
                            course.Size = "MEDIUM";
                        }
                        else
                        {
                            course.Size = "unknown";
                        }
                        //loop thru the list of courses
                        if (hall.Availability == true && course.Size.ToUpper() == hall.Capacity.ToUpper())
                        {
                            bool checkVal = onGoingClass.Exists(x => x.LectureCode == course.Name.ToUpper());
                            //var check = onGoingClass.Find(x => x.LectureCode == course.Name);
                            //{
                            ////add the merged list to the ongoing class
                            //onGoingClass.Add(new OngoingClass { LectureCode = course.Name.ToUpper(), Location = hall.Number });
                            //hall.Availability = false;

                            //}

                            if ((checkVal == true) )
                            {
                                onGoingClass.Remove(new OngoingClass { LectureCode = course.Name.ToUpper(), Location = hall.Number });

                            }
                            else
                            {
                                onGoingClass.Add(new OngoingClass { LectureCode = course.Name.ToUpper(), Location = hall.Number });
                                hall.Availability = false;
                            }


                        }
                    }

                    foreach (var Ahall in listOfHalls)
                    {
                        Ahall.Availability = true;
                    }
                }
            }
            #endregion
            return onGoingClass;

            // return onGoingClass;
        }
    }

    #region This is the Region of Ongoing Courses
    class OngoingClass
    {
        /// <summary>
        /// The Ongoing Lectures
        /// </summary>
        public string LectureCode { get; set; }
        public int Location { get; set; }
        public static List<OngoingClass> OngoingCourses { get; set; } = new List<OngoingClass>();

        public override string ToString()
        {
            return $" {LectureCode} ({Location})";
        }
    }
    #endregion
}
