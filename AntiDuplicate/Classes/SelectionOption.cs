namespace AntiDuplicate.Classes;

public class SelectionOption(string name, int id)
{
    public string Name { get; set; } = name;
    public int Id { get; set; } = id;
}