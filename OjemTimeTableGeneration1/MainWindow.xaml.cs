#region Using

using System;
using System.Windows;
using Microsoft.Win32;
#endregion


namespace OjemTimeTableGeneration1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Props
        private int CountCourse { get; set; }
        private int CountHall { get; set; }

        private string FileNameAndPath { get; set; }
        //private Excel.Excel.Application application 

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            Init();
           
           
        }
        #region External Classes
           
            Courses courses = new Courses();
        #endregion

        public void Init()
        {

            #region Thickneckness and Maximize Disabler
                ResizeMode = ResizeMode.CanMinimize; // This Prevent The Application from being able to Maximize

                Thickness thickness = new Thickness(0, 20, 0, 0);  //Ensures Equal Paddingin the window(dont know if this works)
            #endregion

            #region Display of List of Halls
            //This is the Code for The available  of Halls in the University

            //list_availableHall.ItemsSource = Halls.ListOfHalls();
            #endregion

            #region Displays A Dynamic List of Courses
            //This is the Code for The available  of Courses for the semester in the University
                list_availableCourses.ItemsSource = courses.ReturnCourses();
            #endregion

            #region Disable Timetable Generation Button
            int? checkListCount = 0;
            int? checkHallCount = 0;
            try
            {
                checkListCount = ImportCourses.CourseList.Count;
                checkHallCount = Halls.HallList.Count;
            }
            catch (Exception)
            {

                if ((checkListCount == null || checkListCount == 0) || (checkHallCount == null || checkHallCount == 0))
                {
                    btn_GenerateTimeTable.IsEnabled = false;
                }
                else
                {
                    btn_GenerateTimeTable.IsEnabled = true;

                }
            }
            //int? 
            //int? 
            

            #endregion

        }




        #region Button Click Control that Generates the inputed courses(minimum of 12 based on the Halls) to Timetable

        private void Btn_GenerateTimeTable_Click(object sender, RoutedEventArgs e)
        {
            #region Ensures There is an adequate List of Courses before Generating
            int checkListCount = 0;
            int checkHallCount = 0;
            try
            {
               checkListCount = ImportCourses.CourseList.Count;
               checkHallCount = Halls.HallList.Count;
            }
            catch (Exception)
            {

                MessageBox.Show("Course List / Hall List is Empty", " GENERATION ERROR ");
                Btn_BrowseFile_Click(sender, e);
            }
           
            if ((checkListCount !=0 && checkListCount < checkHallCount) || (checkHallCount == 0 ))
            {
                MessageBox.Show("Course List Must Be 12 Above to Generate Time Table", " GENERATION ERROR ");
            }
            else
            {
                //btn_GenerateTimeTable.IsEnabled = true;
                var timeTableWindow = new TimetableWindow();
                timeTableWindow.Show();
                this.Close();
            }

            #endregion


        }

        #endregion



        

       

        private void Btn_BrowseFile_Click(object sender, RoutedEventArgs e)
        {
            //Create an Open File Dialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                //set the Title
                Title = "Select Spread sheet",
                //We have to set the filter for the File Dialog-- i.e the type of file to select and Default file Extention
                DefaultExt = ".xlsx",
                Filter = "SpreadSheets (*.xls, *.xlsx)|*.xls;*.xlsx"
            };
            //"Image files (*.bmp, *.jpg)|*.bmp;*.jpg|All files (*.*)|*.*"'


            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDialog.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                FileNameAndPath = openFileDialog.FileName;
                string filename = openFileDialog.SafeFileName;
                txtBox_FileName.Text = filename;
            }

        }

        //private void Btn_BrowseFileHall_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void Btn_AddBrowseFile_Click(object sender, RoutedEventArgs e)
        {
            var status = cmb_FileCategory.SelectionBoxItem.ToString();
            var txt = txtBox_FileName.Text;
            if (status == "" || txt== "")
            {
                MessageBox.Show("Select File and Choose File Category");
            }
            else
            {
                if (status == "Halls")
                {
                    var sendFileAndGetStatus = Halls.GetOleDbConnectionSucess(FileNameAndPath);
                    if (sendFileAndGetStatus == null)
                    {
                        MessageBox.Show("Something Wrong With File");
                    }
                    else
                    {
                        var halls = Halls.ListOfHalls();
                        if (halls == null)
                        {
                            MessageBox.Show("Table Name Error:\nEnsure Sheet Name is Renamed to File Name \nEnsure All Editing is Completed", "Warning");

                        }
                        else
                        {
                            CountHall = halls.Count;
                            list_availableHall.ItemsSource = halls;
                           // list_availableCourses.ItemsSource = halls;
                        }
                    }
                }
                else
                {

                    var sendFileAndGetStatus = ImportCourses.GetOleDbConnectionSucess(FileNameAndPath);
                    if (sendFileAndGetStatus == null)
                    {
                        MessageBox.Show("Something Wrong With File");
                    }
                    else
                    {
                        var courses = ImportCourses.ListOfCourses();
                        if (courses == null)
                        {
                            MessageBox.Show("Table Name Error \n Ensure Sheet Name is Renamed to File Name", "Warning");

                        }
                        else
                        {
                            #region Displays Available Courses Count
                            CountCourse = courses.Count;
                           
                            lbl_availableCourses.Content = $"Registered Courses ({CountCourse})";
                           
                            #endregion
                            list_availableCourses.ItemsSource = courses;
                        }
                    }
                }
               
            }
           
            if(CountCourse > 0 && CountHall > 0)
            {
                btn_GenerateTimeTable.IsEnabled = true;
            }

        }
        
    }

}


