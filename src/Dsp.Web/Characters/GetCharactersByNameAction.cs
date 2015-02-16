using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dsp.Web.Characters
{
    public class GetCharactersByNameAction : IControllerAction
    {
        private readonly DspContext _context;

        public GetCharactersByNameAction(DspContext context)
        {
            _context = context;
        }

        public string Name { get; set; }
        public long OwnerAccountId { get; set; }
        public PageSettings PageSettings { get; set; }
        public Uri RequestUri { get; set; }

        async public Task<HttpResponseMessage> Execute()
        {
            IOrderedQueryable<Character> query = _context.characters
                   .Where(c => c.charname.IndexOf(Name) >= 0)
                   .ToPartialCharacter(OwnerAccountId)
                   .OrderBy(c => c.Name);
            Page<Character> page = await Page.FromQueryAsync(query, PageSettings);

            return page.ToMessage(RequestUri);
        }
    }
}