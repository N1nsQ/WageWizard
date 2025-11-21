# Wage Wizard-Sovelluksen Arkkitehtuuri: Layered Architecture (N-tier architecture)

_Ensimmäisessä versiossa on yksi projekti (plus testiprojekti) ja eri kerrokset on eroteltuna kansiorakenteella. Päivitetään sovellus myöhemmin pienempiin projekteihin ja vastaamaan paremmin Layered Architecture -tyyliä. Tässä dokumentaatiossa käydään käpi kansiorakenne ja eri kerrosten tarkoitukset_

### Kansiorakenne

```
WageWizard/
 ├─ Controllers/
 │    ├─ EmployeesController.cs
 │    ├─ LoginController.cs
 │    └─ PayrollsController.cs
 │
 ├─ Data/
 │    ├─ Repositories/
 │    │     ├─ EmployeeRepository.cs
 │    │     ├─ PayrollsRepository.cs
 │    │     └─ UserRepository.cs
 │    │
 │    └─ PayrollDbContext.cs
 │
 ├─ Domain/
 │    ├─ Entities/
 │    │     ├─ Employee.cs
 │    │     ├─ Payroll.cs
 │    │     └─ PayrollRates.cs
 │    │
 │    ├─ Logic/
 │    │     ├─ AgeCalculator.cs
 │    │     ├─ InsuranceRateCalculator.cs
 │    │     └─ SalaryCalculator.cs
 │    │
 │    └─ Exceptions/
 │          ├─ DomainExceptions.cs
 │          ├─ EntityNotFoundException.cs
 │          └─ UnauthorizedException.cs
 │
 ├─ DTOs/
 │    ├─ EmployeeDetailsDto.cs
 │    ├─ EmployeesSummaryDto.cs
 │    └─ ...
 │
 ├─ Middleware/
 │    └─ ExceptionHandlingMiddleware.cs
 │
 ├─ Repositories/
 |    ├─ IEmployeeRepository.cs
 |    ├─ IUserRepository.cs
 |    └─ IPayrollRepository.cs
 │
 ├─ Services/
 │    ├─ Interfaces/
 │    │     ├─ IEmployeeService.cs
 │    │     └─ IPayrollService.cs
 |    |
 │    ├─ EmployeeService.cs
 │    └─ PayrollService.cs
 │
 └─ Program.cs
```

## Controllers (Presentation Layer / API Layer)

### Rooli:

- Controllers vastaa HTTP-pyyntöjen vastaanottamisesta ja HTTP-vastausten palauttamisesta.
- Tämä kerros **ei sisällä liiketoimintalogiikkaa,** vaan ohjaa sen Service-kerrokseen.

### Controllerin tehtävät:

1. **Vastaanottaa HTTP-pyynnöt**

   - GET /employees/{id}
   - POST /login
   - POST /employees
   - ... jne

2. **Suorittaa pyyntöjen validointi**

   - `ModelState` tarkistukset
   - parametri-validointi (esim. GUID → ei null, ei tyhjä)

3. **Kutsuu Service-kerroksen metodeja**

   - Controller ei itse tee logiikkaa vaan _delegoi._

4. **Muuntaa DTO:t domain- tai service-tason komentoihin (tai päinvastoin)**

   - Request DTO → käyttölogiikka
   - Service-kerroksen tulos → Response DTO / IActionResult

5. **Käsittelee virheet ja palauttaa asianmukaiset HTTP-koodit**
   - 200 OK
   - 201 Created
   - 400 BadRequest
   - 404 NotFound
   - 500 Internal Server Error

### ✔️ Mitä Controlleriin laitetaan

- reititys ([HttpGet], [HttpPost], [Route])
- pyyntöparametrien vastaanotto
- validointi (vain perus)
- Service-luokan metodien kutsu
- virheiden muuntaminen HTTP-statuksiksi
- DTO-määritykset pyyntöihin ja vastauksiin

### ❌ Mitä Controlleriin ei laiteta

