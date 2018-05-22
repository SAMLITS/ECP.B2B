using ProtoBuf;
using System.Collections.Generic;

namespace ECP.B2b.ModelDto
{
    /// <summary>
    /// Bootstrap树结构Dto
    /// </summary>
    [ProtoContract]
    public class BootstrapTreeViewDto
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 标识值
        /// </summary>
        public string tags { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<BootstrapTreeViewDto> nodes { get; set; }
    }
}
