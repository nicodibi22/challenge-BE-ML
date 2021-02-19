using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_servicios.servicios
{
    public interface MessageGenerator
    {
        /// <summary>
        /// Devuelve un mensaje que forma mediante el merge de los mensajes recibidos por los satélites.
        /// </summary>
        /// <param name="messages">Mensajes recibidos por cada satélite. Cada mensaje corresponde a un arreglo de palabras.</param>
        /// <returns>Mensaje que se forma a partir de los mensajes recibidos.</returns>
        string GetMessage(params string[][] messages);
    }
}
