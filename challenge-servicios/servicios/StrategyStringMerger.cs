﻿using System;
using System.Collections.Generic;
using System.Text;

namespace challenge_servicios.servicios
{
    public interface StrategyStringMerger
    {
        string[] Merge(string[] stringsOne, string[] stringsTwo);
    }
}