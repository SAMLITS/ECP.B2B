using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECP_B2B_API_SDK.Helper
{
    public class FileHelper
    { 
        private static string baseDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private static object objLock = new object();
        /// <summary>
        /// 读取指定文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string FileReadAllText(string filePath, bool isBasePath = true)
        {
            if (isBasePath)
                filePath = Path.Combine(baseDirectoryPath, filePath);
            if (IsExistsFile(filePath))
                return File.ReadAllText(filePath, Encoding.UTF8);
            else
                return null;
        }
         
        /// <summary>
        /// 写入指定文件指定内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileContent">文件内容</param>
        public static void AddTextFile(string filePath, string fileContent, bool isBasePath = true)
        {
            if (isBasePath)
                filePath = Path.Combine(baseDirectoryPath, filePath);
            CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, fileContent, Encoding.UTF8); 
        }

        /// <summary>
        /// 判断指定文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool IsExistsFile(string filePath, bool isBasePath = true)
        {
            if (isBasePath)
                filePath = Path.Combine(baseDirectoryPath, filePath);
            return File.Exists(filePath);
        }

        /// <summary>
        /// 移除指定文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void RemoveFile(string filePath, bool isBasePath = true)
        {
            if (isBasePath)
                filePath = Path.Combine(baseDirectoryPath, filePath);
            if (IsExistsFile(filePath))
                File.Delete(filePath);
        }

        /// <summary>
        /// 移除指定目录下的所有文件
        /// </summary>
        /// <param name="diretoryPath"></param>
        public static void RemoveAllFileByDiretory(string diretoryPath, bool isBasePath = true)
        {
            if (isBasePath)
                diretoryPath = Path.Combine(baseDirectoryPath, diretoryPath);
            if (Directory.Exists(diretoryPath))
                Directory.Delete(diretoryPath);
        }

        /// <summary>
        /// 获取指定文件夹下面的文件数量
        /// </summary>
        /// <param name="diretoryPath"></param>
        /// <returns></returns>
        public static int GetFileCountByDiretory(string diretoryPath, bool isBasePath = true)
        {
            if (isBasePath)
                diretoryPath = Path.Combine(baseDirectoryPath, diretoryPath);
            if (Directory.Exists(diretoryPath))
                return Directory.GetFiles(diretoryPath).Length;
            else
                return -1;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="diretoryPath"></param>
        public static void CreateDirectory(string diretoryPath, bool isBasePath = true)
        {
            if (isBasePath)
                diretoryPath = Path.Combine(baseDirectoryPath, diretoryPath);
            if (!Directory.Exists(diretoryPath))
                Directory.CreateDirectory(diretoryPath);
        }

        /// <summary>
        /// 返回指定目录下的所有文件路径
        /// </summary>
        /// <param name="diretoryPath"></param>
        /// <returns></returns>
        public static string[] GetFiles(string diretoryPath, bool isBasePath = true)
        {
            if (isBasePath)
                diretoryPath = Path.Combine(baseDirectoryPath, diretoryPath);
            return Directory.GetFiles(diretoryPath);
        }

        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileName(string filePath, bool isBasePath = true)
        {
            if (isBasePath)
                filePath = Path.Combine(baseDirectoryPath, filePath);
            FileInfo fileInfo = new FileInfo(filePath);
            return Path.GetFileNameWithoutExtension(fileInfo.Name);
        }
    }
}
