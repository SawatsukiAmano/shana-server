using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomlyn;
using Tomlyn.Model;

namespace CommonHelper
{
    public static class TomlSettings
    {
        private static string ConfigText = string.Empty;
        private static string TomlPath = string.Empty;
        private static TomlTable TomlModel = null;


        public static string ReadNode(string sessions)
        {
            if (string.IsNullOrWhiteSpace(TomlPath))
            {
                TomlPath = "appsettings.toml";
                if (File.Exists("appsettings.Development.toml")) TomlPath = "appsettings.Development.toml";
            }
            if (string.IsNullOrWhiteSpace(ConfigText)) ConfigText = Fileopera.ReadAllText(TomlPath);
            if (TomlModel == null) TomlModel = Toml.ToModel(ConfigText);
            var list = sessions.Split(":");
            if (list.Length == 1) return (string)TomlModel[sessions];
            var tab = new TomlTable();
            foreach (var item in list)
            {
                if(item!=list.LastOrDefault())
                {
                    tab = (TomlTable)TomlModel[item];
                }
            }
            return (string)tab[list.LastOrDefault()];

        }

    }
}
