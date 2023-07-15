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
        #region 获取文件/文件夹 信息

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
        #endregion

        #region 创建 新增 移动 文件、目录

        public static bool CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }
            Console.WriteLine("Path already exists::" + path);
            return false;
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
                return false;
            }
            return true;
        }

        public static bool DeleteFolder(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            try
            {

                dir.Delete(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete Folder File::" + ex.Message);
                return false;
            }

            return true;
        }

        public static bool DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete File Fail::" + ex.Message);
                return false;
            }
            return true;
        }

        public static (bool, string) MoveFolder(string path, string path2)
        {
            if (!Directory.Exists(path)) return (false, "源目录不存在");
            if (Directory.Exists(path)) return (false, "目的目录已存在");

            try
            {
                System.IO.Directory.Move(path, path2);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Move Folder Fail::" + ex.Message);
                return (false, ex.ToString());
            }
            return (true, string.Empty);
        }

        public static (bool, string) MoveFile(string path, string path2)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return (false, "源文件不存在");
                }
                // Ensure that the target does not exist.
                if (File.Exists(path2))
                {
                    return (false, "目录文件已存在");
                }

                // Move the file.
                File.Move(path, path2);
                Console.WriteLine("{0} was moved to {1}.", path, path2);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return (false, e.ToString());
            }
            return (true, string.Empty);


        }

        #endregion

        #region 读取txt、markdown

        public static string[] ReadAllLines(string fileName) => File.ReadAllLines(fileName);

        public static string ReadAllText(string fileName) => File.ReadAllText(fileName);
        #endregion

        #region 读取各类型文件，返回流


        #endregion

    }
}
