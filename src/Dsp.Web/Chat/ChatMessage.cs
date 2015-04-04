using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Dsp.Web.Chat
{
    public class ChatMessage
    {
        public string Message { get; set; }
        public byte[] MessageContents
        {
            set { Message = Encoding.UTF8.GetString(value); }
        }
        public DateTime Occurrence { get; set; }
        public string Speaker { get; set; }
    }
}