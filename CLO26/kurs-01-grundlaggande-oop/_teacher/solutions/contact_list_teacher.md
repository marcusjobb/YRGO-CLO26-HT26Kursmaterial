# Facit — Kontaktlistan (vecka 06)

Motsvarar: `06_datastrukturer/assignment/contact_list.md`

---

## G-lösning

```csharp
// ContactList.cs
// Kontakthanteringsprogram — vecka 06

class Contact
{
    // Properties för kontaktens uppgifter
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }

    // Konstruktor som sätter alla tre värden
    public Contact(string name, string phone, string email)
    {
        Name = name;
        Phone = phone;
        Email = email;
    }
}

class ContactList
{
    static void Main()
    {
        // Skapa listan och lägg in tre förinlagda kontakter
        List<Contact> contacts = new List<Contact>();
        contacts.Add(new Contact("Anna Andersson", "070-1234567", "anna@mail.se"));
        contacts.Add(new Contact("Erik Berg", "073-9876543", "erik@mail.se"));
        contacts.Add(new Contact("Maria Lind", "076-5554433", "maria@mail.se"));

        bool running = true;

        // Huvudloop — körs tills användaren väljer att avsluta
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== KONTAKTLISTA ===");
            Console.WriteLine("1. Lägg till kontakt");
            Console.WriteLine("2. Visa alla kontakter");
            Console.WriteLine("3. Sök kontakt");
            Console.WriteLine("4. Avsluta");
            Console.Write("Val: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                // Läs in uppgifter från användaren
                Console.Write("Namn: ");
                string name = Console.ReadLine();

                Console.Write("Telefon: ");
                string phone = Console.ReadLine();

                Console.Write("E-post: ");
                string email = Console.ReadLine();

                contacts.Add(new Contact(name, phone, email));
                Console.WriteLine("Kontakt tillagd.");
            }
            else if (choice == "2")
            {
                // Visa alla kontakter med löpnummer
                Console.WriteLine();
                Console.WriteLine("Kontakter (" + contacts.Count + " st):");

                for (int i = 0; i < contacts.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + contacts[i].Name + " — " + contacts[i].Phone + " — " + contacts[i].Email);
                }
            }
            else if (choice == "3")
            {
                // Sök på namn
                Console.Write("Ange namn att söka på: ");
                string searchTerm = Console.ReadLine();

                bool found = false;

                foreach (Contact contact in contacts)
                {
                    if (contact.Name.ToLower().Contains(searchTerm.ToLower()))
                    {
                        Console.WriteLine("Hittade: " + contact.Name + " — " + contact.Phone + " — " + contact.Email);
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Ingen kontakt hittades med det namnet.");
                }
            }
            else if (choice == "4")
            {
                running = false;
                Console.WriteLine("Hejdå!");
            }
            else
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
            }
        }
    }
}
```

---

## VG-lösning (uppdelad i metoder, med validering)

```csharp
// ContactList.cs
// Kontakthanteringsprogram med metoduppdelning — vecka 06 (VG)

class Contact
{
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }

    public Contact(string name, string phone, string email)
    {
        Name = name;
        Phone = phone;
        Email = email;
    }
}

class ContactList
{
    // Lägger till en ny kontakt med validering av inmatning
    static void AddContact(List<Contact> contacts)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();

        if (name.Trim() == "")
        {
            Console.WriteLine("Namn kan inte vara tomt. Kontakt lades inte till.");
            return;
        }

        Console.Write("Telefon: ");
        string phone = Console.ReadLine();

        Console.Write("E-post: ");
        string email = Console.ReadLine();

        contacts.Add(new Contact(name, phone, email));
        Console.WriteLine("Kontakt tillagd.");
    }

    // Skriver ut alla kontakter med löpnummer
    static void ShowAllContacts(List<Contact> contacts)
    {
        Console.WriteLine();
        Console.WriteLine("Kontakter (" + contacts.Count + " st):");

        for (int i = 0; i < contacts.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + contacts[i].Name + " — " + contacts[i].Phone + " — " + contacts[i].Email);
        }
    }

    // Söker kontakter på namn — skiftlägesokänslig
    static void SearchContact(List<Contact> contacts)
    {
        Console.Write("Ange namn att söka på: ");
        string searchTerm = Console.ReadLine().ToLower();

        bool found = false;

        foreach (Contact contact in contacts)
        {
            if (contact.Name.ToLower().Contains(searchTerm))
            {
                Console.WriteLine("Hittade: " + contact.Name + " — " + contact.Phone + " — " + contact.Email);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Ingen kontakt hittades med det namnet.");
        }
    }

    static void Main()
    {
        List<Contact> contacts = new List<Contact>();
        contacts.Add(new Contact("Anna Andersson", "070-1234567", "anna@mail.se"));
        contacts.Add(new Contact("Erik Berg", "073-9876543", "erik@mail.se"));
        contacts.Add(new Contact("Maria Lind", "076-5554433", "maria@mail.se"));

        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== KONTAKTLISTA ===");
            Console.WriteLine("1. Lägg till kontakt");
            Console.WriteLine("2. Visa alla kontakter");
            Console.WriteLine("3. Sök kontakt");
            Console.WriteLine("4. Avsluta");
            Console.Write("Val: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                AddContact(contacts);
            }
            else if (choice == "2")
            {
                ShowAllContacts(contacts);
            }
            else if (choice == "3")
            {
                SearchContact(contacts);
            }
            else if (choice == "4")
            {
                running = false;
                Console.WriteLine("Hejdå!");
            }
            else
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
            }
        }
    }
}
```

---

## Bedömningskommentarer

**G-kontroll:**
- `Contact`-klassen finns med rätt properties och konstruktor
- `List<Contact>` används korrekt
- Minst 3 förinlagda kontakter
- `while`-loop med alla fyra menyval
- Sök fungerar och rapporterar om inget hittas

**Vanliga G-misstag att titta efter:**
- Listan deklareras som `List<string>` istället för `List<Contact>`
- Studerande glömmer att loopa för att visa alla kontakter och skriver ut bara ett objekt
- `while (true)` utan korrekt `break` eller flaggvariabel — acceptabelt men noterbart

**VG-kontroll:**
- `AddContact`, `ShowAllContacts`, `SearchContact` är egna metoder
- `.ToLower().Contains(...)` används för skiftlägesokänslig sökning
- Tom inmatning avvisas (minst för namn)
- Utskriften är välformaterad och konsekvent
