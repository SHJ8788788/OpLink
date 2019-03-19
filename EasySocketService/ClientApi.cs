
using EasySocket.vs13.Core;
using EasySocket.vs13.Serializers;
using EasySocket.vs13.Telegram.Easy;
using Models;
using OpcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySocketService
{
    /// <summary>
    /// API自定义类
    /// 1、必须继承EasyTcpClient，要不然会导致方法无法正常加载
    /// 2、需要使用的远程调用方法在此处描述
    /// </summary>
    public class ClientApi:EasyTcpClient
    {
        [Api]
        public string GetTagValue(string tagName)
        {
            return this
                .OpcData
                .GetTagValue(tagName)
                .Value.ToString();
        }
        [Api]
        public List<TagSimple> GetTags(List<string> tagNames)
        {

         var list = this
               .OpcData
               .GetTags(tagNames)
               .Select(p => new TagSimple { TagName = p.TagName, TagValue = p.Value.ToString(), TagType = p.DataType })
               .ToList();
            return this
                .OpcData
                .GetTags(tagNames)
                .Select(p => new TagSimple { TagName=p.TagName,TagValue=p.Value.ToString(),TagType = p.DataType})
                .ToList();
        }

        [Api]
        public string GetTagValueMaxBetweenDate(string tagName, DateTime dateFrom, DateTime dateTo = default(DateTime))
        {
            return this
                .OpcData
                .GetTagHistory(tagName)
                .ByDateBetweenThan(dateFrom,dateTo).Max().ToString();
        }

        private IGroup OpcData
        {
            get { return EasyTcpClient.Instance.Extra.Tag.Get("opc").As<IOpcClient>()["GroupData"]; }
        }
    }
}
