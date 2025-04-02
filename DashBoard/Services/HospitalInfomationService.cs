using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DashBoard.Services
{
    public class HospitalInfomationService
    {
        private readonly IConfiguration _configuration;

        private string filePath = "C:\\Users\\dltpw\\source\\repos\\DashBoard\\DashBoard\\test.txt";

        public HospitalInfomationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ReadHospitalInfomation()
        {
            if (!File.Exists(filePath))
            {
                return "File not found!";
            }

            foreach (var line in File.ReadLines(filePath))
            {
                if (line.StartsWith("TMAX_HOST_ADDR="))
                {
                    return line.Split('=')[1];
                }
            }

            return "Text not found!";
        }

        
    }
}
