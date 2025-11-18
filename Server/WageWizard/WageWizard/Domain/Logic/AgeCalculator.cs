namespace WageWizard.Domain.Logic
{
    public static class AgeCalculator
    {
        public static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            return age;
        }
    }
}
