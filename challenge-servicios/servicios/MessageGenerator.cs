using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_servicios.servicios
{
    public interface MessageGenerator
    {
        string GetMessage(params string[][] messages);
    }
}
