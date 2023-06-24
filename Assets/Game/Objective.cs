public class Objective 
{
    string title;
    string description;

    public Objective(string title, string description) {
        this.title = title;
        this.description = description;
    }

    public override string ToString()
    {
        return $"{description}";
    }
}
