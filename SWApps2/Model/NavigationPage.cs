﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    public interface NavigationPage
    {
        void Navigate(string pageName, object Parameters);
    }
}