using challenge_servicios.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace challenge_servicios.implementaciones
{
    /// <summary>
    /// Responsabilidad: Formar un mensaje realizando un merge entre los mensajes obtenidos por los satélites
    /// </summary>
    public class SatellitesMessageGenerator : MessageGenerator
    {
        
        public SatellitesMessageGenerator()
        {
        }

        /// <summary>
        /// Devuelve un mensaje que forma mediante el merge de los mensajes recibidos por los satélites.
        /// Se permite un desfasaje de los mensajes de una posición a izquierda o derecha. 
        /// Se contempla que el contenido de una de las posiciones del arreglo del mensaje se pierda, es decir llegue vacío; 
        /// pero no se contempla que se pierda un índice del arreglo del mensaje, por lo que se devuelve un mensaje del tamaño del menor de los arreglos recibidos.
        /// La diferencia del tamaño de los arreglos recibidos no puede ser superior a uno.
        /// </summary>
        /// <param name="messages">Mensajes recibidos por cada satélite. Cada mensaje corresponde a un arreglo de palabras.</param>
        /// <returns>Mensaje que se forma a partir de los mensajes recibidos.</returns>
        /// <exception cref="System.ArgumentException">Si no se recibe la misma cantidad de mensajes que de satélites disponibles</exception>
        /// <exception cref="System.ArgumentException">Si no se recibe la misma cantidad de mensajes que de satélites disponibles</exception>
        /// <exception cref="System.ArgumentNullException">Si la entrada es nula</exception>
        public string GetMessage(params string[][] messages)
        {
            if (messages == null)
            {
                throw new ArgumentNullException("No se recibieron mensajes.");
            }
            if (messages.Length != 3)
            {
                throw new ArgumentException("No se recibieron la cantidad de mensajes adecuados. Cantidad de mensajes recibidos: " + messages.Length );   
            }
            if (messages[0].Length != messages[1].Length && messages[0].Length != messages[2].Length
                && messages[1].Length != messages[2].Length)
            {
                throw new ArgumentException("El tamaño de los mensajes es incorrecto.");
            }
            if (Math.Abs(messages[0].Length - messages[1].Length) > 1 || Math.Abs(messages[0].Length - messages[2].Length) > 1
                || Math.Abs(messages[1].Length - messages[2].Length) > 1)
            {
                throw new ArgumentException("El tamaño de los mensajes es incorrecto.");
            }
            string[] result = Solve(messages);
            return string.Join(" ", result); // Concatena las cadenas del resultado obtenido separandolas por un espacio.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public string[] Solve(params string[][] messages)
        {
            string[][] solutions = Solutions(messages);

            return BestSolution(solutions);
            
        }

        /// <summary>
        /// Obtiene entre las posibles soluciones aquella solución con menos cadenas vacías
        /// y con menos repetición de palabras.
        /// </summary>
        /// <param name="solutions">Todas las posibles soluciones de mensajes a formar.</param>
        /// <returns>La solución determinada cómo la mejor.</returns>
        private string[] BestSolution(string[][] solutions)
        {
            // La primer posible solución comienza siendo la factible hasta que se verifiquen el restos de las posibles soluciones
            string[] solution = solutions[0];

            string[] auxSolution;
            int? repetitions;

            // Se almacena en un diccionario todas las palabras y sus respectivas aparaciones en el posible mensaje
            Dictionary<string, int> stringsRepetitions = solution.GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());

            // Se almacena la cantidad de palabras distintas a vacío que hay en el arreglo del posible mensaje.
            repetitions = stringsRepetitions.Count(x => !x.Key.Equals("")); 

            Dictionary<string, int> auxStringsRepetitions;
            for (int i = 1; i < solutions.Length; i++)
            {
                auxSolution = solutions[i];
                
                // Se almacena en un diccionario auxiliar todas las palabras y sus respectivas aparaciones en el posible mensaje
                auxStringsRepetitions = auxSolution.GroupBy(x => x)
                    .ToDictionary(x => x.Key, x => x.Count());

                // Se verifica si en el diccionario auxiliar hay más palabras sin repetir que en el diccionario original
                // Y se verifica si en los extremos hay palabras repetidas
                if (auxStringsRepetitions.Count(x => !x.Key.Equals("")) > repetitions
                    || (auxStringsRepetitions.Count(x => !x.Key.Equals("")) == repetitions
                    && solution.Length > 1 && (solution[0].Equals(solution[1]) || solution[solution.Length - 1].Equals(solution[solution.Length - 2]))))
                {
                    // Si hay más palabras sin repetir se determina que es un mensaje más certero, 
                    // ya que la repetición de palabras se puede deber al desfasaje del mensaje.
                    // Se modifica diccionario y posible solución
                    repetitions = auxStringsRepetitions.Count(x => !x.Key.Equals(""));
                    solution = auxSolution;
                }
            }
            return solution;
        }

        /// <summary>
        /// Obtiene todas las posibles soluciones de mergear las 3 cadenas obtenidas de los satelites
        /// </summary>
        /// <param name="messages">Arreglo los 3 mensajes obtenidos de los satélites</param>
        /// <returns>Todas las soluciones posibles de mergear las cadenas de los satélites.</returns>
        public string[][] Solutions(params string[][] messages)
        {            
            int length = messages.Min(x => x.Length);
            var resultsMergeM1M2 = StringMerge.MatrixStringMerge(messages[0], messages[1], length);
            var resultMergeFinal = new List<string[]>();
            foreach (var resultMergeM1M2 in resultsMergeM1M2)
            {
                resultMergeFinal.AddRange(StringMerge.MatrixStringMerge(resultMergeM1M2, messages[2], length));
            }

            resultMergeFinal.AddRange(SolutionsAllGaps(messages));


            return resultMergeFinal.ToArray();
        }

        private string[][] SolutionsAllGaps(string[][] messages)
        {
            var resultMergeFinal = new List<string[]>();
            if ((messages[0].First().Equals("") || messages[0].Last().Equals("")) &&
                (messages[1].First().Equals("") || messages[1].Last().Equals("")) &&
                (messages[2].First().Equals("") || messages[2].Last().Equals("")))
            {
                int length = messages.Min(x => x.Length - 1);
                var resultsMergeM1M2 = StringMerge.MatrixStringMerge(messages[0], messages[1], length);
                foreach (var resultMergeM1M2 in resultsMergeM1M2)
                {
                    resultMergeFinal.AddRange(StringMerge.MatrixStringMerge(resultMergeM1M2, messages[2], length));
                }
                
            }
            return resultMergeFinal.ToArray();
        }
    }
}
