using IniParser.Model;
using IniParser;
using Newtonsoft.Json;
using DashBoard.Models;
using System.IO;

namespace DashBoard.Utils
{
    public static class ConfigLoader
    {
        public static ConnectionConfig LoadConfig(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();

            if (ext == ".json")
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<ConnectionConfig>(json);
            }
            else if (ext == ".ini")
            {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(filePath);

                return new ConnectionConfig
                {
                    Host = data["MediCare"]["TMAX_HOST_ADDR"],
                    Port = data["MediCare"]["TMAX_HOST_PORT"],
                    // INI엔 없는 값은 null로 둠
                    IsEncrypted = null,
                    UrlScheme = null,
                    BaseUr1 = null
                };
            }
            else
            {
                throw new NotSupportedException("지원되지 않는 확장자입니다.");
            }
        }
    }
}
