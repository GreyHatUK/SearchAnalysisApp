using SearchAnalysisApp.UserControls;
using SearchSiteManager.Classes;
using SearchSiteManager.Dto;
using SearchSiteManager.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace SearchAnalysisApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ISearchEngine> SearchEngines;
        private BackgroundWorker worker;
        private int SearchCount = 10;
        private List<SearchResultViewDto> results;
        public MainWindow()
        {
            InitializeComponent();
            SearchEngines = new List<ISearchEngine>();
            SearchEngines.Add(new GoogleSearch());
            SearchEngines.Add(new BingSearch());
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += SearchMethod;
            worker.ProgressChanged += SearchProgressChanged;
            worker.RunWorkerCompleted += SearchEnded;
            loadingControl.Visibility = Visibility.Collapsed;
        }

        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            foreach (var searchEngine in SearchEngines)
            {
                searchEngine.StopSearch();
            }
            worker.CancelAsync();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //low on time


        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {       
                worker.RunWorkerAsync(txtSearchTerm.Text);    
            }
            catch(Exception ex)
            {

            }
        }

        private void SearchEnded(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                btnSearch.IsEnabled = true;
                btnCancel.IsEnabled = false;
                loadingControl.Visibility = Visibility.Collapsed;
            });

            if (e.Cancelled)
            {
            }
            else
            {
                foreach(var result in results)
                {
                    spResultList.Children.Add(new ResultItem(result));
                }
            }
        }

        private void SearchMethod(object sender, DoWorkEventArgs e)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    btnSearch.IsEnabled = false;
                    btnCancel.IsEnabled = true;
                    loadingControl.Visibility = Visibility.Visible;
                });
                results = new List<SearchResultViewDto>();
                foreach (var searchEngine in SearchEngines)
                {
                    if (!worker.CancellationPending)
                    {
                        results.AddRange(searchEngine.GetSearchResultListAsync(e.Argument.ToString(), SearchCount).Result);
                    }
                }

                

            }
            catch(Exception ex)
            {

            }
        }

        private void SearchProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          //
        }
    }
}
