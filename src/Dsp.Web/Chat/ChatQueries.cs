using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dsp.Web.Chat
{
    public static class ChatQueries
    {
        public static IQueryable<ChatMessage> ToChatMessage(this IQueryable<audit_chat> query)
        {
            return query.Select(a => new ChatMessage
            {
                MessageContents = a.message,
                Occurrence = a.datetime,
                Recipient = a.recipient,
                Speaker = a.speaker
            });
        }
    }
}