using Microsoft.Win32;
using SearchSiteManager.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SearchAnalysisApp.UserControls
{
    /// <summary>
    /// Interaction logic for ResultItem.xaml
    /// </summary>
    public partial class ResultItem : UserControl
    {

        private SearchResultViewDto SearchResult;
        public ResultItem(SearchResultViewDto searchResult)
        {
            InitializeComponent();
            SearchResult = searchResult;
            lblText.Content = searchResult.ResultText;
            lblTitle.Content = searchResult.ResultTitle;
          
        }

        private void btnViewURL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LinkViewPopup linkView = new LinkViewPopup(SearchResult.LinkURL);
                linkView.Show();
                //var process = new Process();

                //process.StartInfo.FileName = GetBrowserPath();
                //process.StartInfo.UseShellExecute = false;
                //process.StartInfo.Arguments = SearchResult.LinkURL;
                //process.StartInfo.CreateNoWindow = true;
                //process.Start();
            }
            catch(Exception ex)
            {

            }
        }

        private string GetBrowserPath()
        {
            string browserName = "iexplore.exe";
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\shell\\Associations\\UrlAssociations\\http\\UserChoice", false);

            string progIdValue = regkey.GetValue("Progid").ToString();

            if (progIdValue.ToString().ToLower().Contains("chrome"))
                browserName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            else if (progIdValue.ToString().ToLower().Contains("firefox"))
                browserName = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            else if (progIdValue.ToString().ToLower().Contains("safari"))
                browserName = "safari.exe";
            else if (progIdValue.ToString().ToLower().Contains("opera"))
                browserName = "opera.exe";

            return browserName;
        }
    }
}
