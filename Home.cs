using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using AutoUpdaterDotNET;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Configuration;

namespace Louie_Bot
{
    public partial class Home : Form
    {
        DiscordSocketClient _client;
        CommandService _commands;
        IServiceProvider _services;
        sqlConnector _sql;

        public Home()
        {
            InitializeComponent();
            Home.CheckForIllegalCrossThreadCalls = false;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            RunBotAsync();
            lblVersion.Text = "v" + Application.ProductVersion;
        }

        async Task RunBotAsync()
        {
            _sql = new sqlConnector();
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            _sql.Setup();
            
            var botToken = ConfigurationManager.AppSettings["token"];

            _client.Log += Log;

            await RegisterCommandsAsync();
            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();

            await Task.Delay(-1);

        }

        Task Log(LogMessage arg)
        {
            string curdatetime = DateTime.Now.ToString("HH:mm:ss");

            txtOutput.AppendText(curdatetime + " " + arg.Message + Environment.NewLine);

            return Task.CompletedTask;
        }

        Task Log(string log)
        {
            txtOutput.AppendText(log + Environment.NewLine);

            return Task.CompletedTask;
        }

        async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null) return;

            int argPos = 0;

            string curdatetime = DateTime.Now.ToString("HH:mm:ss");

            //Thread sqlFunctions = new Thread(() => sqlCalls(arg));
            //sqlFunctions.Start();

            //Delete Messages
            if (arg.Author.Username.Contains("Dyno#3861"))
            {
                var delmsgcount = int.Parse(_sql.GetSingleValue("SELECT msgCount FROM stats"));
                _sql.Execute($"UPDATE stats SET messageCount ={delmsgcount - 1}");
            }

            if (message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);
                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                {
                    await message.DeleteAsync();
                    await context.Channel.SendMessageAsync("Error: " + result.ErrorReason);
                    await Log(curdatetime + " Error       " + result.ErrorReason);
                }
                else
                {
                    await message.DeleteAsync();
                    await Log(curdatetime + " Command     " + message.Content + "     " + message.Author);
                }

            }

            //Delete Messages in Stream Channel
            if (arg.Channel.Name.Contains("streaming") && (arg.Author.IsBot == false))
            {
                await arg.DeleteAsync();
            }
        }

        private void BtnCheckForUpdate_Click(object sender, EventArgs e)
        {
            Thread check = new Thread(CheckForUpdate);
            check.Start();
        }

        private void TmrUpdateCheck_Tick(object sender, EventArgs e)
        {
            Thread check = new Thread(CheckForUpdate);
            check.Start();
        }

        public void CheckForUpdate() {
            AutoUpdater.DownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.Start("https://discord.gvesports.org/Louie.xml");
        }
    }
}
