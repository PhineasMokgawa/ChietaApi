using System.Threading.Tasks;

namespace CHIETAMIS.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}