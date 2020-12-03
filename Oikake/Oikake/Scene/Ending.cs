using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Device;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;//リソースへのアクセス
using Microsoft.Xna.Framework.Media;//MP3
using Microsoft.Xna.Framework.Audio;//WAVデータ

namespace Oikake.Scene
{
    class Ending:IScene
    {
        private bool isEndFlag;
        private Sound sound;
        IScene backGroundScene;

        public Ending(IScene scene)
        {
            isEndFlag = false;
            backGroundScene = scene;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        public void Draw(Renderer renderer)
        {
            //シーンごとにrenderer.Begin()～End()を
            //書いているのに注意
            //背景となるゲームプレイシーン
            backGroundScene.Draw(renderer);

            renderer.Begin();
            renderer.DrawTexture("GAMEOVER", new Vector2(0, 0));
            renderer.End();
        }
        public void Initialize()
        {
            isEndFlag = false;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }
        
        public Scene Next()
        {
            return Scene.Title;
        }
        public void Shutdown()
        {
            sound.StopBGM();
        }
        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("endingbgm");

            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
                sound.PlaySE("endingse");
            }
        }
    }
}
