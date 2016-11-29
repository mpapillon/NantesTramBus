using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TransportsNantais.Services
{
    public class ApplicationBarService : IApplicationBarService
    {
        public IApplicationBar ApplicationBar
        {
            get
            {
                PhoneApplicationPage currentPage = App.RootFrame.Content as PhoneApplicationPage;

                if (currentPage.ApplicationBar == null)
                {
                    currentPage.ApplicationBar = new ApplicationBar();
                }
                return currentPage.ApplicationBar;
            }
        }

        public void AddButton(string title, Uri imageUrl, Action OnClick)
        {
            ApplicationBarIconButton newButton = new ApplicationBarIconButton()
            {
                Text = title,
                IconUri = imageUrl,
            };
            newButton.Click += ((sender, e) => { OnClick.Invoke(); });

            ApplicationBar.Buttons.Add(newButton);
        }
    }
}
