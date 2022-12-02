using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rebita.Classes.Discord
{
    /// <summary>
    /// 디스코드 관련 기능을 처리하는 클래스
    /// </summary>
    internal partial class Manager
    {
        private DiscordSocketClient client;

        private RichTextBox data_log;
        private RichTextBox data_input;

        private Excel.Application application;
        private Excel.Workbook workbook;
        private Excel.Worksheet worksheet;

        private VoteManager vote_manager;
        private List<SocketUser> authors;

        private int help;

        /// <summary>
        /// 마지막으로 메세지가 오갔던 채널
        /// </summary>
        public ISocketMessageChannel LastChannel { get; set; }

        /// <summary>
        /// 디스코드 매니저 생성자
        /// </summary>
        /// <param name="data_log"> 출력 텍스트 박스 </param>
        /// <param name="data_input"> 입력 텍스트 박스 </param>
        public Manager(RichTextBox data_log, RichTextBox data_input)
        {
            this.data_log = data_log;
            this.data_input = data_input;
            this.help = 0;
        }

        /// <summary>
        /// 봇을 시작하고, 엑셀 시트를 연결
        /// </summary>
        public async void StartBot()
        {
            this.client = new DiscordSocketClient();

            this.client.Log += Log;
            this.client.Ready += Ready;
            this.client.MessageReceived += MessageReceived;

            string token = Classes.Token.token;

            await this.client.LoginAsync(TokenType.Bot, token);
            await this.client.StartAsync();

            this.application = new Excel.Application();
            this.workbook = application.Workbooks.Open(Environment.CurrentDirectory + "\\DB.xlsx");
            this.worksheet = workbook.Worksheets[1];

            await Task.Delay(-1);
        }

        /// <summary>
        /// 봇을 종료하고, 엑셀 시트 연결을 해제
        /// </summary>
        public async void StopBot()
        {
            this.workbook.Save();
            this.workbook.Close();
            this.application.Quit();

            await this.client.StopAsync();
        }

        /// <summary>
        /// 봇의 연결을 준비함
        /// </summary>
        /// <returns> 연결 상태를 반환 </returns>
        private Task Ready()
        {
            AddDataLogText($"Connected {this.client.CurrentUser}");

            return Task.CompletedTask;
        }

        /// <summary>
        /// 봇의 상태를 기록
        /// </summary>
        /// <param name="msg"> 기록할 메세지 </param>
        /// <returns> 연결 상태를 반환 </returns>
        private Task Log(LogMessage msg)
        {
            AddDataLogText(msg.ToString());

            return Task.CompletedTask;
        }

        /// <summary>
        /// 출력 텍스트 박스에 로그를 기록
        /// </summary>
        /// <param name="text"> 기록할 출력 텍스트 박스 </param>
        public void AddDataLogText(string text)
        {
            this.data_log.Invoke((MethodInvoker)
                delegate
                {
                    this.data_log.Text += text + "\r\n";
                });
        } 
    }
}
