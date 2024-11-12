namespace API.Extensions;

//não pode ser instanciada diretamente, ou seja, você não pode criar um objeto da classe static usando o operador new.
public static class DateTimeExtensions 
{
    public static int CalculateAge(this DateOnly dob) //dob = date of birth
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        if (dob > today) {
            throw new ArgumentException("Data de nascimento inválida");
        }
        var age = today.Year - dob.Year;
        if (dob.Month > today.Month
            || (dob.Month == today.Month 
            && dob.Day > today.Day)) {
            age--;
        }
   
        return age;
    }
}
