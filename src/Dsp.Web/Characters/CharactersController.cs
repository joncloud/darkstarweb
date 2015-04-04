using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dsp.Web.Characters
{
    [Authorize]
    [RoutePrefix("api/Characters")]
    public class CharactersController : ApiController
    {
        private DspContext _context;

        public CharactersController()
        {
            _context = new DspContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        [Route("{id}")]
        public Task<Character> Get(long id)
        {
            var action = new GetCharacterAction(_context);
            action.CharacterId = id;
            action.OwnerAccountId = User.Identity.GetAccountId();
            return action.Execute();
        }

        [Route]
        public Task<HttpResponseMessage> Get([FromUri]PageSettings pageSettings)
        {
            var action = new GetAllCharactersAction(_context);
            action.OwnerAccountId = User.Identity.GetAccountId();
            action.PageSettings = pageSettings;
            action.RequestUri = Request.RequestUri;
            return action.Execute();
        }

        [Route("Name/{name}")]
        public Task<HttpResponseMessage> GetByName(string name, [FromUri]PageSettings pageSettings)
        {
            var action = new GetCharactersByNameAction(_context);
            action.Name = name;
            action.OwnerAccountId = User.Identity.GetAccountId();
            action.PageSettings = pageSettings;
            action.RequestUri = Request.RequestUri;
            return action.Execute();
        }

        [Route("{id}/Linkshells")]
        public Task<HttpResponseMessage> GetLinkshells(long id, [FromUri]PageSettings pageSettings)
        {
            var action = new GetLinkshellsAction(_context);
            action.CharacterId = id;
            action.PageSettings = pageSettings;
            action.RequestUri = Request.RequestUri;
            return action.Execute();
        }

        [Route("My")]
        public Task<HttpResponseMessage> GetMyCharacters([FromUri]PageSettings pageSettings)
        {
            var action = new GetMyCharactersAction(_context);
            action.OwnerAccountId = User.Identity.GetAccountId();
            action.PageSettings = pageSettings;
            action.RequestUri = Request.RequestUri;
            return action.Execute();
        }

        [Route("Online")]
        public Task<HttpResponseMessage> GetOnlineCharacters([FromUri]PageSettings pageSettings, string name = "")
        {
            var action = new GetOnlineCharactersAction(_context);
            action.Name = name;
            action.OwnerAccountId = User.Identity.GetAccountId();
            action.PageSettings = pageSettings;
            action.RequestUri = Request.RequestUri;
            return action.Execute();
        }

        [HttpPut]
        [Route("{id}/Location")]
        public Task<HttpResponseMessage> PutLocation(long id, [FromBody]ResetLocation resetLocation)
        {
            // TODO Determine the user's role and let administrators maintain other characters.
            var action = new ResetLocationAction(_context);
            action.CharacterId = id;
            action.IPAddress = this.GetIPAddress();
            action.Location = resetLocation;
            action.OwnerAccountId = User.Identity.GetAccountId();
            return action.Execute();
        }
    }
}