using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dsp.Web.Chat
{
    public class GetLinkshellChatMessages : IControllerAction
    {
        private readonly DspContext _context;

        public GetLinkshellChatMessages(DspContext context)
        {
            _context = context;
        }

        public string LinkshellName { get; set; }
        public long OwnerAccountId { get; set; }
        public PageSettings PageSettings { get; set; }
        public Uri RequestUri { get; set; }

        async public Task<HttpResponseMessage> Execute()
        {
            List<long> validLinkshellItems = Enumerable.Range(512, 16).Select(i => (long)i).ToList();
            IOrderedQueryable<ChatMessage> query = _context.characters
                .Where(c => c.accid == OwnerAccountId).SelectMany(c => c.character_inventories)
                .Where(i => validLinkshellItems.Contains(i.itemId) && i.signature == LinkshellName)
                .SelectMany(i => _context.audit_chats.Where(c => c.type == "LINKSHELL" && c.recipient == i.signature))
                .ToChatMessage()
                .OrderByDescending(a => a.Occurrence);

            Page<ChatMessage> page = await Page.FromQueryAsync(query, PageSettings);

            return page.ToMessage(RequestUri);
        }
    }
}