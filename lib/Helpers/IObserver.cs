﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.lib.Helpers
{
    internal interface IObserver<T>
    {
        void Update(T type);
    }
}
