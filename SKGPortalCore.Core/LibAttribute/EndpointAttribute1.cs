using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Core.LibEnum;

namespace SKGPortalCore.Core.LibAttribute
{
    public sealed class EndPointAttribute : Attribute
    {
        public EndType EndType { get; }

        public EndPointAttribute(EndType endType)
        {
            EndType = endType;
        }
    }
}
