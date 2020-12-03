using Microsoft.Xna.Framework;
using Oikake.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Scene
{
    class Hp
    {
        private int hp;//合計HP
        private int poolHp;//一時保存用のHP

        public Hp()
        {
            Initialize();
        }

        public void Initialize()
        {
            hp = 3;
            poolHp = 3;
        }

        public void Add()
        {
            poolHp--;
        }

        public void Add(int num)
        {
            poolHp = poolHp - num;
        }

        public void Update(GameTime gameTime)
        {
            if (poolHp > 0)
            {
                hp += 1;
                poolHp -= 1;
            }
            else if (poolHp < 0)
            {
                hp -= 1;
                poolHp += 1;
            }
        }
        public void Draw(Renderer renderer)
        {
            Vector2 position = new Vector2(50, 10);
            renderer.DrawTexture("score", position);
            Vector2 position2 = new Vector2(250, 13);
            renderer.DrawNumber("number", position2, hp);
        }
        public void Shutdown()
        {
            hp += poolHp;
            if (hp < 0)
            {
                hp = 0;
            }
            poolHp = 0;
        }
        public int GetScore()
        {
            int currentHp = hp + poolHp;
            if (currentHp < 0)
            {
                currentHp = 0;
            }
            return currentHp;
        }
    }
}
