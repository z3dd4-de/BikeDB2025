using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDB2024
{
    public enum ActivityType { Fahrrad, Auto, Laufen, Wandern, Klettern }

    public class StatisticRegistry
    {
        private readonly Dictionary<ActivityType, List<IStatistic>> _map = new();

        public void Register(ActivityType type, IStatistic stat)
        {
            if (!_map.TryGetValue(type, out var list))
            {
                list = new List<IStatistic>();
                _map[type] = list;
            }
            list.Add(stat);
        }

        public IEnumerable<IStatistic> GetFor(ActivityType type) => _map[type];
    }
}
