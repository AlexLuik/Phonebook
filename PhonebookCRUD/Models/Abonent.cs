namespace PhonebookCRUD.Models;

public class Abonent
{
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public Abonent(string name, string phoneNumber)
    {
        this.Name = name;
        this.PhoneNumber = phoneNumber;
    }

    public override string ToString()
    {
        return $"{Name}:{PhoneNumber}";
    }

    public override bool Equals(object? obj)
    {
        var contact = obj as Abonent;

        if (contact is null)
            return false;

        return (contact.Name == this.Name && contact.PhoneNumber == this.PhoneNumber);
    }
}
