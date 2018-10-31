using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.ViewModel
{
    public class ViewModelLocator
    {

        /// <summary>
        /// This property can be used to force the application to run with design time data.
        /// </summary>
        public static bool UseDesignTimeData
        {
            get
            {
                return false;
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);


            if (ViewModelBase.IsInDesignModeStatic || UseDesignTimeData)
            {
                
            }
            else
            {
                
            }

            SimpleIoc.Default.Register<EstablishmentListViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EstablishmentListViewModel EstablishmentListViewModel => ServiceLocator.Current.GetInstance<EstablishmentListViewModel>();
    }
}
