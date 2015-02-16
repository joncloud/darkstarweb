using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dsp.Web.Characters
{
    public class GetMyCharactersAction : IControllerAction
    {
        private readonly DspContext _context;

        public GetMyCharactersAction(DspContext context)
        {
            _context = context;
        }

        public long OwnerAccountId { get; set; }
        public PageSettings PageSettings { get; set; }
        public Uri RequestUri { get; set; }

        async public Task<HttpResponseMessage> Execute()
        {
            IOrderedQueryable<Character> query = _context.accounts.Where(a => a.id == OwnerAccountId)
                .SelectMany(s => s.characters)
                .ToPartialCharacter(OwnerAccountId)
                .OrderBy(c => c.Name);
            Page<Character> page = await Page.FromQueryAsync(query, PageSettings);
            return page.ToMessage(RequestUri);
        }
    }
}