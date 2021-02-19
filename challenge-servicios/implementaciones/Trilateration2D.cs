using challenge_servicios.servicios;
using challenge_servicios.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_servicios.implementaciones
{
    public class Trilateration2D : Trilateration
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
        public PointDouble GetCoordinate(PointDouble point1, PointDouble point2, PointDouble point3, double distance1, double distance2, double distance3)
        {
            PointDouble resultPoint = new PointDouble();

            // p2p1Distance = ‖P2 - P1‖
            double p2p1Distance = Math.Pow(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2), 0.5);

            // ex = (P2 - P1) / p2p1Distance
            PointDouble ex = new PointDouble() { X = (point2.X - point1.X) / p2p1Distance, Y = (point2.Y - point1.Y) / p2p1Distance };

            // i = ex(P3 - P1)
            PointDouble aux = new PointDouble() { X = point3.X - point1.X, Y = point3.Y - point1.Y };            
            double i = ex.X * aux.X + ex.Y * aux.Y;

            // ey = (P3 - P1 - i · ex) / ‖P3 - P1 - i · ex‖
            PointDouble aux2 = new PointDouble() { X = point3.X - point1.X - i * ex.X, Y = point3.Y - point1.Y - i * ex.Y };
            PointDouble ey = new PointDouble() { X = aux2.X / norm(aux2), Y = aux2.Y / norm(aux2) };

            // j = ey(P3 - P1)
            double j = ey.X * aux.X + ey.Y * aux.Y;

            // x = (r12 - r22 + 2 · p2p1Distance) / 2 · p2p1Distance
            double x = (Math.Pow(distance1, 2) - Math.Pow(distance2, 2) + Math.Pow(p2p1Distance, 2)) / (2 * p2p1Distance);
            // y = (r12 - r32 + i2 + j2) / 2j - ix / j
            double y = (Math.Pow(distance1, 2) - Math.Pow(distance3, 2) + Math.Pow(i, 2) + Math.Pow(j, 2)) / (2 * j) - i * x / j;
            
            // Resultado final
            double finalX = point1.X + x * ex.X + y * ey.X;
            double finalY = point1.Y + x * ex.Y + y * ey.Y;
            resultPoint.X = finalX;
            resultPoint.Y = finalY;
            return resultPoint;
        }

        /// <summary>
        /// Obtiene la norma de un vector ‖P‖ = sqrt((P.X)^2 + (P.Y)^2)
        /// </summary>
        /// <param name="point">Punto a obtener la norma</param>
        /// <returns>Norma de un vector</returns>
        private double norm(PointDouble point)
        {
            return Math.Pow(Math.Pow(point.X, 2) + Math.Pow(point.Y, 2), .5);
        }
    }
}
