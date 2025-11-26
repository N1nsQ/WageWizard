using WageWizard.Domain.Exceptions;

namespace WageWizardTests.TestUtils
{
    public class TestDomainException : DomainException
    {
        public TestDomainException(string message) : base(message) { }
    }
}
