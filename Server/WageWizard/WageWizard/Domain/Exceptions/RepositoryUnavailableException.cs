namespace WageWizard.Domain.Exceptions
{
    public class RepositoryUnavailableException : Exception
    {
        public RepositoryUnavailableException(string message) : base(message) { }
    }
}
