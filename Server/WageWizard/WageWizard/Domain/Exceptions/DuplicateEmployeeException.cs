namespace WageWizard.Domain.Exceptions
{
    public class DuplicateEmployeeException : Exception
    {
        public DuplicateEmployeeException(string message) : base(message)
        {
        }
    }
}
