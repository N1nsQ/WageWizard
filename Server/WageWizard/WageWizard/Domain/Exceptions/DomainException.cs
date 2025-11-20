namespace WageWizard.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message) { }
    }

    public class NotFoundException : DomainException
    {
        public NotFoundException(string message) : base(message) { }
    }
}
