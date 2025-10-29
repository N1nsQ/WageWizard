namespace WageWizard.Utils
{
    public class EmployeeHelperFunctions
    {
        public static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            return age;
        }
    }
}
