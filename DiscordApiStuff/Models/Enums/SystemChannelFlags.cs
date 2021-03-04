using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Enums
{
    [Flags]
    public enum SystemChannelFlags : byte
    {
        SurrpressJoinNotification = 1 << 0,
        SurrpressPremiumSubscriptions = 1 << 1
    }
}
