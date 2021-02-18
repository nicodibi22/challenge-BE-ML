using challenge_servicios.implementaciones;
using challenge_servicios.servicios;
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
        public void Test1()
        {
            Trilateration trilateration = new Trilateration2D();
            Point p1 = new Point() { X = 4.0, Y = 4.0 };
            Point p2 = new Point() { X = 9.0, Y = 7.0 };
            Point p3 = new Point() { X = 9.0, Y = 1.0 };
            Point p4 = trilateration.GetCoordinate(p1, p2, p3, 4, 3, 3.25);
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            Trilateration trilateration = new Trilateration2D();
            Point p1 = new Point() { X = 39.0, Y = 28.0 };
            Point p2 = new Point() { X = 13.0, Y = 39.0 };
            Point p3 = new Point() { X = 16.0, Y = 40.0 };
            Point p4 = trilateration.GetCoordinate(p1, p2, p3, 8, 11, 8);
            Assert.Pass();
        }

        [Test]
        public void Test3()
        {
            Trilateration trilateration = new Trilateration2D();
            Point p1 = new Point() { X = 14.0, Y = 45.0 };
            Point p2 = new Point() { X = 80.0, Y = 70.0 };
            Point p3 = new Point() { X = 71.0, Y = 50.0 };
            Point p4 = trilateration.GetCoordinate(p1, p2, p3, 39.0, 50.0, 29.0);
            
            Assert.Pass();
        }

        [Test]
        public void Test4()
        {
            Trilateration trilateration = new Trilateration2D();
            Point p1 = new Point() { X = 0.0, Y = 2.0 };
            Point p2 = new Point() { X = -2.0, Y = 0.0 };
            Point p3 = new Point() { X = 2.0, Y = 0.0 };
            Point p4 = trilateration.GetCoordinate(p1, p2, p3, 2.0, 2.0, 2.0);
            Assert.AreEqual(p4.X, 0, 0.00001);
            Assert.AreEqual(p4.Y, 0, 0.00001);
            Assert.Pass();
        }
    }
}