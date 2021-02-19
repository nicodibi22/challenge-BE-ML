using challenge_servicios.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace challenge_servicios.implementaciones
{
    public class StringMerge
    {
        /// <summary>
        /// Obtiene una matriz con todos los resultados de mergear dos arreglos.
        /// </summary>
        /// <param name="arrayMessage1">Mensaje 1 con arreglo de cadenas</param>
        /// <param name="arrayMessage2">Mensaje 2 con arreglo de cadenas</param>
        /// <param name="length">Tamaño del arreglo resultante de mergear</param>
        /// <returns>Matriz con todos los posibles arreglos mergeados</returns>
        public static string[][] MatrixStringMerge(string[] arrayMessage1, string[] arrayMessage2, int length)
        {

            var result = new List<string[]>();
            for (int i = 0; i <= arrayMessage1.Length - length; i++)
            {
                for (int j = 0; j <= arrayMessage2.Length - length; j++)
                {
                    result.Add(ArrayStringMerge(arrayMessage1.Skip(i).Take(length).ToArray(), arrayMessage2.Skip(j).Take(length).ToArray()));
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Obtiene un arreglo que se obtiene de mergear dos arreglos con los siguientes criterios:
        /// - Si la palabra es la misma en el mismo índice de los arreglos, se mantiene la palabra en ese índice del resultado.
        /// - Si una palabra es vacío y otra no lo es, en el mismo índice, predomina la palabra no vacía para ese índice del resultado.
        /// - Si las palabras en el mismo índice son distintas, en ese índice del resultado corresponde vacío.
        /// </summary>
        /// <param name="arrayMessage1">Mensaje 1 con arreglo de cadenas</param>
        /// <param name="arrayMessage2">Mensaje 2 con arreglo de cadenas</param>
        /// <returns>Un arreglo con el merge de ambos mensajes</returns>
        public static string[] ArrayStringMerge(string[] arrayMessage1, string[] arrayMessage2)
        {
            var result = new List<string>();

            for (int index = 0; index < arrayMessage1.Length; index++)
            {
                if (arrayMessage1[index].Equals(arrayMessage2[index]))
                    result.Add(arrayMessage1[index]);
                else if (string.IsNullOrEmpty(arrayMessage1[index]))
                    result.Add(arrayMessage2[index]);
                else if (string.IsNullOrEmpty(arrayMessage2[index]))
                    result.Add(arrayMessage1[index]);
                else
                    result.Add(string.Empty);
            }

            return result.ToArray();
        }
    }
}
