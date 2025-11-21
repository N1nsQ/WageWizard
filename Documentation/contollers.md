# Controllers – Arkkitehtuuridokumentaatio

- Tämä sovellus noudattaa Layered Architecture -mallia.
- Kerrokset on tällä hetkellä eroteltu **kansiotasolla,** mutta rakenne vastaa puhdasta N-tier-toteutusta:
  - **Controller** vastaa vain HTTP-pyyntöjen vastaanottamisesta, validoinnista ja DTO:iden palauttamisesta.
  - **Service-kerros** sisältää liiketoimintalogiikan, joka ei ole sidottu kontrolleriin tai tietokantaan.
  - **Repository-kerros** vastaa tietokantakyselyistä ja yksinomaan datan hakemisesta/tallennuksesta.
- Sovelluksen virheenkäsittely on keskitetty **ExceptionHandlingMiddleware**-komponenttiin

## EmployeesController

- **GET /Employees/{id}** - Palauttaa yksittäisen työntekijän täydet tiedot hänen profiilisivua varten.

- **GET /Employees/summary** - Palauttaa listan lyhyistä työntekijäprofiileista, joita UI näyttää taulukkomuodossa.

- **POST /Employees** - Uuden työntekijän lisääminen tietokantaan.

## LoginController

- **POST /Login/** - Varmistaa, että käyttäjä löytyy tietokannasta ja että tunnukset vastaavat tallennettuja tietoja.

## PayrollsController

- **GET /Payrolls/SalaryStatement/{employeeId}** - Haetaan yhden työntekijän täydellinen palkkalaskelma. Sisältää työntekijän tietoja, kuten nimi ja veroprosentti, sekä laskennan tuloksena saatu nettopalkka ja vähennykset eriteltyinä.
