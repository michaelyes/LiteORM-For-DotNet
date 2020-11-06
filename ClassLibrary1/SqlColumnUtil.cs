namespace YEasyModel
{
    public class SqlColumnUtil
    {
        /// <summary>
        /// 字段ISNULL处理
        /// </summary>
        /// <param name="column"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string IsNull(string column, string defaultValue)
        {
            return defaultValue;
        }

        /// <summary>
        /// 字段ISNULL处理
        /// </summary>
        /// <param name="column"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int IsNull(int column, int defaultValue)
        {
            return defaultValue;
        }

        /// <summary>
        /// 字段ISNULL处理
        /// </summary>
        /// <param name="column"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long IsNull(long column, long defaultValue)
        {
            return defaultValue;
        }

        /// <summary>
        /// 字段ISNULL处理
        /// </summary>
        /// <param name="column"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal IsNull(decimal column, decimal defaultValue)
        {
            return defaultValue;
        }
    }
}
