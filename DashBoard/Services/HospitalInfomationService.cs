using System.IO;
using System.Windows;
using DashBoard.Utils;
using Microsoft.Extensions.Configuration;

namespace DashBoard.Services
{
    public class HospitalInfomationService
    {
        private readonly IConfiguration? _configuration;
        private FileSystemWatcher? _fileSystemWatcher;

        private string _hospitalConfigPath;
        private string? FILE_PATH;
        private string? FILE_NAME;
        private List<string>? FILE_EXTENTIONS;       
        private Dictionary<string, string>? _hospitalCodeMap;

        // MPM에서 병원 정보가 변경되면 실행
        public event Action<string>? HospitalInfoChanged;

        public HospitalInfomationService(IConfiguration configuration)
        {
            try
            {
                _configuration = configuration;

                // 병원 정보 읽기
                LoadHospitalConfig();

                // _fileSystemWatcher 초기화
                _fileSystemWatcher = new FileSystemWatcher(FILE_PATH);
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
            try
            {
                // TMax Connection config 파일 설정
                FILE_PATH = _configuration["HospitalInfomationService:FilePath"] ?? throw new Exception("FilePath not configured");
                FILE_NAME = _configuration["HospitalInfomationService:FileName"] ?? throw new Exception("FileName not configured");
                FILE_EXTENTIONS = _configuration
                    .GetSection("HospitalInfomationService:FileExtenstions")
                    .Get<List<string>>() ?? throw new Exception("FileExtensions not configured");                

                // 병원 아이피, 이름, 아이디 및 비밀번호 관련된 파일 경로
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
            catch (Exception ex)
            {
                _hospitalCodeMap = new Dictionary<string, string>();
                MessageBox.Show(ex.Message, $"{this.GetType().Name}", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void OnHospitalInfoChanged()
        {
            HospitalInfoChanged?.Invoke(ReadHospitalInfomation());       
        }

        private string GetHospitalFilePath()
        {
            string basePath = Path.Combine(FILE_PATH!, FILE_NAME!);
            return FILE_EXTENTIONS!
                    .Select(ext => basePath + ext)
                    .FirstOrDefault(File.Exists)!;
        }

        public string ReadHospitalInfomation()
        {
            try
            {
                string file = GetHospitalFilePath();

                if (!File.Exists(file))
                {
                    return "File not found!";
                }

                var connectionConfig = ConfigLoader.LoadConfig(file);

                if (_hospitalCodeMap!.TryGetValue(connectionConfig.Host, out var hospitalName))
                {
                    return hospitalName;
                }
                return "";
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
            string file = GetHospitalFilePath();

            if (!File.Exists(file)) return null;

            var connectionConfig = ConfigLoader.LoadConfig(file);
            return connectionConfig.Host;
        }
    }
}
