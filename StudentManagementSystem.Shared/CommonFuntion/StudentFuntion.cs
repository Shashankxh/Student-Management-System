using System.Text;

namespace StudentManagementSystem.Shared.CommonFuntion;

public class StudentFuntion
{

    public static string GenerateAdmitionId(string admisionno)
    {
        string numericString = new string([.. admisionno.Where(char.IsDigit)]);

        if (int.TryParse(numericString, out int result))
        {
            return "DAV" + (result + 1).ToString("D4");
        }
        else
        {
            return admisionno;
        }
    }

    public static string GenerateReciptId(string reciptNumber)
    {
        string numericString = new([.. reciptNumber.Where(char.IsDigit)]);

        numericString = numericString[5..];
        if (int.TryParse(numericString, out int result))
        {
            return "REC/" + DateTime.Now.Year + "/"+ (result + 1).ToString("D3");
        }
        else
        {
            return reciptNumber;
        }
    }

    public static string FormatDate(string date)
    {
        StringBuilder result = new ();
        if (date == null) { return ""; }
        for(int i = 0; i < date.Length; i++)
        {
            if (date[i] == '/' || date[i]== '-')
            {
                result.Append(" ");
            }
            else
            {
                result.Append(date[i]);
            }
        }
        string formattedDate = result.ToString();
        return formattedDate;
    }

}
