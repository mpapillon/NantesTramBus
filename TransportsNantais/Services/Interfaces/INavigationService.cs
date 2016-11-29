using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace TransportsNantais.Services
{
    public interface INavigationService
    {
        event NavigatingCancelEventHandler Navigating;
        void NavigateTo(string pageUri);
        void NavigateTo(string pageUri, IDictionary<string, string> parameters);
        void RemoveBackEntry();
        void GoBack();
    }
}
