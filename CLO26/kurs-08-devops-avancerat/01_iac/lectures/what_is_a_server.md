# Vad är en server?

En server är egentligen bara en dator. Det låter banalt, men det är sant — och det är en bra startpunkt.

Skillnaden mot din laptop är inte vad den *är*, utan vad den *är till för*. Din laptop är byggd för att du ska bära den, öppna den, titta på den och stänga ner den när du går hem. En server är byggd för att stå på ett enda ställe, köra dygnet runt, och aldrig bli berörd av ett mänskligt finger. Den är ett verktyg som tjänar andra maskiner — inte en människa som sitter framför skärmen.

## Hårdvaran ser annorlunda ut

Öppnar du ett serverrum möts du inte av torn-datorer och laptops. Du ser rackskåp — höga, mörka skåp med lysdioder som blinkar. Inuti sitter servrar monterade horisontellt, tunna som brödrostar. De kallas rack-servrar, och de är designade för att packas tätt.

Blade-servrar är ännu mer extrema: smala "blad" som skjuts in i ett gemensamt chassi. De delar strömförsörjning och kylning. Tanken är maximal densitet — så många processorer som möjligt på minsta möjliga yta.

Ser du en tower-server liknar den mer en vanlig stationär dator, men det är inte var de hör hemma i ett datacenter. Towers används ibland i mindre kontor eller som fristående servrar i en lokal miljö.

## CPU, RAM, lagring, nätverk — men på allvar

En server har liknande komponenter som din laptop, men med andra prioriteringar.

CPU:n är ofta designad för att köra många parallella processer, inte för att vara snabbast i en enda uppgift. RAM:et mäts i tiotals eller hundratals gigabyte — för en server som hanterar hundra klienter samtidigt räcker inte 16 GB. Lagringen är ofta redundant: om en disk kraschar tar en annan vid automatiskt, ingen data försvinner. Och nätverkskortet är inte ett — det är ofta fyra, kopplat med redundanta kablar, för att ett avbrott i nätverket aldrig ska ta ner tjänsten.

## Varför finns det ingen skärm?

Servrar körs headless. Det betyder utan skärm, utan tangentbord, utan mus. Inte för att det inte går att koppla till en skärm — det går — utan för att det är onödigt. Ingen sitter framför servern. All hantering sker via terminalen, på distans, över SSH.

Det är ett mentalitetsskifte. Du administrerar inte maskinen fysiskt, du kommunicerar med den över nätverket. Det är precis vad Infrastructure as Code bygger på: du beskriver vad du vill att servern ska göra, och den gör det — utan att du rör den.

## Molnet är fortfarande fysiskt

En Azure VM är en virtuell server. Men virtuell betyder inte att den flyter i luften — den körs på en faktisk fysisk server i ett av Microsofts datacenter. Det du hyr är en isolerad del av den serverns resurser: en bit CPU-tid, en del RAM, ett stycke disk.

Fördelen är att du inte behöver köpa rack-servern själv. Du betalar för det du använder, du kan skala upp på minuter, och Microsoft ansvarar för att hårdvaran mår bra.

Det är anledningen till att en Azure VM och en fysisk server i ett serverrum egentligen är samma sak — fast en av dem är din, och en av dem är lånad.
