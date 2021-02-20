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
        public void MensajesSinDesfasaje_MensajesNoVacios_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            string result = messageGenerator.GetMessage(new string[] { "este", "", "", "mensaje", ""}, new string[] { "", "es", "", "", "secreto" }, new string[] { "este", "", "un", "", "" });
            Assert.AreEqual("este es un mensaje secreto", result);
        }

        [Test]
        public void MensajesConDesfasajeUnicaPalabra_UnMensajeVacios_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            string result = messageGenerator.GetMessage(new string[] { "1", "" }, new string[] { "", "1", "" }, new string[] { "", "", "" });
            Assert.AreEqual("1 ", result);
        }

        [Test]
        public void MensajesSinDesfasaje_UnMensajeVacio_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            string result = messageGenerator.GetMessage(new string[] { "este", "", "un", "mensaje", "" }, new string[] { "", "es", "", "", "secreto" }, new string[] { "", "", "", "", "" });
            Assert.AreEqual("este es un mensaje secreto", result);
        }

        [Test]
        public void MensajesConDesfasaje_MensajesNoVacios_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            string result = messageGenerator.GetMessage(new string[] { "", "este", "es", "un", "mensaje" }, new string[] { "este", "", "un", "mensaje" }, new string[] { "", "", "es", "", "mensaje" });
            Assert.AreEqual("este es un mensaje", result);
        }

        [Test]
        public void MensajesNulos_ArgumentNullException_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            Assert.Throws<ArgumentNullException>(() => messageGenerator.GetMessage(null));            
        }

        [Test]
        public void MenosMensajesQueSatelistes_ArgumentException_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            Assert.Throws<ArgumentException>(() => messageGenerator.GetMessage(new string[] { "m1" }));
        }

        [Test]
        public void MasMensajesQueSatelistes_ArgumentException_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            Assert.Throws<ArgumentException>(() => messageGenerator.GetMessage(new string[] { "m1" }, new string[] { "m2" }, new string[] { "m3" }, new string[] { "m4" }));
        }

        [Test]
        public void ArreglosConTamaniosInvalidos_ArgumentException_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            Assert.Throws<ArgumentException>(() => messageGenerator.GetMessage(new string[] { "m1" }, new string[] { "m2", "mensaje" }, new string[] { "m3", "prueba", "mensaje" }));
        }

        [Test]
        public void ArreglosConMasDeUnaPosicionDeDiferencia_ArgumentException_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            Assert.Throws<ArgumentException>(() => messageGenerator.GetMessage(new string[] { "m1" }, new string[] { "m2", "", "mensaje" }, new string[] { "m3", "prueba", "mensaje" }));
        }

        [Test]
        public void MensajesSinDesfasaje_UnIndiceVacioTodosMensajes_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            string result = messageGenerator.GetMessage(new string[] { "este", "", "", "mensaje", "" }, new string[] { "", "es", "", "", "secreto" }, new string[] { "", "es", "", "mensaje", "" });
            Assert.AreEqual("este es  mensaje secreto", result);
        }

        [Test]
        public void DosMensajesConDesfasaje_MensajesCompletos_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            string result = messageGenerator.GetMessage(new string[] { "", "este", "es", "un", "mensaje" }, new string[] { "este", "es", "un", "mensaje" }, new string[] { "", "este", "es", "un", "mensaje" });
            Assert.AreEqual("este es un mensaje", result);
        }

        [Test]
        public void TresMensajesConDesfasaje_MensajesCompletos_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            string result = messageGenerator.GetMessage(new string[] { "", "este", "es", "un", "mensaje" }, new string[] { "este", "es", "un", "mensaje", "" }, new string[] { "", "este", "es", "un", "mensaje" });
            Assert.AreEqual("este es un mensaje", result);
        }

        [Test]
        public void DosMensajesConDesfasaje_SoloPalabrasRepetidas_Test()
        {
            MessageGenerator messageGenerator = new SatellitesMessageGenerator();
            string result = messageGenerator.GetMessage(new string[] { "", "este", "este" }, new string[] { "este", "este" }, new string[] { "este", "", "" });
            Assert.AreEqual("este este", result);
        }
    }
}