using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace CommonHelper
{
    /// <summary>
    /// appsettings.json操作类
    /// </summary>
    public class Appsettings
    {
        private static IConfiguration _configuration;

        public Appsettings(string contentPath)
        {
            string path = "appsettings.json";

            //如果你把配置文件 是 根据环境变量来分开了，可以这样写
            path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";

            _configuration = new ConfigurationBuilder()
               .SetBasePath(contentPath)
               .Add(new JsonConfigurationSource { Path = path, Optional = false, ReloadOnChange = true })//这样的话，可以直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
               .Build();
        }

        public Appsettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region 静态方法

        public static string ReadNode(string session)
        {
            string str = _configuration[session];
            if (!string.IsNullOrEmpty(str)) return str;
            return string.Empty;
        }
        /// <summary>
        /// 读取指定节点信息
        /// </summary>
        /// <param name="sessions">节点名称</param>
        /// <returns></returns>
        public static string ReadNode(params string[] sessions)
        {
            if (sessions.Length == 1) return _configuration[sessions[0]];
            string? str = _configuration[string.Join(":", sessions)];
            if (!string.IsNullOrEmpty(str)) return str;
            return string.Empty;
        }

        /// <summary>
        /// 读取实体信息
        /// </summary>
        /// <param name="sessions">节点名称</param>
        /// <returns></returns>
        public static T ReadT<T>(params string[] sessions) where T : class, new()
        {
            T data = new();
            _configuration.Bind(string.Join(":", sessions), data);
            return data;
        }

        /// <summary>
        /// 读取实体数组信息
        /// </summary>
        /// <param name="sessions">节点名称</param>
        /// <returns></returns>
        public static List<T> ReadList<T>(params string[] sessions) where T : class
        {
            List<T> list = new();
            _configuration.Bind(string.Join(":", sessions), list);
            return list;
        }
        #endregion
    }
}
