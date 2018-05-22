using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.HtmlHelper
{
    /// <summary>
    /// 文件上传 条件过滤 传输实体
    /// </summary>
    public class FileUploadFilterEntity
    {
        public FileUploadFilterEntity() { }
        public FileUploadFilterEntity(string _uploadPath)
        {
            this.uploadPath = _uploadPath;
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileAccept fileAccept { get; set; } = FileAccept.file;
        /// <summary>
        /// 允许上传文件后缀    
        /// zip|rar|7z
        /// </summary>
        public string exts { get; set; }

        /// <summary>
        /// 限制文件大小，单位 KB   0代表不限制
        /// </summary>
        public int size { get; set; } = 0;

        /// <summary>
        /// 是否可多选
        /// </summary>
        public bool multiple { get; set; } = false;

        /// <summary>
        /// 上传文件路径
        /// </summary>
        public string uploadPath { get; set; }

        /// <summary>
        /// 选择成功回调js事件名称   onSuccessCallback(obj)
        /// params obj object
        ///       filePaths:string array
        /// </summary>
        public string onSuccessCallback { get; set; }
    }

    public enum FileAccept
    {
        /// <summary>
        /// （图片）
        /// </summary>
        images,
        /// <summary>
        /// （所有文件）
        /// </summary>
        file,
        /// <summary>
        /// （视频）
        /// </summary>
        video,
        /// <summary>
        /// （音频）
        /// </summary>
        audio
    }
}
