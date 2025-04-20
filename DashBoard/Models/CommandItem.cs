namespace DashBoard.Models
{
    public class CommandItem
    {
        private string? name;
        private string? fileName;
        private string? arguments;

        public string? Name { get => name; set => name = value; }
        public string? FileName { get => fileName; set => fileName = value; }
        public string? Arguments { get => arguments; set => arguments = value; }
    }
}
