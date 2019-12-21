using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.IO;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.Windows.Markup;
using Microsoft.Win32;
using PdfSharp.Xps;

namespace OjemTimeTableGeneration1
{
    /// <summary>
    /// Interaction logic for TimetableWindow.xaml
    /// </summary>
    public partial class TimetableWindow : Window
    {
        public TimetableWindow()
        {
            InitializeComponent();
            Init();

            

        }

        public void Init()
        {
            
            var period = SplitToPeriod.SplitToTimeTablePeriod();


            try
            {
                list_period0.ItemsSource = period[0];
                list_period1.ItemsSource = period[1];
                list_period2.ItemsSource = period[2];
                list_period3.ItemsSource = period[3];
                list_period4.ItemsSource = period[4];
                list_period5.ItemsSource = period[5];
                list_period6.ItemsSource = period[6];
                list_period7.ItemsSource = period[7];
                list_period8.ItemsSource = period[8];
                list_period9.ItemsSource = period[9];
                list_period10.ItemsSource = period[10];
                list_period11.ItemsSource = period[11];
                list_period12.ItemsSource = period[12];
                list_period13.ItemsSource = period[13];
                list_period14.ItemsSource = period[14];
                list_period15.ItemsSource = period[15];
                list_period16.ItemsSource = period[16];
                list_period17.ItemsSource = period[17];
                list_period18.ItemsSource = period[18];
                list_period19.ItemsSource = period[19];
                list_period20.ItemsSource = period[20];
                list_period21.ItemsSource = period[21];
                list_period22.ItemsSource = period[22];
                list_period23.ItemsSource = period[23];
                list_period24.ItemsSource = period[24];
                list_period25.ItemsSource = period[25];
                list_period26.ItemsSource = period[26];
                list_period27.ItemsSource = period[27];




            }
            catch (Exception)
            {
                List<string> empty = new List<string>
                {
                      " empty",  " empty",  " empty",

                };
                list_period1.ItemsSource = empty;
                list_period2.ItemsSource = empty;
                list_period3.ItemsSource = empty;
                list_period4.ItemsSource = empty;
                list_period5.ItemsSource = empty;
                list_period6.ItemsSource = empty;


            }


            //list_period3.ItemsSource = period[2];
        }
       
        private void Btn_goBack_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult confirmationDialog = MessageBox.Show($" Do You Want To Go Back? You Might Lose All Generated Result ", " EXIT WINDOW ", MessageBoxButton.YesNo);

            //string result;
            if (confirmationDialog == MessageBoxResult.Yes)
            {
                // MessageBox.Show(courseStatus);

                var mainWindow = new MainWindow();
                ClearLists();
                mainWindow.Show();
                this.Close();
            }


        }

        private static void ClearLists()
        {
            ImportCourses.ClearCourseList();
            Halls.ClearHallList();

        }

        private void Btn_goNext_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new NextTable();
            mainWindow.Show();
            this.Close();
        }

        #region Convert to Pdf Non-Working Code
        //private void SaveToPDF1_Click(object sender, RoutedEventArgs e)
        //{

        //    //try
        //    //{

        //    var a = this.ActualWidth;
        //    var b = this.ActualHeight;
        //    //TestPage fixedPage = new TestPage();
        //    // SaveToPDF.Visibility = Visibility.Collapsed;
        //    FixedDocument fixedDoc = new FixedDocument();
        //    PageContent pageContent = new PageContent();
        //    FixedPage fixedPage = new FixedPage();




        //    PrintDialog printDlg = new PrintDialog();

        //    //Size pageSize = new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight - 100);
        //    Size pageSize = new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight - 100);


        //    var visual = ((Panel)Content).Children[0] as UIElement;
        //    ((Panel)Content).Children.Remove(visual);
        //    fixedPage.Children.Add(visual);

        //    ((IAddChild)pageContent).AddChild(fixedPage);

        //    fixedDoc.Pages.Add(pageContent);

        //    // STEP 2: Convert this WPF Visual to an XPS Document
        //    MemoryStream lMemoryStream = new MemoryStream();
        //    {
        //        Package package = Package.Open(lMemoryStream, FileMode.Create);
        //        XpsDocument doc = new XpsDocument(package);
        //        XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
        //        writer.Write(fixedDoc);
        //        doc.Close();
        //        package.Close();
        //    }

        //    // STEP 3: Convert this XPS Document to a PDF file
        //    MemoryStream lOutStream = new MemoryStream();
        //    NiXPS.Converter.XpsToPdf(lMemoryStream, lOutStream);
        //    File.WriteAllBytes(@"..\..\..\..\..\..\kinggty.pdf", lOutStream.ToArray());

        //    //if()
        //    //{




        //    //}
        //    //catch (Exception)
        //    //{
        //    //    MessageBox.Show("File Safe Unsucessfull \n Try Again", "ERROR MESSAGE");
        //    //}

        //}
        #endregion

        private void SaveToPDF_Click(object sender, RoutedEventArgs e)
        {

                    var dialog = new SaveFileDialog();

                    //dialog.AddExtension = true;
                    dialog.DefaultExt = "pdf";
                    dialog.Filter = "PDF Document (*.pdf)|*.pdf";

                    if (dialog.ShowDialog() == false)
                        return;
            #region Shrinks the uI Properly
                //align the page properly
                table1.Margin = new Thickness(-13, 116, 0, -16);
                //Height="647" Margin="-15,91,0,0"
                header1.Margin = new Thickness(7, 17, 12, 570);
            #endregion


                    FixedDocument fixedDoc = new FixedDocument();
                    PageContent pageContent = new PageContent();
                    FixedPage fixedPage = new FixedPage();
            #region This Flips the Paper (A4 by standard ro be landscape)
                    fixedPage.Width = 11.6 * 96;
                    fixedPage.Height = 8.27 * 96;
            #endregion


                    //PrintDialog printDlg = new PrintDialog();
                    //Size pageSize = new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight - 100);

            UIElement visual = ((Panel)Content).Children[0] as UIElement ;
            UIElement visual1 = ((Panel)Content).Children[1] as UIElement;

            ((Panel)Content).Children.Remove(visual);
            ((Panel)Content).Children.Remove(visual1);
            fixedPage.Children.Add(visual);
            fixedPage.Children.Add(visual1);

            ((IAddChild)pageContent).AddChild(fixedPage);

                    fixedDoc.Pages.Add(pageContent);
           

            
            // write to PDF file
                    string tempFilename = "temp.xps";
                    File.Delete(tempFilename);
                    XpsDocument xpsd = new XpsDocument(tempFilename, FileAccess.Write);
                    XpsDocumentWriter xw = XpsDocument.CreateXpsDocumentWriter(xpsd);
                    
                    xw.Write(fixedDoc);
            
                    xpsd.Close();
            try
            {

                XpsConverter.Convert(tempFilename, dialog.FileName, 1);

            }
            catch (Exception)
            {

                MessageBox.Show("File To be Replaced is used by Another Application \n Try Closing the File and Try Again");

            }
            finally
            {
                fixedPage.Children.Remove(visual);
                ((Panel)Content).Children.Add(visual);
                fixedPage.Children.Remove(visual1);
                ((Panel)Content).Children.Add(visual1);

                header1.Margin = new Thickness(4, -1, 15, 588);
                table1.Margin = new Thickness(45, 81, 0, 0);
            }

        }
    }
    

    
}
