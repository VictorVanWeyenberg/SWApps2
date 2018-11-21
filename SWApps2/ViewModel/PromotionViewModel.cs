using SWApps2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.ViewModel
{
    public class PromotionViewModel
    {
        public Promotion _promotion;
        public PromotionViewModel(Promotion promotion)
        {
            this._promotion = promotion;
        }
        public Establishment Establishment { get { return this._promotion.Establishment; } }
        public string Name { get { return this._promotion.Name; } }
        public string Description { get { return this._promotion.Description; } }
        public string Date { get { return this._promotion.Start.ToString("dd/MM/yyyy"); } }
        public string Time {
            get {
                return new StringBuilder("From ").Append(_promotion.Start.ToString("HH:mm")).Append(" to ").Append(_promotion.End.ToString("HH:mm")).ToString();
            }
        }
    }
}
