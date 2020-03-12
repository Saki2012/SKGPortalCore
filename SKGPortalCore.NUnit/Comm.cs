using NUnit.Framework;
using SKGPortalCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKGPortalCore.NUnit
{
    internal static class Comm
    {
        internal static void SpecAssertMessage(List<MessageCode> msgCodeList, MessageCode code, bool isTrue)
        {
            if (isTrue)
                Assert.IsTrue(msgCodeList.Contains(code), code.ToString());
            else
                Assert.IsFalse(msgCodeList.Contains(code), code.ToString());
        }
    }
}
