using System.Windows.Forms;

namespace ModelApp
{
    public class Global
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public static string config_file = System.IO.Path.GetFileName(Application.ExecutablePath) + ".config";
    }
}
