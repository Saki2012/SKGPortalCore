﻿using NUnit.Framework;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKGPortalCore.NUnit
{
    public class BaseUnitTest
    {
        public ApplicationDbContext DataAccess;
        [OneTimeSetUp]
        public void CreateDataAccess()
        {
            DataAccess = LibDataAccess.CreateDataAccess();
        }
    }
    internal static class Comm
    {
        internal static void SpecAssertMessage(List<MessageCode> msgCodeList, MessageCode code, bool isTrue)
        {
            if (isTrue)
                Assert.IsTrue(msgCodeList.Contains(code), code.ToString());
            else
                Assert.IsFalse(msgCodeList.Contains(code), code.ToString());
        }

        internal static void CheckInputValueIsNull<TSet>(TSet set)
        {
            foreach (var property in typeof(TSet).GetProperties())
            { 
            
            }
            //Assert.IsNotNull(null);
        }

    }
}
