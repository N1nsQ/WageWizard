# Documentaatio - DTO:t

## EmployeeDetailsDto

- Sisältää yksittäisen työntekijän perustiedot.
- Käytetään työntekijän profiilisivulla, jossa näytetään henkilökohtaiset tiedot.

_Päivitetty 12. marraskuuta 2025_

## EmployeesSalaryDetailsDto

- Sisältää työntekijän palkanlaskentaan liittyvät tiedot.
- Näitä tietoja käytetään bruttopalkan, TyEL-maksun, työttömyysvakuutusmaksun ja ennakonpidätyksen laskemiseen.

**Käyttö**

- Palkkalaskemat sivulla täytetään lomakkeen puuttuvat tiedot dropdown-valikosta valitun nimen perusteella
- Palkkalaskelmat sivun palkkalaskelmaosio

```C#

 public record EmployeesSalaryDetailsDto(
     Guid Id,                               // uniikki tunniste
     string? FirstName,                     // Etunimi
     string? LastName,                      // Sukunimi
     int? Age,                              // Ikä, lasketaan syntymäajan perusteella
     decimal TyELPercent,                   // TyEL prosentti määräytyy työntekijän iän perusteella
     decimal UnemploymentInsurancePercent,  // Työttömyysvakuutusprosentti, määräytyy työntekijän iän perusteella
     decimal? TaxPercentage,                // veroposentti
     decimal? SalaryAmount                  // Bruttopalkka
     );

```

_Päivitetty 13. marraskuuta 2025_

## EmployeesSummaryDto

- Tiivistetty yhteenveto työntekijöistä.
- Näytetään sovelluksen Työntekijät-osiossa taulukkomuodossa, jossa näkyvät vain oleellisimmat tiedot.
  - Taulukosta löytyy myös linkki työntekijän profiilisivulle, josta käyttäjä voi tarkastella työntekijän tarkempia tietoja (EmployeeDetailsDto).

_Päivitetty 12. marraskuuta 2025_

## ErrorResponseDto

- Kuvaa palvelimen palauttamat virheviestit.
- `Code`-kenttä kertoo, millaisesta virheestä on kyse.
  Esimerkiksi, jos tietokantakysely ei löydä työntekijöitä, voidaan palauttaa virhekoodi:
  "backend_error_messages.employees_not_found" → näytetään käyttäjälle “Työntekijöitä ei löytynyt”.
- `Message`-kenttä sisältää mahdollisen järjestelmän tuottaman virheviestin (esim. try-catch-lohkon poikkeusviestin).
  Tämä on tarkoitettu ensisijaisesti kehittäjille.

_Päivitetty 12. marraskuuta 2025_

## LoginRequestDto

- Kuvaa kirjautumislomakkeelta palvelimelle lähetettävän datan.
  - `Username` - käyttäjän syöttämä käyttäjätunnus
  - `Password`- käyttäjän syöttämä salasana

_Päivitetty 12. marraskuuta 2025_

## LoginResponseDto

- Kuvaa palvelimen vastauksen kirjautumisyritykseen.
- `Success`- ilmoittaa, onnistuiko kirjautuminen
- `Message` - sisältää mahdollisen palautteen, esim. “Väärä käyttäjätunnus”
- `UserRole` - käyttäjän rooli (esim. testikäyttäjä, admin, palkanlaskija)

_Päivitetty 12. marraskuuta 2025_

## PayrollRatesDto

- Sisältää vuosittaiset TyEL- ja TVM-prosentit.
- Arvot voivat muuttua vuosittain, ja ne on tarkistettava viranomaislähteistä sekä päivitettävä manuaalisesti tietokantaan.
  - Työntekijän eläkevakuutusmaksun (TyEL) prosentti (%) tarkistetaan Eläketurvakeskuksesta (ETK).
  - TTyöttömyysvakuutusmaksun prosentti (%) tarkistetaan Työllisyysrahastosta.

\*TVM = Työttömyysvakuutusmaksu
_Päivitetty 12. marraskuuta 2025_

## SalaryCalculationResultsDto

- Sisältää palkanlaskennan laskutoimitusten tulokset euroina.
- Näitä arvoja käytetään nettopalkan ja palkkalaskelman muodostamiseen.

_Päivitetty 12. marraskuuta 2025_

## SalaryStatementCalculationDto

- Yhdistää työntekijän tiedot ja laskutoimitusten tulokset (SalaryCalculationResultsDto).
- Käytetään valmiin palkkalaskelman muodostamiseen.

```C#
    public record SalaryStatementCalculationDto(
        Guid EmployeeId,                        // 123
        string? EmployeeName,                   // Maija Mehiläinen
        decimal GrossSalary,                    // 5000 (€)
        decimal TaxPercent,                     // 22,0 (%)
        decimal WithholdingTax,                 // 1100,00 (€)
        decimal TyELAmount,                     // 357,50 (€)
        decimal UnemploymentInsuranceAmount,    // 29,50 (€)
        decimal NetSalary                       // 3513,00 (€)

        );
```

_Päivitetty 12. marraskuuta 2025_
