using challenge_servicios.implementaciones;
using challenge_servicios.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_servicios.servicios
{
    public interface Trilateration
    {
        /// <summary>
        /// Obtiene un punto a través de tres puntos conocidos y la distancia de estos hacia el punto a obtener
        /// </summary>
        /// <param name="point1">Coordenada del punto 1</param>
        /// <param name="point2">Coordenada del punto 2</param>
        /// <param name="point3">Coordenada del punto 3</param>
        /// <param name="distance1">Distancia del punto 1 al punto a encontrar</param>
        /// <param name="distance2">Distancia del punto 2 al punto a encontrar</param>
        /// <param name="distance3">Distancia del punto 3 al punto a encontrar</param>
        /// <returns>Punto a encontrar</returns>
        PointDouble GetCoordinate(PointDouble point1, PointDouble point2, PointDouble point3,
            double distance1, double distance2, double distance3);
    }
}
