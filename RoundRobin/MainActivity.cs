using RoundRobin;

namespace RoundRobin
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private TextView RoundResult { get; set; }
        private EditText Players { get; set; }

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Button upButton = FindViewById<Button>(Resource.Id.SetRound);
            upButton.Click += SetRound_click;
        }
        public void SetRound_click(object sender, System.EventArgs e)
        {
            RoundResult = FindViewById<TextView>(Resource.Id.showResult);
            try
            {
                Players = FindViewById<EditText>(Resource.Id.Players);
                RoundResult.Text = "";
                string[] _Players = Players.Text.Split("\n");
                RoundRobin game = new RoundRobin(_Players);
                game.SetDefaultRound();
                for(int i = 0;i < _Players.Length * (_Players.Length-1) / 2; i++)
                {
                    game.SetRandomNewRound();
                }
                RoundResult.Text = "";
                for (int i = 0; i < game.result.Count; i++)
                {
                    RoundResult.Text += $"第{i+1}場 : {game.result[i].Player1} vs. {game.result[i].Player2}\n";
                }
                if (game.rounds.Count > 0)
                {
                    RoundResult.Text += "-------------------\n以下賽程受限規則無法排入，可以重新點擊排點以重啟本程式重排或是自行手動處理:\n";
                    for (int i = 0; i < game.rounds.Count; i++)
                    {
                        RoundResult.Text += $"{game.rounds[i].Player1} vs. {game.rounds[i].Player2}\n";
                    }
                }
            }
            catch (Exception ex)
            {
                RoundResult.Text = ex.ToString();
            }
        }
    }
}