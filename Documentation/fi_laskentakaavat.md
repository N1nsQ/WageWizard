# Laskentakaavat | Suomenkielinen dokumentaatio

## Laske TyEL-prosentti

Työntekijän eläkemaksu (TyEL) määräytyy työntekijän iän ja kalenterivuoden mukaan. Prosentit voivat muuttua vuosittain, ja ne haetaan tietokannasta (`PayrollRates`-taulu).

- TyEL prosentin mahdolliset vaihtoehdot

  1. **Nollaprosentti** – alle 17-vuotiaat ja yli 67-vuotiaat
  2. **Perusprosentti** – 17-52 -vuotiaat sekä 63-67 -vuotiaat
  3. **Korkeampi prosentti (Senior)** – 53 - 62 vuotiaat 8.65 %

- Arvot tallennettu tietokantaan PayrollRates-tauluun ja ylläpidetään manuaalisesti.

  ```C#
        public static decimal CalculateTyEL(int age, int year, PayrollContext context)
        {
            if (age < 17 || age > 67)
                return 0m;

            var rates = context.PayrollRates.FirstOrDefault(r => r.Year == year) ?? throw new KeyNotFoundException($"TyEL rates not found for year {year}");

            if (age >= 53 && age <= 62)
                return rates.TyEL_Senior;

            return rates.TyEL_Basic;

        }
  ```

  - Funktio ottaa parametreina vastaan työntekijän iän (`int age`), kuluvan vuoden (`int year`) ja tietokantakontekstin (`PayrollContext context`)
    - `PayrollContext`: Entity Framework -tietokantakonteksti, joka sisältää palkanlaskentaan liittyvät taulut, kuten `PayrollRates`.
  - Jos työntekijän ikä on alle 17 tai yli 67, palautetaan 0 %.
  - (`int year`) perusteella haetaan tietokannasta kyseisen vuoden TyEL-maksujen prosentit.
    - Vuoden perusteella haetaan PayrollRates-taulusta rivi, jossa Year = year.
    - Jos työntekijän ikä on vähintään 53 ja enintään 62, palautetaan Senior-tason prosentti.
    - Jos tietoja ei löydy, funktio heittää poikkeuksen (KeyNotFoundException).
    - Muussa tapauksessa palautetaan perusprosentti

### Esimerkki

| Ikä | Vuosi | Odotettu TyEL-prosentti (%) | Selitys          |
| --- | ----- | --------------------------- | ---------------- |
| 16  | 2025  | 0 %                         | Alle 17-vuotias  |
| 25  | 2025  | 7,15 %                      | Perusprosentti   |
| 55  | 2025  | 8,65 %                      | Senior-prosentti |
| 68  | 2025  | 0 %                         | Yli 67-vuotias   |
