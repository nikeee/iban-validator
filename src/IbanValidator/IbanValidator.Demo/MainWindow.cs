using System;
using System.Text;
using System.Windows.Forms;

namespace IbanValidator.Demo
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            ibanTextbox.Text = "DE68210501700012345678"; // Sample IBAN
        }

        private void IbanTextboxTextChanged(object sender, EventArgs e)
        {
            validIbanLabel.Text = ValidateIban();
        }

        private string ValidateIban()
        {
            var potentialIban = ibanTextbox.Text;

            if (string.IsNullOrWhiteSpace(potentialIban))
            {
                DisplayCountryInformation(null);
                return "No IBAN given.";
            }

            Iban iban;
            if (!Iban.TryParse(potentialIban, out iban))
                return "IBAN not even close to be valid.";

            var isValid = iban.IsValid;

            DisplayCountryInformation(iban);

            if (isValid)
                ibanTextbox.Text = iban.ToString();

            return isValid ? "IBAN seems to be valid" : "IBAN is not valid";
        }

        private void DisplayCountryInformation(Iban iban)
        {
            if (iban == null)
            {
                ibanInformationLabel.Text = "No information available.";
                return;
            }

            var sb = new StringBuilder();

            sb.AppendLine("Country code: " + iban.CountryCode);
            sb.AppendLine("    Checksum: " + iban.Checksum);
            sb.AppendLine("        Bban: " + iban.Bban);

            if (iban.CountryCode == "DE")
                DisplayGermanySpecificInformation(iban, sb);
            ibanInformationLabel.Text = sb.ToString();
        }

        private void DisplayGermanySpecificInformation(Iban iban, StringBuilder sb)
        {
            sb.AppendLine();

            if (!iban.IsValid)
            {
                sb.AppendLine("No information available (IBAN incorrect)");
                return;
            }

            sb.AppendLine("German information");
            var info = new Specialized.Germany.GermanyIbanInformationProvider(iban);
            sb.AppendLine("        Kontonummer: " + info.Kontonummer.Value);
            sb.AppendLine("       Bankleitzahl: " + info.Bankleitzahl.Value);
            sb.AppendLine("       Bankengruppe: " + info.Bankleitzahl.Bankengruppe);
            sb.AppendLine("      Clearing Area: " + info.Bankleitzahl.ClearingArea);
            sb.AppendLine("Individuelle Nummer: " + info.Bankleitzahl.IndividualNumber);
        }
    }
}
