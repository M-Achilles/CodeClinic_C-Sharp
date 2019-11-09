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
            GetChartData();
        }

        async void OnClick_ChooseFile(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".txt");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                FileContent = FileImport.ImportFile(file.Path);
            }
        }
        async void OnClick_ChooseFolder(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FolderPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".txt");
            var files = await picker.PickSingleFolderAsync();
            if (files != null)
            {
                FileContent = FileImport.ImportFile(files.Path);
            }
        }

        void calculateChartData()
        {
            BarometricDataInput bdi = new BarometricDataInput();
            bdi.InputData = FileContent;
            bdi.InitializeStringReader();
            bdi.ParseData();

            BarometricCoordinates bc = new BarometricCoordinates();

            //Test purpose
            var start = new DateTime(2010, 01, 01, 00, 02, 14);
            var end = new DateTime(2019, 01, 01, 00, 02, 14);

            bc.CalculateCoordinates(start, end, bdi.Data);

            (lineChart.Series[0] as LineSeries).ItemsSource = bc.Coordinates;
        }

        void GetChartData()
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates()
            {
                X = "Test1",
                Y = 1
            });
            coordinates.Add(new Coordinates()
            {
                X = "Test2",
                Y = 5
            });
            coordinates.Add(new Coordinates()
            {
                X = "Test3",
                Y = 10
            });

            (lineChart.Series[0] as LineSeries).ItemsSource = coordinates;
        }
    }

    public class Coordinates
    {
        public string X { get; set; }
        public int Y { get; set; }
    }
}
