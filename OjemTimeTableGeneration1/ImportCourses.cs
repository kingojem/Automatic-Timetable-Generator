using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;




namespace OjemTimeTableGeneration1
{
    class ImportCourses
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int CourseUnit { get; set; }
        public string Size { get; set; } = "unknown";
        private static string FilePath { get; set; }
        public static List<ImportCourses> CourseList { get; set; }
        private static OleDbConnection OleDbConnectionSucess { get; set; }

        public override string ToString()
        {
            return $"Course Name: {Name.ToUpper()} Course Unit: {Status.ToUpper()}";
        }

        public static OleDbConnection GetOleDbConnectionSucess(string filePath)
        {
            FilePath = filePath;
            OleDbConnectionSucess = OpenConnection(FilePath);
            return OleDbConnectionSucess;
        }

        public static List<ImportCourses> ListOfCourses()
        {



            CourseList = (List<ImportCourses>)Hallssy(OleDbConnectionSucess);


            return CourseList;

        }
        public static void ClearCourseList()
        {
            CourseList.Clear();
            
        }
        private static OleDbConnection OpenConnection(string path)
        {
            OleDbConnectionSucess = null;
            try
            {
                if (Path.GetExtension(path) == ".xls")
                {
                    OleDbConnectionSucess = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + path + "Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                    OleDbConnectionSucess.Open();

                }
                else if (Path.GetExtension(path) == ".xlsx")
                {
                    OleDbConnectionSucess = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;';");
                    OleDbConnectionSucess.Open();
                }
                else if (Path.GetExtension(path) == ".xlsm")
                {
                    OleDbConnectionSucess = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + path + ";Extended Properties='Excel 12.0 Macro;HDR=YES;';");
                    OleDbConnectionSucess.Open();
                }
                else if (Path.GetExtension(path) == ".xlsb")
                {
                    OleDbConnectionSucess = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;';");
                    OleDbConnectionSucess.Open();
                }
            }
            catch (Exception)
            {
                return OleDbConnectionSucess;
            }
            return OleDbConnectionSucess;
        }

        private static IList<ImportCourses> Hallssy(OleDbConnection OleDbConnectionSucess)
        {
            List<ImportCourses> halls = null;
            try
            {
                OleDbCommand dbCommand = new OleDbCommand();
                OleDbDataAdapter dbDataAdapter = new OleDbDataAdapter();
                DataSet dataSet = new DataSet();

                dbCommand.Connection = OleDbConnectionSucess;
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = "SELECT * FROM [" + Path.GetFileNameWithoutExtension(FilePath) + "$]";
                dbDataAdapter = new OleDbDataAdapter(dbCommand);
                dbDataAdapter.Fill(dataSet);


                halls = dataSet.Tables[0].AsEnumerable().Select(s => new ImportCourses
                {
                    Name = Convert.ToString(s["Course Code"] != DBNull.Value ? s["Course Code"] : "empty"),
                    Status = Convert.ToString(s["Course Status"] != DBNull.Value ? s["Course Status"] : "unknown"),
                    CourseUnit = Convert.ToInt16(s["Course Unit"] != DBNull.Value ? s["Course Unit"] : "0"),
                }).ToList();



            }
            catch (Exception)
            {

                OleDbConnectionSucess.Close();
                return halls;
            }
            finally
            {
                OleDbConnectionSucess.Close();

            }




            // Name = Convert.ToString(s["Hall Name"] != DBNull.Value ? s["Hall Name"] : ""),




            return halls;
        }

    }
}
