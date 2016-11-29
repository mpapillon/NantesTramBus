using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsNantais.Services
{
    public interface IApplicationBarService
    {
        IApplicationBar ApplicationBar { get; }

        void AddButton(string title, Uri imageUrl, Action OnClick);
    }
}
