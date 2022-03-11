using System;
using System.Collections.Generic;
using System.Text;

namespace Ediux.HomeSystem
{
    public class ExtendPropertiesChangeEventArgs : EventArgs
    {
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
    }
}
