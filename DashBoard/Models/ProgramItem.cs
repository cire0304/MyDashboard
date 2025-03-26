namespace DashBoard.Models.IntroLauncher
{
    public class ProgramItem
    {
        private string? name;
        private string? path;
        private string? hpno;
        private string? sykd;
        private string? sskd;

        public string? Name { get => name; set => name = value; }
        public string? Path { get => path; set => path = value; }
        public string? Hpno { get => hpno; set => hpno = value; }
        public string? Sykd { get => sykd; set => sykd = value; }
        public string? Sskd { get => sskd; set => sskd = value; }

        public string getArgument(string id, string password)
        {
            return $"{Hpno} {Sykd} {Sskd} {id} {password}";
        }
    }
}
