using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.ModelView
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            //if(ViewModelBase.IsInDesignModeStatic)
            //{
            //  //Create design time view services and models
            //  SimpleIoc.Default.Register<IDataService, DesignDataService>();
            //}
            //else{
            //  Create run time view services and models
            //  SimpleIoc.Default.Register<IDataService, DataService>();
            //}

            //Register ViewModels
            SimpleIoc.Default.Register<MainViewModel>();

            //More ViewModels...
           
        }

        //ViewModels as Properties
        public MainViewModel MainVM
        {
            get {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public static void Cleanup()
        {
            //TODO Clear ViewModels
        }
    }
}
