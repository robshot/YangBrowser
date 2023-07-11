using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangTreeView.Yang.YangObjects
{
    public class Uses
    {
        public string Name { get; set; }

        public string InternalPrefix { get; set; }

        public string ExternalPrefix { get; set; }

        public bool IsParsed { get; set; }

        public Uses()
        {

        }

        public Uses(string line, string prefix)
        {
            string[] fullUses = line.Split(' ')[1].Replace(";", string.Empty).Split(':');
            if (fullUses.Length == 1)
            {
                InternalPrefix = prefix;
                Name = fullUses[0];
                ExternalPrefix = prefix;
            }
            else
            {
                InternalPrefix = fullUses[0];
                Name = fullUses[1];
                if (InternalPrefix == prefix)
                {
                    ExternalPrefix = prefix;
                }
            }
        }
    }
}
