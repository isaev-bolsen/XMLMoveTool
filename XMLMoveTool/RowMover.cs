﻿using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

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
                using (XmlTextWriter XmlTextWriter = new XmlTextWriter(destination.FullName, null)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 1,
                    IndentChar = '\t'
                })
                    destinationXML.Save(XmlTextWriter);
            }
        }

        public static void RemoveInactive(FileInfo source)
        {
            XDocument xDoc = XDocument.Load(source.FullName);
            RemoveInactive(xDoc);
            xDoc.Save(source.FullName);
        }

        public static void RemoveInactive(XDocument xDocument, string collectionElementName = "rows")
        {
            RemoveInactive(xDocument.Descendants(collectionElementName).Single());
        }

        public static void RemoveInactive(XElement source)
        {
            XElement[] toRemove = source.Elements().Where(e => e.Attribute("IsActive").Value == "0").ToArray();
            foreach (XElement elt in toRemove) elt.Remove();
        }

        public static void MoveContent(XDocument source, XDocument destination, string collectionElementName = "rows")
        {
            MoveContent(source.Descendants(collectionElementName).SingleOrDefault(), destination.Descendants(collectionElementName).SingleOrDefault());
        }

        public static void MoveContent(XElement source, XElement destination)
        {
            if (source == null || destination == null) return;
            destination.Add(source.Elements());
        }

        public static void MoveBetweenFolders(DirectoryInfo source, DirectoryInfo destination)
        {
            foreach (FileInfo destinationFile in destination.GetFiles("*.xml"))
            {
                FileInfo sourceFile = source.GetFiles(destinationFile.Name).SingleOrDefault();
                if (sourceFile == null) continue;
                new MovingTask(sourceFile, destinationFile).Move();
            }
        }
    }
}
