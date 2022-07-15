using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeRunners.Data.Model {
    public class UsageData : CommonData {
        public IList<UsageD> data { get; set; }
    }

    public class ProgressData {
        public double progress { get; set; }
        public string direction { get; set; }
    }

    public class UsageD {
        public string usage_id { get; set; }
        public string device_id { get; set; }
        public string date_recorded { get; set; }
        public double price { get; set; }
        public string power_type { get; set; }
        public int units { get; set; }
        public ProgressData progress_data { get; set; }
    }
}
