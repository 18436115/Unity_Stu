//***********************************************************
// 描述：将Excel文件转化为 bytedata文件
// 作者：fanwei 
// 创建时间：2021-03-13 14:44:44 
// 版本：1.0 
// 备注：
//***********************************************************
using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Text;
using UnityEditor;

/// <summary>
/// 将Excel转化为data文件
/// </summary>
public class ExcelToData : Editor
{
    /// <summary>
    /// 将Excel转化为data数据
    /// </summary>
    /// <param name="path"></param>
    public static bool DoExcelToData(string path, int type = 0)
    {
        DataTable dt = LoadExcelData(path);

        if (dt == null)
        {
            return false;
        }

        CreateData(path, dt, type);

        return true;
    }

    /// <summary>
    /// 查看所有数据
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string DoViewData(string path)
    {

        int[] strLens;
        //找到最长的资源
        using (DataTableParser parser = new DataTableParser(path))
        {
            int colums = parser.FieldName.Length;
            strLens = new int[colums];
            for (int i = 0; i < colums; i++)
            {
                string str = parser.FieldName[i];
                strLens[i] = getByteCount(str);
            }

            while (parser.Eof == false)
            {
                for (int i = 0; i < colums; i++)
                {
                    string str = parser.GetFileValue(parser.FieldName[i]);
                    if (strLens[i] < getByteCount(str))
                    {
                        strLens[i] = getByteCount(str);
                    }
                }
                //下一个
                parser.Next();
            }
        }

        StringBuilder sb = new StringBuilder();
        using (DataTableParser parser = new DataTableParser(path))
        {
            int colums = parser.FieldName.Length;
            for (int i = 0; i < colums; i++)
            {
                string str = parser.FieldName[i];
                sb.Append(PadLeftEx(str, strLens[i] + 4, ' '));
            }
            sb.AppendLine();

            while (parser.Eof == false)
            {
                for (int i = 0; i < colums; i++)
                {
                    string str = parser.GetFileValue(parser.FieldName[i]);
                    sb.Append(PadLeftEx(str, strLens[i] + 4, ' '));
                }
                sb.AppendLine();

                //下一个
                parser.Next();
            }
        }
        return sb.ToString();
    }

    private static int getByteCount(string str)
    {
        if (string.IsNullOrEmpty(str)) return 1;

        Encoding coding = Encoding.GetEncoding("gb2312");
        int dcount = coding.GetByteCount(str);

        return dcount;
    }

    private static string PadLeftEx(string str, int totalByteCount, char c)
    {
        if (string.IsNullOrEmpty(str))
        {
            str = " ";
        }

        Encoding coding = Encoding.GetEncoding("gb2312");
        int dcount = 0;
        foreach (char ch in str.ToCharArray())
        {
            if (coding.GetByteCount(ch.ToString()) == 2)
                dcount++;
        }
        string w = str.PadLeft(totalByteCount - dcount, c);
        return w;
    }

