using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Oikake.Util
{
    class CountDownTimer:Timer
    {
        public CountDownTimer() : base()
        {
            //自分の初期化メソッドで初期化
            Initialize();
        }

        public CountDownTimer(float secound) : base(secound)
        {
            Initialize();
        }

        public override void Initialize()
        {
            currentTime = limitTime;
        }

        public override bool IsTime()
        {
            //0以下になってたら設定した時間を超えたのでtrueを返す
            return currentTime <= 0.0f;
        }

        public override void Update(GameTime gameTime)
        {
            //現在の時間を減らす。ただし最小値は0.0
            currentTime = Math.Max(currentTime - 1f, 0.0f);
        }
        public override float Rate()
        {
            return 1.0f - currentTime / limitTime;
        }
    }
}
