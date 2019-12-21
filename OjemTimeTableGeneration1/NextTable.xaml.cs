using Microsoft.Win32;
using PdfSharp.Xps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace OjemTimeTableGeneration1
{
    /// <summary>
    /// Interaction logic for NextTable.xaml
    /// </summary>
    public partial class NextTable : Window
    {
        public NextTable()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {

            var period = SplitToPeriod.SplitToTimeTablePeriod();


            try
            {
                list_period30.ItemsSource = period[0];
                list_period31.ItemsSource = period[29];
                list_period32.ItemsSource = period[30];
                list_period33.ItemsSource = period[31];
                list_period34.ItemsSource = period[32];
                list_period35.ItemsSource = period[33];
                list_period36.ItemsSource = period[34];
                list_period37.ItemsSource = period[35];
                list_period38.ItemsSource = period[36];
                list_period39.ItemsSource = period[37];
                list_period40.ItemsSource = period[38];
                list_period41.ItemsSource = period[39];
                list_period42.ItemsSource = period[40];
                list_period43.ItemsSource = period[41];
                list_period44.ItemsSource = period[42];
                list_period47.ItemsSource = period[43];
                list_period48.ItemsSource = period[44];
                list_period49.ItemsSource = period[45];




            }
            catch (Exception)
            {
                List<string> empty = new List<string>
                {
                      " empty",  " empty",  " empty",

                };
                list_period43.ItemsSource = empty;
                list_period44.ItemsSource = empty;
                list_period47.ItemsSource = empty;
                list_period48.ItemsSource = empty;
                list_period49.ItemsSource = empty;

            }


            //list_period3.ItemsSource = period[2];
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new TimetableWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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

            UIElement visual = ((Panel)Content).Children[0] as UIElement;
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
                
                fixedPage.Children.Remove(visual1);
                ((Panel)Content).Children.Add(visual1);
                ((Panel)Content).Children.Add(visual);
                header1.Margin = new Thickness(5);
                table1.Margin = new Thickness(51, 38, 0, 0);

            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
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
    }

   
}
