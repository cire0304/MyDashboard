namespace DashBoard.Models.IntroLauncher
{
    public class ProgramItem
    {
        private string? name;
        private string? path;

        public string? Name { get => name; set => name = value; }
        public string? Path { get => path; set => path = value; }

        public ProgramItem(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
