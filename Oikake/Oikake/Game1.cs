// このファイルで必要なライブラリのnamespaceを指定
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Oikake.Actor;//Playerなどの
using Oikake.Def;//Renderer
using Oikake.Device;//Screen
using Oikake.Scene;
using Oikake.Util;
using System.Collections.Generic;//list,dictionary用
using Microsoft.Xna.Framework.Content;//リソースへのアクセス
using Microsoft.Xna.Framework.Media;//MP3
using Microsoft.Xna.Framework.Audio;//WAVデータ
using System.Diagnostics;//Assert用



/// <summary>
/// プロジェクト名がnamespaceとなります
/// </summary>
namespace Oikake
{
    /// <summary>
    /// ゲームの基盤となるメインのクラス
    /// 親クラスはXNA.FrameworkのGameクラス
    /// </summary>
    public class Game1 : Game
    {
        // フィールド（このクラスの情報を記述）
        private GraphicsDeviceManager graphicsDeviceManager;//グラフィックスデバイスを管理するオブジェクト
        private GameDevice gameDevice;
        private Renderer renderer;//描画オブジェクト

        private SceneManager sceneManager;//シーン管理者

        /// <summary>
        /// コンストラクタ
        /// （new で実体生成された際、一番最初に一回呼び出される）
        /// </summary>
        public Game1()
        {
            //グラフィックスデバイス管理者の実体生成
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            //コンテンツデータ（リソースデータ）のルートフォルダは"Contentに設定
            Content.RootDirectory = "Content";
            //Screenクラスの値で画面サイズを設定
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height;

            Window.Title = "追いかけ";
            
        }

        /// <summary>
        /// 初期化処理（起動時、コンストラクタの後に1度だけ呼ばれる）
        /// </summary>
        protected override void Initialize()
        {
            // この下にロジックを記述
            //ゲームデバイスの実体生成と取得
            gameDevice = GameDevice.Instance(Content, GraphicsDevice);

            sceneManager = new SceneManager();
            sceneManager.Add(Scene.Scene.Title, new SceneFader(new Title()));//シーンフェーダー
            IScene addScene = new GamePlay();
            sceneManager.Add(Scene.Scene.GamePlay, new SceneFader (addScene));
            sceneManager.Add(Scene.Scene.Ending, new SceneFader(new Ending(addScene)));
            //sceneManager.Add(Scene.Scene.GoodEnding, new SceneFader(new GoodEnding(addScene)));
            sceneManager.Change(Scene.Scene.Title);//最初のシーンはタイトルに変更

            // この上にロジックを記述
            base.Initialize();// 親クラスの初期化処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// コンテンツデータ（リソースデータ）の読み込み処理
        /// （起動時、１度だけ呼ばれる）
        /// </summary>
        protected override void LoadContent()
        {
            //レンダラーの実体生成
            //renderer = new Renderer(Content, GraphicsDevice);
            renderer = gameDevice.GetRenderer();

            // この下にロジックを記述
            renderer.LoadContent("black");
            renderer.LoadContent("ending");
            renderer.LoadContent("number");
            renderer.LoadContent("score");
            renderer.LoadContent("stage");
            renderer.LoadContent("timer");
            renderer.LoadContent("title");
            renderer.LoadContent("white");
            renderer.LoadContent("pipo-btleffect");
            renderer.LoadContent("oikake_player_4anime");
            renderer.LoadContent("oikake_enemy_4anime");
            renderer.LoadContent("puddle");
            renderer.LoadContent("good");
            renderer.LoadContent("particle");
            renderer.LoadContent("particleBlue");
            renderer.LoadContent("rea");
            renderer.LoadContent("enemy1");
            renderer.LoadContent("Item");
            renderer.LoadContent("enemy1_bullet");
            renderer.LoadContent("Player1");
            renderer.LoadContent("tyuubosu");
            renderer.LoadContent("tyuubosu_iwa");
            renderer.LoadContent("tyuubosu_tama");
            renderer.LoadContent("teki2");
            renderer.LoadContent("teki3");
            renderer.LoadContent("teki3_tama");
            renderer.LoadContent("player_sentouki");
            renderer.LoadContent("GAMEOVER");
            renderer.LoadContent("haikei");
            renderer.LoadContent("TitLe");
            renderer.LoadContent("jiki_kyouka_tama");
            renderer.LoadContent("jiki_tama");
            renderer.LoadContent("titlee");

            Texture2D fade = new Texture2D(GraphicsDevice, 1, 1);
            Color[] colors = new Color[1 * 1];
            colors[0] = new Color(0, 0, 0);
            fade.SetData(colors);
            renderer.LoadContent("fade", fade);

            Sound sound = gameDevice.GetSound();
            string filepath = "./Sound/";
            sound.LoadBGM("titlebgm", filepath);
            sound.LoadBGM("gameplaybgm", filepath);
            sound.LoadBGM("endingbgm", filepath);
            sound.LoadBGM("goodendingbgm", filepath);

            sound.LoadSE("titlese", filepath);
            sound.LoadSE("gameplayse", filepath);
            sound.LoadSE("endingse", filepath);
            // この上にロジックを記述
        }

        /// <summary>
        /// コンテンツの解放処理
        /// （コンテンツ管理者以外で読み込んだコンテンツデータを解放）
        /// </summary>
        protected override void UnloadContent()
        {
            // この下にロジックを記述


            // この上にロジックを記述
        }

        /// <summary>
        /// 更新処理
        /// （1/60秒の１フレーム分の更新内容を記述。音再生はここで行う）
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Update(GameTime gameTime)
        {
            // ゲーム終了処理（ゲームパッドのBackボタンかキーボードのエスケープボタンが押されたら終了）
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                 (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                Exit();
            }

            // この下に更新ロジックを記述
            //ゲームデバイスを更新
            gameDevice.Update(gameTime);//必ずこの１回のみ
            //シーンの管理者更新
            sceneManager.Update(gameTime);
            
            // この上にロジックを記述
            base.Update(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Draw(GameTime gameTime)
        {
            // 画面クリア時の色を設定
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // この下に描画ロジックを記述

            //シーン管理者描画
            sceneManager.Draw(renderer);


            //この上にロジックを記述
            base.Draw(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }
    }
}
