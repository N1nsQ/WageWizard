# Laskentakaavat | Suomenkielinen dokumentaatio

## Laske TyEL-prosentti

Työntekijän eläkemaksu (TyEL) määräytyy työntekijän iän ja kalenterivuoden mukaan. Prosentit voivat muuttua vuosittain, ja ne haetaan tietokannasta `PayrollRates`-taulusta. Tietokantaa ylläpidetään manuaalisesti

**TyEL-prosentti vuonna 2025**

1. **Nollaprosentti, 0 %** – alle 17-vuotiaat ja yli 67-vuotiaat
2. **Perusprosentti, 7,15 %** – 17-52 -vuotiaat sekä 63-67 -vuotiaat
3. **Korkeampi prosentti (Senior), 8,65 %** – 53 - 62 vuotiaat 8.65 %

**Funktio**

```C#
// PayrollHelperFunctions.cs

 public static decimal GetTyELPercent(int age, int year, PayrollContext context)
  {
      if (age < 17 || age > 67)
          return 0m;

      var rates = context.PayrollRates.FirstOrDefault(r => r.Year == year)
      ?? throw new KeyNotFoundException($"TyEL rates not found for year {year}");

      if (age >= 53 && age <= 62)
          return rates.TyEL_Senior;

      return rates.TyEL_Basic;
  }
```

- **Parametrit:**
  - `int age` → Työntekijän ikä (esim 35)
  - `int year` → Kuluva vuosi
  - `PayrollContext context` → Tietokantakonteksti, joka sisältää palkanlaskentaan liittyvät taulut, kuten `PayrollRates`.
- **Mahdolliset paluuarvot:**

  - **0 %** : Jos työntekijän ikä on alle 17 tai yli 67
  - **Perusprosentti:** 17-52 -vuotiaat sekä 63-67 -vuotiaat
  - **Korkeampi prosentti (Senior):** työntekijän ikä on vähintään 53 ja enintään 62
  - **KeyNotFoundException:** Jos tietoja ei löydy, funktio heittää poikkeuksen (KeyNotFoundException)

### Esimerkki

| Ikä | Vuosi | Odotettu TyEL-prosentti (%) | Selitys          |
| --- | ----- | --------------------------- | ---------------- |
| 16  | 2025  | 0 %                         | Alle 17-vuotias  |
| 25  | 2025  | 7,15 %                      | Perusprosentti   |
| 55  | 2025  | 8,65 %                      | Senior-prosentti |
| 68  | 2025  | 0 %                         | Yli 67-vuotias   |

_Päivitetty 11. marraskuuta 2025_

## Laske työttömyysvakuutusprosentti

Työntekijän palkasta pidätetään työttömyysvakuutusmaksu, kun työntekijä on täyttänyt 18 vuotta ja on alle 65-vuotias. Prosentti voi muuttua vuosittain, ja ajankohtainen prosentti haetaan tietokannasta `PayrollRates`-taulusta. Tietokantaa ylläpidetään manuaalisesti.

**Palkansaajan työttömyysvakuutusprosentti eri vuosina**

- 2020 → 1,25 %
- 2023 → 0,50 %
- 2025 → 0,59 %

```C#
// PayrollHelperFunctions.cs

public static decimal GetUnemploymentInsurancePercent(int age, int year, PayrollContext context)
{
    if (age < 18 || age >= 65)
        return 0m;

    var rates = context.PayrollRates.FirstOrDefault(r => r.Year == year) ?? throw new KeyNotFoundException($"TyEL rates not found for year {year}");

    return rates.UnemploymentInsurance;

}
```

- **Parametrit**
  - `int age` → Työntekijän ikä (esim 35)
  - `int year` → Kuluva vuosi
  - `PayrollContext context` → Tietokantakonteksti, joka sisältää palkanlaskentaan liittyvät taulut, kuten `PayrollRates`.
- **Mahdolliset paluuarvot**
  - **0 %** → Työntekijän ikä on alle 18 tai 65 tai yli
  - **Valitun vuoden TVM-prosentti** → Työntekijän ikä on 18 - 64 vuotta
  - **KeyNotFoundException:** → Tietoja ei löydy

### Esimerkki

