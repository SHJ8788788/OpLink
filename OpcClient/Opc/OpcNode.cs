using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpcClient.Opc
{
    /// <summary>
    /// 节点
    /// </summary>
    public class OpcNode
    {
        private string nodeName;
        private string nodeId;
        private string key;
        private OpcNodeAttribute attribute;
        /// <summary>
        /// 是否有子节点
        /// </summary>
        private bool isExpand;

        public OpcNode(string nodeName, string nodeId, string key, bool isExpand)
        {
            this.nodeName = nodeName;
            this.nodeId = nodeId;
            this.key = key;
            this.isExpand = isExpand;
        }

        public string NodeName { get => nodeName;}
        public string NodeId { get => nodeId;}
        public string Key { get => key;}
        public bool IsExpand { get => isExpand;}
        public OpcNodeAttribute Attribute { get => attribute; set => attribute = value; }
    }
    /// <summary>
    /// 读取属性过程中用于描述的
    /// </summary>
    public class OpcNodeAttribute
    {
        /// <summary>
        /// 属性的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 属性的类型描述
        /// </summary>
        public object Type { get; set; }
        /// <summary>
        /// 操作结果状态描述
        /// </summary>
        //public StatusCode StatusCode { get; set; }
        /// <summary>
        /// 属性的值，如果读取错误，返回文本描述
        /// </summary>
        public object Value { get; set; }

        public string AccessLevel {get;set;}
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 节点类型
        ///Unspecified = 0,
        ///Object = 1,
        ///Variable = 2,
        ///Method = 4,
        ///ObjectType = 8,
        ///VariableType = 16,
        ///ReferenceType = 32,
        ///DataType = 64,
        ///View = 128
        /// </summary>
        public string NodeClass { get; set; }
    }
}
