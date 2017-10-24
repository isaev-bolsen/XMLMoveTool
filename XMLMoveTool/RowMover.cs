using System.Linq;
using System.Xml.Linq;

namespace XMLMoveTool
{
    public static class RowMover 
    {
       public static void MoveContent (XDocument source, XDocument destination , string collectionElementName = "rows")
        {
            MoveContent(source.Descendants(collectionElementName).Single(), destination.Descendants(collectionElementName).Single());
        }

        public static void MoveContent(XElement source, XElement destination)
        {
            destination.Add(source.Elements());
        }
    }
}
