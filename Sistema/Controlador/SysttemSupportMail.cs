using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    class SysttemSupportMail: MasterMailServer
    {
        public SysttemSupportMail()
        {
            senderMail = "noreplysoportecbr@gmail.com";
            password = "haqdhamhxdusspxo";
            host = "smtp.gmail.com";
            port = 587;
            ssl = true;
            initializeSmtpClient();
        }
    }
}
