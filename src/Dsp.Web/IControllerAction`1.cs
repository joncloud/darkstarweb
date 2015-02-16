using System.Threading.Tasks;

namespace Dsp.Web
{
    public interface IControllerAction<T>
    {
        Task<T> Execute();
    }
}