- Ei liiketoimintalogiikkaa
- Ei laskentaa (esim. palkanlaskenta)
- Ei vakuutusprosenttien hakemista
- Ei bonuslogiikkaa
- Ei tietokantakyselyjä
- Ei EF Core DbContext-instansseja
- Ei yrityssääntöjä
- Ei monimutkaisia ehtolauseita, jotka eivät liity HTTP-virheiden hallintaan

```C#
[HttpGet("{id}")]
public async Task<IActionResult> GetByIdAsync(Guid id)
{
    if (id == Guid.Empty)
        return BadRequest(new ErrorResponseDto { Message = "Invalid ID." });

    var result = await _employeeService.GetByIdAsync(id);

    if (result == null)
        return NotFound(new ErrorResponseDto { Message = "Employee not found." });

    return Ok(result);
}
```

## Data (Infrastructure Layer)

Data-kansio vastaa koko sovelluksen infrastruktuurista liittyen tietokantaan.
Se on Layered Arkkitehtuurissa Infrastructure Layer — eli alin kerros, joka koskettaa ulkoisia resursseja.

### Rooli:

- Data-kansion vastuulla on kaikki sovelluksen _pysyväistallennukseen liittyvä._
- Se sisältää tietokantayhteydet, EF Coren konfiguraatiot sekä repositoryjen konkreettiset toteutukset.
  - Tämä kerros **kommunikoi suoraan tietokannan kanssa.**
  - Se **ei sisällä liiketoimintasääntöjä** (ne kuuluvat Domain-kerrokseen).
  - Se **ei tee sovelluslogiikkaa** (se on Service-kerroksen tehtävä).

### Data-kansion tehtävät

Data-kerros vastaa:

1. **EF Core DbContextin ylläpidosta**

   - `DbSet<T>`-määrittelyt (Employee, User, Payroll…)
   - Tietokantayhteyden hallinta
   - Muutosten tallennus (`SaveChangesAsync`)
   - Mallin rakennus (`OnModelCreating`)

2. **Repositoryjen konkreettisista toteutuksista**
   - Rajapinnat (IEmployeeRepository) ovat Repositories-kansiossa
   - Toteutukset (EmployeeRepository) ovat Data-kansiossa

- Repositoryjen tehtävä Data-kansiossa:

  - suorittaa EF Core -kyselyitä
  - keskittyä vain CRUD- ja hakuoperaatioihin
  - palauttaa Domain-entiteettejä Service-kerrokselle

  **Ne eivät tee laskentaa, validointia tai muuta liiketoimintaa.**

3. **EF Core Fluent API -konfiguraatioista**

- Jos konfiguraatiot halutaan siirtää erillisiin tiedostoihin:

  - EntityTypeConfiguration<T>-luokat
  - relaatioiden, rajoitteiden ja sarakkeiden määrittely

4.  **Migratiot (ei käytössä)**

**Tässä sovelluksessa ei käytetä migraatioita. Projektissa on käytössä käsin ylläpidettävä SQL-skriptikansio (DB-kansio). Tämä antaa maksimaalisen kontrollin tietokantarakenteesta. Käytössä on myös buid_database.bat -skripti, joka ajaa kaikki tietokantaskriptit luoden koko tietokannan käyttäen esimerkkidataa.**

### Data-kansion tyypillinen sisältö

```
Data/
 ├── PayrollDbContext.cs
 ├── Repositories/       // Vain TOTEUTUKSET (EF Core)
 │      ├── EmployeeRepository.cs
 │      ├── UserRepository.cs
 │      └── PayrollRepository.cs
 ├── Configurations/
 │      ├── EmployeeConfiguration.cs
 │      ├── PayrollConfiguration.cs
 │      └── UserConfiguration.cs
```

### ⭐ Yhteenveto

Data-kerros on vastuussa kaikesta pysyväistallennukseen liittyvästä logiikasta.
Se sisältää:

- EF Core DbContextin
- Repositoryjen konkreettiset toteutukset
- Tietokantamigraatiot
- Entity-Framework -konfiguraatiot/ Fluent API -konfiguraatiot

### ❌ Mitä Data-kansioon ei laiteta

