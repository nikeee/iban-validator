
namespace IbanValidator
{
    public abstract class BbanValidator
    {
        public BbanValidator()
        { }

        public abstract bool Validate(string bban);
    }
}
