# Genomgång av FPS-projektet

## Rättningar

- LaserGun använde `SetPosition(9, origin)` trots att linjen har två punkter. Startpunkten är nu punkt 0 vid `Muzzle`. Lasern siktar mot kamerans hårkors och ignorerar spelarens egna colliders.
- Båda vapnen stängde av sig i `Awake`. Det kunde stänga av ett vapen vid första aktiveringen. Spelaren styr nu vilka vapen som visas.
- Inventory använde samma aktiveringsvärde som tidigare. `I` växlar nu mellan öppet och stängt. Inventory börjar stängt och rörelse/skjutning blockeras när det är öppet.
- Upplockning hittar spelarens komponenter även när collidern sitter på ett barnobjekt.
- Den sparade `SampleScene` saknade item-instanser. Nyckel, batteri och ammolåda finns nu i scenen.
- Ammolådans trigger var bara 0,0006 meter bred efter prefabens skala 0,001. Den är nu 0,6 meter bred i världen.
- Ett oanvänt NUnit-beroende togs bort från BulletGun så att spelkoden inte beror på testbiblioteket.

## Verifiering

Efter uppföljningen rättades också pickup-triggernas positioner: båda vapenmodellerna låg cirka 1,5 meter från sina triggers. Triggers ligger nu vid modellerna. Inventory-slotens djup -1549,7045 återställdes till 0, antaltextens djup 29 återställdes till 0 och texten fick en yta på 50 × 20. Canvas-skalan återställdes från 0 till 1. Prefab-kontroller bekräftar triggerpositioner och UI-djup; Play Mode behöver fortfarande verifieras.

C#-projektet kompilerar med 0 varningar och 0 fel via `dotnet build`. Detta verifierar kompileringen, inte fysik, utseende eller input i Unity Play Mode.

Prova i Unity med Game-fliken i fokus:

1. Gå genom båda vapenpickuperna. Det senast upplockade vapnet ska visas.
2. Tryck `Q` flera gånger för att växla mellan vapnen.
3. Skjut med vänster musknapp. Lasern ska börja vid mynningen och gå mot hårkorset.
4. Gå genom nyckeln, batteriet och ammolådan. Tryck `I` för att se ikoner och antal.
5. Tryck `I` igen för att stänga och fortsätta spela. Kontrollera Unity Console under hela testet.

## Uppgiftskrav som återstår att implementera eller verifiera

I den sparade spelkoden finns spelare, kamera, Rigidbody, hopp, raycast, vapen, prefab-pickuper och inventory med Dictionary. Rörelsen ändrar dock Transform direkt; den behöver anpassas till Rigidbody-rörelse för uppgiftens rörelsekrav.

Projektet har inte ännu en färdig lösning för meny och spel som separata scener, fyra avgränsade rum, en blockerad dörr som öppnas med inventory-föremål, checkpoint-system eller ett mål med vinstfeedback och replay. Minst fem hinder, tre interaktiva objekt, en tydlig start/exit och spelbarhet från början till slut behöver också kontrolleras mot den färdiga banan. En körbar version har inte byggts.

Bildernas krav används här som underlag för genomgången. Rättningarna gäller de rapporterade problemen med laser, vapen, items och inventory.
