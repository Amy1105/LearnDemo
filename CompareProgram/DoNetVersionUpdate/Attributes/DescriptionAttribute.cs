using System;
using System.Collections.Generic;
using System.Text;

namespace DoNetVersionUpdate
{
    internal class DescriptionAttribute:Attribute
    {
        private readonly string description;
        public DescriptionAttribute(string description)
        {
            this.description = description;
        }

        public string Description
        {
            get { return description; }
        }
    }
}
