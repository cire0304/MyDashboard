using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;

namespace DashBoard.Services
{
    public class HospitalInfomationService
    {
        private readonly IConfiguration _configuration;
        private FileSystemWatcher _fileSystemWatcher;

        private string FILE_PATH = "C:\\Users\\dltpw\\source\\repos\\DashBoard\\DashBoard";
        private string FILE_NAME = "test.txt";
        private string PARSING_KEY = "TMAX_HOST_ADDR";

        public event Action<string>? HospitalInfoChanged;

        private void OnHospitalInfoChanged()
        {
            HospitalInfoChanged?.Invoke(ReadHospitalInfomation());
        }

        public HospitalInfomationService(IConfiguration configuration)
        {
            _configuration = configuration;

            _fileSystemWatcher = new FileSystemWatcher(FILE_PATH, FILE_NAME);
            _fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;

            _fileSystemWatcher.Changed += (s, e) => OnHospitalInfoChanged();
            _fileSystemWatcher.EnableRaisingEvents = true;
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
                            return line.Split('=')[1];
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러 발생", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return "Text not found!";
        }
    }
}
