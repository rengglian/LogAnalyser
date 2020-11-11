using PatternAnalysis.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OxyPlot;

namespace PatternAnalysis.Tests
{
    [TestClass]
    public class JsonReaderTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<DataPoint> points = JsonReader.Read("");
            Assert.IsTrue(points.Count == 0);
        }
    }
}
