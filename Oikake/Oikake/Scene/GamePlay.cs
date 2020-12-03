using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Actor;
using Oikake.Device;
using Oikake.Util;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;//リソースへのアクセス
using Microsoft.Xna.Framework.Media;//MP3
using Microsoft.Xna.Framework.Audio;//WAVデータ

namespace Oikake.Scene
{
    class GamePlay : IScene, IGameMediator
    {
        //private Player player;//プレイヤーとなる白玉
        //private List<Character> characters;

        private CharacterManager characterManager;//キャラクター管理者
        private Timer timer;//ゲームプレイ時間
        private TimerUI timerUI;//時間UI
        private Score score;//得点
        private Sound sound;
        private Player playe;
        private bool isEndFlag;//シーン終了フラグ
        private int time;


        public GamePlay()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public void Draw(Renderer renderer)
        {
            //描画開始
            renderer.Begin();
            //背景を描画
            renderer.DrawTexture("haikei", Vector2.Zero);

            characterManager.Draw(renderer);//キャラクター管理者の描画
            score.Draw(renderer);
            timerUI.Draw(renderer);
            //キャラクターを一括描画
            //characters.ForEach(c => c.Draw(renderer));
            //プレイヤーを描画
            // player.Draw(renderer);
            //エンディングを描画
            // if (timer.IsTime())
            // {
            //   renderer.DrawTexture("ending", new Vector2(150, 150));
            //  }
            //描画終了
            renderer.End();
        }
        public void Initialize()
        {
            //シーン終了フラグを初期化
            isEndFlag = false;

            //キャラクターマネージャーの実体生成
            characterManager = new CharacterManager();
             playe = new Player(this);
            //キャラクターマネージャーにプレイヤーを追加
            characterManager.Add(playe);//追尾系統を追加したら修正

            //動かない敵を追加

            //バレットエネミー追加
            characterManager.Add(new bulletenemy(this));
            //ボーナスエネミー追加
            characterManager.Add(new BonusEnemy(this));

           // characterManager.Add(new MediumBoss(this));

            characterManager.Add(new Zako2(this, new AttackAI(playe)));

            characterManager.Add(new zako3(this));



            //Player player = new Player(this);
            //characterManager.Add(player);

            //突っ込んでくる敵を追加
            //characterManager.Add(new Enemy(this, new AttackAI(player)));

            //characterManager.Add(new Enemy(this,new TraceAI()));

            //ランダム移動の敵10体登録
            //for (int i = 0; i < 10; i++)
            //{
            //  characterManager.Add(new Enemy(this, new RandomAI()));
            //}


            //プレイヤーの実態生成
            //player = new Player();
            //プレイヤーの初期化
            //player.Initialize();

            //listの実態生成
            //characters = new List<Character>();

            //listにcharacterのオブジェクト(継承したい子たち)を登録
            //characters.Add(new Enemy());//動かない敵を登録
            //characters.Add(new TateBound(this));
            //10体登録
            // for (int i = 0; i < 3; i++)
            {
                //       characters.Add(new NanameBound());
            }
            // for (int i = 0; i < 3; i++)
            {
                //      characters.Add(new TateBound());
            }
            // for (int i = 0; i < 3; i++)
            {
                //     characters.Add(new BoundEnemy());
            }
            // for (int i = 0; i < 3; i++)
            {
                //      characters.Add(new HantaiNanameBound());
            }

            //登録したキャラクターを一気に初期化(foreach文)
            //   foreach (var c in characters)
            {
                //       c.Initialize();
            }
            //時間関連
            timer = new CountDownTimer(150);
            timerUI = new TimerUI(timer);
            //スコア関連
            score = new Score();
        }
        public bool IsEnd()
        {
            return isEndFlag;
        }
        public Scene Next()
        {
            Scene nextScene = Scene.Ending;//通常はエンディングシーン
            //一定得点以上だとGoodEndingシーン
            //if (score.GetScore() >= 2000)
            //{
            //    nextScene = Scene.GoodEnding;
            //}
            return nextScene;
        }
        public void Shutdown()
        {
            sound.StopBGM();
        }
        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("gameplaybgm");

            timer.Update(gameTime);//時間更新
            score.Update(gameTime);//スコア更新

            //キャラクターマネージャーを更新
            characterManager.Update(gameTime);

            time += 1;
            int a = time % 300;
            if(a==0)
            {
                characterManager.Add(new Zako2(this, new AttackAI(playe)));
            }
            if (timer.Now()==30)
            {
                characterManager.Add(new MediumBoss(this));
            }
            //時間切れか？
            if (timer.IsTime())
            {
                //計算途中のスコアを全部加算
                score.Shutdown();
                //シーン終了
                isEndFlag = true;//シーン終了へ
            }
        }
        public void AddActor(Character character)
        {
            characterManager.Add(character);
            sound.PlaySE("gameplayse");
        }

        public void AddScore()
        {
            score.Add();
        }

        public void AddScore(int num)
        {
            score.Add(num);
        }
        public void AddHp()
        {

        }
        public void AddHp(int num)
        {

        }
    }
}
