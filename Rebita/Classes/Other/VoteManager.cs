using Discord.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebita.Classes
{
    /// <summary>
    /// 투표 기능을 관리하는 매니저 클래스
    /// </summary>
    internal class VoteManager
    {
        /// <summary>
        /// 각각의 투표 항목 클래스
        /// </summary>
        internal class VoteContent
        {
            public uint Id { get; set; }
            public string Name { get; set; }
            public uint Vote { get; set; }
        }

        private List<VoteContent> contents;

        /// <summary>
        /// 투표 관련 메세지 저장 변수로, 주로 투표 목록을 저장하는데 사용
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 매니저가 관리하는 투표 목록의 갯수
        /// </summary>
        public int ContentsCount => this.contents.Count;

        /// <summary>
        /// 투표의 목록 메세지 관련 저장 객체
        /// </summary>
        public RestUserMessage SubMsg { get; }

        /// <summary>
        /// 투표 매니저 생성자
        /// </summary>
        /// <param name="submsg"> 계속 수정될 투표 목록 </param>
        public VoteManager(RestUserMessage submsg)
        {
            this.Msg = "...";
            this.SubMsg = submsg;
            this.contents = new List<VoteContent>();
        }

        /// <summary>
        /// 투표 목록 추가
        /// </summary>
        /// <param name="name"> 추가할 목록 이름 </param>
        public void AddVote(string name)
        {
            this.contents.Add(new VoteContent() { Id = (uint)this.contents.Count + 1, Name = name, Vote = 0 });
        }

        /// <summary>
        /// 투표를 진행
        /// </summary>
        /// <param name="id"> 뽑을 투표의 번호 (1부터 시작)</param>
        public void PickVote(int id)
        {
            this.contents[id - 1].Vote++;
        }

        /// <summary>
        /// 승패 정보를 반환
        /// </summary>
        /// <returns> 순위대로 정렬된 문자열 </returns>
        public string WhoWin()
        {
            this.contents.Sort((x, y) => { if (x.Vote > y.Vote) return -1; else return 0; });

            string result = "";

            foreach (VoteContent content in this.contents)
            {
                result += "" + content.Id + ". " + content.Name + ": " + content.Vote + "표\r\n";
            }

            return result;
        }
    }
}
