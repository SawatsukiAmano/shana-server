using CommonHelper;
using DbMigrator;
using log4net;
using log4net.Config;

//[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
Init.InitDB();
//Console.WriteLine(TomlSettings.ReadNode("ConnectionStrings.SqlEngine"));
//Console.WriteLine(TomlSettings.ReadNode("Test"));
//Console.WriteLine(TomlSettings.ReadNode("ConnectionStrings:PgSql"));
string path = "D:\\夏娜";

//var logger = LogManager.GetLogger(typeof(Program));
//logger.Debug("Debug");
//logger.Info("Info");
//logger.Warn("Warn");
//logger.Error("Error");
//logger.Fatal("Fatal");
var lo=new Log<Program>();
lo._log4.Debug("Debug");
lo._log4.Info("Info");
lo._log4.Warn("Warn");
lo._log4.Error("Error");
lo._log4.Fatal("Fatal");
lo._log4.Fatal("TT");
Console.WriteLine(path);
//Console.WriteLine(string.Join(",", list));