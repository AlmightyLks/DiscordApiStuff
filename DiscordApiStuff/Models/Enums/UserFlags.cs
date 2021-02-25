using System;

namespace DiscordApiStuff.Models.Enums
{
    [Flags]
    public enum UserFlags
    {
        None = 0 << 0,

        DiscordEmployee = 1 << 0,

        PartneredServerOwner = 1 << 1,

        HypeSquadEvents = 1 << 2,

        BugHunterLevel1 = 1 << 3,

        // 1 << 4 non-existent

        // 1 << 5 non-existent

        HouseBravery = 1 << 6,

        HouseBrilliance = 1 << 7,

        HouseBalance = 1 << 8,

        EarlySupporter = 1 << 9,

        TeamUser = 1 << 10,

        // 1 << 11 non-existent

        System = 1 << 12,

        // 1 << 13 non-existent

        BugHunterLevel2 = 1 << 14,

        // 1 << 15 non-existent

        VerifiedBot = 1 << 16,

        EarlyVerifiedBotDeveloper = 1 << 17
    }
}