using PatternAnalysis.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternAnalysis.Tests
{
    [TestClass]
    public class JsonReaderTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var points = JsonReader.Read("");
            Assert.IsTrue(points.Count == 0);
        }
    }
}
