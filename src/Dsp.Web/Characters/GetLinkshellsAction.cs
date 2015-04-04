using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dsp.Web.Characters
{
    public class GetLinkshellsAction : IControllerAction
    {
        private DspContext _context;

        public GetLinkshellsAction(DspContext context)
        {
            _context = context;
        }

        public long CharacterId { get; set; }
        public long OwnerAccountId { get; set; }
        public PageSettings PageSettings { get; set; }
        public Uri RequestUri { get; set; }

        async public Task<HttpResponseMessage> Execute()
        {
            IQueryable<long> ownerCharacterIds = _context.characters.Where(c => c.accid == OwnerAccountId).Select(c => c.charid);

            List<long> validLinkshellItems = Enumerable.Range(512, 16).Select(i => (long)i).ToList();
            IOrderedQueryable<CharacterLinkshell> query = _context.character_inventories
                .Where(i => validLinkshellItems.Contains(i.itemId) && 
                            i.charid == CharacterId)
                .Select(i => new CharacterLinkshell {
                    Name = i.signature,
                    Status = (LinkshellStatus)i.itemId,

                    // Current character is owned by the owner account
                    UserCanView = ownerCharacterIds.Contains(i.charid) || 

                                    // Owner character owns a linkshell item of the same name.
                                    ownerCharacterIds.Any(c => _context.character_inventories.Any(ci => ci.charid == c && 
                                                                                                        ci.signature == i.signature && 
                                                                                                        validLinkshellItems.Contains(ci.itemId)))
                })
                .OrderBy(l => l.Name);
            Page<CharacterLinkshell> page = await Page.FromQueryAsync(query, PageSettings);

            return page.ToMessage(RequestUri);
        }
    }
}