using System;
using System.Windows.Forms;
using System.Xml;

namespace ModelApp
{
    public class ParamSetting
    {
        /// <summary>
        /// 读取配置文件的值(XML)
        /// </summary>
        /// <param name="configPath">配置文件名</param>
        /// <param name="key">配置项名</param>
        /// <returns></returns>
        public static string GetConfigValue(string configPath, string key)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //string configPath = "config.xml";
            xmlDoc.Load(configPath);
            XmlElement xElem = xmlDoc.SelectSingleNode("//appSettings/add[@key='" + key + "']") as XmlElement;
            if (xElem != null)
                return xElem.GetAttribute("value");
            else
                return string.Empty;
        }


        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="_FilePath"></param>
        /// <param name="_Key_Value"></param>
        /// <param name="str"></param>
        public static bool SaveConfig(string configPath, string strkey, string strValue)
        {
            bool b = false;
            try
            {
                XmlDocument doc = new XmlDocument();
                //获得配置文件的全路径
                string strFileName = configPath.Trim();
                doc.Load(strFileName);
                //找出名称为“add”的所有元素
                XmlNodeList nodes = doc.GetElementsByTagName("add");
                for (int i = 0; i < nodes.Count; i++)
                {
                    //获得将当前元素的key属性
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素
                    if (att.Value == strkey)
                    {
                        //对目标元素中的第二个属性赋值
                        att = nodes[i].Attributes["value"];
                        att.Value = strValue;
                        break;
                    }
                }
                //保存上面的修改
                doc.Save(strFileName);
                b = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存配置信息出错:" + ex.Message.ToString());
            }
            return b;
        }
    }
}
