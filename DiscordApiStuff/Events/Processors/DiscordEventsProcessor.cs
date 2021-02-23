using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using DiscordApiStuff.Events.EventArgs.Guild;
using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Events.Handlers;
using DiscordApiStuff.Payloads.Gateway;

namespace DiscordApiStuff.Events.Processors
{
    public delegate void DiscordEvent<T>(ref T EventItem) where T : IDiscordEventArgs;

    public struct DiscordEventsProcessor
    {
        public void ProcessPayload(GeneralPayload payload)
        {
            switch (payload.Event)
            {
                //Guild
                case "GUILD_CREATE":
                {
                    
                    break;
                }
                case "GUILD_DELETE":
                {
                    
                    
                    break;
                }
                case "GUILD_ROLE_CREATE":
                {
                    
                    
                    break;
                }
                case "GUILD_ROLE_UPDATE":
                {
                    
                    
                    break;
                }
                case "GUILD_ROLE_DELETE":
                {
                    
                    
                    break;
                }
                case "CHANNEL_CREATE":
                {
                    break;
                }
                case "CHANNEL_UPDATE":
                {
                    break;
                }
                case "CHANNEL_DELETE":
                {
                    break;
                }
                case "CHANNEL_PINS_UPDATE":
                {
                    break;
                }
                //Guild Members
                case "GUILD_MEMBER_ADD":
                {
                    
                    
                    break;
                }
                case "GUILD_MEMBER_UPDATE":
                {
                    
                    
                    break;
                }
                case "GUILD_MEMBER_REMOVE":
                {
                    
                    
                    break;
                }
                //Guild Bans
                case "GUILD_BAN_ADD":
                {
                    
                    
                    break;
                }
                case "GUILD_BAN_REMOVE":
                {
                    
                    
                    break;
                }
                //Guild Emojis
                case "GUILD_EMOJIS_UPDATE":
                {
                    
                    
                    break;
                }
                //Guild Integrations
                case "GUILD_INTEGRATIONS_UPDATE":
                {
                    
                    
                    break;
                }
                //Guild Webhooks
                case "WEBHOOKS_UPDATE":
                {
                    
                    
                    break;
                }
                //Guild Invites
                case "INVITE_CREATE":
                {
                    
                    
                    break;
                }
                case "INVITE_DELETE":
                {
                    
                    
                    break;
                }
                //Guild Voice States
                case "VOICE_STATE_UPDATE":
                {
                    
                    
                    break;
                }
                //Guild PRESENCE
                case "PRESENCE_UPDATE":
                {
                    
                    
                    break;
                }
                //Guild Message Reactions
                case "MESSAGE_REACTION_ADD":
                {

                    
                    break;
                }
                case "MESSAGE_REACTION_REMOVE":
                {

                    
                    break;
                }
                case "MESSAGE_REACTION_REMOVE_ALL":
                {

                    
                    break;
                }
                case "MESSAGE_REACTION_REMOVE_EMOJI":
                {

                    
                    break;
                }
                //Guild / Direct Message Typing
                case "TYPING_START":
                {

                    
                    break;
                }
                //Messages
                case "MESSAGE_CREATE":
                {
                    
                    
                    break;
                }
                case "MESSAGE_UPDATE":
                {

                    
                    break;
                }
                case "MESSAGE_DELETE":
                {

                    
                    break;
                }
            }
        }
    }
}