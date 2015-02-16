using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dsp.Web.Characters
{
    public class GetCharacterAction : IControllerAction<Character>
    {
        private readonly DspContext _context;

        public GetCharacterAction(DspContext context)
        {
            _context = context;
        }

        public long CharacterId { get; set; }
        public long OwnerAccountId { get; set; }

        async public Task<Character> Execute()
        {
            Character character = await _context.characters
                                                .Where(c => c.charid == CharacterId)
                                                .ToCharacter(OwnerAccountId)
                                                .SingleOrDefaultAsync();
            if (character == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return character;
        }
    }
}