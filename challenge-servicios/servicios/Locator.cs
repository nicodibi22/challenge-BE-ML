using challenge_servicios.implementaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_servicios.servicios
{
    public interface Locator
    {
        Point GetLocation(params float[] distances);
    }
}
