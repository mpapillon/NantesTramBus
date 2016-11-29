using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using TransportsNantais.Services;
using TransportsNantais.ViewModels;

namespace TransportsNantais
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<ITanRestService, Services.Design.TanRestServiceDesign>();
                //SimpleIoc.Default.Register<IDataBaseService, Services.Design.DataServiceDesign>();
            }
            else
            {
                SimpleIoc.Default.Register<INavigationService, NavigationService>();
                SimpleIoc.Default.Register<ITanRestService, TanRestService>();
                SimpleIoc.Default.Register<IDataBaseService, SQLiteService>();
                SimpleIoc.Default.Register<ISettingsService, PhoneSettingsService>();
                SimpleIoc.Default.Register<IApplicationBarService, ApplicationBarService>();
            }

            SimpleIoc.Default.Register<NearStopsViewModel>();
            SimpleIoc.Default.Register<LinesViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<LineDetailsViewModel>();
            SimpleIoc.Default.Register<StopDetailsViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public NearStopsViewModel NearStops
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NearStopsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public StopDetailsViewModel StopDetails
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StopDetailsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public LinesViewModel Lines
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LinesViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public LineDetailsViewModel LineDetails
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LineDetailsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SearchViewModel Search
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchViewModel>();
            }
        }
    }
}