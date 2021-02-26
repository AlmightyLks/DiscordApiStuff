using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Enums
{
    [Flags]
    public enum MessageFlags
    {
        CrossPosted = 1 << 0,
        IsCrosspost = 1 << 1,
        SuppressEmbeds = 1 << 2,
        SourceMessageDeleted = 1 << 3,
        Urgent = 1 << 4
    }
}
