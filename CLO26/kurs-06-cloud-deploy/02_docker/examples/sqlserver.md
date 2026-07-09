# SQLServer på docker

🟢


(Istället för localDB)
Ifall ni inte kan köra localdb på era datorer kan ni använda SQLServer på docker istället.

För din skull hoppas jag du har Docker installerat LOL, annars måste du installera det.

Install Docker Desktop on Mac, Windows, or Linux.

## Ladda ner SQLServer image

Kör detta i konsolen för att ladda ner SQLServer image

```bash
docker run --name sqlserver_container -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Campus@2024" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

När den startar kan du öppna SSMS och ansluta till servern med följande inställningar:

- Server: **localhost,1433**
- Användarnamn: **sa**
- Lösenord: **Campus@2024**
- [x] Remember password
- [x] Trust server certificate

Såhär ska det se ut

![SSMS inloggning](images/ssms.png)

Använd samma inställningar på Visual Studio.
![Visual Studio](images/vstudio.png)

## Connectionstring

```plaintext
Server=localhost,1433;Database=CampusDB;User Id=sa;Password=Campus@2024;
```