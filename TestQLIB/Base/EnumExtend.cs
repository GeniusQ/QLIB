using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLIB.Base;

namespace TestQLIB.Base
{
    [TestClass]
    public class EnumExtend
    {
        [TestMethod]
        public void GetName()
        {
            TestEnum target = TestEnum.Name1;
            string r = target.GetName();
            Assert.AreEqual("Name1", r, "枚举名称不对");
        }
    }

    public enum TestEnum { 
        Name1
    }
}
