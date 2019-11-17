using Barometer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.ComponentModel;
using Windows.UI.Notifications;

namespace BarometricApp
{
    public sealed partial class MainPage : Page
    {
        string FileContent { get; set; }
        List<Coordinate> Coordinates = new List<Coordinate>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        void OnClick_GenerateChartAndForecast(object sender, RoutedEventArgs e)
        {
            CalculateChartData();
            var forecast = CalculateForcast();
            ShowToastNotification("Forecast", forecast);

        }

        private void ShowToastNotification(string title, string stringContent)
        {
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(stringContent));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml)
            {
                ExpirationTime = DateTime.Now.AddSeconds(4)
            };
            ToastNotifier.Show(toast);
        }

        async void OnClick_ChooseFile(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            filePicker.FileTypeFilter.Add(".txt");

            StorageFile file = await filePicker.PickSingleFileAsync();

            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                using (StreamReader reader = new StreamReader(stream.AsStream()))
                {
                    FileContent = reader.ReadToEnd();
                }
            }
        }
        async void OnClick_ChooseFolder(object sender, RoutedEventArgs e)
        {
            var folderPicker = new FolderPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            folderPicker.FileTypeFilter.Add(".txt");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                string folderContent = string.Empty;

                Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                var files = await folder.GetFilesAsync();

                foreach (var file in files)
                {
                    var stream = await file.OpenAsync(FileAccessMode.Read);
                    using (StreamReader reader = new StreamReader(stream.AsStream()))
                    {
                        if (folderContent == string.Empty)
                        {
                            folderContent = reader.ReadToEnd();
                        }
                        else
                        {
                            string.Concat(folderContent, reader.ReadToEnd());
                        }
                    }
                }
                FileContent = folderContent;
            }
        }

        void CalculateChartData()
        {
            BarometricDataInput bdi = new BarometricDataInput
            {
                InputData = FileContent
            };
            bdi.InitializeStringReader();
            bdi.ParseData();

            BarometricCoordinates bc = new BarometricCoordinates();

            //Test purpose
            var start = new DateTime(2012, 01, 01, 00, 02, 14);
            var end = new DateTime(2012, 01, 03, 00, 02, 14);

            bc.CalculateCoordinates(start, end, bdi.Data);
            Coordinates = bc.Coordinates;

            (lineChart.Series[0] as LineSeries).ItemsSource = Coordinates;
        }

        string CalculateForcast()
        {
            var slope = DataAnalyzer.CalculateSlope((Coordinates[0].X, Coordinates[0].Y), (Coordinates[Coordinates.Count - 1].X, Coordinates[Coordinates.Count - 1].Y));

            if (slope > 0)
            {
                return "Looks like good weather is comming.";
            }
            else if (slope < 0)
            {
                return "Looks like bad weather is comming.";
            }
            else
            {
                return "Maybe good, maybe bad.";
            }
        }
    }
}
