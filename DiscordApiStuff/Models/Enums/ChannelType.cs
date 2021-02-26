﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Enums
{
    public enum ChannelType : byte
    {
        GuildText,
        DirectMessage,
        GuildVoice,
        GroupDM,
        GuildCategory,
        GuildNews,
        GuildStore
    }
}
