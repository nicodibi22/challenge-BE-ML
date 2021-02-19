using challenge_servicios.servicios;
using challenge_servicios.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_servicios.implementaciones
{
    public class Trilateration2D : Trilateration
    {
        public Point GetCoordinate(Point point1, Point point2, Point point3, double distance1, double distance2, double distance3)
        {
            Point resultPose = new Point();
            //unit vector in a direction from point1 to point 2
            double p2p1Distance = Math.Pow(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2), 0.5);
            Point ex = new Point() { X = (point2.X - point1.X) / p2p1Distance, Y = (point2.Y - point1.Y) / p2p1Distance };
            Point aux = new Point() { X = point3.X - point1.X, Y = point3.Y - point1.Y };
            //signed magnitude of the x component
            double i = ex.X * aux.X + ex.Y * aux.Y;
            //the unit vector in the y direction. 
            Point aux2 = new Point() { X = point3.X - point1.X - i * ex.X, Y = point3.Y - point1.Y - i * ex.Y };
            Point ey = new Point() { X = aux2.X / norm(aux2), Y = aux2.Y / norm(aux2) };
            //the signed magnitude of the y component
            double j = ey.X * aux.X + ey.Y * aux.Y;
            //coordinates
            double x = (Math.Pow(distance1, 2) - Math.Pow(distance2, 2) + Math.Pow(p2p1Distance, 2)) / (2 * p2p1Distance);
            double y = (Math.Pow(distance1, 2) - Math.Pow(distance3, 2) + Math.Pow(i, 2) + Math.Pow(j, 2)) / (2 * j) - i * x / j;
            //result coordinates
            double finalX = point1.X + x * ex.X + y * ey.X;
            double finalY = point1.Y + x * ex.Y + y * ey.Y;
            resultPose.X = finalX;
            resultPose.Y = finalY;
            return resultPose;
        }

        private double norm(Point p) // get the norm of a vector
        {
            return Math.Pow(Math.Pow(p.X, 2) + Math.Pow(p.Y, 2), .5);
        }
    }
}
