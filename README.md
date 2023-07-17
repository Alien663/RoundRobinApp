# RoundRobinApp

源於有個打球的團，是一個場地要排6個點的循環不好排，所以想要寫個手機APP可以處理這個問題。

## 關於APP使用
排點邏輯是先將所有點都列出來，然後抽籤選一組加到結果後面。

按照想要的順序輸入後，會依照以下邏輯做排點

1. 預設排點 : 如果是人數為雙數，前幾點會按照順序排(1 vs. 2，3 vs. 4)，若為單數，則會安排第一人跟最後一人打一次。
2. 隨機排點 : 會判斷是否有人連續上場，若有，則會重抽。
3. 重新排點 : 因為是隨機，所以可能會有剩下的組別一定造成連續上場問題(剩下 1 vs. 2、2 vs. 4兩組尚未抽出)，此時會將結果以及尚未被抽走的組別分開顯示。
4. 重新排點 : 除了第3點問題外，也有可能真的機率問題導致發生(莫非定律)，目前程式碼設定是:如果連續50次都有連續上場問題，就認定為此次抽籤失敗。

該APP由於全隨機，所以一定會出現一些尷尬狀況 : 有人休1打1，有人休4打1，結果一定是都打到同樣場數，但是過程不盡人意。該APP主要是希望協助排點順利，因此若有狀況不順，希望使用者可以自行重新調整結果公布，或是讓程式多重排幾次也可以。

## 關於RoundRobin程式碼

### RoundRobin

該物件為排點所需的功能，有以下幾個:

1. RoundRobin : 根據選手清單，創出所有排點可能，邏輯可以參考以下表格
   ` | A | B | C | D | E
   --- | --- | --- | --- | --- | ---
   A | X | O | O | O | O
   B | X | X | O | O | O
   C | X | X | X | O | O
   D | X | X | X | X | O
   E | X | X | X | X | X
   
3. setDefaultRound : 以不隨機的方式做isContinue的抽籤
4. setRandomNewRound : 以隨機的方式做isContinue的抽籤，並設定有Seeds，如果籤筒沒有籤，或是抽50次都沒有成功，就放棄本次抽籤。
5. Show : 排版並回傳文字結果
6. isContinue : 判斷本回合抽籤結果，是否有人與上一回合重複 (未來可以考慮用Operation Overloading去做?)

### Main

根據RoundRobin第一點的規則，我們可以從選手人數知道要抽幾次籤，公式為 :

$$總比賽場數 = \frac { 人數 * (人數 - 1)} {2}$$

因此程式碼只要跑簡單的For loop就可以了
```Csharp
RoundRobin game = new RoundRobin(Players.Text);
game.SetDefaultRound();
for(int i = 0;i < game.players.Length * (game.players.Length-1) / 2; i++)
{
    game.SetRandomNewRound();
}
Console.WriteLine(game.ShowResult());
```
