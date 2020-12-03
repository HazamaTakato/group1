using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Oikake.Util
{
    class CountUpTimer:Timer
    {
        public CountUpTimer():base()
        {
            Initialize();
        }
        public CountUpTimer(float second):base(second)
        {
            Initialize();
        }

        public override void Initialize()
        {
            currentTime = 0.0f;
        }
        public override bool IsTime()
        {
            //現在の時間が制限時間以上になったらtrueを返す
            return limitTime <= currentTime;
        }
        public override void Update(GameTime gameTime)
        {
            //現在の時間を増やす。ただし最大値は制限時間
            currentTime = Math.Min(currentTime + 1f, limitTime);
        }

        public override float Rate()
        {
            return currentTime / limitTime;
        }
    }
}
