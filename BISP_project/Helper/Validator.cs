using System.Reflection;
namespace BIS_project.Helper;
using System.Threading.Tasks;

public class Validator
{
    public async Task<bool> Validate<T>(T classType, object obj)
    {
        if (obj == null)
        {
            return false;
        }
        PropertyInfo[] properties = classType.GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (property.GetValue(obj) == null)
            {
                return false;
            }
        }

        return true;
    }

    
}