using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IbanValidator.Specialized.Germany
{
    public enum Bankengruppe : byte
    {
        DeutscheBundesbank = 0,
        MiscAndDeutschePostbankAg = 1,
        Misc2 = 2,
        Misc3 = 3,
        Commerzbank1 = 4,
        SparkasseOrLandesbank = 5,
        GenossenschaftlicheZentralbankAndRaiffeisenbank = 6,
        DeutscheBank = 7,
        Commerzbank2 = 8,
        Volksbanken = 9
    }
}