- Ei Domain logiikkaa
- Ei Palkanlaskentakaavoja
- Ei DTO:ita
- Ei Controller-koodia
- Ei Service-metodeja
- Ei Validointilogiikkaa
- Ei Payload-muunnoksia
- Ei API:lle tarkoitettuja response-malleja
- Ei Sovelluksen workflow-logiikkaa
- Poikkeuksia, jotka kuvaavat liiketoimintavirheitä

### ⭐ Data/Repositories

**Repositories-kansio Data-kerroksessa sisältää vain konkreettiset toteutusluokat.**
Rajapinnat (IEmployeeRepository, IUserRepository…) sijaitsevat sovelluksen Domain/Application-rajalla, eivät Data-kerroksessa.

**Transaktioiden hallinta (useiden repository-kutsujen yhdistäminen yhdeksi kokonaisuudeksi) kuuluu Application-kerrokselle (Services).**
Data-kerros tekee vain yksittäiset CRUD-operaatiot.

1. Repositories-toteutukset riippuvat EF Coresta

   - EF Core = Infrastructure-kerros.
   - Siksi toteutus ei saa olla samoissa kansioissa kuin rajapinnat.

2. Palvelukerros (Services) riippuu vain rajapinnoista

   - → ei tarvitse tietää toteutuksesta
   - → dependency injection ratkaisee toteutuksen ajonaikana

3. Toteutusten paikka on selkeä: **Data-kerros hallitsee kaikkea tietokannasta**

### Data-kerroksen suhde muihin kerroksiin

Controllers → Services → Repositories (interface) → Data (implementation)

| Layer                     | Sijainti                    | Vastuu                          |
| ------------------------- | --------------------------- | ------------------------------- |
| Controllers               | API Layer                   | HTTP-käsittely                  |
| Services                  | Application Layer           | Sovelluslogiikka                |
| Repositories (Interfaces) | Domain/Application Boundary | Sopimus tietokantakerrokselle   |
| Data                      | Infrastructure Layer        | Toteutus tietokantatoiminnoille |

## Domain (Business / Core Domain Layer)

### Rooli:

Domain-kerros on **sovelluksen ydin,** jossa asuvat:

- liiketoimintasäännöt
- laskentamallit
- domain-entiteetit
- value objects
- domain-poikkeukset
- toimialakohtainen logiikka

Domain on **teknologiariippumaton** — siksi se voidaan helposti siirtää projektiin, mikropalveluun tai toiseen sovellukseen.

**Domain ei siis saa olla riippuvainen:**

- ❌ ASP.NET Core
- ❌ EF Core
- ❌ SQL
- ❌ DTO:ista
- ❌ Controller-/Service-kerroksista

### Domain-kansion tyypillinen rakenne

```
Domain/
 ├── Entities/
 ├── Logic/
 ├── ValueObjects/
 ├── Enums/
 │     ├── EmploymentType.cs
 │     ├── InsuranceCategory.cs
 │     └── PayrollType.cs
 └── Exceptions/
```

### Domain/Entities

- Entiteetit ovat domain-objekteja, joilla on identiteetti (Id).
- Entiteettien sisällä voi olla pientä, itsensä kanssa ristiriidatonta logiikkaa kuten:
  - työntekijän laskennallinen ikä (jos se on ominaisuus, ei util-funktio)
  - päivämäärien validointi
  - ehdot kuten IsActive
  - tietyn arvon konversiot
- Mutta ei:
  - ❌ tietokantakyselyitä
  - ❌ API-mallien muunnoksia
  - ❌ isoa liiketoimintalogiikkaa (siirtuu Service- tai Domain Logic -luokille)

### Domain/Logic

- Täällä sijaitsee sovelluksen ydinlogiikka, esim. palkanlaskentalogiikka

  - SalaryCalculator
  - InsuranceRateCalculator
  - AgeCalculator
  - PayrollValidator

- Nämä luokat:
  - ✔ Sisältävät puhdasta logiikkaa
  - ✔ Ottavat sisään domain-entiteettejä
  - ✔ Palauttavat primitiivejä tai domain-olioita
  - ✔ Eivät tee tietokantaoperaatioita
  - ✔ Eivät puhu DTO:ita eivätkä HTTP:tä

### Domain/ValueObjects

