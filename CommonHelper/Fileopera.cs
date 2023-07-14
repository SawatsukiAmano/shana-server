using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public static class Fileopera
    {
        //获取目录子文件名称集合
        public static List<string> GetDirectorySubFileName(string path) =>
            new DirectoryInfo(path).GetDirectories().Select(r => r.Name).ToList();

        //获取目录子文件完整名称集合
        public static List<string> GetDirectorySubFileFullName(string path) =>
            new DirectoryInfo(path).GetDirectories().Select(r => r.FullName).ToList();

        public static List<DirectoryInfo> GetDirectoryInfos(string path) =>
            new DirectoryInfo(path).GetDirectories().ToList();

        public static List<FileInfo> GetFiles(DirectoryInfo[] directoryInfos)
        {
            var path = directoryInfos.Select(x => x.FullName).ToArray();
            var files = new List<FileInfo>();
            for (int i = 0; i < path.Length; i++)
            {
                files.Add(new FileInfo(path[i]));
            }
            return files;
        }

        public static void CreatePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                Console.WriteLine("Path already exists::" + path);
            }
        }

        public static bool CreateFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.CreateNew))
                {
                }
            }
            else
            {
                Console.WriteLine("File already exists::" + fileName);
            }
            return true;
        }


        public static string[] ReadAllLines(string fileName) => File.ReadAllLines(fileName);

        public static string ReadAllText(string fileName) => File.ReadAllText(fileName);
    }
}
