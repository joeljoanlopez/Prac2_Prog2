using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using TCEngine;

namespace TCGame
{
    class SuperAwesomeGame : Game
    {
        public void Init()
        {
            CreateBackground();
            CreateMainCharacter();
            CreateTanksSpawner();
        }

        public void DeInit()
        {
        }

        public void Update(float _dt)
        {
        }

        private void CreateMainCharacter()
        {
            Actor mainCharacterActor = new Actor("MainCharacterActor");

            AnimatedSpriteComponent animatedSpriteComponent = mainCharacterActor.AddComponent<AnimatedSpriteComponent>("Data/Textures/Player/Plane.png", 4u, 1u);
            animatedSpriteComponent.m_RenderLayer = RenderComponent.ERenderLayer.Middle;
            TransformComponent transformComponent = mainCharacterActor.AddComponent<TransformComponent>();
            transformComponent.Transform.Position = new Vector2f(500.0f, 300.0f);
            transformComponent.Transform.Rotation = 90.0f;

            animatedSpriteComponent.Center();
            mainCharacterActor.AddComponent<BoxCollisionComponent>(animatedSpriteComponent.GetGlobalBounds(), ECollisionLayers.Player);

            Vector2f planeForward = new Vector2f(1.0f, 0.0f);
            List<ECollisionLayers> enemyLayers = new List<ECollisionLayers>();
            enemyLayers.Add(ECollisionLayers.Enemy);

            // TODO (1.1): Add the missing components to the Main Character
            //   - CharacterControllerComponent
            mainCharacterActor.AddComponent<CharacterControllerComponent>();
            // TODO (2.1): Add the missing components to the Main Character
            //   - CannonComponent
            CannonComponent cannonComponent = mainCharacterActor.AddComponent<CannonComponent>(enemyLayers);



            TecnoCampusEngine.Get.Scene.AddActor(mainCharacterActor);

        }


        private void CreateTanksSpawner()
        {
            Actor tanksSpawner = new Actor("Tank Spawner");
            ActorSpawnerComponent<ActorPrefab> spawnerComponent = tanksSpawner.AddComponent<ActorSpawnerComponent<ActorPrefab>>();

            // TODO (5.1): Fix the spawn position for the tanks
            //    - They should spawn on the right side of the window

            spawnerComponent.m_MinPosition = new Vector2f(0, -10);
            spawnerComponent.m_MaxPosition = new Vector2f(TecnoCampusEngine.Get.ViewportSize.X, -10);
            spawnerComponent.m_MinTime = 0.5f;
            spawnerComponent.m_MaxTime = 5f;
            spawnerComponent.Reset();

            Vector2f tankForward = new Vector2f(0.0f, 1.0f);
            List<ECollisionLayers> tankEnemyLayers = new List<ECollisionLayers>();
            tankEnemyLayers.Add(ECollisionLayers.Person);

            for (int i = 1; i <= 2; ++i)
            {
                ActorPrefab tankPrefab = new ActorPrefab("Tank0" + i);
                SpriteComponent spriteComponent = tankPrefab.AddComponent<SpriteComponent>("Data/Textures/Enemies/Tank0" + i + ".png");
                spriteComponent.m_RenderLayer = RenderComponent.ERenderLayer.Back;
                TransformComponent tankTransform = tankPrefab.AddComponent<TransformComponent>();

                // TODO (5.2): Add the Missing components to the Tank Prefab
                //   - ForwardMovementComponent
                //   - CannonComponent (remember to use the correct texture and enable Autofire)
                ForwardMovementComponent _tankForward = tankPrefab.AddComponent<ForwardMovementComponent>(50, new Vector2f(0f, 1f));
                CannonComponent _cannonComponent = tankPrefab.AddComponent<CannonComponent>(tankEnemyLayers, _tankForward.Forward);
                _cannonComponent.AutomaticFire = true;
                _cannonComponent.BulletTextureName = "Data/Textures/Bullets/TankBullet.png";

                tankPrefab.AddComponent<OutOfWindowDestructionComponent>();
                tankPrefab.AddComponent<BoxCollisionComponent>(spriteComponent.GetGlobalbounds(), ECollisionLayers.Enemy);
                tankPrefab.AddComponent<ExplosionComponent>();

                spawnerComponent.AddActorPrefab(tankPrefab);
            }

            TecnoCampusEngine.Get.Scene.AddActor(tanksSpawner);
        }



        private void CreateBackground()
        {
            Actor backgroundActor = new Actor("Background");

            SpriteComponent spriteComponent = backgroundActor.AddComponent<SpriteComponent>("Data/Textures/Background.jpg");
            spriteComponent.m_RenderLayer = RenderComponent.ERenderLayer.Background;

            TecnoCampusEngine.Get.Scene.AddActor(backgroundActor);
        }

    }
}
