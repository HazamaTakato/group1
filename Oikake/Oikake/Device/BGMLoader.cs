using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Oikake.Device
{
    class BGMLoader:Loader
    {
        private Sound sound;

        public BGMLoader(string[,] resources) : base(resources)
        {
            sound = GameDevice.Instance().GetSound();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //まず終了フラグを有効にして
            isEndFlag = true;
            //カウンタが最大に達していないか？
            if (counter < maxNum)
            {
                //BGMの読み込み
                sound.LoadBGM(resources[counter, 0], resources[counter, 1]);
                //カウンタを増やす
                counter += 1;
                //読み込むものがあったので終了フラグを継続に設定
                isEndFlag = false;
            }
        }
    }
}
