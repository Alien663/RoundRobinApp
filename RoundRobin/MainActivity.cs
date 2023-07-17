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
            RoundResult = FindViewById<TextView>(Resource.Id.showResult);
            upButton.Click += SetRound_click;
            RoundResult.Text = "賽程排點結果 : \n";
        }
        public void SetRound_click(object sender, System.EventArgs e)
        {
            RoundResult = FindViewById<TextView>(Resource.Id.showResult);
            Players = FindViewById<EditText>(Resource.Id.Players);
            RoundResult.Text = "賽程排點結果 : \n";
            try
            {
                RoundRobin game = new RoundRobin(Players.Text);
                game.SetDefaultRound();
                for(int i = 0;i < game.players.Length * (game.players.Length-1) / 2; i++)
                {
                    game.setRandomRound();
                }
                RoundResult.Text += game.ShowResult();
            }
            catch (Exception ex)
            {
                RoundResult.Text = ex.ToString();
            }
        }
    }
}