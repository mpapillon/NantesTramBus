using GalaSoft.MvvmLight;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsNantais.Bases
{
    public class ApplicationViewModelBase : ViewModelBase
    {
        public virtual void OnPageNavigatedTo(IDictionary<string, string> parameters)
        {
            // Override this in any of the ViewModel to hook to the OnNavigatedTo event on the page
        }

        public virtual void OnNavigatingFrom()
        {
            // Override this in any of the ViewModel to hook to the OnNavigatingFrom event on the page
        }
    }
}
