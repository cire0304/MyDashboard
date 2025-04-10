using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static System.Windows.Forms.Design.AxImporter;

namespace DashBoard.Services
{
    public class HospitalInfomationService
    {
        private readonly IConfiguration? _configuration;
        private FileSystemWatcher? _fileSystemWatcher;

        private string _hospitalConfigPath;
        private string? FILE_PATH;
        private string? FILE_NAME;
        private string? PARSING_KEY;
        private Dictionary<string, string>? _hospitalCodeMap;

        public event Action<string>? HospitalInfoChanged;

        public HospitalInfomationService(IConfiguration configuration)
        {
            try
            {
                _configuration = configuration;

                // 병원 정보 읽기
                LoadHospitalConfig();

                // _fileSystemWatcher 초기화
                _fileSystemWatcher = new FileSystemWatcher(FILE_PATH, FILE_NAME);
                _fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
                _fileSystemWatcher.Changed += (s, e) => OnHospitalInfoChanged();
                _fileSystemWatcher.EnableRaisingEvents = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, $"{this.GetType().Name}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadHospitalConfig()
        {
            FILE_PATH = _configuration["HospitalInfomationService:FilePath"] ?? throw new Exception("FilePath not configured");
            FILE_NAME = _configuration["HospitalInfomationService:FileName"] ?? throw new Exception("FileName not configured");
            PARSING_KEY = _configuration["HospitalInfomationService:ParsingKey"] ?? throw new Exception("ParsingKey not configured");
            _hospitalConfigPath = _configuration["HospitalInfomationService:HospitalConfigFile"] ?? throw new Exception("_hospitalConfigPath not configured");
            _hospitalConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _hospitalConfigPath);

            if (File.Exists(_hospitalConfigPath))
            {
                var json = File.ReadAllText(_hospitalConfigPath);
                var jsonObj = Newtonsoft.Json.Linq.JObject.Parse(json);
                _hospitalCodeMap = jsonObj["HospitalCodeMap"]?.ToObject<Dictionary<string, string>>() ?? new Dictionary<string, string>();
            }
            else
            {
                _hospitalCodeMap = new Dictionary<string, string>();
            }
        }

        private void OnHospitalInfoChanged()
        {
            HospitalInfoChanged?.Invoke(ReadHospitalInfomation());       
        }

        public string ReadHospitalInfomation()
        {
            try
            {
                string file = Path.Combine(FILE_PATH, FILE_NAME);
                if (!File.Exists(file))
                {
                    return "File not found!";
                }

                using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(stream))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith(PARSING_KEY))
                        {
                            string value = line.Split('=')[1].Trim();
                            
                            if (_hospitalCodeMap.TryGetValue(value, out var hospitalName))
                            {
                                return hospitalName;
                            }

                            return "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러 발생", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return "";
        }

        private void SaveHospitalConfig()
        {
            var jsonObj = new Newtonsoft.Json.Linq.JObject
            {
                ["HospitalCodeMap"] = Newtonsoft.Json.Linq.JObject.FromObject(_hospitalCodeMap)
            };
            File.WriteAllText(_hospitalConfigPath, jsonObj.ToString());
        }

        public void UpdateHospitalCode(string newHospitalName)
        {
            // 예를 들어 현재 파일에서 읽은 병원 코드가
            string? currentCode = GetCurrentHospitalCode();

            if (string.IsNullOrWhiteSpace(currentCode)) return;

            _hospitalCodeMap[currentCode] = newHospitalName;

            SaveHospitalConfig();
        }

        public string? GetCurrentHospitalCode()
        {
            string file = Path.Combine(FILE_PATH, FILE_NAME);

            if (!File.Exists(file)) return null;

            using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(PARSING_KEY))
                    {
                        return line.Split('=')[1].Trim();
                    }
                }
            }

            return null;
        }
    }
}
