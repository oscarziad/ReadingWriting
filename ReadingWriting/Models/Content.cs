using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingWriting.Models
{
    public class Content
    {
        public string Text { get; set; }

        public Content(string text)
        {
            Text = text;
        }
    }
}