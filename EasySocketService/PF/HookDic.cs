using OpcClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySocketService.PF
{
    public class HookDic : TagsChangedListener, ITagsChangedListener
    {
        /// <summary>
        /// 组内数据项集合
        /// 采用动态数据集合ObservableCollection
        /// </summary>
        private ObservableCollection<Tag> tags = new ObservableCollection<Tag>();
        public ObservableCollection<Tag> Tags { get { return tags; } }

        public HookDic()
        {
            //为组增加监听，当集合数量发生变化时，调用TagsCollectionChanged事件,绑定tags
            this.Tags.CollectionChanged += this.TagsCollectionChanged;
        }      
    }
}
