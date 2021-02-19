using challenge_be_ml.Models;
using challenge_be_ml.Utils;
using challenge_servicios.servicios;
using challenge_servicios.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Satellite = challenge_be_ml.Models.Satellite;

namespace challenge_be_ml
{
    [Route("v1")]
    public class TopSecretController : ControllerBase
    {
        private MessageGenerator _messageGenerator;
        private Locator _locator;
        private AppSettings _appSettings;

        public TopSecretController(Locator locator, MessageGenerator messageGenerator,
            IOptions<AppSettings> appSettings)
        {
            this._locator = locator;
            this._messageGenerator = messageGenerator;
            this._appSettings = appSettings.Value;
        }

        [HttpPost("topsecret")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]        
        public async Task<IActionResult> TopSecretAllSatellites([FromBody]ListSatellites satellites)
        {
            try
            {
                PointFloat position = _locator.GetLocation(satellites.satellites.Select(s => s.distance).ToArray());
                string message = _messageGenerator.GetMessage(satellites.satellites.Select(s => s.message).ToArray());
                return Ok(new { position = new { x = position.X, y = position.Y }, message = message });
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return StatusCode(int.Parse(HttpStatusCode.InternalServerError.ToString()), "Ha ocurrido un error al procesar la petición");
            }

        }

        [HttpPost("topsecret_split/{satellite_name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<IActionResult> TopSecretSatelliteName(string satellite_name, [FromBody]Models.Satellite satellite)
        {
            
            if (!_appSettings.satellites.Exists(s => s.Name.ToLower().Equals(satellite_name.ToLower())))
                return BadRequest("La información proporcionada es incorrecta. Satélite: " + satellite_name);
            var satelliteData = JsonConvert.SerializeObject(satellite);
            HttpContext.Session.SetString(satellite_name.ToLower(), satelliteData);
            return Ok(HttpContext.Session);           

        }

        [HttpGet("topsecret_split")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> TopSecret_Split()
        {
            try
            {
                _appSettings.satellites.ForEach(s => { if (HttpContext.Session.GetString(s.Name.ToLower()) == null) throw new NullReferenceException("No hay suficiente información "); });
                Satellite satellite1 = JsonConvert.DeserializeObject<Satellite>(HttpContext.Session.GetString(_appSettings.satellites[0].Name.ToLower()));
                Satellite satellite2 = JsonConvert.DeserializeObject<Satellite>(HttpContext.Session.GetString(_appSettings.satellites[1].Name.ToLower()));
                Satellite satellite3 = JsonConvert.DeserializeObject<Satellite>(HttpContext.Session.GetString(_appSettings.satellites[2].Name.ToLower()));
                PointFloat position = _locator.GetLocation(new float[] { satellite1.distance, satellite2.distance, satellite3.distance });
                string message = _messageGenerator.GetMessage(satellite1.message, satellite2.message, satellite3.message);
                return Ok(new { position = new { x = position.X, y = position.Y }, message = message });
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                //_logger.LogError(ex.Message);
                return StatusCode(int.Parse(HttpStatusCode.InternalServerError.ToString()), "Ha ocurrido un error al procesar la petición");
            }

        }


    }
}
