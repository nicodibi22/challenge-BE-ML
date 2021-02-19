using challenge_servicios.implementaciones;
using challenge_servicios.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_servicios.servicios
{
    public interface Trilateration
    {
        Point GetCoordinate(Point point1, Point point2, Point point3,
            double distance1, double distance2, double distance3);
    }
}
