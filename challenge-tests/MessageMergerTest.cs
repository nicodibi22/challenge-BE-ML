using challenge_servicios.implementaciones;
using challenge_servicios.servicios;
using NUnit.Framework;
using System;

namespace challenge_tests
{
    public class MessageMergerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MensajesConDesfasaje_MensajesNoVaciosTest()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator(new SimpleArrayStringMerge());
            string result = messageGenerator.GetMessage(new string[] { "1", "" }, new string[] { "", "1", "" }, new string[] { "", "", "2" });
            Assert.AreEqual("1 2", result);
        }

        [Test]
        public void MensajesSinDesfasaje_MensajesNoVaciosTest()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator(new SimpleArrayStringMerge());
            string result = messageGenerator.GetMessage(new string[] { "este", "", "", "mensaje", ""}, new string[] { "", "es", "", "", "secreto" }, new string[] { "este", "", "un", "", "" });
            Assert.AreEqual("este es un mensaje secreto", result);
        }

        [Test]
        public void MensajesConDesfasaje_UnMensajesVaciosTest()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator(new SimpleArrayStringMerge());
            string result = messageGenerator.GetMessage(new string[] { "1", "" }, new string[] { "", "1", "" }, new string[] { "", "", "" });
            Assert.AreEqual("1 1", result);
        }

        [Test]
        public void MensajesSinDesfasaje_UnMensajeVaciosTest()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator(new SimpleArrayStringMerge());
            string result = messageGenerator.GetMessage(new string[] { "este", "", "", "mensaje", "" }, new string[] { "", "es", "", "", "secreto" }, new string[] { "", "", "", "", "" });
            Assert.AreEqual("este es  mensaje secreto", result);
        }
    }
}