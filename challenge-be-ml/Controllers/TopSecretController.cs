using challenge_be_ml.Models;
using challenge_servicios.servicios;
using challenge_servicios.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace challenge_be_ml
{
    [Route("v1/topsecret")]
    public class TopSecretController : ControllerBase
    {
        private MessageGenerator _messageGenerator;
        private Locator _locator;

        public TopSecretController(Locator locator, MessageGenerator messageGenerator)
        {
            this._locator = locator;
            this._messageGenerator = messageGenerator;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> TopSecret([FromBody]ListSatellites satellites)
        {
            try
            {
                Point position = _locator.GetLocation(satellites.satellites.Select(s => s.distance).ToArray());
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
    }
}
