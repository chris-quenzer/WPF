using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;

namespace WpfTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region On Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // get every logical drive on machine
            foreach (var drive in Directory.GetLogicalDrives())
            {

                // create new item for it
                var item = new TreeViewItem()
                {
                    // set the header and path
                    Header = drive,
                    // and the full path
                    Tag = drive
                };

                

                // add a dummy item
                item.Items.Add(null);

                // listen out for item being expanded
                item.Expanded += Folder_Expanded;

                // add it to the main tree-view
                FolderView.Items.Add(item);
            }
        }
        #endregion

        #region Folder Expanded
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial checks
            var item = (TreeViewItem)sender;

            // if the item only contains dummy data
            if(item.Items.Count != 1 || item.Items[0] != null)
            {
                return;
            }

            // clear dummy data
            item.Items.Clear();
            // get full path
            var fullPath = (string)item.Tag;
            #endregion

            #region Get Folders
            // create blank list for directories
            var directories = new List<string>();

            // try and get dirrectories from the folder
            // ignoring any issues doing so
            try // <-- usually bad practice, rarely do this
            {
                var dirs = Directory.GetDirectories(fullPath);

                if(dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }
            }
            catch { }

            // for each directory...
            directories.ForEach(directoryPath =>
            {
                // create directory item
                var subItem = new TreeViewItem()
                {
                    // set header as folder name
                    Header = GetFileFolderName(directoryPath),
                    // add tag as full path
                    Tag = directoryPath
                };
                // add dummy item so we can expand folder
                subItem.Items.Add(null);
                // handle expanding
                subItem.Expanded += Folder_Expanded;

                item.Items.Add(subItem);
            });
            #endregion

            #region Get Files
            // create blank list for directories
            var files = new List<string>();

            // try and get dirrectories from the folder
            // ignoring any issues doing so
            try // <-- usually bad practice, rarely do this
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                {
                    files.AddRange(fs);
                }
            }
            catch { }

            // for each directory...
            directories.ForEach(filePath =>
            {
                // create directory item
                var subItem = new TreeViewItem()
                {
                    // set header as folder name
                    Header = GetFileFolderName(filePath),
                    // add tag as full path
                    Tag = filePath
                };

                item.Items.Add(subItem);
            });
            #endregion
        }
        #endregion

        #region Helpers
        /// <summary>
        /// When a folder is expanded, find the subfolders/files
        /// </summary>
        public static string GetFileFolderName(string path)
        {
            // C:\Something\a folder
            // C:\Something\a file.png
            // a file file.png

            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // make all slashes backslashes
            var normalizePath = path.Replace('/', '\\');
            // find the last backslash in the path
            var lastIndex = normalizePath.LastIndexOf('\\');

            // if we don't find a backslash, return the path itself
            if (lastIndex <= 0)
                return path;

            return path.Substring(lastIndex + 1);
        }
        #endregion
    }
}
