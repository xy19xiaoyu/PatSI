using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IData
{
    public delegate void ValueChangedEventHandler(object sender, int success, int skip, int overwrite, string status);
}