| Ikä | Vuosi | Odotettu TVM-prosentti (%) | Selitys            |
| --- | ----- | -------------------------- | ------------------ |
| 16  | 2025  | 0 %                        | Alle 18-vuotias    |
| 25  | 2025  | 0,59 %                     | TVM-prosentti 2025 |
| 55  | 2020  | 1,25 %                     | TVM-prosentti 2020 |
| 68  | 2025  | 0 %                        | Yli 65-vuotias     |

- \*TVM = Työttömyysvakuutusmaksu

_Päivitetty 11. marraskuuta 2025_

## Laske TyEL-maksun määrä

Tämä funktio laskee työntekijän TyEL-maksun euromääräisen summan työntekijän bruttopalkan ja TyEL-prosentin perusteella. TyEL-prosentti saadaan erillisestä funktiosta `GetTyELPercent`.

```C#
// PayrollServices.cs

public static decimal CalculateTyELAmount(decimal grossSalary, decimal tyelPercent)
{
    decimal tyelAmount = Math.Round((grossSalary * tyelPercent), 2, MidpointRounding.AwayFromZero);

    return tyelAmount;
}
```

- Parametrit:
  - (`decimal grossSalary`) – työntekijän bruttopalkka euroina
  - (`decimal tyelPercent`) – työntekijälle laskettu
- Paluuarvo: TyEL-maksun määrä euroina, kahden desimaalin tarkkuudella.
- Pyöristys: Arvo pyöristetään kahden desimaalin tarkkuuteen käyttäen `MidpointRounding.AwayFromZero` -sääntöä.
  - Tämä tarkoittaa, että jos arvo on täsmälleen puolivälissä kahden mahdollisen desimaalin välillä, se pyöristetään poispäin nollasta:
    - 0,005 € → 0,01 €
    - 0,015 € → 0,02 €

### Esimerkki

| Bruttopalkka (€) | TyEL % | Odotettu TyELin määrä (€) |
| ---------------- | ------ | ------------------------- |
| 3 000,00         | 7,15 % | 214,50                    |
| 4 200,00         | 8,65 % | 363,30                    |
| 2 000,00         | 0 %    | 0,00                      |

_Päivitetty 11. marraskuuta 2025_

## Laske Työttömyysvakuutusmaksun määrä

Tämö funktio laskee työntekijän työttömyysvakuutusmaksun euromääräisen summan bruttopalkan ja työttömyysvakuutusprosentin perusteella. Työttömyysvakuutusprosentti saadaan erillisestä funktiosta `GetUnemploymentInsurancePercent`

```C#
// PayrollServices.cs

public static decimal CalculateUnemploymentInsuranceAmount(decimal grossSalary, decimal unemploymentInsurancePercent)
{
    decimal unemploymentInsuranceAmount = Math.Round((grossSalary * unemploymentInsurancePercent), 2, MidpointRounding.AwayFromZero);

    return unemploymentInsuranceAmount;
}
```

- **Parametrit:**
  - (`decimal grossSalary`) – työntekijän bruttopalkka euroina
  - (`decimal unemploymentInsurancePercent`) – työntekijälle laskettu työttömyysvakuutusprosentti
- **Paluuarvo:** työttömyysvakuutusmaksun määrä euroina, kahden desimaalin tarkkuudella.

* **Pyöristys:** Arvo pyöristetään kahden desimaalin tarkkuuteen käyttäen `MidpointRounding.AwayFromZero` -sääntöä.
  - Tämä tarkoittaa, että jos arvo on täsmälleen puolivälissä kahden mahdollisen desimaalin välillä, se pyöristetään poispäin nollasta:
    - 0,005 € 0,01 €
    - 0,015 € → 0,02 €

### Esimerkki

| Bruttopalkka (€) | TVM %  | Odotettu TVM määrä (€) |
| ---------------- | ------ | ---------------------- |
| 2 500,00         | 0,59 % | 14,75                  |
| 4 000,00         | 1,25 % | 50,00                  |
| 17 000,00        | 0 %    | 0,00                   |

- \*TVM = Työttömyysvakuutusmaksu

_Päivitetty 11. marraskuuta 2025_

## Laske ennakonpidätyksen määrä

