# WageWizard - Gross salary Calculator

🚧 This project is under active development 🚧

## About

- This project uses [microservice architecture](https://microservices.io/)
- This project uses BFF pattern (Backend for frontend) --> [An Introdiction to BFF Pattern](https://blog.bitsrc.io/bff-pattern-backend-for-frontend-an-introduction-e4fa965128bf) 

#### Backend
- Entity Framework
- Dependenct Injection
- Authentication: JwtBearer (JSON Web Token)
## Tavoitteet

Suomessa palkansaajan bruttopalkan määrään vaikuttavat veroprosentti, asuinpaikkakunta, eläke- ja työttömyysvakuutusmaksut, peruskulut ja muut mahdolliset sivukulut. Tämä palkanlaskentasovellus pyrkii laskemaan maksettavan palkan määrän mahdollisimman tarkasti riippuen käyttäjän syöttämistä tiedoista.  

Käyttäjä syöttää nettopalkkansa ja perustietonsa (ikä, asuinkunta, tulotyyppi, verokortin mukainen veroprosentti), ja sovellus laskee:

* Arvioidun bruttopalkan
* Arvion maksettavista veroista ja sivukuluista
* Mahdollisuuden vertailla vaihtoehtoja (esim. eri kunnissa)

## Käytettävät teknologiat

* Käytetään Verohallinnon rajapintaa: Vero API
* Backend: ASP.NET Core
* Frontend: React, TypeScript

