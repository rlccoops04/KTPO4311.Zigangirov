using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.Lib.src.LogAn
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}
