using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace OjemTimeTableGeneration1
{

    class Courses
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int CizeOfRegisteredStudent { get; set; }
        private int CourseUnit { get; set; }
        private string SettingStatus { get; set; }
        public string Size { get; set; }
        public string Sizee { get; set; }

        //public  List<Courses> CourseList { get; set; }
        private ObservableCollection<Courses> CourseList { get; set; }
        public static List<Courses> SendList { get; set; } = new List<Courses>();

        public Courses()
        {
            CourseList = new ObservableCollection<Courses>();

        }


        public override string ToString()
        {
            //return base.ToString();
            return $"Course Name: {Name} Course Unit: {Size}";
        }

        public bool MakeCourses(Courses courses)
        {
            if (courses.Status == "Compulsory")
            {
                SettingStatus = "Big";
                Sizee = "C";
               
            }
            else if (courses.Status == "Required")
            {
                SettingStatus = "Medium";
                Sizee = "R";

            }
            else
            {
                if (courses.Status == "Elective")
                {
                    SettingStatus = "Small";
                    Sizee = "E";

                }
            }
            int count = CourseList.Count<Courses>(chk => chk.Name == courses.Name);
            if (count > 0)
            {
                return false;
            }
            else
            {
                CourseList.Add(new Courses
                {
                    Name = courses.Name,
                    Status = SettingStatus,
                    Size = Sizee
                    
                    
                });
                CourseList.CollectionChanged += CourseList_CollectionChanged;
            }
            //int search = CourseList.IndexOf(courses.Name);
            // CourseList.Contains(courses.Name);
            //CourseList.Where(chk=> chk.CourseList.Contains(courses.Name))

            return true;

        }

        public void RemoveCourses(Courses course)
        {


            var remove = CourseList.Remove(CourseList.Where(rmv => rmv.Name == course.Name).Single());

            if (remove)
            {
                CourseList.CollectionChanged += CourseList_CollectionChanged;

            }

        }

        private void CourseList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // throw new NotImplementedException();
            //return CourseList;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ReturnCourses();
                CourseListCount();
                SendList = MakeToList();
            }
            else
            {
                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    ReturnCourses();
                    CourseListCount();
                    SendList = MakeToList();

                }
            }
        }

        public IEnumerable<Courses> ReturnCourses()
        {
            return CourseList;
        }

        public int CourseListCount()
        {
            ///<summary>
            ///This Method returns the total count of the Dynamic Observable List
            /// </summary>

            return ReturnCourses().Count();
            //return SendList.Count;

        }
        private List<Courses> MakeToList()
        {
            return ReturnCourses().ToList();
        }

    }

}
