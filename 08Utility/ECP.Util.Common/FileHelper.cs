using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECP.Util.Common
{
    public class FileHelper
    {
        private static Logger  logger = new Logger(typeof(FileHelper));
        private static object objLock = new object();
        public static void AppendText(string filePath, string content, bool isOriginal = false)
        {
            try
            {
                //判断文件夹是否存在
                if (Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    //是否原文写入  
                    if (!isOriginal)
                        File.AppendAllText(filePath, DateTime.Now + "：" + content + "\n", Encoding.UTF8);
                    else
                        File.AppendAllText(filePath, content, Encoding.UTF8);
                }
                else
                {
                    lock (objLock)
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                        }
                    }
                    AppendText(filePath, content);
                }
            }
            catch (Exception ex)
            {
                logger.Error("写入Log信息时异常：",ex: ex);
            }
        }

        /// <summary>
        /// 读取指定文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string FileReadAllText(string filePath)
        {
            return File.ReadAllText(filePath, Encoding.UTF8);
        }


        //private static object writeLock = new object();
        /// <summary>
        /// 写入指定文件指定内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileContent">文件内容</param>
        public static void AddTextFile(string filePath, string fileContent)
        { 
            File.WriteAllText(filePath, fileContent, Encoding.UTF8); 
        }

        /// <summary>
        /// 判断指定文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool IsExistsFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 移除指定文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void RemoveFile(string filePath)
        {
            if (IsExistsFile(filePath))
                File.Delete(filePath);
        }

        /// <summary>
        /// 移除指定目录下的所有文件
        /// </summary>
        /// <param name="diretoryPath"></param>
        public static void RemoveAllFileByDiretory(string diretoryPath)
        {
            if (Directory.Exists(diretoryPath))
                Directory.Delete(diretoryPath);
        }

        /// <summary>
        /// 获取指定文件夹下面的文件数量
        /// </summary>
        /// <param name="diretoryPath"></param>
        /// <returns></returns>
        public static int GetFileCountByDiretory(string diretoryPath)
        {
            if (Directory.Exists(diretoryPath))
                return Directory.GetFiles(diretoryPath).Length;
            else
                return -1;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="diretoryPath"></param>
        public static void CreateDirectory(string diretoryPath)
        {
            if (!Directory.Exists(diretoryPath))
                Directory.CreateDirectory(diretoryPath);
        }

        /// <summary>
        /// 返回指定目录下的所有文件路径
        /// </summary>
        /// <param name="diretoryPath"></param>
        /// <returns></returns>
        public static string[] GetFiles(string diretoryPath)
        {
            return Directory.GetFiles(diretoryPath);
        }

        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileName(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return Path.GetFileNameWithoutExtension(fileInfo.Name);
        }
    }
}
