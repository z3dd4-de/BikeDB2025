using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace BikeDB2024
{
    public interface IStatistic
    {
        string DisplayName { get; }          // "Gesamtkilometer pro Monat"
        SeriesChartType PreferredChartType { get; } // Bar, Line, Pie...
        //ChartData Compute(IEnumerable<Fahrt> fahrten);
    }
}
