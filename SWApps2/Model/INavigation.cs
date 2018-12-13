using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// This interface allows page to page navigation
    /// </summary>
    public interface INavigation
    {
        /// <summary>
        /// Navigate to a page
        /// </summary>
        /// <param name="pageName">The name of the destination page</param>
        /// <param name="Parameters">Any parameters that should be passed along with the navigation</param>
        void Navigate(string pageName, object Parameters);
        void UpdateView();
    }
}
