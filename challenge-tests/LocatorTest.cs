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
            settings.satellites.Add(new Satellite() { CoordinateX = 1.0F, CoordinateY = 1.0F, Name = "Kenobi" });
            settings.satellites.Add(new Satellite() { CoordinateX = 1.0F, CoordinateY = 1.0F, Name = "Sato" });
            settings.satellites.Add(new Satellite() { CoordinateX = 1.0F, CoordinateY = 1.0F, Name = "Skywalker" });
            return settings;
        }

        [Test]
        public void ObtenerUbicacionConUnaDistancia_ArgumentExcepcion_Test()
        {            
            Locator locator = new SpaceshipLocator(new Trilateration2D(), _settings.Object);
            Assert.Throws<ArgumentException>(() => locator.GetLocation(new float[] { 2.0F }));
        }

        [Test]
        public void ObtenerUbicacionConCuatroDistancias_ArgumentExcepcion_Test()
        {
            Locator locator = new SpaceshipLocator(new Trilateration2D(), _settings.Object);
            Assert.Throws<ArgumentException>(() => locator.GetLocation(new float[] { 2.0F, 12.0F, 13.0F, 10.0F }));
        }

        [Test]
        public void ObtenerUbicacionConTresDistancias_Ok_Test()
        {
            Locator locator = new SpaceshipLocator(new Trilateration2D(), _settings.Object);
            PointFloat point = locator.GetLocation(new float[] { 10.0F, 10.0F, 10.0F });
            Assert.IsNotNull(point.X);
            Assert.IsNotNull(point.Y);
        }
    }
}
