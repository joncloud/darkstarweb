using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dsp.Web.Characters
{
    public class GetOnlineCharactersAction : IControllerAction
    {
        private readonly DspContext _context;

        public GetOnlineCharactersAction(DspContext context)
        {
            _context = context;
        }

        public string Name { get; set; }
        public long OwnerAccountId { get; set; }
        public PageSettings PageSettings { get; set; }
        public Uri RequestUri { get; set; }

        async public Task<HttpResponseMessage> Execute()
        {
            IQueryable<character> characterQuery = _context.account_sessions
                .Select(s => s.character);

            if (!string.IsNullOrEmpty(Name))
            {
                characterQuery = characterQuery.Where(c => c.charname.IndexOf(Name) >= 0);
            }

            IOrderedQueryable<Character> query = characterQuery
                .ToPartialCharacter(OwnerAccountId)
                .OrderBy(c => c.Name);
            Page<Character> page = await Page.FromQueryAsync(query, PageSettings);
            return page.ToMessage(RequestUri);
        }
    }
}