using challenge_servicios;
using challenge_servicios.implementaciones;
using challenge_servicios.servicios;
using challenge_servicios.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_tests
{
    public class LocatorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MensajesConDesfasaje_MensajesNoVaciosTest()
        {
            Locator locator = new SpaceshipLocator(new Trilateration2D());
            Point result = locator.GetLocation(null);
            Assert.Pass();
        }
    }
}
