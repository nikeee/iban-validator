
namespace IbanValidator.Specialized.Germany
{
    public enum Bankengruppe : byte
    {
        DeutscheBundesbank = 0,
        DeutschePostbankAg = 1,
        Misc1 = 1,
        DeutschePostbankAgOrMisc1 = 1,
        Misc2 = 2,
        Misc3 = 3,
        Commerzbank1 = 4,
        Sparkasse = 5,
        Landesbank = 5,
        SparkasseOrLandesbank = Sparkasse | Landesbank,
        GenossenschaftlicheZentralbank = 6,
        Raiffeisenbank = 6,
        GenossenschaftlicheZentralbankOrRaiffeisenbank = GenossenschaftlicheZentralbank | Raiffeisenbank,
        DeutscheBank = 7,
        Commerzbank2 = 8,
        Volksbank = 9
    }
}
