iban-validator
==============

A small library used for IBAN and BBAN validation.

Example usage:
```C#
var iban = new Iban("DE", 68, "210501700012345678");
Console.WriteLine(iban.IsValid);

// Parse:
iban = Iban.Parse("DE88 2008 0000 0970 3757 10");
Console.WriteLine(iban.IsValid);

// TryParse:
if(Iban.TryParse("DE88 2008 0000 0970 3757 10", out iban))
{
    Console.WriteLine(iban.IsValid);

    var info = new GermanyIbanInformationProvider(iban);
    Console.WriteLine("Bankengruppe: " + info.Bankleitzahl.Bankengruppe.ToString());
    Console.WriteLine("Clearing area: " + info.Bankleitzahl.ClearingArea.ToString());
}
```
