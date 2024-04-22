//***********************************************************
// 描述： 抽象实体信息类
// 作者：fanwei 
// 创建时间：2021-03-12 17:42:51 
// 版本：1.0 
// 备注：
//***********************************************************

namespace Vk.Core.Data
{
    /// <summary>
    /// 抽象实体信息类
    /// </summary>
    public abstract class AbstractEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
    }
}