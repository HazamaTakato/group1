using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Scene;
using Oikake.Device;
using Microsoft.Xna.Framework;
using Oikake.Util;

namespace Oikake.Actor.Effects
{
    class ParticleSmall:Particle
    {
        private Timer timer;//制限時間

        public ParticleSmall(string name, Vector2 position, Vector2 velocity, IParticleMediator mediator) :
            base(name, position, velocity, mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(1/10, 6/10));
            velocity -= velocity * 0.006f;
        }
        public ParticleSmall(IParticleMediator mediator) :
            base(mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(1/10, 6/10));
            velocity -= velocity * 0.006f;
            name = "particleSmall";
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timer.Update(gameTime);
            isDeadFlag = timer.IsTime();
        }
    }
}