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

        [PreferredConstructor]
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);


            if (ViewModelBase.IsInDesignModeStatic || UseDesignTimeData)
            {
                return;
            }

            SimpleIoc.Default.Register<EstablishmentEventListViewModel>();
            SimpleIoc.Default.Register<EstablishmentViewModel>();
            SimpleIoc.Default.Register<EstablishmentListViewModel>();
            SimpleIoc.Default.Register<PromotionListViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<RegisterViewModel>();
            SimpleIoc.Default.Register<RegisterEstablishmentViewModel>();
            SimpleIoc.Default.Register<EstablishmentEventViewModel>();
            SimpleIoc.Default.Register<PromotionViewModel>();
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EstablishmentEventListViewModel EventList {
            get { return ServiceLocator.Current.GetInstance<EstablishmentEventListViewModel>(); }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EstablishmentViewModel Establishment {
            get { return ServiceLocator.Current.GetInstance<EstablishmentViewModel>(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EstablishmentListViewModel EstablishmentList {
            get { return ServiceLocator.Current.GetInstance<EstablishmentListViewModel>(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PromotionListViewModel PromotionList {
            get { return ServiceLocator.Current.GetInstance<PromotionListViewModel>(); }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
             Justification = "This non-static member is needed for data binding purposes.")]
        public LoginViewModel LoginViewModel {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public RegisterViewModel RegisterViewModel {
            get { return ServiceLocator.Current.GetInstance<RegisterViewModel>(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public RegisterEstablishmentViewModel RegisterEstablishmentViewModel {
            get { return ServiceLocator.Current.GetInstance<RegisterEstablishmentViewModel>(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EstablishmentEventViewModel Event {
            get { return ServiceLocator.Current.GetInstance<EstablishmentEventViewModel>(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PromotionViewModel Promotion {
            get { return ServiceLocator.Current.GetInstance<PromotionViewModel>(); }
        }
    }
}
