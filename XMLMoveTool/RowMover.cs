using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace XMLMoveTool
{
    public static class RowMover 
    {
        private class MovingTask
        {
            private FileInfo source;
            private FileInfo destination;

            private XDocument sourceXML;
            private XDocument destinationXML;


            public MovingTask(FileInfo source, FileInfo destination)
            {
                this.source = source;
                this.destination = destination;

                sourceXML = XDocument.Load(source.FullName);
                destinationXML = XDocument.Load(destination.FullName);
            }

            public void Move()
            {
                MoveContent(sourceXML, destinationXML);
                destinationXML.Save(destination.FullName);
            }
        }

       public static void MoveContent (XDocument source, XDocument destination , string collectionElementName = "rows")
        {
            MoveContent(source.Descendants(collectionElementName).Single(), destination.Descendants(collectionElementName).Single());
        }

        public static void MoveContent(XElement source, XElement destination)
        {
            destination.Add(source.Elements());
        }

        private static MovingTask CreateMovingTask(DirectoryInfo sourceFolder, FileInfo destinatinFile)
        {
            return new MovingTask(sourceFolder.GetFiles(destinatinFile.Name).Single(), destinatinFile);
        }

        public static void MoveBetweenFolders(DirectoryInfo source, DirectoryInfo destination)
        {
            foreach (MovingTask task in destination.GetFiles("*.xml").Select(f => CreateMovingTask(source, f))) task.Move();
        }
    }
}
