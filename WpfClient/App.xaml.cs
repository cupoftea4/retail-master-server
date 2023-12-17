using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic firstor App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HttpClient HttpClient { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5043/") // Replace with your API's base address
            };
            // Configure HttpClient further if needed (e.g., headers, timeout)
        }
        
        
    }
}