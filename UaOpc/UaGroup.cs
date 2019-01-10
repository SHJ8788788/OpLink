using OpcClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace UaOpcClient
{
    /// <summary>
    /// OPC数据分组，
    /// 带有1、OPC组信息
    ///     2、组下子项信息
    public class UaGroup : TagsChangedListener, IGroup
    {
        private UaOpc uaOpc;
        /// <summary>
        /// GroupName
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 组内数据项集合
        /// 采用动态数据集合ObservableCollection
        /// </summary>
        private ObservableCollection<Tag> tags = new ObservableCollection<Tag>();
        /// <summary>
        ///  OPCTags 信号集合
        ///  监听 ObservableCollection<T> 集合的“增加元素”事件，然后，监听这个T对象的属性修改事件，属性修改（value发生变化时）,调用广播事件TriggerEvent.obj_PropertyChanged
        /// </summary>
        public ObservableCollection<Tag> Tags { get { return tags; } }
        /// <summary>
        /// 点新增后触发
        /// </summary>
        public Action<IGroup> AddItemsComplete { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="group"></param>     
        public UaGroup(string groupName,UaOpc uaOpc)
        {
            this.uaOpc = uaOpc;
            this.GroupName = groupName;
            //为组增加监听，当集合数量发生变化时，调用TagsCollectionChanged事件,绑定tags
            this.Tags.CollectionChanged += this.TagsCollectionChanged;
        }
        public bool AddItem(Tag bi)
        {
            try
            {
                #region*********集合已包含此Tag,则退出*************************
                foreach (Tag item in this.Tags)
                {
                    if (item.TagName == bi.TagName)
                    {
                        MessageBox.Show("Tag重复：在在重复的Tag点", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                #endregion           
                //关键，此步骤将标签添加入OPCclient的监听列表中
                Tags.Add((Tag)(Object)bi);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IGroup AddItems(List<Tag> biList)
        {
            #region 传入的标签集合为空时，直接返回
            if (biList.Count == 0) return this;
            #endregion
            #region 当前标签集合已包含此Tag,则退出
            if (Tags.Count(p => biList.Select(b => b.TagName).Contains(p.TagName)) != 0)
            {
                MessageBox.Show("Tag重复：在在重复的Tag点", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return this;
            }
            #endregion
            foreach (var tag in biList)
            {
                Tags.Add(tag);
            }
            AddItemsComplete?.Invoke(this);
            return this;
        }
        /// <summary>
        /// 启用历史记录功能,会以queue方式保存历史记录
        /// 在AddItems、AddItem后使用
        /// </summary>
        public IGroup AddQueue(int queueMaxNum)
        {
            foreach (var tag in tags)
            {
                tag.addQueue(queueMaxNum);
            }
            return this;
        }
        /// <summary>
        /// 根据tag名称查询tag点历史集合
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public List<Tag> GetTagHistory(string tagName)
        {
            Tag tag = Tags.Where(t => t.TagName == tagName).FirstOrDefault();
            return tag != null ? tag.TagHistory : null;
        }

        public List<Tag> GetTags(List<string> tagNames=null)
        {
            if (tagNames==null)
            {
                return Tags.AsParallel().ToList();
            }
            else
            {
                return Tags.AsParallel().Where(t => tagNames.Contains(t.TagName)).ToList();
            }            
        }

        public Tag GetTagValue(string tagName)
        {
            return Tags.AsParallel().Where(t => t.TagName == tagName).FirstOrDefault();
        }

        public Tag GetTagValue(Tag bi)
        {
            return Tags.Where(t => t.TagName == bi.TagName).FirstOrDefault();
        }

        public List<Tag> GetTagValues(List<Tag> biList)
        {
            return Tags.Where(t => biList
                 .Select(b => b.TagName)
                 .Contains(t.TagName)).ToList();
        }

        public void RemoveItem(Tag bi)
        {
            try
            {
                if (Tags.Count == 0) return;//标签不存在时，返回false           
                foreach (var item in Tags.Where(p => p.TagName == bi.TagName))
                {
                    Tags.Remove(item);//监控标签集合删除指定位置的标签
                }           
            }
            catch (System.AccessViolationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void RemoveItems(List<Tag> biList)
        {
            foreach (var item in Tags
               .Where(a => biList
               .Select(p => p.TagName)
               .Contains(a.TagName)))
            {
                Tags.Remove(item);//监控标签集合删除指定位置的标签
            }
        }

        public void RemoveItemsAll()
        {
            Tags.Clear();//监控集合删除所有位置的标签            
        }

        /// <summary>
        /// 清空历史记录
        /// </summary>
        public IGroup RemoveQueue()
        {
            foreach (var tag in tags)
            {
                tag.RemoveQueue();
            }
            return this;
        }

        public IGroup SetUpdateRate(int updateRate)
        {
            return this;
        }
        #region IDisposable

        /// <summary>
        /// 获取是否已释放
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 关闭和释放所有相关资源
        /// </summary>
        public void Dispose()
        {
            // 如果资源未释放 这个判断主要用了防止对象被多次释放
            if (this.IsDisposed == false)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
            this.IsDisposed = true;
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~UaGroup()
        {
            this.Dispose(false);
        }
        /// <summary>
        /// 释放资源
        ///参数为true表示释放所有资源，只能由使用者调用
        //参数为false表示释放非托管资源，只能由垃圾回收器自动调用
        //如果子类有自己的非托管资源，可以重载这个函数，添加自己的非托管资源的释放
        //但是要记住，重载此函数必须保证调用基类的版本，以保证基类的资源正常释放
        /// </summary>
        /// <param name="disposing">是否也释放托管资源</param>
        protected virtual void Dispose(bool disposing)
        {
            //释放非托管资源
            //this.OPCGroup.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(this.KepGroup_DataChange);
            //this.Tags.CollectionChanged -= this.TagsCollectionChanged;
            //this.OPCGroup = null;            
            // 释放托管资源(一般用不到，不需要手动释放，依赖垃圾回收器自动回收)
            if (disposing)
            {
                //暂无
            }
        }
        #endregion
    }
}
