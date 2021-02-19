using challenge_servicios.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_servicios.servicios
{
    public interface Locator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="distances"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        Point GetLocation(params float[] distances);
    }
}
