﻿using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfTreeView
{
    /// <summary>
    /// Converts a full path to an image source
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // get the full path
            var path = (string)value;

            // if the path is null, ignore
            if (path == null)
                return null;

            // get the name of the file/folder
            var name = MainWindow.GetFileFolderName(path);

            // 

            // default set as file image
            var image = "Images/FileIcon.png";

            // if the name is blank, we presume it's a drive as we cannot have a blank file or folder name
            if (string.IsNullOrEmpty(name))
            {
                image = "Images/DriveIcon.png";
            }
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
            {
                image = "Images/DirIcon.png";
            }

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
