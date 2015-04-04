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
            set { Message = Deserialize(value); }
        }
        public DateTime Occurrence { get; set; }
        public string Recipient { get; set; }
        public string Speaker { get; set; }

        private static string Deserialize(byte[] contents)
        {
            StringBuilder text = new StringBuilder();
            bool translate = false;
            int start = 0;
            for (int i = 0; i < contents.Length; i++)
            {
                byte b = contents[i];

                // 0xFD surrounds the auto translate information.
                if (b == 0xFD)
                {
                    if (translate)
                    {
                        translate = false;

                        // For now just print the values indicating that this is an auto translate text.
                        text.Append("(")
                             .Append(BitConverter.ToString(contents, start + 1, i - start - 1))
                             .Append(")");
                        start = i;
                    }
                    else
                    {
                        translate = true;

                        // Print all of the text up until the auto translate text.
                        text.Append(Encoding.UTF8.GetString(contents, start, i - start));
                        start = i;
                    }
                }
            }

            // If there was no auto translate text, then just convert the text.
            if (text.Length == 0 && contents.Length > 0)
            {
                text.Append(Encoding.UTF8.GetString(contents));
            }

            return text.ToString();
        }
    }
}