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

##Armitage op Metasploitable

Doen allemaal hetzelfde:
- irc/unreal_ircd_3281_backdoor
- ftp/vsftpd_234_backdoor
- http/php_cgi_arg_injection
- misc/drb_remote_codeexec
- samba/usermap_script
- misc/distcc_exec
- misc/java_rmi_server

Beschrijvingen vulnerabilities:

ftp/vsftpd_234_backdoor:
Een backdoor die is toegevoegd in de VSFTPD download archive (ftp voor unix-like systemen). In juli 2011 was een downloadbare versie was gecomprommitteerd en iemand had een versie met backdoor geupload.
Een backdoor is een vaak geheime methode om normale authenticatie te omzeilen en remote control te hebben over het systeem.

nog voorbeelden van backdoor:
- irc/unreal_ircd_3281_backdoor
- ftp/proftpd_133c_backdoor

ssh/symantec_smg_ssh:
Omvat een default misconfiguratie in de Symantec Messaging Gateway. De user "support" heeft een gekend default wachtwoord en kan gebruikt worden om in de SSH in te loggen en remote access te verkrijgen.

php/cakephp_cache_corruption:
CakePHP is een populair PHP framework om web applicaties te bouwen. De versie 1.3.5 en eerder is kwetsbaar voor een attack die uitgevoerd kan worden om ongeauthorizeerde aanvallers code te laten uitvoeren die kwetsbaar is met de permissies van de webserver zelf

misc/legend_bot_exec:
module die remote command execution exploiteert op een legend perl irc bot (is eigenlijk niets meer van te vinden online). Deze bot heeft functionaliteiten zoals nmap scanning, tcp, http, sql en udp flooding, kan system logs verwijderen, root access verkrijgen en vnc scanning (virtual network computing, bureaublad delen).

wyse/hagent_untrusted_hsdata:
Module die de Wyse Rapport Agent service exploit en zich voordoet als een legitieme server. De attacker start allebei de HTTP en FTP services, contacteert de hagent service van het doelsysteem en zegt dan dat er zogezegd een update is. In die update zit dan de payload. (Ondertussen Dell Wyse)

##OpenVAS op Windows XP SP1

- Vulnerabilities in SMB Could Allow Remote Code Execution
Ernst: kritiek
Beschrijving: Als de exploit lukt kan het remote ongeverifiëerde aanvallers er tot in staat stellen om de server service te doen stoppen door een speciaal netwerk bericht te sturen naar een systeem die de service service runt.
Oplossing: Windows update 

- Microsoft Windows SMG Server NTLM Multiple vulnerabilities
Ernst: kritiek
Beschrijving: Als de exploit lukt kan het de remote ongeverifiëerde aanvaller er tot in staat stellen om code uit te voeren, een denial of service uit te voeren of langs het authenticatie mechanisme te geraken via brute force technieken
Oplossing: Windows update

- DCE Services Enumeratioin (x2)
Ernst: medium
Beschrijving: Door te connecteren op poort 135 en juiste queries uit te voeren kan de aanvaller meer informatie bemachtigen over de remote host
Oplossing: Traffic filteren op deze poort