    /// <summary>
    /// 读取Excel表格数据
    /// </summary>
    /// <returns></returns>
    private static DataTable LoadExcelData(string path)
    {

        if (string.IsNullOrEmpty(path)) return null;

        DataTable dataTable = null;

        using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            //2003版本 使用CreateBinaryReader
            //2007以上版本 使用CreateOpenXmlReader
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream))
            {
                DataSet result = excelReader.AsDataSet();

                dataTable = result.Tables[0];
            }
        }
        return dataTable;
    }

    /// <summary>
    /// 创建加密数据
    /// </summary>
    /// <param name="path"></param>
    /// <param name="dt"></param>
    private static void CreateData(string path, DataTable dt, int type)
    {
        //文件夹路径
        string filePath = path.Substring(0, path.LastIndexOf('/') + 1);
        //文件全称含后缀
        string fileFullName = path.Substring(path.LastIndexOf('/') + 1);
        //文件名称
        string fileName = fileFullName.Substring(0, fileFullName.LastIndexOf('.'));

        //-------------------------------
        // 第一步 读取文件byte数组
        //------------------------------
        byte[] buffer = null;
        string[,] dataArr;//字段名称 字段类型 字段描述
        using (MemoryStreamTool ms = new MemoryStreamTool())
        {
            int row = dt.Rows.Count;
            int column = dt.Columns.Count;

            int realcolumn = 0;
            //先判断有效行数
            for (int j = 0; j < column; j++)
            {
                //查看类似是否是Hide
                if (string.Compare(dt.Rows[1][j].ToString().Trim(),"Hide",System.StringComparison.InvariantCultureIgnoreCase) != 0)
                {
                    realcolumn++;
                }
            }

            dataArr = new string[realcolumn, 3];

            //先写入行列
            ms.WriteInt(row);
            ms.WriteInt(realcolumn);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0, col =0; j < realcolumn; col++)
                {
                    //查看类似是否是Hide
                    if (string.Compare(dt.Rows[1][col].ToString().Trim(), "Hide", System.StringComparison.InvariantCultureIgnoreCase) != 0)
                    {
                        //第一是字段名称 第二行是字段类型 第三行是字段描述
                        if (i < 3)
                        {
                            dataArr[j, i] = dt.Rows[i][col].ToString().Trim();
                        }
                        //写入内容
                        ms.WriteUTF8String(dt.Rows[i][col].ToString().Trim());

                        j++;
                    }
                }
            }
            buffer = ms.ToArray();
        }

        //-------------------------------
        // 第二步 xor 加密
        //------------------------------
        buffer = SecurityUtil.Xor(buffer);

        //-------------------------------
        // 第三步 压缩
        //------------------------------
        buffer = ZlibHelper.CompressBytes(buffer);

        //-------------------------------
        // 第四步 写入文件
        //------------------------------
        FileStream fs = new FileStream(string.Format("{0}{1}", filePath, fileName + ".data"), FileMode.Create);
        fs.Write(buffer, 0, buffer.Length);
        fs.Close();


        //本地数据
        if (type == 0)
        {
            //同时创建 DBModel 
            CreateDBModel(filePath, fileName, dataArr);

            //和 Entity
            CreateEntity(filePath, fileName, dataArr);
        }
        //用户数据
        else
        {
            //同时创建 DBModel 
            CreateUserDBModel(filePath, fileName, dataArr);

            //和 Entity
            CreateUserEntity(filePath, fileName, dataArr);
        }
    }

    /// <summary>
    /// 自动生成数据管理类
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="fileName"></param>
    /// <param name="dataArr">字段信息数组</param>
    private static void CreateDBModel(string filePath, string fileName, string[,] dataArr)
    {
        if (dataArr == null) return;


        string savePath = string.Format("{0}/Create", filePath);
        if (Directory.Exists(savePath) == false)
        {
            Directory.CreateDirectory(savePath);
        }

        StringBuilder sb = new StringBuilder();

        sb.Append("//***********************************************************");
        sb.AppendLine();
        sb.Append(string.Format("// 描述：{0}数据管理类", fileName));
        sb.AppendLine();
        sb.Append("// 作者：fanwei ");
        sb.AppendLine();
        sb.Append("// 版本：1.0 ");
        sb.AppendLine();
        sb.Append("// 备注：此代码为工具生成 请勿手工修改");
        sb.AppendLine();
        sb.AppendLine("//***********************************************************");
        sb.AppendLine("using Vk.Core.Data;");
        //sb.AppendLine("using Vk.Core.Num;");
        sb.AppendLine("/// <summary>");
        sb.Append(string.Format("/// {0}数据管理类", fileName));
        sb.AppendLine();
        sb.Append("/// </summary>");
        sb.AppendLine();
        sb.Append(string.Format("public partial class {0}DBModel : AbstractDBModel<{0}DBModel, {0}Entity>", fileName));
        sb.AppendLine();
        sb.Append("{");
        sb.AppendLine();
        sb.Append("    /// <summary>");
        sb.AppendLine();
        sb.Append("   /// 文件名称");
        sb.AppendLine();
        sb.Append("   /// </summary>");
        sb.AppendLine();
        sb.Append(string.Format("   protected override string FileName {{ get {{  return \"{0}.data\"; }} }}", fileName));
        sb.AppendLine();
        sb.Append("   /// <summary>");
        sb.AppendLine();
        sb.Append("   /// 创建实体");
        sb.AppendLine();
        sb.Append("   /// </summary>");
        sb.AppendLine();
        sb.Append("   /// <param name=\"parser\"></param>");
        sb.AppendLine();
        sb.Append("   /// <returns></returns>");
        sb.AppendLine();
        sb.Append(string.Format("   protected override {0}Entity MakeEntity(DataTableParser parser)", fileName));
        sb.AppendLine();
        sb.Append("   {");
        sb.AppendLine();
        sb.AppendFormat("      {0}Entity entity = new {0}Entity();", fileName);
        sb.AppendLine();
        for (int i = 0; i < dataArr.GetLength(0); i++)
        {
            if (dataArr[i, 1] == "IdleNum")
            {
                sb.Append(string.Format("      entity.{0} = new IdleNum(parser.GetFileValue(parser.FieldName[{1}]));", dataArr[i, 0], i));
            }
            else
            {
                sb.Append(string.Format("      entity.{0} = parser.GetFileValue(parser.FieldName[{1}]){2};", dataArr[i, 0], i, ChangeToType(dataArr[i, 1])));
            }
            sb.AppendLine();
        }
        sb.Append("      return entity;");
        sb.AppendLine();
        sb.Append("   }");
        sb.AppendLine();
        sb.Append("}");
        sb.AppendLine();

        //写入文件
        using (FileStream fs = new FileStream(string.Format("{0}/{1}DBModel.cs", savePath, fileName), FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(sb.ToString());
            }
        }
    }

    /// <summary>
    /// 自动创建数据实体类
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="fileName"></param>
    /// <param name="dataArr"></param>
    private static void CreateEntity(string filePath, string fileName, string[,] dataArr)
    {
        if (dataArr == null) return;


        string savePath = string.Format("{0}/Create", filePath);
        if (Directory.Exists(savePath) == false)
        {
            Directory.CreateDirectory(savePath);
        }

        StringBuilder sb = new StringBuilder();

        sb.Append("//***********************************************************");
        sb.AppendLine();
        sb.Append(string.Format("// 描述：{0}实体类", fileName));
        sb.AppendLine();
        sb.Append("// 作者：fanwei ");
        sb.AppendLine();
        sb.Append("// 版本：1.0 ");
        sb.AppendLine();
        sb.Append("// 备注：此代码为工具生成 请勿手工修改");
        sb.AppendLine();
        sb.AppendLine("//***********************************************************");
        sb.AppendLine("using Vk.Core.Data;");
        //sb.AppendLine("using Vk.Core.Num;");
        sb.Append("/// <summary>");
        sb.AppendLine();
        sb.Append(string.Format("/// {0}实体类", fileName));
        sb.AppendLine();
        sb.Append("/// </summary>");
        sb.AppendLine();
        sb.Append(string.Format("public partial class {0}Entity : AbstractEntity", fileName));
        sb.AppendLine();
        sb.Append("{");
        sb.AppendLine();
        for (int i = 1; i < dataArr.GetLength(0); i++)
        {
            sb.Append("   /// <summary>");
            sb.AppendLine();
            sb.Append(string.Format("   /// {0}", dataArr[i, 2]));
            sb.AppendLine();
            sb.Append("   /// </summary>");
            sb.AppendLine();
            sb.Append(string.Format("   public {0} {1} {{ get; set; }}", dataArr[i, 1], dataArr[i, 0]));
            sb.AppendLine();
        }
        sb.Append("}");
        sb.AppendLine();

        //写入文件
        using (FileStream fs = new FileStream(string.Format("{0}/{1}Entity.cs", savePath, fileName), FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(sb.ToString());
            }
        }
    }

    /// <summary>
    /// 根据类型字段
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static string ChangeToType(string type)
    {
        string result = "";
        switch (type)
        {
            case "int":
                result = ".ToInt()";
                break;
            case "float":
                result = ".ToFloat()";
                break;
            case "long":
                result = ".ToLong()";
                break;
            case "bool":
                result = ".ToBool()";
                break;
        }
        return result;
    }


    /// <summary>
    /// 自动生成数据管理类
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="fileName"></param>
    /// <param name="dataArr">字段信息数组</param>
    private static void CreateUserDBModel(string filePath, string fileName, string[,] dataArr)
    {
        if (dataArr == null) return;


        string savePath = string.Format("{0}/Create", filePath);
        if (Directory.Exists(savePath) == false)
        {
            Directory.CreateDirectory(savePath);
        }

        StringBuilder sb = new StringBuilder();

        sb.Append("//***********************************************************");
        sb.AppendLine();
        sb.Append(string.Format("// 描述：{0}用户数据管理类", fileName));
        sb.AppendLine();
        sb.Append("// 作者：fanwei ");
        sb.AppendLine();
        sb.Append("// 版本：1.0 ");
        sb.AppendLine();
        sb.Append("// 备注：此代码为工具生成 请勿手工修改");
        sb.AppendLine();
        sb.AppendLine("//***********************************************************");
        sb.AppendLine("using Vk.Core.Data;");
        //sb.AppendLine("using Vk.Core.Num;");
        sb.Append("/// <summary>");
        sb.AppendLine();
        sb.Append(string.Format("/// {0}用户数据管理类", fileName));
        sb.AppendLine();
        sb.Append("/// </summary>");
        sb.AppendLine();
        sb.Append(string.Format("public partial class {0}DBModel : AbstractUserDBModel<{0}DBModel, {0}Entity>", fileName));
        sb.AppendLine();
        sb.Append("{");
        sb.AppendLine();
        sb.Append("    /// <summary>");
        sb.AppendLine();
        sb.Append("   /// 文件名称");
        sb.AppendLine();
        sb.Append("   /// </summary>");
        sb.AppendLine();
        sb.Append(string.Format("   protected override string FileName {{ get {{  return \"{0}.data\"; }} }}", fileName));
        sb.AppendLine();
        sb.Append("   /// <summary>");
        sb.AppendLine();
        sb.Append("   /// 创建实体");
        sb.AppendLine();
        sb.Append("   /// </summary>");
        sb.AppendLine();
        sb.Append("   /// <param name=\"parser\"></param>");
        sb.AppendLine();
        sb.Append("   /// <returns></returns>");
        sb.AppendLine();
        sb.Append(string.Format("   protected override {0}Entity MakeEntity(DataTableParser parser)", fileName));
        sb.AppendLine();
        sb.Append("   {");
        sb.AppendLine();
        sb.AppendFormat("      {0}Entity entity = new {0}Entity();", fileName);
        sb.AppendLine();
        for (int i = 0; i < dataArr.GetLength(0); i++)
        {
            if (dataArr[i, 1] == "IdleNum")
            {
                sb.Append(string.Format("      entity.{0} = new IdleNum(parser.GetFileValue(parser.FieldName[{1}]));", dataArr[i, 0], i));
            }
            else
            {
                sb.Append(string.Format("      entity.{0} = parser.GetFileValue(parser.FieldName[{1}]){2};", dataArr[i, 0], i, ChangeToType(dataArr[i, 1])));
            }
            sb.AppendLine();
        }
        sb.Append("      return entity;");
        sb.AppendLine();
        sb.Append("   }");
        sb.AppendLine();
        sb.Append("   /// <summary>");
        sb.AppendLine();
        sb.Append("   /// 编码实体");
        sb.AppendLine();
        sb.Append("   /// </summary>");
        sb.AppendLine();
        sb.Append("   /// <param name=\"parser\"></param>");
        sb.AppendLine();
        sb.Append("   /// <returns></returns>");
        sb.AppendLine();
        sb.Append(string.Format("   protected override string [] EncodeEntity({0}Entity entity)", fileName));
        sb.AppendLine();
        sb.Append("   {");
        sb.AppendLine();
        sb.AppendFormat("      string [] strArrs = new string[{0}];", dataArr.GetLength(0));
        sb.AppendLine();
        for (int i = 0; i < dataArr.GetLength(0); i++)
        {
            sb.Append(string.Format("      strArrs[{0}] = entity.{1}{2};", i, dataArr[i, 0], ChangeToString(dataArr[i, 1])));
            sb.AppendLine();
        }
        sb.Append("      return strArrs;");
        sb.AppendLine();
        sb.Append("   }");
        sb.AppendLine();
        sb.Append("}");
        sb.AppendLine();

        //写入文件
        using (FileStream fs = new FileStream(string.Format("{0}/{1}DBModel.cs", savePath, fileName), FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(sb.ToString());
            }
        }
    }

    /// <summary>
    /// 自动创建数据实体类
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="fileName"></param>
    /// <param name="dataArr"></param>
    private static void CreateUserEntity(string filePath, string fileName, string[,] dataArr)
    {
        if (dataArr == null) return;


        string savePath = string.Format("{0}/Create", filePath);
        if (Directory.Exists(savePath) == false)
        {
            Directory.CreateDirectory(savePath);
        }

        StringBuilder sb = new StringBuilder();

        sb.Append("//***********************************************************");
        sb.AppendLine();
        sb.Append(string.Format("// 描述：{0}用户实体类", fileName));
        sb.AppendLine();
        sb.Append("// 作者：fanwei ");
        sb.AppendLine();
        sb.Append("// 版本：1.0 ");
        sb.AppendLine();
        sb.Append("// 备注：此代码为工具生成 请勿手工修改");
        sb.AppendLine();
        sb.AppendLine("//***********************************************************");
        sb.AppendLine("using Vk.Core.Data;");
       // sb.AppendLine("using Vk.Core.Num;");
        sb.Append("/// <summary>");
        sb.AppendLine();
        sb.Append(string.Format("/// {0}用户实体类", fileName));
        sb.AppendLine();
        sb.Append("/// </summary>");
        sb.AppendLine();
        sb.Append(string.Format("public partial class {0}Entity : AbstractUserEntity", fileName));
        sb.AppendLine();
        sb.Append("{");
        sb.AppendLine();
        for (int i = 1; i < dataArr.GetLength(0); i++)
        {
            sb.Append("   /// <summary>");
            sb.AppendLine();
            sb.Append(string.Format("   /// {0}", dataArr[i, 2]));
            sb.AppendLine();
            sb.Append("   /// </summary>");
            sb.AppendLine();
            sb.Append(string.Format("   public {0} {1} {{ get; set; }}", dataArr[i, 1], dataArr[i, 0]));
            sb.AppendLine();
        }
        sb.Append("}");
        sb.AppendLine();

        //写入文件
        using (FileStream fs = new FileStream(string.Format("{0}/{1}Entity.cs", savePath, fileName), FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(sb.ToString());
            }
        }
    }

    /// <summary>
    /// 根据类型字段
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static string ChangeToString(string type)
    {
        string result = "";
        switch (type)
        {
            case "int":
            case "float":
            case "long":
            case "bool":
                result = ".ToString()";
                break;
            case "IdleNum":
                result = ".ToStringFormat()";
                break;
        }
        return result;
    }
}