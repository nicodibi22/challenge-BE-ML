using challenge_servicios.servicios;
using challenge_servicios.Utils;
using System;

namespace challenge_servicios
{
    public class SpaceshipLocator : Locator
    {
        Trilateration _trilateration;
        public SpaceshipLocator(Trilateration trilateration)
        {
            this._trilateration = trilateration;
        }
        public Point GetLocation(params float[] distances)
        {
            if (distances.Length != 3)
                throw new ArgumentException("La cantidad de distancias obtenidas no es la esperada para calcular la posición. Cantidad distancias recibidas: " + distances.Length);
            return _trilateration.GetCoordinate(new Point() { X = -500, Y = -200 }, new Point() { X = 100, Y = -100}, 
                new Point() { X = 500, Y = 100 }, distances[0], distances[1], distances[3]);

        }
    }
}
