using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Dsp.Web.Chat
{
    public class GetTellChatMessages : IControllerAction
    {
        private readonly DspContext _context;

        public GetTellChatMessages(DspContext context)
        {
            _context = context;
        }

        public long CharacterId { get; set; }
        public long OwnerAccountId { get; set; }
        public PageSettings PageSettings { get; set; }
        public Uri RequestUri { get; set; }
        
        async public Task<HttpResponseMessage> Execute()
        {
            IOrderedQueryable<ChatMessage> query = _context.characters
                .Where(c => c.accid == OwnerAccountId && c.charid == CharacterId)
                .SelectMany(i => _context.audit_chats.Where(c => c.type == "TELL" && (c.recipient == i.charname || c.speaker == i.charname)))
                .ToChatMessage()
                .OrderByDescending(a => a.Occurrence);

            Page<ChatMessage> page = await Page.FromQueryAsync(query, PageSettings);

            return page.ToMessage(RequestUri);
        }
    }
}