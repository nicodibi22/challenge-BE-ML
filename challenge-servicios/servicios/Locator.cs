using challenge_servicios.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_servicios.servicios
{
    public interface Locator
    {
        /// <summary>
        /// Obtiene la posición de una nave mediante la distancia a cada uno de los satélites a la misma.
        /// </summary>
        /// <param name="distances">Distancias de cada uno de los satélites a la nave a localizar.</param>
        /// <returns>Coordenada de la nave a localizar.</returns>
        /// <exception cref="System.ArgumentException">La cantidad de distancias no coincide con la cantidad de satélites</exception>
        PointFloat GetLocation(params float[] distances);
    }
}
