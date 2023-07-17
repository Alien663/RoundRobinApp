namespace RoundRobin
{
    public class RoundRobin
    {
        public string[] players;
        private List<RoundsModel> result = new List<RoundsModel>();
        private List<RoundsModel> rounds = new List<RoundsModel>();
        public RoundRobin(string players, string splitchar = "\n")
        {
            this.players = players
                .Split(splitchar)
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();
            if(this.players.Length == 0 )
            {
                throw new Exception("參賽者清單是空的，請輸入參賽者");
            }
            for (int i = 0; i < this.players.Length; i++)
            {
                for (int j = i + 1; j < this.players.Length; j++)
                {
                    rounds.Add(new RoundsModel { Player1 = this.players[i], Player2 = this.players[j] });
                }
            }
        }
        public void setRandomRound(int seeds = 50)
        {
            for(int i = 0; i < seeds; i++)
            {
                if (this.rounds.Count == 0)
                {
                    break;
                }
                this.rounds = ShuffleRounds(this.rounds);
                if (SetNewRound(this.rounds[0]))
                {
                    break;
                }
            }
        }

        public bool SetNewRound(RoundsModel round)
        {
            if (this.result.Count == 0 || isContinue(round))
            {
                this.result.Add(round);
                this.rounds.RemoveAt(0);
                return true;
            }
            return false;
        }

        public void SetDefaultRound()
        {
            List<RoundsModel> temp_round = new List<RoundsModel>();
            temp_round = this.rounds.GetRange(0, this.rounds.Count);
            foreach (RoundsModel round in temp_round)
            {
                SetNewRound(round);
            }
            if(int.IsOddInteger(this.players.Length))
            {
                RoundsModel temp = this.rounds.First(x => x.Player2 == this.players.Last());
                this.result.Add(temp);
                this.rounds.Remove(temp);
            }
        }

        public string ShowResult()
        {
            string Result = "\n---------------\n";
            for(int i = 0; i < this.result.Count; i++)
            {
                Result += $"第{i+1, 4}場 : {this.result[i].Player1} vs. {this.result[i].Player2}\n";
            }
            Result += "---------------\n";
            if(this.rounds.Count > 0)
            {
                Result += "以下賽程受限規則無法排入，可以輸入re以重啟本程式重排或是自行手動處理\n";
                for(int i = 0; i< this.rounds.Count; i++)
                {
                    Result += $"{this.rounds[i].Player1} vs. {this.rounds[i].Player2}\n";
                }
                Result += "\n\n";
            }
            return Result;
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

        private bool isContinue(RoundsModel rounds)
        {
            if (this.result.Last().Player1 != rounds.Player1
                && this.result.Last().Player1 != rounds.Player2
                && this.result.Last().Player2 != rounds.Player1
                && this.result.Last().Player2 != rounds.Player2
            ) return true;
            return false;
        }
    }
}
