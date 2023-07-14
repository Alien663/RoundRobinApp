using Android.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Android.Graphics.Paint;

namespace RoundRobin
{
    public class RoundRobin
    {
        private string[] players;
        public List<RoundsModel> result = new List<RoundsModel>();
        public List<RoundsModel> rounds = new List<RoundsModel>();
        public RoundRobin(string[] players)
        {
            this.players = players;
            for(int i = 0; i < players.Length; i++)
            {
                for(int j = i + 1; j < players.Length; j++)
                {
                    rounds.Add(new RoundsModel { Player1 = players[i], Player2 = players[j] });
                }
            }
        }

        public bool SetRandomNewRound()
        {
            for(int i = 0; i < 50; i++)
            {
                if(this.rounds.Count == 0) return false;
                this.rounds = ShuffleRounds(this.rounds);
                if(this.result.Count == 0)
                {
                    this.result.Add(this.rounds[0]);
                    this.rounds.RemoveAt(0);
                    return true;
                }
                else if (
                       this.result.Last().Player1 != this.rounds[0].Player1
                    && this.result.Last().Player1 != this.rounds[0].Player2
                    && this.result.Last().Player2 != this.rounds[0].Player1
                    && this.result.Last().Player2 != this.rounds[0].Player2
                )
                {
                    this.result.Add(this.rounds[0]);
                    this.rounds.RemoveAt(0);
                    return true;
                }
            }
            return false;
        }

        public void SetDefaultRound()
        {
            this.result.Add(this.rounds[0]);
            this.rounds.RemoveAt(0);
            List<RoundsModel> temp_round = new List<RoundsModel>();
            temp_round = this.rounds.GetRange(0, this.rounds.Count);
            foreach (RoundsModel round in temp_round)
            {
                if(
                       this.result.Last().Player1 != round.Player1
                    && this.result.Last().Player1 != round.Player2
                    && this.result.Last().Player2 != round.Player1
                    && this.result.Last().Player2 != round.Player2
                )
                {
                    this.result.Add(round);
                    this.rounds.Remove(round);
                }
            }

            if(int.IsOddInteger(this.players.Length))
            {
                RoundsModel temp = this.rounds.First(x => x.Player2 == this.players.Last());
                this.result.Add(temp);
                this.rounds.Remove(temp);
            }
        }

        public void ShowResult()
        {
            Console.WriteLine("\n---------------");
            for(int i = 0; i < this.result.Count; i++)
            {
                Console.WriteLine($"第{i}場 : {this.result[i].Player1} vs. {this.result[i].Player2}");
            }
            Console.WriteLine("---------------\n");
            if(this.rounds.Count > 0)
            {
                Console.WriteLine("以下賽程受限規則無法排入，可以輸入re以重啟本程式重排或是自行手動處理");
                for(int i = 0; i< this.rounds.Count; i++)
                {
                    Console.WriteLine($"{this.rounds[i].Player1} vs. {this.rounds[i].Player2}");
                }
                Console.WriteLine("\n\n");
            }
        }

        private static List<RoundsModel> ShuffleRounds(List<RoundsModel> list)
        {
            var random = new Random();
            var newShuffledList = new List<RoundsModel>();
            var listcCount = list.Count;
            for (int i = 0; i < listcCount; i++)
            {
                var randomElementInList = random.Next(0, list.Count);
                newShuffledList.Add(list[randomElementInList]);
                list.Remove(list[randomElementInList]);
            }
            return newShuffledList;
        }
    }
}
