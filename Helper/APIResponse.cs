namespace BIS_project.Helper;

public class APIResponse
{
    public int ResponseCode { get; set; }
    public dynamic Result { get; set; }
    public string Errormessage { get; set; } = string.Empty;

    public APIResponse(int responseCode, dynamic result, string errormessage)
    {
        ResponseCode = responseCode;
        Result = result;
        Errormessage = errormessage;
    }
}   