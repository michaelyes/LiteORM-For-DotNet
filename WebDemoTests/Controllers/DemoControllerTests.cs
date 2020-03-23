using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebDemo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDemo.Controllers.Tests
{
    [TestClass()]
    public class DemoControllerTests
    {
        [TestMethod()]
        public void GetCrashLogModelsTest()
        {
            DemoController demoController = new DemoController();

            Assert.IsNull(demoController.GetCrashLogModels());
        }
    }
}