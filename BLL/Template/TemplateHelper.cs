using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace BLL.Template
{
    public class TemplateHelper
    {
        public TemplateHelper(string tmpname)
        {

        }
        /// <summary>
        /// 模板名称
        /// </summary>
        /// <param name="tmpName"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public string getMappingColumnName(string colName)
        {
            return colName;
        }
        private static string BasePath = System.AppDomain.CurrentDomain.BaseDirectory + "temple\\";

        private static BinaryFormatter formmatter = new BinaryFormatter();
        private static MemoryStream stream = new MemoryStream();
        private static ILog log = LogManager.GetLogger("System");

        public static ImportTemplate getTemplate(string tmpName, string type)
        {
            ImportTemplate tmp;
            string[] files = Directory.GetFiles(BasePath, type + "_" + tmpName);
            if (files.Length == 0)
            {
                tmp = new ImportTemplate() { FirstRowNum = 0, HasHeadColumn = true, Type = type, Name = "新建模板" };
            }
            else
            {
                BinaryFormatter formmatter = new BinaryFormatter();
                Byte[] bytesobject;
                using (FileStream sw = new FileStream(files[0], FileMode.Open, FileAccess.Read))
                {
                    bytesobject = new byte[sw.Length];
                    int rl = sw.Read(bytesobject, 0, (int)sw.Length);
                }
                MemoryStream stream = new MemoryStream(bytesobject);
                tmp = formmatter.Deserialize(stream) as ImportTemplate;
            }
            return tmp;
        }
        public static bool SaveTemplate(ImportTemplate tmp)
        {
            BinaryFormatter formmatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            Byte[] bytesobject;
            formmatter.Serialize(stream, tmp);
            bytesobject = stream.ToArray();
            using (FileStream sw = new FileStream(BasePath + tmp.Type + "_" + tmp.Name, FileMode.OpenOrCreate, FileAccess.Write))
            {
                sw.Write(bytesobject, 0, bytesobject.Length);
            }
            if (tmp.Id != "0")
            {
                DBA.MySqlDbAccess.ExecNoQuery(System.Data.CommandType.Text, "update cfg_template set name='" + tmp.Name + "',dbtype='" + tmp.Type + "'  where id=" + tmp.Id);
            }
            else
            {
                DBA.MySqlDbAccess.ExecNoQuery(System.Data.CommandType.Text, "insert into cfg_template (name,dbtype,filetype,path)  values ('" + tmp.Name + "','" + tmp.Type + "','xls|xlsx','')");
            }
            log.Info("保存模板：" + tmp.Name + "\t" + DateTime.Now);
            return true;
        }

        public static bool Delete(string Name)
        {
            try
            {
                File.Delete(BasePath + Name);
                log.Info("删除模板：" + Name + "\t" + DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return false;
            }
        }
    }
}
