using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeRunners.Data.Model {
    public class Authentication:CommonData {
        public string auth_token {
            get {
                return token.auth_token;
            }
        }
        public string refresh_token {
            get {
                return token.refresh_token;
            }
        }
        public int auth_expire {
            get {
                return token.auth_expire;
            }
        }
        public string auth_username {
            get {
                return token.username;
            }
        }
        public dynamic token { get; set; }
    }
}