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
                    RoundResult.Text += $"��{i+1}�� : {game.result[i].Player1} vs. {game.result[i].Player2}\n";
                }
                if (game.rounds.Count > 0)
                {
                    RoundResult.Text += "-------------------\n�H�U�ɵ{�����W�h�L�k�ƤJ�A�i�H���s�I�����I�H���ҥ��{�����ƩάO�ۦ��ʳB�z:\n";
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