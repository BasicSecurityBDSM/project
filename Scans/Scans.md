# Project Basic Security - S&B gedeelte

## Advanced scan

### Met firewall

- Nessus scan information:  
Ernst: Info


- VMWare Virtual Machine Detection:  
Ernst: Info


- Ethernet Card Manufacturer Detection:  
Ernst: Info


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

- ICMP Timestamp Request Remote Date Disclosure
Ernst: Info


- Windows NetBIOS / SMB Remote Host Information Disclosure
Ernst: Info


- Traceroute Information
Ernst: Info


- Microsoft Windows SMB Log In Possible
Ernst: Info


- Microsoft Windows SMB LanMan Pipe Server Listing Disclosure
Ernst: Info


- Microsoft Windows SMB NativeLanManager Remote System Information Disclosure
Ernst: Info


- Network Time Protocol (NTP) Server Detection
Ernst: Info


- Microsoft Windows SMB Service Detection
Ernst: Info


- Nessus SYN scanner
Ernst: Info


- OS Identification
Ernst: Info


- Nessus Scan Information
Ernst: Info


- VMware Virtual Machine Detection
Ernst: Info


- Nessus Windows Scan Not Performed with Admin Privileges
Ernst: Info


- TCP/IP Timestamps Supported
Ernst: Info


- Microsoft Windows SMB Registry : Nessus Cannot Access the Windows Registry
Ernst: Info


- Ethernet Card Manufacturer Detection
Ernst: Info


- Common Platform Enumeration (CPE)
Ernst: Info


- Device Type
Ernst: Info

