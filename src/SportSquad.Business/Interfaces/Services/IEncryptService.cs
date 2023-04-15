namespace SportSquad.Business.Interfaces.Services;

public interface IEncryptService
{
    string EncryptPassword(string password);
}