# Contribution Statistics

🟢


Denna action genererar en markdown-fil med statistik över hur många commits varje person har gjort, hur många rader de har lagt till och tagit bort samt vilken commit som var den senaste.

```yaml
name: Generate Contribution Statistics

on:
  push:
    branches:
      - main

jobs:
  generate-stats:
    runs-on: ubuntu-latest
    permissions:
      contents: write
    if: "github.event.pusher.email != 'action@github.com'"

    steps:
      - name: Checkout code
        uses: actions/checkout@v3
        with:
          ref: ${{ github.event.pull_request.head.sha }}
          fetch-depth: 0

      - name: Set up Git user
        run: |
          git config user.name "GitHub Action"
          git config user.email "action@github.com"

      - name: Generate statistics
        run: |
          echo "# Commit statistik" > CONTRIBUTORS.md
          echo "| Contributor | Email | Commits | Added | Deleted | Last Commit |" > CONTRIBUTORS.md
          echo "|-------------|-------|---------|-------|---------|--------------|" >> CONTRIBUTORS.md
          temp_file=$(mktemp)
          git log --format='%aN <%aE>' | sort -u | while read contributor; do
            name=$(echo $contributor | cut -d ' ' -f1,2)
            email=$(echo $contributor | cut -d '<' -f2 | cut -d '>' -f1)
            commits=$(git log --author="$email" --pretty=tformat: --numstat | grep -v -e '^$' | wc -l)
            lines_added=$(git log --author="$email" --pretty=tformat: --numstat | awk '{sum+=$1} END {print sum}')
            lines_deleted=$(git log --author="$email" --pretty=tformat: --numstat | awk '{sum+=$2} END {print sum}')
            last_commit=$(git log --author="$email" --pretty=format:"%h" -n 1)
            echo "$commits|$name|$email|$lines_added|$lines_deleted|$last_commit" >> $temp_file
          done
          sort -t'|' -k1,1 -nr $temp_file | while IFS="|" read -r commits name email lines_added lines_deleted last_commit; do
            echo "| $name | $email | $commits | $lines_added | $lines_deleted | $last_commit |" >> CONTRIBUTORS.md
          done
          rm $temp_file

      - name: Add and commit CONTRIBUTORS.md file
        run: |
          git add CONTRIBUTORS.md
          git commit -m "Update CONTRIBUTORS.md file" || echo "No changes to commit"

      - name: Push changes
        run: |
          git push

      - name: Yay!
        run: echo Marcus Was Here!
```

## Förklaring av koden

### Name

Namnet på actionen.

### On

När actionen ska köras. I detta fall när någon pushar till main-branchen. Man kan även köra den när någon pushar till en pull request eller när någon skapar en pull request.

Exempelvis:

```yaml
on:
  pull_request:
    branches:
      - main
```

### Jobs

En jobb är en samling av steg som ska köras. I detta fall har vi bara ett jobb som heter `generate-stats`.

### Runs-on

Vilken typ av maskin som jobbet ska köras på. I detta fall kör vi på en ubuntu-maskin, men kan även köra på en windows-maskin eller en mac-maskin.

Exempelvis:

```yaml
runs-on: windows-latest
```

eller

```yaml
runs-on: macos-latest
```

### Permissions

Vilka rättigheter actionen ska ha. I detta fall behöver vi rättigheten `contents: write` för att kunna skriva till repot.

### If

Villkor för när actionen ska köras. I detta fall vill vi inte att actionen ska köras när vi pushar till repot, utan bara när någon annan pushar till repot. Därför kollar vi om `github.event.pusher.email` är `action@github.com` och om det är det så kör vi inte actionen. Detta för att slippa att automatiserade commits triggar actionen.

### Steps

Stegen som ska köras i jobbet. I detta fall har vi 7 steg. Varje steg har ett namn, en beskrivning och en kommando som ska köras.

#### Checkout code

Detta steg klonar repot till den maskin som actionen körs på.

#### Set up Git user

Detta steg sätter upp en användare för git som actionen kan använda för att göra commits. Du kan ange dina egna uppgifter här om du vill, men då rekommenderas att du använder en github secret för att inte lägga ut dina uppgifter publikt.

#### Generate statistics

- Detta steg genererar statistiken. Detta görs genom att först skapa en markdown-fil med en tabell med rubriker.
- Sedan skapar vi en temporär fil där vi skriver ut statistiken för varje person.
- Sedan sorterar vi den temporära filen efter antal commits och skriver ut den sorterade filen till markdown-filen.
- Slutligen tar vi bort den temporära filen.

#### Add and commit CONTRIBUTORS.md file

Detta steg lägger till och committar den genererade markdown-filen till repot. Om det inte finns några ändringar att committa så ignoreras felet.

#### Push changes

Detta steg pushar ändringarna till repot.

#### Yay!

Detta steg skriver ut en text i loggen för att visa att actionen har körts.

## Exempel på resultat

| Contributor | Email                   | Commits | Added | Deleted | Last Commit |
| ----------- | ----------------------- | ------- | ----- | ------- | ----------- |
| Robin       | robin@cave.org          | 152     | 1     | 5       | 1234567     |
| Superman    | superman@metropolis.org | 25      | 2     | 8       | 1234567     |
| Batman      | batman@cave.org         | 20      | 2     | 21      | 1234567     |
| Joker       | joker@lol.com           | 1       | 1     | 9999    | 1234567     |

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