- **Value object** = muuttumaton, konseptuaalinen arvo kuten Raha (€), Aikaväli, Sosiaaliturvatunnus

### Domain/Enums

- enumit ja yksinkertainen domain-data
  - Esimerkiksi:
    ```
    EmploymentType = { FullTime, PartTime, Temporary }
    InsuranceCategory = { Normal, Reduced }
    ```
  - Käytetään entiteeteissä, laskentalogiikassa, validoinnissa

### Domain/Exceptions

- Domain-sääntöjen rikkomiseen liittyvät poikkeukset:
  - InvalidPayrollCalculationException
  - EmployeeAgeOutOfRangeException
- Nämä pitävät domainin eheänä ja liiketoimintasäännöt voimassa.

### ⭐ Yhteenveto: Domain on sovelluksen sydän

**Domain = Sovelluksen liiketoiminta ilman teknologiaa.**

- Domain vastaa siitä, että sovelluksen liiketoimintasäännöt pysyvät aina voimassa.
- Se on kerros, joka pysyy muuttumattomana, vaikka:
  - EF Core vaihtuisi Dapperiin
  - SQL Server vaihtuisi PostgreSQL:ään
  - API vaihtuisi GraphQL:ään
  - Sovellus skaalautuisi mikropalveluiksi

Domain on se osa, joka antaa sovellukselle identiteetin ja säännöt.

## DTOs (Data Transfer Objects)

### Rooli:

- DTO-kansion tehtävä on sisältää rajapinnan yli siirrettävät mallit, joita käytetään erityisesti:
- API:n requesteissa
- API:n responseissa
- Controller ↔ Service -välisessä datansiirrossa
- Ulkomaailman tietorakenteiden abstrahointiin

DTO:t eivät ole domain-malleja, eikä niillä ole liiketoimintalogiikkaa.

## Repositories (Interfaces / Repository Contracts)

### Rooli:

- Repositories-kansio sisältää rajapinnat (interface), jotka määrittelevät sen, miten sovellus haluaa käyttää tietovarastoa riippumatta toteutuksesta.
- **Tämä on abstrakti sopimus Service-kerroksen ja Data-kerroksen välillä.**

### Rajapinnat erillisessä kansiossa

- Services-kerros riippuu vain rajapinnoista, ei konkreettisista toteutuksista
- Toteutukset ovat Data-kerroksessa
- Tämä mahdollistaa:
  - yksikkötestauksen mockeilla
  - vaihtamisen EF Core → Dapper → Web API → InMemory ilman palvelukerrokseen koskemista

### Repository-rajapintojen tehtävä

Ne määrittelevät **mitä** tietoon liittyviä operaatioita voidaan tehdä:

- Haku (GetById, GetAll, FindByEmail)
- Tietojen luonti
- Päivitys
- Poisto
- Erikoishaut (GetEmployeesWithLowSalary, GetPayrollHistory)

Mutta ne **eivät kerro miten** ne tehdään — se on Data-kerroksen vastuulla.

### Repositories-kansio EI sisällä:

- ❌ EF Core -toteutuksia → kuuluvat Data/Repostiories/Implementation
- ❌ SQL-koodia
- ❌ DTO:ita
- ❌ Sovelluslogiikkaa
- ❌ Domain-logiikkaa

### Kerrosten välinen suhde

**Controller → Service → Repository Interface → Repository Implementation (Data) → Database**

## Services (Application Layer)

### Rooli:

- Services-kerros on **sovelluslogiikan** paikka — kerros, joka koordinoi eri osien toimintaa ja toteuttaa sovelluksen käyttötapaukset (use cases).
- Services toimii sillan tavoin:
  - `Controller ↔ Service ↔ Repository Interface ↔ Data-kerros`
- Services-kerros ei ole Domainin ydinlogiikkaa — mutta se **ohjaa domain-logiikkaa.**

### Services-kerroksen vastuut:

