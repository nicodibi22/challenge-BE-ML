using challenge_be_ml.Utils;
using challenge_servicios;
using challenge_servicios.implementaciones;
using challenge_servicios.servicios;
using challenge_servicios.Utils;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_tests
{
    public class LocatorTest
    {
        private  Mock<IOptions<AppSettings>> _settings;
        [SetUp]
        public void Setup()
        {
            _settings = new Mock<IOptions<AppSettings>>();
            _settings.SetupGet(s => s.Value).Returns(GetAppSettingsMock());
        }

        private AppSettings GetAppSettingsMock()
        {
            AppSettings settings = new AppSettings();
            settings.satellites = new List<Satellite>();
            settings.satellites.Add(new Satellite() { CoordinateX = 1.0F, CoordinateY = 1.0F });
            settings.satellites.Add(new Satellite() { CoordinateX = 1.0F, CoordinateY = 1.0F });
            settings.satellites.Add(new Satellite() { CoordinateX = 1.0F, CoordinateY = 1.0F });
            return settings;
        }

        [Test]
        public void MensajesConDesfasaje_MensajesNoVaciosTest()
        {            
            Locator locator = new SpaceshipLocator(new Trilateration2D(), _settings.Object);
            Assert.Throws<ArgumentException>(() => locator.GetLocation(new float[] { 2.0F }));
        }
    }
}
