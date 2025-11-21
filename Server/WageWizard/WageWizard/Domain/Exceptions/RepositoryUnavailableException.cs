namespace WageWizard.Domain.Exceptions
{
    public class RepositoryUnavailableException : DomainException
    {
        public RepositoryUnavailableException(string message) : base(message) { }
    }
}
