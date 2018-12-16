using GalaSoft.MvvmLight;
using SWApps2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.ViewModel
{
    public class EstablishmentListViewItemViewModel : ViewModelBase
    {
        private bool _isMenuOpen;
        public Establishment Establishment { get; set; }

        public bool IsMenuOpen {
            get { return _isMenuOpen; }
            set {
                if (CanShowMenu)
                {
                    if (_isMenuOpen != value) _isMenuOpen = value;
                }
                else
                {
                    _isMenuOpen = false;
                }
            }
        }

        public string Address { get { return Establishment.Address.ToString(); } }
        public string Name { get { return Establishment.Name; } }

        public bool CanShowMenu { get;  set; }

        public EstablishmentListViewItemViewModel() { }

    }
}
