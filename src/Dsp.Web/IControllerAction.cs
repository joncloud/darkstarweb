using System.Net.Http;
using System.Threading.Tasks;

namespace Dsp.Web
{
    public interface IControllerAction
    {
        Task<HttpResponseMessage> Execute();
    }
}
