using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Oikake.Device;
using Oikake.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Scene
{
    class Score
    {
        private int score;//合計得点
        private int poolScore;//一時保存用のプール得点

        public Score()
        {
            Initialize();
        }

        public void Initialize()
        {
            score = 0;
            poolScore = 0;
        }

        public void Add()
        {
            poolScore++;
        }

        public void Add(int num)
        {
            poolScore = poolScore + num;
        }
        
        public void Update(GameTime gameTime)
        {
            if (poolScore > 0)
            {
                score += 1;
                poolScore -= 1;
            }
            else if(poolScore < 0)
            {
                score -= 1;
                poolScore += 1;
            }
        }
        public void Draw(Renderer renderer)
        {
            Vector2 position = new Vector2(50, 10);
            renderer.DrawTexture("score", position);
            Vector2 position2 = new Vector2(250, 13);
            renderer.DrawNumber("number", position2,score);
        }
        public void Shutdown()
        {
            score += poolScore;
            if (score < 0)
            {
                score = 0;
            }
            poolScore = 0;
        }
        public int GetScore()
        {
            int currentScore = score + poolScore;
            if (currentScore < 0)
            {
                currentScore = 0;
            }
            return currentScore;
        }
    }
}
