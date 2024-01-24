namespace LTT.Common
{
    public interface IOtpService
    {
        string GenerateAndSendOtp(string email);
        string ValidateOtp(string email, string enteredOtp);
    }
}
