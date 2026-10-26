SQL-Injections
Varför behöver vi parametrar när vi kommunicerar med databasen? Det borde väl vara enklare att bara
skicka in en SQL-sträng, precis som vi gör på SSMS. Varför krånglar vi till det i C#?
Av den enkla orsaken att vi aldrig skulle få för oss att släppa loss helt främmande människor in i vår
inloggade och öppna SSMS.
Men för att förklara det tydligare… Vi tar exemplet med Bobby Tables.

Vad hände egentligen där?
Om vi tänker oss att skolan har en databas som heter School, och en tabell kallad Students
Ungefär som denna
CREATE TABLE [dbo].[Students](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Name] [varchar](50) NULL,
[Address] [varchar](50) NULL,
[Age] [int] NULL
) ON [PRIMARY]

Nu skapar vi ett C# projekt som ska ta hand om input till den databasen. I denna skapar vi våra vanliga
properties och sedan en metod. Faktum är att vi kan göra allt i Program klassen.
internal static string ConnectionString { get; set; } = @"Data
Source=.\SQLExpress;Integrated Security=true;database={0}";
internal static string DatabaseName { get; set; } = "Population";

Vi skapar nu en metod enligt följande.
internal void AddStudent(string name, int age)
{
var sql = $"INSERT INTO Students (age,name) VALUES('{age}','{name}');";
var connString = string.Format(ConnectionString, DatabaseName);
var cnn = new SqlConnection(connString);
cnn.Open();
var command = new SqlCommand(sql, cnn);
command.ExecuteNonQuery();
}

Man måste ändå medge att det ser snyggt ut att skriva koden på detta sätt, utan parametrar… och en
massa annat kludd. Nu ska vi testa metoderna från vår Program.main()
private static void Main()
{
DatabaseName = "School";
addStudent("James Willis", 13);
AddStudent("Bruce Woods", 14);
AddStudent("Robert'); DROP TABLE Students;--", 13); // Little Bobby Tables!!!
AddStudent("Peter Wayne", 14);
AddStudent("Bruce Parker", 13);
AddStudent("Clark Stark", 13);
AddStudent("Tony Kent", 13);
}

Snyggt och enkelt, eller hur?
Kör programmet och kolla sen på tabell i SSMS hur namnen sparats.
Testa nu att köra samma sak, fast den här gången så använder du parametrar.
internal void AddStudent(string name, int age)
{
var sql = $"INSERT INTO Students (age,name) VALUES(@age,@name);";
var cmd = new SqlCommand(sqlString, Connection); // Instansiera kommando
cmd.Parameters.AddWithValue("@age", age);
// Lägg in parameter
cmd.Parameters.AddWithValue("@name", name);
// Lägg in parameter
cmd.ExecuteNonQuery();
// Exekvera

}

Kör programmet nu och kolla på din tabell i SSMS
Kolla speciellt på rad 3
När du sett resultatet på att köra utan och med
parameter så kanske det känns mer lockande att
hålla sig till parameter i fortsättningen
… och nu förstår du också humorn i seriestrippen
/Marcus
