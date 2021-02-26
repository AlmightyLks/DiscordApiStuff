using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Enums
{
    public enum MessageType : byte
    {
        Default,
        RecipientAdd,
        RecipientRemove,
        Call,
        ChannelNameChange,
        ChannelIconChange,
        ChannelPinnedMessage,
        GuildMemberJoin,
        UserPremiumGuildSubscription,
        UserPremiumGuildSubscriptionTier1,
        UserPremiumGuildSubscriptionTier2,
        UserPremiumGuildSubscriptionTier3,
        ChannelFollowAdd,
        GuildDiscoveryDisqualified,
        GuildDiscoveryRequalified,
        Reply,
        ApplicationCommand
    }
}
