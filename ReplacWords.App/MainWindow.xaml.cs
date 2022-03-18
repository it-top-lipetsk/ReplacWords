using System;
using System.Threading;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using ReplacWords.Lib;

namespace ReplacWords.App
{
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellation;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OpenDirectory_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.ShowDialog();
            Input_Directory.Text = dialog.FileName;
        }

        private async void Button_Start_OnClick(object sender, RoutedEventArgs e)
        {
            Button_Stop.IsEnabled = true;

            var search = new Search(Input_Directory.Text);
            search.forbiddenWords = Input_BadWords.Text.Split(' ');

            try
            {
                _cancellation = new CancellationTokenSource();
                _cancellation.Token.Register(() => MessageBox.Show("Отмена поиска"));

                var progress = new Progress<int>(i => ProgressBar_Process.Value = i);
                await search.StartSearchAsync(progress, _cancellation.Token);
            }
            catch
            {
                //
            }
            finally
            {
                _cancellation.Dispose();
            }
        }

        private void Button_Stop_OnClick(object sender, RoutedEventArgs e)
        {
            Button_Stop.IsEnabled = false;
            
            _cancellation.Cancel();
        }
    }
}