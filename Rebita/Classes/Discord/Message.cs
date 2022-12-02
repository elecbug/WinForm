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
    internal partial class Manager
    {
        private readonly string prefix = "-";
       
        /// <summary>
        /// 주요 메세지 전달 기능
        /// </summary>
        /// <param name="msg"> 입력받은 메세지의 정보 </param>
        /// <returns> 연결 상태를 반환 </returns>
        private async Task MessageReceived(SocketMessage msg)
        {
            try
            {
                AddDataLogText(DateTime.Now.ToString("HH:mm:ss") + " " + msg.Author.Username + ": " + msg.Content);

                string[] msg_split = msg.Content.Split(' ');
                this.LastChannel = msg.Channel;

                // 본인 메세지는 신경 안쓰게
                if (msg.Author.Id == this.client.CurrentUser.Id)
                {
                    return;
                }

                // 도움말
                else if (msg_split[0] == this.prefix + "help" ||
                         msg_split[0] == this.prefix + "h")
                {
                    if (this.help == 0)
                    {

                        await msg.Channel.SendMessageAsync("레비타는 여러분을 도와줄 만큼 한가하지 않아요! 이번만이에요!\r\n" +
                            "\r\n" +
                            "> 기본 접두 문자는 `-`로 모든 명령어는 `-help`와 같은 형태로 이루어져요!\r\n" +
                            "> 모든 명령어는 두 단어까진 줄여쓸 수 있어요!\r\n" +
                            "> 예를들어 `-help`는 `-h`로, `-time add mon 18:00 20:00`이라면 `-t a mon 18:00 20:00`으로 써도 돼요!\r\n" +
                            "> 아래는 명령어 목록과 설명이에요! 잘 기억해 두세요!\r\n" +
                            "\r\n" +
                            "> `-time`: 공강을 맞춰보기 위해 만들어진 시간표 매칭 기능이에요!\r\n" +
                            "> `[DATE]`는 요일의 영어 약자 3자리고, `[TIME]`은 0:00 ~ 23:59 사이로 써야해요!\r\n" +
                            "> >`add [DATE] [TIME1] [TIME2]`: 나의 공강 시간을 등록해 놔요! `[TIME1]` ~ `[TIME2]`까지에요!\r\n" +
                            "> >`remove [DATE] [TIME1] [TIME2]`: 실수로 잘못 등록한 공강 시간을 삭제해줘요. 위와 같아요!\r\n" +
                            "> >`check [DATE] [TIME]`: 해당 시간에 공강인 다른 사람을 검색해 봐요!\r\n" +
                            "> >`major [DATE]`: 해당 요일에서 많은 사람이 몰린 시간대를 찾아줘요!\r\n" +
                            "\r\n" +
                            "> `-vote`: 투표 관련 기능이에요!\r\n" +
                            "> >`start`: 투표를 시작해요!\r\n" +
                            "> >`end`: 투표를 끝내고 결과를 확인해요! 비밀 투표라 관리자도 몰라요...!\r\n" +
                            "> >`add [NAME]`: 투표 목록을 추가해요!\r\n" +
                            "> >`pick [NUMBER]`: 목록에서 해당 번호에 투표해요! 두 번은 안돼요!\r\n" +
                            "\r\n" +
                            "이젠 확실히 아셨죠? 저는 바쁜 토끼라구요!");

                        this.help += 3;
                    }
                    else if (this.help > 0)
                    {
                        switch (new Random().Next(0, 5))
                        {
                            case 0:
                                await msg.Channel.SendMessageAsync("죄송한데 조금 바빠요! 조금 있다가 다시와요!");
                                break;
                            case 1:
                                await msg.Channel.SendMessageAsync("벌써 까먹으셨어요? 잠시만 기다려 줄래요?");
                                break;
                            case 2:
                                await msg.Channel.SendMessageAsync("오늘따라 일이 많네요... 조금 이따 와요!");
                                break;
                            case 3:
                                await msg.Channel.SendMessageAsync("한번 위로 스크롤 해보세요!");
                                break;
                            case 4:
                                await msg.Channel.SendMessageAsync("말할 때 적어 두셨어야죠! 어라, 다른 분인가?");
                                break;
                        }
                        this.help--;
                    }
                }

                // 공강 조율 기능
                else if (msg_split[0] == this.prefix + "time" ||
                         msg_split[0] == this.prefix + "t")
                {
                    if (msg_split[1] == "add" || msg_split[1] == "a")
                    {
                        int day = 0;

                        switch (msg_split[2].ToLower())
                        {
                            case "mon": day = 1; break;
                            case "tue": day = 2; break;
                            case "wed": day = 3; break;
                            case "thu": day = 4; break;
                            case "fri": day = 5; break;
                        }

                        string[] start_str = msg_split[3].Split(':');
                        string[] end_str = msg_split[4].Split(':');

                        if (int.Parse(start_str[0]) < 8 || int.Parse(start_str[0]) > 22 ||
                            int.Parse(start_str[1]) < 0 || int.Parse(start_str[1]) >= 60 ||
                            int.Parse(end_str[0]) < 8 || int.Parse(end_str[0]) > 22 ||
                            int.Parse(end_str[1]) < 0 || int.Parse(end_str[1]) >= 60)
                        {
                            throw new Exception();
                        }

                        int start_time = ((int.Parse(start_str[0]) - 8) * 12) + (int.Parse(start_str[1]) / 5) + 1;
                        int end_time = ((int.Parse(end_str[0]) - 8) * 12) + (int.Parse(end_str[1]) / 5) + 1;

                        for (int now_time = start_time; now_time < end_time; now_time++)
                        {
                            string cell_value = this.worksheet.Cells[now_time, day].Value2;

                            if (cell_value == null)
                            {
                                cell_value = "";
                            }

                            if (!cell_value.Contains(msg.Author.Username))
                            {
                                cell_value += msg.Author.Username + "\r\n";
                            }

                            this.worksheet.Cells[now_time, day].Value2 = cell_value;
                        }
                    }
                    else if (msg_split[1] == "remove" || msg_split[1] == "r")
                    {
                        int day = 0;

                        switch (msg_split[2].ToLower())
                        {
                            case "mon": day = 1; break;
                            case "tue": day = 2; break;
                            case "wed": day = 3; break;
                            case "thu": day = 4; break;
                            case "fri": day = 5; break;
                        }

                        string[] start_str = msg_split[3].Split(':');
                        string[] end_str = msg_split[4].Split(':');

                        if (int.Parse(start_str[0]) < 8 || int.Parse(start_str[0]) > 22 ||
                            int.Parse(start_str[1]) < 0 || int.Parse(start_str[1]) >= 60 ||
                            int.Parse(end_str[0]) < 8 || int.Parse(end_str[0]) > 22 ||
                            int.Parse(end_str[1]) < 0 || int.Parse(end_str[1]) >= 60)
                        {
                            throw new Exception();
                        }

                        int start_time = ((int.Parse(start_str[0]) - 8) * 12) + (int.Parse(start_str[1]) / 5) + 1;
                        int end_time = ((int.Parse(end_str[0]) - 8) * 12) + (int.Parse(end_str[1]) / 5) + 1;

                        for (int now_time = start_time; now_time < end_time; now_time++)
                        {
                            string cell_value = this.worksheet.Cells[now_time, day].Value2;

                            if (cell_value == null)
                            {
                                cell_value = "";
                            }

                            if (cell_value.Contains(msg.Author.Username))
                            {
                                cell_value = cell_value.Replace(msg.Author.Username + "\r\n", "");
                            }

                            this.worksheet.Cells[now_time, day].Value2 = cell_value;
                        }
                    }
                    else if (msg_split[1] == "check" || msg_split[1] == "c")
                    {
                        int day = 0;

                        switch (msg_split[2].ToLower())
                        {
                            case "mon": day = 1; break;
                            case "tue": day = 2; break;
                            case "wed": day = 3; break;
                            case "thu": day = 4; break;
                            case "fri": day = 5; break;
                        }

                        string[] start_str = msg_split[3].Split(':');

                        if (int.Parse(start_str[0]) < 8 || int.Parse(start_str[0]) > 22 ||
                            int.Parse(start_str[1]) < 0 || int.Parse(start_str[1]) >= 60)
                        {
                            throw new Exception();
                        }

                        int check_time = ((int.Parse(start_str[0]) - 8) * 12) + (int.Parse(start_str[1]) / 5) + 1;

                        string cell_value = this.worksheet.Cells[check_time, day].Value2;

                        if (cell_value == null)
                        {
                            await msg.Channel.SendMessageAsync("저런... 이때는 아무도 시간이 없네요...");
                            return;
                        }

                        cell_value = cell_value.Replace("\r", "");

                        string[] cell_value_split = cell_value.Split('\n');

                        string result = "";

                        for (int i = 0; i < cell_value_split.Length - 1; i++)
                        {
                            result += "[" + cell_value_split[i] + "]님이 ";

                            int private_time = check_time;
                            int time = 0;

                            while (true)
                            {
                                if (private_time > 1 &&
                                    this.worksheet.Cells[private_time, day].Value2 != null &&
                                    this.worksheet.Cells[private_time, day].Value2.Contains(cell_value_split[i]))
                                {
                                    private_time--;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            private_time++;

                            int start_time = private_time;

                            while (true)
                            {
                                if (this.worksheet.Cells[private_time, day].Value2 != null &&
                                    this.worksheet.Cells[private_time, day].Value2.Contains(cell_value_split[i]))
                                {
                                    private_time++;
                                    time += 5;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            result += "" + ((start_time - 1) / 12 + 8) + "시 " + (((start_time - 1) % 12) * 5) + "분부터 "
                                + time + "분간 시간이 있으시네요!\r\n";
                        }

                        await msg.Channel.SendMessageAsync(result);
                        return;
                    }
                    else if (msg_split[1] == "major" || msg_split[1] == "m")
                    {
                        int day = 0;

                        switch (msg_split[2].ToLower())
                        {
                            case "mon": day = 1; break;
                            case "tue": day = 2; break;
                            case "wed": day = 3; break;
                            case "thu": day = 4; break;
                            case "fri": day = 5; break;
                        }

                        string result = "인기 있는 시간대에요!\r\n";

                        for (int i = 1; i <= 180; i++)
                        {
                            string cell_value = this.worksheet.Cells[i, day].Value2;

                            if (cell_value != null && cell_value.Split('\n').Length > 3)
                            {
                                result += "" + ((i - 1) / 12 + 8) + "시 " + (((i - 1) % 12) * 5) + "분에 "
                                    + (cell_value.Split('\n').Length - 1) + "명이 시간이 있어요!\r\n";
                            }
                        }

                        await msg.Channel.SendMessageAsync(result);
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }

                    await msg.Channel.SendMessageAsync("수정된 내용을 전송했어요!");
                }

                //투표 기능
                else if (msg_split[0] == this.prefix + "vote" ||
                         msg_split[0] == this.prefix + "v")
                {
                    if (msg_split[1] == "start" || msg_split[1] == "s")
                    {
                        string vote_msg = "레비타가 투표를 준비하고 있어요!";

                        await msg.Channel.SendMessageAsync(vote_msg);

                        RestUserMessage submsg = await msg.Channel.SendMessageAsync("...");

                        this.vote_manager = new VoteManager(submsg);
                        this.authors = new List<SocketUser>();
                    }
                    else if (msg_split[1] == "end" || msg_split[1] == "e")
                    {
                        string vote_msg = "투표가 종료 됐어요! 결과는...\r\n" + this.vote_manager.WhoWin();

                        this.vote_manager = null;
                        this.authors = new List<SocketUser>();

                        await msg.Channel.SendMessageAsync(vote_msg);
                    }
                    else if (msg_split[1] == "add" || msg_split[1] == "a")
                    {
                        this.vote_manager.AddVote(msg_split[2]);

                        await msg.Channel.ModifyMessageAsync(this.vote_manager.SubMsg.Id,
                            x =>
                            {
                                if (this.vote_manager.ContentsCount == 1)
                                {
                                    this.vote_manager.Msg = "";
                                }

                                this.vote_manager.Msg += "\r\n" + this.vote_manager.ContentsCount + ". " + msg_split[2];

                                x.Content = this.vote_manager.Msg;
                            });
                    }
                    else if (msg_split[1] == "pick" || msg_split[1] == "p")
                    {
                        if (this.authors.Find(x => x == msg.Author) != null)
                        {
                            await msg.Channel.SendMessageAsync("욕심쟁이! 투표는 한 번만이에요!");
                        }
                        else
                        {
                            this.authors.Add(msg.Author);
                            this.vote_manager.PickVote(int.Parse(msg_split[2]));
                        }

                        await msg.DeleteAsync();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                // 사랑해요
                else if (msg_split[0] == this.prefix + "love")
                {
                    await msg.Channel.SendMessageAsync("벌써 " + ++Classes.Love.Default.love + "번째로 사랑받았어요! 사랑해요!");

                    Classes.Love.Default.Save();
                }

                // 하이 레비타
                else if (msg_split[0] == this.prefix + "hi")
                {
                    if (msg_split[1].ToLower() == "rebita")
                    {
                        await msg.Channel.SendMessageAsync("안녕하세요 반가워요!");
                    }
                    else
                    {
                        await msg.Channel.SendMessageAsync("이름이 틀렸잖아요!");
                    }
                }

                // 그 외 오타
                else if (msg_split[0].StartsWith(this.prefix))
                {
                    throw new Exception();
                }
            }
            catch
            {
                await msg.Channel.SendMessageAsync("죄송해요... 이해하지 못했어요...\r\n" +
                    "`-help`를 사용해 보시면 어떨까요?");
            }
        }
    }
}
