using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Actor.Effects
{
    class ParticleMiddle:Particle
    {
        private Timer timer;//制限時間
        private ParticleMiddle particleMiddle;

        public ParticleMiddle(string name, Vector2 position, Vector2 velocity, IParticleMediator mediator) :
            base(name, position, velocity, mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(2 / 10, 8 / 10));
            velocity -= velocity * 0.006f;
            particleMiddle.position(Player);
        }
        public ParticleMiddle(IParticleMediator mediator) :
            base(mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(2 / 10, 8 / 10));
            velocity -= velocity * 0.006f;
            name = "particleMiddle";
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timer.Update(gameTime);
            particleMiddle.Update(gameTime);
            isDeadFlag = timer.IsTime();
        }
    }
}
