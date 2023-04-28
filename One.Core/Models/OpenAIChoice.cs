using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One.Core.Models
{
    public class OpenAIChoice
    {
        public string text { get; set; }

        public double? logpropbs { get; set; }

        public string[] tokens { get; set; }
    }
}
