using challenge_servicios.implementaciones;
using challenge_servicios.servicios;
using challenge_servicios.Utils;
using NUnit.Framework;
using System;

namespace challenge_tests
{
    public class TrilaterationTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TrilateracionCoordenadasYDistanciasValidasTest()
        {
            Trilateration trilateration = new Trilateration2D();
            PointDouble p1 = new PointDouble() { X = 14.0, Y = 45.0 };
            PointDouble p2 = new PointDouble() { X = 80.0, Y = 70.0 };
            PointDouble p3 = new PointDouble() { X = 71.0, Y = 50.0 };
            PointDouble p4 = trilateration.GetCoordinate(p1, p2, p3, 39.0, 50.0, 29.0);            
            Assert.AreEqual(p4.X, 50, 0.000001);
            Assert.AreEqual(p4.Y, 30, 0.000001);
        }

        [Test]
        public void TrilateracionPuntoDestinoOrigenTest()
        {
            Trilateration trilateration = new Trilateration2D();
            PointDouble p1 = new PointDouble() { X = 0.0, Y = 2.0 };
            PointDouble p2 = new PointDouble() { X = -2.0, Y = 0.0 };
            PointDouble p3 = new PointDouble() { X = 2.0, Y = 0.0 };
            PointDouble p4 = trilateration.GetCoordinate(p1, p2, p3, 2.0, 2.0, 2.0);
            Assert.AreEqual(p4.X, 0, 0.00001);
            Assert.AreEqual(p4.Y, 0, 0.00001);
        }

    }
}