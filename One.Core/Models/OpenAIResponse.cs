using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One.Core.Models
{
    public class OpenAIResponse
    {
        public OpenAIChoice[] choices { get; set; }
    }
}
