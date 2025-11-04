namespace WageWizard.Utils
{
    public static class EmployeeHelperFunctions
    {
        public static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            return age;
        }
    }
}
