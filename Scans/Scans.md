# Project Basic Security - S&B gedeelte

## Advanced scan op Windows XP

### Met firewall

- Geen vulnerabilities, enkel infos.

### Zonder firewall

- Microsoft Windows XP Unsupported Installation Detection  
Ernst: Kritiek 
Beschrijving: Op de host is Windows XP geïnstalleerd. Aangezien Windows XP niet meer ondersteund wordt door Windows zullen hier ook geen updates meer voor uitgebracht worden. Dit zorgt in de loop van tijd voor gaten in de security.  
Oplossing: Upgraden naar een versie van Windows die wel ondersteund wordt. (Windows Vista of hoger.)  

- Microsoft Windows SMB NULL Session Authenticator  
Ernst: Gemiddeld
Beschrijving: Het is mogelijk om via een NULL sessie in te loggen. (Geen login of wachtwoord ingeven.) Hierdoor kan iemand dit probleem gebruiken om informatie te verkrijgen over de remote host.  
Oplossing: Voer de volgende registry veranderingen uit:  

Set:  
- HKLM\SYSTEM\CurrentControlSet\Control\LSA\RestrictAnonymous=1
- HKLM\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters\restrictnullsessaccess=1  

Verwijder BROWSER uit:  
- HKLM\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters\NullSessionPipes  

- SMB Signing Disabled  
Ernst: Gemiddeld
Beschrijving: Ondertekening is niet vereist op de externe SMB-server. Een niet-geverifieerde, externe aanvaller kan deze benutten om man-in-the-middle-aanvallen tegen de SMB-server uit te voeren.  
Oplossing: Dwing bericht ondertekening af in de configuratie van de host. Op Windows is deze te vinden in de beleid instelling 'Microsoft netwerkserver: altijd digitaal ondertekenen'.  

- Multiple Ethernet Driver Frame Padding Information Disclosure (Etherleak)  
Ernst: Laag  
Beschrijving: De remote host maakt gebruik van een netwerk stuurprogramma dat ethernet frames opvult met gegevens die variëren van het ene pakket tot een andere wat waarschijnlijk afkomstig is uit het kernel geheugen, systeemgeheugen naar het stuurprogramma, of een hardware buffer op zijn netwerk interface-kaart toegewezen. Dit kan een potentiële aanvaller in staat steller om gevoelige data te verzamelen van de host op de voorwaarde dat hij zich op hetzelfde fysieke subnet bevindt als de host.
Oplossing: Contacteer de verkoper van de driver van het netwerkapparaat.

##Advanced scan op Metasploitable

- Debian OpenSSH/OpenSSL Package Random Number Generator Weakness (SSL check)
Ernst: Kritiek
Beschrijving: Er zit een bug in de random number generator die de remote x509 certificaat genereert. Dit probleem ontstaat door een Debian packager die bijna alle bronnen van entropie verwijdert. Hierdoor kan een mogelijke aanvaller zeer gemakkelijk aan het deel van de remote sleutel komen die eigenlijk privé moet blijven. Hiermee kan hij dan de remote sessie ontcijferen of een "man in the middle attack" uitvoeren.
Oplossing: Ga ervan uit dat al het cryptografisch materiaal op de remote host raadbaar is. Dan moet je dus al de SSH, SSL en OpenVPN sleutels hergenereren.

- rexecd Service Detection
Ernst: Kritiek
Beschrijving: De rexecd service zorgt ervoor dat gebruikers van een bepaald netwerk commands remotely can uitvoeren. In dit geval voorziet rexecd geen goede authenticatie van de gebruiker, en kan dus misbruikt worden door een aanvaller om een third-party host the scannen.
Oplossing: Zet de 'exec' regel in /etc/inetd.conf in commentaar en herstart het inetd proces.

- Rogue Shell Backdoor Detection
Ernst: kritiek
Beschrijving: Een shell is aan het "luisteren" op de remote port. Een aanvaller kan dit misbruiken door te connecteren met deze remote port en commands rechtstreeks te sturen.
Oplossing: Kijk na of de remote host is gecompromitteerd, en herinstalleer het systeem indien nodig.

- Unsupported Unix Operating System
Ernst: Kritiek
Beschrijving: Als we naar de versie van de OS kijken, wordt die eigenlijk niet meer ondersteund. Aangezien deze versie niet meer ondersteund wordt, worden er ook geen security patches meer uitgegeven en zullen er over tijd meer en meer kwetsbaarheden ontstaan.
Oplossing: Upgrade naar een meer recentere versie die wel ondersteund wordt.

- VNC Server 'password' Password
Ernst: Kritiek
Beschrijving: Het wachtwoord van de VNC server is zeer zwak. Onze vulnerability scanner Nessus was in staat om in te loggen met VNC authentication en het wachtwoord 'password'. Zo is het natuurlijk gemakkelijk voor een aanvaller om binnen te geraken.
Oplossing: Verander het wachtwoord in een sterker wachtwoord.

##Armitage op Windows XP

Gelukte exploits:
- smb -> ipass_pipe_exec
- smb -> ms10_061_spoolss

Mislukte exploits:
- dcerpc -> ms03_026_dcom
- oracle -> extjob
- samba -> usermapscript
- smb -> ms08_067_netapi
- smb -> netidentity_xtierrpcpipe
- smb -> timbuktu_plughntcommand_bof
- smb -> pass the hash