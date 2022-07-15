using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeRunners.Data {
    public class DataCore {
        private const string a2z = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_=#";
        private Random rand;
        public DataCore() {
            rand = new Random();
        }

        public string generateCode(int length) {
            string data = string.Empty;
            
            for(int i = 0; i <= length; i++) {
                data+= a2z[rand.Next(0, a2z.Length)];
            }
            return data;
        }
    }
}
