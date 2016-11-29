using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsNantais.Bases
{
    public abstract class ApplicationPageBase : PhoneApplicationPage
    {
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (this.DataContext != null)
                (this.DataContext as ApplicationViewModelBase).OnPageNavigatedTo(this.NavigationContext.QueryString);

        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            if (this.DataContext != null)
                (this.DataContext as ApplicationViewModelBase).OnNavigatingFrom();
        }
    }
}