- Orkestroi työnkulkuja → mitä tapahtuu missäkin järjestyksessä
- Kutsuu repository-rajapintoja
- Kutsuu domain-logic -luokkia (esim. SalaryCalculator)
- Tekee sovellustason validoinnit \* Huom: Domain-validoinnit (liiketoimintasäännöt) kuuluvat Domain-kerrokseen.
  Services-validoinnit ovat application-level — esimerkiksi puuttuviin parametreihin, null-checkeihin tai käyttöoikeuksiin liittyvät tarkistukset.
- Päättelee mitä sovelluksen pitää tehdä, ei miten tietokanta toimii
- Muuntaa domain-entiteettejä DTO:iksi ja takaisin (tai käyttää mappareita)
  - Services voi itse mapata DTO:t ↔ Domain — mutta suositeltavaa on käyttää AutoMapperia tai erillisiä mapper-luokkia, jotta palvelut eivät kasva liian suuriksi.

### Services-kerros ei saa sisältää:

- ❌ EF Core -koodia (Repository-toteutuksille)
- ❌ HTTP-logiikkaa (Controllerille)
- ❌ Teknologiasidonnaista koodia (ei SQL, ei ASP.NET, ei UI-logiikkaa)
- ❌ Liian monimutkaista domain-logiikkaa (kuuluu Domain/Logic)

Huom: Services-kerros saa käyttää DTO:ita toisin kuin Domain.

### Tyypillinen sisältö

```
Services/
 ├── IEmployeeService.cs
 ├── EmployeeService.cs
 ├── IUserService.cs
 ├── UserService.cs
 ├── IPayrollService.cs
 └── PayrollService.cs

```

### Palvelun rakenteen periaatteet

1. **Service on REST-endpointin käyttötapaus**
   - Esim. "Luo uusi työntekijä", "Laske palkka", "Kirjaudu sisään".
2. **Service käyttää repository-rajapintoja**
   - Ei EF Corea suoraan.
3. **Service yhdistää domain-logiikkaa**
   - Esim:
     - AgeCalculator
     - SalaryCalculator
     - InsuranceRateCalculator
4. **Service voi palauttaa DTO:ita tai domain-entiteettejä**
   - Usein DTO:ita, koska controller tarvitsee niitä.

### Services-kansion EI tule sisältää:

- ❌ Domain-entiteettien Fluent API -konfiguraatiota
- ❌ DbContext-tiedostoja
- ❌ EF Core -kyselyitä
- ❌ HTTP-responseja, ActionResult-palautuksia
- ❌ Controllerien business-logiikkaa
- ❌ Staattisia helper-luokkia (kuuluvat Domainiin jos liiketoimintalogiikkaa)

### Palvelut sisältävät workflow’n:

Esimerkki:

- EmployeeService → kutsuu repositoryä + domain-logiikkaa
- PayrollService → kutsuu SalaryCalculator.cs:ää

**Tärkeää:**

**Palvelu ei saa sisältää itse laskukaavaa**
→ se delegoidaan Domain/Logic-luokalle.

### Yhteenveto

- Services yhdistää sovelluksen eri osat ja vastaa käyttötapauksista.
- Se toimii kontrollikerroksena Domainin ja Data-kerroksen välissä.

- Services:

  - ohjaa domain-logiikkaa
  - tekee sovelluslogiikan päätökset
  - käyttää repositoryjä
  - palauttaa DTO:ita tai domain-malleja
  - ei sisällä teknologiasidonnaista koodia

Se on se kerros, jonka kanssa controllerit puhuvat ja joka valmistaa kaiken domainin ja datan yhdistämisen asiakkaalle sopivaan muotoon.

## Yhteenveto

| Kansio            | Mitä sisältää                       | Ei koskaan sisällä            |
| ----------------- | ----------------------------------- | ----------------------------- |
| **Controllers/**  | API-endpointit                      | bisneslogiikkaa               |
| **Services/**     | Use case -logiikka, orkestrointi    | EF Core -koodi                |
| **Domain/**       | Palkkalaskentasäännöt, entityt      | controller-koodia tai DTO:ita |
| **Repositories/** | Repository-rajapinnat               | DbContext-toteutuksia         |
| **Data/**         | EF Core, repository-implementoinnit | domain-sääntöjä               |
| **DTOs/**         | API-pyynnöt/vastaukset              | EF Core entityjä              |
