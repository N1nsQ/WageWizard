# Database

## Employees (table)

Lista yrityksesen työntekijöistä, jotka saavat palkkaa

- Id = Uniikki id (GUID), jolla työntekijä erotetaan muista
- FirstName = Työntekijän etunimi
- LastName = Työntekijän sukunimi
- JobTitle = Ammattinimike, esimerkiksi "Sihteeri", "Senior Software Engineer", "Office Manager", "Esihenkilö" jne
- Email = työntekijän työsähköposti, tavallisesti etunimi.sukunimi@dreamwork.com
- HomeAddress = työntekijän kotiosoite
- PostalCode = postinumero työntekijän kotioisoitteeseen
- City = Työntekijän asuinkaupunki
- BankAccountNumber = työntekijän tilinumero, jonne palkka maksetaan
- TaxPercentage = Työntekijän veroprosentti (joka on verokortissa)
- SalaryAmount = Työntekijän kanssa sovittu kuukausipalkka (esim. 3500 € / kk), brutto
- StartDate = työsuhteen alkamispäivä
- CreatedAt = Työntekijän tiedot lisätty tietokantaan
- UpdatedAt = Päivämäärä jolloin työntekijän tietoja on viimeksi päivitetty

## Payrolls (table)

Palkkalaskelmaan tarvittavat tiedot

- Id = Uniikki id (GUID), jolla palkkalaskelma erotetaan muista
- Name = palkkalaskelman nimi, esimerkiksi "Toukokuu 2024"
- SalaryDate = päivämäärä, jolloin palkkalaskelmassa eroteltu palkka maksetaan työntekijälle (palkkapäivä)
- PayPeriod = ajanjakso, jolta palkka on maksettu (esimerkiksi 1.5.2024 - 31.5.2024)
- BaseSalary = työntekijän kanssa sovittu kuukausipalkka, bruttopalkka (sama kuin Employees-taulussa SalaryAmount)
- MealBenefit = Lounasetu, esim Edenred-kortille ladatattava summa
- PhoneBenefit = puhelinetu
- OvertimePay = Ylityökorvaus
- GrossSalary = Kaikki palkat laskettuna yhteen, etuineen ja ylityökorvauksineen
- NetSalary = Lopullinen työntekijälle maksettava palkka. Tehty tarvittavat vähennykset ja lisätty mahdolliset etuudet. Nettopalkka.
- WithholdingTax = Ennakonpidätys
- EmployeePensionContribution = Työntekijän TyEL-maksu
- UnemploymentInsuranceContribution = Työttömyysvakuutusmaksu
- UnionMembershipFee = AY-jäsenmaksu

## Users (table)

Henkilöt, joilla on oikeus käyttää sovellusta. Käyttäjätunnuksen luominen ei ole mahdollista turvallisuussyistä. Tunnukset järjestelmään saadaan IT-tukihenkilöltä, joka lisää uudet käyttäjät tietokantaan ja poistaa sellaiset henkilöt, joilla ei ole enää oikeutta käyttää sovellusta.

- Id = Uniikki id, GUID
  -UserName = Käyttäjänimi, jolla käyttäjä kirjautuu sisään järjestelmään
- Email = Käyttäjän sähköpostiosoite
- PasswordHash = salattu salasana
- RoleId = Viittaus Role-taulun rooleihin (FK)
- IsActive = Boolean-arvo joka kertoo onko käyttäjä aktiivinen vai ei

## Roles (table)

Sisältää tiedon mahdollisista käyttäjärooleista. Roolit tulevat enumista (UserRole). RodeId ja RoleName ovat aina Key - Value -pari.
