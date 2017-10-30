using System.IO;

namespace XMLMover
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLMoveTool.RowMover.MoveBetweenFolders(new DirectoryInfo(args[0]), new DirectoryInfo(args[1]));
        }
    }
}
