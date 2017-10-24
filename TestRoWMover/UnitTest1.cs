using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Xml.Linq;

namespace TestRoWMover
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRowMover()
        {
            XDocument source = new XDocument(new XElement("data",
                new XElement("table"),
                new XElement("rows", new XElement("row"))
                ));
            XDocument destination = new XDocument(new XElement("data",
                new XElement("table"),
                new XElement("rows")
                ));

            XMLMoveTool.RowMover.MoveContent(source, destination);
            XElement resultingRow = destination.Descendants("rows").Single();
            Assert.AreEqual(1, resultingRow.Elements().Count());
        }
    }
}
