using GalaSoft.MvvmLight.Threading;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace TransportsNantais.Services
{
    public class NavigationService : INavigationService
    {
        private PhoneApplicationFrame _mainFrame;

        public event NavigatingCancelEventHandler Navigating;

        public void NavigateTo(string pageUri)
        {
            if (EnsureMainFrame())
            {
                DispatcherHelper.UIDispatcher.BeginInvoke(() => 
                {
                    _mainFrame.Navigate(new Uri(pageUri, UriKind.Relative));
                });
            }
        }

        public void NavigateTo(string pageUri, IDictionary<string, string> parameters)
        {
            if (EnsureMainFrame() == false)
            {
                return;
            }

            StringBuilder uriBuilder = new StringBuilder();
            uriBuilder.Append(pageUri);

            if (parameters != null && parameters.Count > 0)
            {
                uriBuilder.Append("?");
                bool prependAmp = false;
                foreach (KeyValuePair<string, string> parameterPair in parameters)
                {
                    if (prependAmp)
                    {
                        uriBuilder.Append("&");
                    }
                    uriBuilder.AppendFormat("{0}={1}", parameterPair.Key, parameterPair.Value);
                    prependAmp = true;
                }
            }

            pageUri = uriBuilder.ToString();

            DispatcherHelper.UIDispatcher.BeginInvoke(() =>
            {
                _mainFrame.Navigate(new Uri(pageUri, UriKind.Relative));
            });
        }

        public void GoBack()
        {
            if (EnsureMainFrame() && _mainFrame.CanGoBack)
            {
                DispatcherHelper.UIDispatcher.BeginInvoke(() =>
                {
                    _mainFrame.GoBack();
                });
            }
        }

        public void RemoveBackEntry()
        {
            if (_mainFrame.BackStack != null && _mainFrame.BackStack.Any())
            {
                DispatcherHelper.UIDispatcher.BeginInvoke(() =>
                {
                    _mainFrame.RemoveBackEntry();
                });
            }
        }

        private bool EnsureMainFrame()
        {
            if (_mainFrame != null)
            {
                return true;
            }

            _mainFrame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (_mainFrame != null)
            {
                // Could be null if the app runs inside a design tool
                _mainFrame.Navigating += (s, e) =>
                {
                    if (Navigating != null)
                    {
                        Navigating(s, e);
                    }
                };

                return true;
            }

            return false;
        }

    }
}
