//***********************************************************
// 描述：UIViewPathInfo数据管理类
// 作者：fanwei 
// 版本：1.0 
// 备注：此代码为工具生成 请勿手工修改
//***********************************************************
using Vk.Core.Data;
/// <summary>
/// UIViewPathInfo数据管理类
/// </summary>
public partial class UIViewPathInfoDBModel : AbstractDBModel<UIViewPathInfoDBModel, UIViewPathInfoEntity>
{
    /// <summary>
   /// 文件名称
   /// </summary>
   protected override string FileName { get {  return "UIViewPathInfo.data"; } }
   /// <summary>
   /// 创建实体
   /// </summary>
   /// <param name="parser"></param>
   /// <returns></returns>
   protected override UIViewPathInfoEntity MakeEntity(DataTableParser parser)
   {
      UIViewPathInfoEntity entity = new UIViewPathInfoEntity();
      entity.Id = parser.GetFileValue(parser.FieldName[0]).ToInt();
      entity.uiType = parser.GetFileValue(parser.FieldName[1]);
      entity.uiPath = parser.GetFileValue(parser.FieldName[2]);
      return entity;
   }
}
