using challenge_be_ml.Utils;
using challenge_servicios.servicios;
using challenge_servicios.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace challenge_servicios
{
    public class SpaceshipLocator : Locator
    {
        private Trilateration _trilateration;
        private AppSettings _appSettings;
        public SpaceshipLocator(Trilateration trilateration, IOptions<AppSettings> appSettings)
        {
            this._trilateration = trilateration;
            this._appSettings = appSettings.Value;
        }

        /// <summary>
        /// Obtiene la posición de una nave mediante la distancia a cada uno de los 3 satélites a la misma.
        /// </summary>
        /// <param name="distances">Distancias de cada uno de los 3 satélites a la nave a localizar.</param>
        /// <returns>Coordenada de la nave a localizar.</returns>
        /// <exception cref="System.ArgumentException">La cantidad de distancias no coincide con la cantidad de satélites de triangulación</exception>
        public PointFloat GetLocation(params float[] distances)
        {
            if (distances.Length != _appSettings.satellites.Count)
                throw new ArgumentException("La cantidad de distancias obtenidas no es la esperada para calcular la posición. Cantidad distancias recibidas: " + distances.Length);
            PointDouble pointResult = _trilateration.GetCoordinate(new PointDouble() { X = _appSettings.satellites[0].CoordinateX, Y = _appSettings.satellites[0].CoordinateY }, 
                new PointDouble() { X = _appSettings.satellites[1].CoordinateX, Y = _appSettings.satellites[1].CoordinateY }, 
                new PointDouble() { X = _appSettings.satellites[2].CoordinateX, Y = _appSettings.satellites[2].CoordinateY },
                distances[0], distances[1], distances[2]);
            return new PointFloat() { X = Convert.ToSingle(pointResult.X), Y = Convert.ToSingle(pointResult.Y) };
        }
    }
}
