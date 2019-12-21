using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.OleDb;
using System.IO;




namespace OjemTimeTableGeneration1
{
    class Halls
    {
        public string Name { get; set; }
        public string Capacity { get; set; }
        public int Number { get; set; }
        public bool Availability { get; set; }
        private  static string FilePath { get; set; }
        public static List<Halls> HallList { get; set; }
        private static OleDbConnection OleDbConnectionSucess { get; set; } 

        public override string ToString()
        {
            return $" Number: {Number} --> Name: {Name.ToUpper()}";
        }

        public static OleDbConnection GetOleDbConnectionSucess(string filePath)
        {
            FilePath = filePath;
            OleDbConnectionSucess = OpenConnection(FilePath);
            return OleDbConnectionSucess;
        }

        public static List<Halls> ListOfHalls()
        {

           

            HallList =(List<Halls>) Hallssy(OleDbConnectionSucess);


            return HallList;
            
        }
        public static void ClearHallList()
        {
            HallList.Clear();
           
        }
        private static OleDbConnection OpenConnection(string path)
          {
            OleDbConnectionSucess = null;
                try
                {
                    if(Path.GetExtension(path) == ".xls")
                    {
                        OleDbConnectionSucess = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + path + "Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                        OleDbConnectionSucess.Open();

                    }
                    else if(Path.GetExtension(path) == ".xlsx")
                    {
                        OleDbConnectionSucess = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source="+ path +";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;';");
                        OleDbConnectionSucess.Open();
                    }else if (Path.GetExtension(path) == ".xlsm")
                    {
                        OleDbConnectionSucess = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + path + ";Extended Properties='Excel 12.0 Macro;HDR=YES;';");
                        OleDbConnectionSucess.Open();
                    }else if (Path.GetExtension(path) == ".xlsb")
                    {
                        OleDbConnectionSucess = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;';");
                        OleDbConnectionSucess.Open();
                    }
            }
                catch(Exception)
                {
                    return OleDbConnectionSucess;
                }
            return OleDbConnectionSucess;
          }
        
        private static IList<Halls> Hallssy (OleDbConnection OleDbConnectionSucess)
        {
            List<Halls> halls = null ;
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

                
                        halls = dataSet.Tables[0].AsEnumerable().Select(s => new Halls
                        {
                            Number = Convert.ToInt16(s["Hall Number"] != DBNull.Value ? s["Hall Number"] : "-1"),
                            Name = Convert.ToString(s["Hall Name"] != DBNull.Value ? s["Hall Name"] : "empty"),
                            Capacity = Convert.ToString(s["Hall Capacity"] != DBNull.Value ? s["Hall Capacity"] : "unknown"),
                            Availability = Convert.ToBoolean(s["Hall Availability"] != DBNull.Value ? s["Hall Availability"] : false),
                        }).ToList();

                OleDbConnectionSucess.Close();

            }
            catch (Exception)
            {

                
                return halls;
            }
            //finally
            //{
            //    OleDbConnectionSucess.Close();

                
            //}

           


               // Name = Convert.ToString(s["Hall Name"] != DBNull.Value ? s["Hall Name"] : ""),
            



            return halls;
        }

    }
}
