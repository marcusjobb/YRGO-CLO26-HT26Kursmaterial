# Format-jämförelse

🟢

Samma data uttryckt i JSON, XML, CSV och BSON — och när du väljer vilket.

---

## Samma data i olika format

**JSON:**
```json
{
  "id": 1,
  "name": "Anna Andersson",
  "email": "anna@example.com",
  "address": {
    "street": "Storgatan 1",
    "city": "Stockholm",
    "zip": "11122"
  },
  "tags": ["vip", "premium"]
}
```

**XML:**
```xml
<?xml version="1.0" encoding="UTF-8"?>
<customer>
  <id>1</id>
  <name>Anna Andersson</name>
  <email>anna@example.com</email>
  <address>
    <street>Storgatan 1</street>
    <city>Stockholm</city>
    <zip>11122</zip>
  </address>
  <tags>
    <tag>vip</tag>
    <tag>premium</tag>
  </tags>
</customer>
```

**CSV:**
```csv
id,name,email,address_street,address_city,address_zip,tags
1,"Anna Andersson","anna@example.com","Storgatan 1","Stockholm","11122","vip;premium"
```

**BSON (konceptuellt — binärt format):**
```
\x16\x00\x00\x00               // Document size
\x10id\x00\x01\x00\x00\x00    // int32 id = 1
\x02name\x00\x10\x00\x00\x00Anna Andersson\x00
...
```

---

## Jämförelsetabell

| Funktion | JSON | XML | CSV | BSON |
|----------|------|-----|-----|------|
| **Läsbarhet** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐ (binärt) |
| **Kompakthet** | ⭐⭐⭐⭐ | ⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ |
| **Nested data** | ✅ | ✅ | ❌ | ✅ |
| **Datatyper** | Begränsade | String-baserade | Inga | Rika (Date, ObjectId, etc.) |
| **Parsing-hastighet** | Snabb | Långsam | Mycket snabb | Mycket snabb |
| **Användning** | API:er, config | SOAP, äldre system | Excel, data-export | MongoDB |

---

Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
