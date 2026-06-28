---

title: Commit historik
author: Marcus Ackre Medina
type: article
topic: git
difficulty: 1
language: java
status: adapted
marcus_voice: true
source: "Old_courses/books/JIN23/github/code/history.md"
description: "Denna action genererar en markdown-fil med en lista över alla commits som har gjorts i repot och av vem."
tags: ["commit", "git", "historik", "history", "java", "sql"]
week_fit: []
---

# Commit historik

🟢


Denna action genererar en markdown-fil med en lista över alla commits som har gjorts i repot och av vem.

```yaml
name: Generate Commit History

on:
  push:
    branches:
      - main

jobs:
  generate-history:
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

      - name: Generate commit history
        run: |
          echo "# Commit historik" > HISTORY.md
          echo "| Date | Author | Email | Message | Commit Hash |" > HISTORY.md
          echo "|------|--------|-------|---------|--------------|" >> HISTORY.md
          git log --pretty=format:'| %cd | %an | %ae | %s | %h |' --date=short | grep -v 'action@github.com'  >> HISTORY.md

      - name: Add and commit HISTORY.md file
        run: |
          git add HISTORY.md
          git commit -m "Update HISTORY.md file" || echo "No changes to commit"

      - name: Push changes
        run: |
          git push

      - name: Yay!
        run: echo Marcus Was Here!
```

## Förklaring av koden

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

#### Generate commit history

Detta steg genererar en markdown-fil med en lista över alla commits som har gjorts i repot och av vem.

#### Add and commit HISTORY.md file

Detta steg lägger till och committar den genererade markdown-filen.

#### Push changes

Detta steg pushar ändringarna till repot.

#### Yay!

Detta steg skriver ut en glad hälsning i loggen.

## Exempel på resultat

| Date       | Author | Email             | Message                                                                 | Commit Hash |
| ---------- | ------ | ----------------- | ----------------------------------------------------------------------- | ----------- |
| 2021-10-08 | Batman | batman@cave.org   | Fixed Riddler's riddle in README                                        | 7d7e8f9     |
| 2021-10-08 | Robin  | robin@batcave.org | Added a riddle to the README by the Riddler, seems legit?               | 6e6f7g8     |
| 2021-10-08 | Batman | batman@cave.org   | Refactored Bat-Signal integration                                       | 5d5e6f7     |
| 2021-10-07 | Robin  | robin@batcave.org | Added support for Bat-Signal via SMS. Cool, right?                      | 4c4d5e6     |
| 2021-10-07 | Batman | batman@cave.org   | Corrected typos in Bat-Comments, Robin...                               | 3c3d4f5     |
| 2021-10-06 | Robin  | robin@batcave.org | Added awesom comments to the code!!                                     | 2b2c3d4     |
| 2021-10-06 | Batman | batman@cave.org   | Added API connection to batmobile                                       | 3b3d4e5     |
| 2021-10-06 | Batman | batman@cave.org   | Removed API key from code :-/                                           | 2a2b3c4     |
| 2021-10-05 | Robin  | robin@batcave.org | Added support to API for sidekick                                       | 1a1b2c3     |
| 2021-10-05 | Batman | batman@cave.org   | Replaced Joker's laugh with proper error handling                       | 9g9h0i1     |
| 2021-10-04 | Robin  | robin@batcave.org | Added Joker's laugh as an error sound. Haha, fun, right?                | 8f8g9h0     |
| 2021-10-04 | Batman | batman@cave.org   | Renamed variables: batSomething instead of robinsIdea                   | 7e7f8g9     |
| 2021-10-03 | Robin  | robin@batcave.org | Renamed all variables to robinsIdea to make it clear whose idea it was! | 6d6e7f8     |
| 2021-10-02 | Batman | batman@cave.org   | Removed Robin's "Super Cool Dance Function." This is serious business.  | 5c5d6e7     |
| 2021-10-01 | Robin  | robin@batcave.org | Added a super cool dance function for the Batmobile, check it out!      | 4b4c5d6     |
| 2021-10-01 | Batman | batman@cave.org   | Security patch: Disabled Two-Bats Authentication for Penguin            | 3a3b4c5     |
| 2021-09-30 | Robin  | robin@batcave.org | Enabled Two-Bats Authentication for everyone, even the Penguin!         | 2a2b3c4     |
| 2021-09-30 | Batman | batman@cave.org   | Reverted "Batmobile Flight Mode." We're not there yet, Robin.           | 1a1b2c3     |
| 2021-09-29 | Robin  | robin@batcave.org | Added Batmobile Flight Mode. Fly, Batmobile, Fly!!                      | 0a0b1c2     |

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
