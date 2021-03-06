﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Louie_Bot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        sqlConnector sql = new sqlConnector();

        [Command("ping")]
        [Remarks("!ping")]
        [Summary("This is to check the server connection time.")]
        public async Task Ping()
        {
            await ReplyAsync($"**Latency:  ** {Context.Client.Latency} ms");
        }

        [Command("help")]
        [Remarks("!help")]
        [Summary("Sends back a link to recieve help.")]
        public async Task Help()
        {
            await ReplyAsync("**Please Refer to the link below for help.**" + Environment.NewLine + Environment.NewLine + "https://goo.gl/g1jT4V");
        }

        [Command("stream")]
        [Remarks("!stream (link)")]
        [Summary("Displays the link passed in by the user.")]
        public async Task Stream([Remainder] string link)
        {
            if (!link.Contains("https://") && !link.Contains("twitch.tv"))
            {
                await ReplyAsync("Please make sure you are adding a link at the end of your command!");
            }
            else
            {
                await ReplyAsync(link + " is streaming! Go check them out.");
            }
        }

        [Command("msgcount")]
        [Remarks("!msgcount")]
        [Summary("Gets the message count of user.")]
        public async Task MessageCount()
        {
            sql.Setup();
            string userMsgCount = sql.GetSingleValue($"SELECT msgCount FROM users WHERE id = '{Context.Message.Author.Id}'");
            string guildMessageCount = sql.GetSingleValue("SELECT msgCount FROM stats WHERE id = 'gves'");

            await ReplyAsync("There are currently **" + (int.Parse(guildMessageCount) + 1) + "** messages in this discord!" + Environment.NewLine + Environment.NewLine + "<@" + Context.Message.Author.Id.ToString() + "> has sent **" + userMsgCount + "** messages in this discord!");
        }

        [Command("mystream")]
        [Remarks("!mystream")]
        [Summary("Displays the stream of linked user.")]
        public async Task MyStream()
        {
            sql.Setup();

            try
            {
                string link = sql.GetSingleValue($"SELECT link FROM streamTeam WHERE id = '{Context.Message.Author.Id}'");
                await ReplyAsync(link + " is streaming! Go check them out.");
            }
            catch
            {
                await ReplyAsync("Stream not linked to account. Message SmokeyFish to get this setup!");
            }

        }

        [Command("usercount")]
        [Remarks("!usercount")]
        [Summary("Outputs current count of users.")]
        public async Task UserCount()
        {
            await ReplyAsync("There are currently **" + Context.Client.GetGuild(347170618250231809).Users.Count + "** users in this discord!");
        }

        [Command("mydiscord")]
        [Remarks("!mydiscord")]
        [Summary("Get stats of the user that calls this command.")]
        public async Task MyDiscord()
        {
            sql.Setup();
            string msgString = "";
            string id = Context.Message.Author.Id.ToString();
            string username = sql.GetSingleValue($"SELECT username FROM users WHERE id = '{id}'");
            string joinedAt = sql.GetSingleValue($"SELECT joinedAt FROM users WHERE id = '{id}'");
            string createdAt = sql.GetSingleValue($"SELECT createdAt FROM users WHERE id = '{id}'");
            string msgCount = sql.GetSingleValue($"SELECT msgCount FROM users WHERE id = '{id}'");

            msgString += "**Nickname: **" + username + Environment.NewLine;
            msgString += "**Joined Server: **" + joinedAt + Environment.NewLine;
            msgString += "**Created Account: **" + createdAt + Environment.NewLine;
            msgString += "**Message Count: **" + msgCount + Environment.NewLine;

            await ReplyAsync(msgString);
        }

        [Command("set")]
        [Remarks("!set (role)")]
        [Summary("Sets a new role to the user")]
        public async Task SetRole([Remainder] string game)
        {
            sql.Setup();

            await ReplyAsync("");
        }

        [Command("role")]
        [Remarks("!role (role)")]
        [Summary("Outputs a list of users in that role")]
        public async Task GetRoleList([Remainder] string role)
        {
            role = role.ToLower();
            var socket = Context.Client.GetGuild(Context.Guild.Id);
            List<SocketGuildUser> userList = socket.Users.Cast<SocketGuildUser>().ToList();

            List<string> outList = new List<string>();

            foreach (SocketGuildUser user in userList) {
                foreach (SocketRole userRole in user.Roles) {
                    if (userRole.Name.ToLower() == role) {
                        outList.Add(user.Username.ToString());
                    }
                }
            }

            if (outList.Count == 0)
            {
                await ReplyAsync("There are no users in this role.");
            }
            else {
                string msgString = $"There are currently {outList.Count} users in {role}!" ;

                foreach (string user in outList)
                {
                    msgString += Environment.NewLine + user;
                }

                await ReplyAsync(msgString);
            }

        }
    }
}