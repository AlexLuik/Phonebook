namespace PhonebookCRUD.Models;

// Телефонная книга
public class Phonebook
{
    private const string Filename = "phonebook.txt";
    private static Phonebook? _phonebook;
    private Action<string>? _onAddContact;
    private Action<string>? _onUpdateContact;
    private Action<string>? _onRemoveContact;
    private Action<string>? _onError;

    public event Action<string> OnAddContact
    {
        add => _onAddContact += value;
        remove
        {
            if (_onAddContact is not null)
                _onAddContact -= value;
        }
    }

    public event Action<string> OnUpdateContact
    {
        add => _onUpdateContact += value;
        remove
        {
            if (_onUpdateContact is not null)
                _onUpdateContact -= value;
        }
    }

    public event Action<string> OnRemoveContact
    {
        add => _onRemoveContact += value;
        remove
        {
            if (_onRemoveContact is not null)
                _onRemoveContact -= value;
        }
    }

    public event Action<string> OnError
    {
        add => _onError += value;
        remove
        {
            if (_onError is not null)
                _onError -= value;
        }
    }

    private Phonebook()
    {
        if (!File.Exists(Filename))
            File.Create(Filename).Close();
    }

    
    // Метод получения экземпляра класса
 
    public static Phonebook GetInstance()
    {
        if (_phonebook is null)
            _phonebook = new Phonebook();
        return _phonebook;
    }


    // Получить список всех контактов

    public async Task<List<Abonent>> GetContactsAsync()
    {
        var contacts = new List<Abonent>();
        var lines = await File.ReadAllLinesAsync(Filename);
        foreach (var line in lines)
        {
            var data = line.Split('-');
            contacts.Add(new Abonent(data[0], data[1]));
        }
        return contacts;
    }

    
    // Поиск контакта по имени
  
    public async Task<Abonent?> FindContactAsync(string name)
    {
        var contacts = await GetContactsAsync();
        return contacts.FirstOrDefault(c => c.Name == name);
    }

 
    // Добавить контакт
  
    public async void AddContact(Abonent contact)
    {
        var contacts = await GetContactsAsync();
        var index = contacts.FindIndex(c => c.Name == contact.Name);
        if (index == -1)
        {
            _onAddContact?.Invoke($"Контакт {contact.Name} добавлен");
            await File.AppendAllTextAsync(Filename, $"{contact}\n");
        }
        else
        {
            _onError?.Invoke($"Контакт {contact.Name} уже существует");
        }
    }

  
    // Добавить контакт

    public void AddContact(string name, string phoneNumber)
    {
        var contact = new Abonent(name, phoneNumber);
        this.AddContact(contact);
    }


    // Обновить номер контакта
   
    public async void UpdateContact(Abonent contact)
    {
        var contacts = await GetContactsAsync();
        var index = contacts.FindIndex(c => c.Name == contact.Name);
        if (index >= 0)
        {
            var oldContact = contacts[index];
            if (!oldContact.Equals(contact))
            {
                _onUpdateContact?.Invoke($"Контакт {contact.Name} обновлён");
                await File.WriteAllLinesAsync(Filename, contacts.Select(c => c.ToString()));
            }
        }
        else
        {
            _onError?.Invoke($"Контакт {contact.Name} не найден");
        }
    }

 
    // Обновить номер контакта
   
    public void UpdateContact(string name, string phoneNumber)
    {
        var contact = new Abonent(name, phoneNumber);
        this.UpdateContact(contact);
    }

 
    // Удалить контакт
   
   
    public async void DeleteContact(Abonent contact)
    {
        var contacts = await GetContactsAsync();
        var index = contacts.FindIndex(c => c.Name == contact.Name);
        if (index >= 0)
        {
            contacts.RemoveAt(index);
            _onRemoveContact?.Invoke($"Контакт {contact.Name} удалён");
            await File.WriteAllLinesAsync(Filename, contacts.Select(c => c.ToString()));
        }
        else
        {
            _onError?.Invoke($"Контакт {contact.Name} не найден");
        }
    }


    // Удалить контакт
   
    
    public async void DeleteContact(string name)
    {
        var contact = await this.FindContactAsync(name);
        if (contact is not null)
        {
            this.DeleteContact(contact);
        }
        else
        {
            _onError?.Invoke($"Контакт {name} не найден");
        }
    }
}