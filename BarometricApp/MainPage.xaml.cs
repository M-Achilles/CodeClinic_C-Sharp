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

namespace BarometricApp
{
    public sealed partial class MainPage : Page
    {
        public string FileContent { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
        }

        void OnClick_GenerateChart(object sender, RoutedEventArgs e)
        {
            CalculateChartData();
        }

        async void OnClick_ChooseFile(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker();
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            filePicker.SuggestedStartLocation = PickerLocationId.Desktop;
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
            var folderPicker = new FolderPicker();
            folderPicker.ViewMode = PickerViewMode.Thumbnail;
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
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
            BarometricDataInput bdi = new BarometricDataInput();
            bdi.InputData = FileContent;
            bdi.InitializeStringReader();
            bdi.ParseData();

            BarometricCoordinates bc = new BarometricCoordinates();

            //Test purpose
            var start = new DateTime(2012, 01, 01, 00, 02, 14);
            var end = new DateTime(2012, 01, 03, 00, 02, 14);

            bc.CalculateCoordinates(start, end, bdi.Data);

            (lineChart.Series[0] as LineSeries).ItemsSource = bc.Coordinates;
        }
    }
}
