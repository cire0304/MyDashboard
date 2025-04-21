using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using DashBoard.Models;
using DashBoard.Utils;
using Microsoft.Extensions.Configuration;

namespace DashBoard.Services
{
    public class HospitalInfomationService
    {
        private readonly IConfiguration? _configuration;       

        private readonly string HOSPITAL_INFO_MAP = "HospitalInfoMap";
        private readonly string HOSPITAL_INFO_FILE;
        private readonly string HOSPTIAL_CONFIG_DIRECTORY;
        private readonly List<string> HOSPITAL_CONFIG_FILES;

        private Dictionary<string, HospitalInfo>? _hospitalnfoMap;

        public event Action<HospitalInfo>? HospitalInfoChanged;

        public HospitalInfomationService(IConfiguration configuration)
        {
            _configuration = configuration;

            // 병원 로그인 정보 읽기
            try
            {
                // TMax Connection config 파일 설정
                HOSPTIAL_CONFIG_DIRECTORY = _configuration["HospitalInfomationService:ConnectionConfigDirectory"] ?? throw new Exception("ConnectionConfigDirectory not configured");
                HOSPITAL_CONFIG_FILES = _configuration.GetSection("HospitalInfomationService:ConnectionConfigFiles").Get<List<string>>() ?? throw new Exception("ConnectionConfigFiles not configured");

                // 병원 아이피, 이름, 아이디 및 비밀번호 관련된 파일 경로
                HOSPITAL_INFO_FILE = _configuration["HospitalInfomationService:HospitalInfoFile"] ?? throw new Exception("_hospitalConfigPath not configured");
                HOSPITAL_INFO_FILE = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HOSPITAL_INFO_FILE);

                if (File.Exists(HOSPITAL_INFO_FILE))
                {
                    var json = File.ReadAllText(HOSPITAL_INFO_FILE);
                    var jsonObj = Newtonsoft.Json.Linq.JObject.Parse(json);
                    _hospitalnfoMap = jsonObj[HOSPITAL_INFO_MAP]
                        ?.ToObject<Dictionary<string, HospitalInfo>>()
                        ?? new Dictionary<string, HospitalInfo>();
                }
                else
                {
                    _hospitalnfoMap = new Dictionary<string, HospitalInfo>();
                }

            }
            catch (Exception ex)
            {
                _hospitalnfoMap = new Dictionary<string, HospitalInfo>();
                MessageBox.Show(ex.Message, $"{this.GetType().Name}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                        
        }

        private string GetHospitalFilePath()
        {
            return HOSPITAL_CONFIG_FILES
                .Select(name => Path.Combine(HOSPTIAL_CONFIG_DIRECTORY!, name))
                .FirstOrDefault(File.Exists) ?? throw new FileNotFoundException("GetHospitalFilePath Error"); ;
        }

        public HospitalInfo ReadHospitalInfomation()
        {            
            try
            {
                
                string file = GetHospitalFilePath();

                var connectionConfig = ConfigLoader.LoadConfig(file);

                if (_hospitalnfoMap!.TryGetValue(connectionConfig.Host, out var hospitalName))
                {
                    return hospitalName;
                }

                return new HospitalInfo();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, ex.FileName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러 발생[ReadHospitalInfomation]", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return new HospitalInfo() ;
        }

        private void SaveHospitalConfig()
        {
            var jsonObj = new Newtonsoft.Json.Linq.JObject
            {
                [HOSPITAL_INFO_MAP] = Newtonsoft.Json.Linq.JObject.FromObject(_hospitalnfoMap)
            };
            File.WriteAllText(HOSPITAL_INFO_FILE, jsonObj.ToString());
        }

        public void UpdateHospitalInfo(HospitalInfo newHospitalName)
        {
            string? currentCode = GetCurrentHospitalCode();

            if (string.IsNullOrWhiteSpace(currentCode)) return;

            _hospitalnfoMap[currentCode] = newHospitalName;

            SaveHospitalConfig();
        }

        public string? GetCurrentHospitalCode()
        {
            try
            {
                string file = GetHospitalFilePath();

                if (!File.Exists(file)) return null;

                var connectionConfig = ConfigLoader.LoadConfig(file);
                return connectionConfig.Host;
            }
            catch(FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, ex.FileName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "에러 발생[GetCurrentHospitalCode]", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return "";
        }
    }
}
