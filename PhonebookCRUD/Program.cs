using PhonebookCRUD.Models;


var phonebook = Phonebook.GetInstance();
phonebook.OnAddContact += DisplayAddContact;
phonebook.OnUpdateContact += DisplayUpdateContact;
phonebook.OnRemoveContact += DisplayRemoveContact;
phonebook.OnError += DisplayOnError;

string? errorMessage = null;

while (true)
{
    Console.Clear();
    Console.WriteLine("1. Список контактов");
    Console.WriteLine("2. Добавить контакт");
    Console.WriteLine("3. Обновить контакт");
    Console.WriteLine("4. Удалить контакт");
    Console.WriteLine("5. Выйти");
    if (errorMessage != null) Console.WriteLine(errorMessage);
    Console.Write("Введите номер команды: ");
    int choice;
    try
    {
        choice = int.Parse(Console.ReadLine());
        errorMessage = null;
    }
    catch (Exception e)
    {
        errorMessage = "Произошла неизвестная ошибка";
        continue;
    }

    switch (choice)
    {
        case 1:
        {
            var contactList = await phonebook.GetContactsAsync();

            Console.Clear();
            Console.WriteLine("Список контактов");
            for (var i = 0; i < contactList.Count; i++)
                Console.WriteLine($"{i + 1}. {contactList[i]}");
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadKey();
            break;
        }
        case 2:
        {
            var contactList = await phonebook.GetContactsAsync();

            Console.Clear();
            Console.WriteLine("Список контактов");
            for (var i = 0; i < contactList.Count; i++)
                Console.WriteLine($"{i + 1}. {contactList[i]}");

            Console.WriteLine("\nДобавление контакта");
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Write("Введите номер телефона: ");
            string phoneNumber = Console.ReadLine();

            phonebook.AddContact(name, phoneNumber);

            Thread.Sleep(2000);
            break;
        }
        case 3:
        {
            var contactList = await phonebook.GetContactsAsync();
            errorMessage = null;

            Console.Clear();
            Console.WriteLine("Список контактов");
            for (var i = 0; i < contactList.Count; i++)
                Console.WriteLine($"{i + 1}. {contactList[i]}");

            Console.WriteLine("\nОбновление контакта");
            if (errorMessage != null)
                Console.WriteLine(errorMessage);
            Console.Write("Введите имя контакта: ");
            string name = Console.ReadLine();

            Console.Write("Введите новый номер телефона: ");
            string newPhoneNumber = Console.ReadLine();

            phonebook.UpdateContact(name, newPhoneNumber);

            Thread.Sleep(2000);
            break;
        }
        case 4:
        {
            var contactList = await phonebook.GetContactsAsync();
            errorMessage = null;

            Console.Clear();
            Console.WriteLine("Список контактов");
            for (var i = 0; i < contactList.Count; i++)
                Console.WriteLine($"{i + 1}. {contactList[i]}");

            Console.WriteLine("\nУдаление контакта");
            if (errorMessage != null) Console.WriteLine(errorMessage);
            Console.Write("Введите имя контакта: ");
            string name = Console.ReadLine();

            phonebook.DeleteContact(name);

            Thread.Sleep(2000);
            break;
        }
        case 5:
        {
            return;
        }
    }

    errorMessage = null;
}

void DisplayAddContact(string contactData)
{
    var prevConsoleBackgroundColor = Console.BackgroundColor;
    Console.BackgroundColor = ConsoleColor.Green;
    Console.WriteLine(contactData);
    Console.BackgroundColor = prevConsoleBackgroundColor;
}

void DisplayUpdateContact(string contactData)
{
    var prevConsoleBackgroundColor = Console.BackgroundColor;
    Console.BackgroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine(contactData);
    Console.BackgroundColor = prevConsoleBackgroundColor;
}

void DisplayRemoveContact(string contactData)
{
    var prevConsoleBackgroundColor = Console.BackgroundColor;
    Console.BackgroundColor = ConsoleColor.Red;
    Console.WriteLine(contactData);
    Console.BackgroundColor = prevConsoleBackgroundColor;
}

void DisplayOnError(string message)
{
    var prevConsoleBackgroundColor = Console.BackgroundColor;
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.WriteLine(message);
    Console.BackgroundColor = prevConsoleBackgroundColor;
}