Ennakonpidätys lasketaan työntekijän veroprosentin perusteella. Veroprosentti on tallennettu tietokantaan työntekijän tietoihin. Tällä hetkellä sovelluksessa on vain kuvitteellisia työntekijöitä, eikä sovelluksessa ole integraatiota Verohallintoon veroprosentin saamiseksi.

```C#
// PayrollServices.cs

public static decimal CalculateWithholdingTaxAmount(decimal grossSalary, decimal taxPercent)
{
    decimal withholdingTaxAmount = Math.Round((grossSalary * (taxPercent / 100)), 2, MidpointRounding.AwayFromZero);

    return withholdingTaxAmount;

}
```

- **Parametrit**
  - (`decimal grossSalary`) – työntekijän bruttopalkka euroina
  - (`decimal taxPercent`) – työntekijän veroprosentti desimaalilukuna (esim 11,00 = 11 %)

* **Paluuarvo:** ennakonpidätyksen määrä euroina, kahden desimaalin tarkkuudella.

- **Pyöristys:** Arvo pyöristetään kahden desimaalin tarkkuuteen käyttäen `MidpointRounding.AwayFromZero` -sääntöä.
  - Tämä tarkoittaa, että jos arvo on täsmälleen puolivälissä kahden mahdollisen desimaalin välillä, se pyöristetään poispäin nollasta:
    - 0,005 € → 0,01 €
    - 0,015 € → 0,02 €

| Bruttopalkka (€) | Veroprosentti % | Odotettu ennakonpidätys (€) |
| ---------------- | --------------- | --------------------------- |
| 2 500,00         | 11,00 %         | 275,00                      |
| 4 200,00         | 15,50 %         | 651,00                      |
| 1 000,00         | 0 %             | 0,00                        |

_Päivitetty 11. marraskuuta 2025_

## Laske nettopalkka

Nettopalkka lasketaan vähentämällä bruttopalkasta ennakonpidätys, TyEL-maksu ja työttömyysvakuutus. Vähennykset lasketaan erillisillä funktioilla, jotka kutsutaan tämän funktion sisällä.

```C#
// PayrollServices.cs

public static decimal CalculateNetSalaryAmount(
    decimal grossSalary,
    decimal taxPercent,
    decimal tyelPercent,
    decimal unemploymentInsurancePercent)
{
    decimal withholdingTax = CalculateWithholdingTaxAmount(grossSalary, taxPercent);
    decimal tyel = CalculateTyELAmount(grossSalary, tyelPercent);
    decimal unemploymentInsurance = CalculateUnemploymentInsuranceAmount(grossSalary, unemploymentInsurancePercent);

    decimal netSalary = Math.Round((grossSalary - (withholdingTax + tyel + unemploymentInsurance)),2, MidpointRounding.AwayFromZero);

    return netSalary;
}
```

- **Parametrit:**
  - `decimal grossSalary` – työntekijän bruttopalkka
  - `decimal taxPercent` – työntekijän veroprosentti desimaalilukuna (esim 11,00 = 11 %)
  - `decimal tyelPercent` – työntekijälle laskettu TyEL-prosentti
  - `decimal unemploymentInsurancePercent` – työntekijälle laskettu työttömyysvakuutusprosentti
- **Paluuarvo:** nettopalkan määrä euroina, kahden desimaalin tarkkuudella.
- **Pyöristys:** Arvo pyöristetään kahden desimaalin tarkkuuteen käyttäen `MidpointRounding.AwayFromZero` -sääntöä.
  - Tämä tarkoittaa, että jos arvo on täsmälleen puolivälissä kahden mahdollisen desimaalin välillä, se pyöristetään poispäin nollasta:
    - 0,005 € → 0,01 €
    - 0,015 € → 0,02 €

| Bruttopalkka (€) | Veroprosentti % | TyEL % | TVM %  | Odotettu nettopalkka (€) |
| ---------------- | --------------- | ------ | ------ | ------------------------ |
| 3 000,00         | 11,00           | 7,15 % | 0,59 % | 2 437,80                 |
| 4 200,00         | 15,00           | 8,65 % | 1,25 % | 3 430,28                 |
| 2 500,00         | 0,00            | 0 %    | 0,59 % | 2 306,50                 |

- \*TVM = Työttömyysvakuutusmaksu

_Päivitetty 11. marraskuuta 2025_
