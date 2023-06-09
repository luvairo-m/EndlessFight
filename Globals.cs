﻿using EndlessFight.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace EndlessFight
{
    public struct Globals
    {
        public static Game1 MainGame;
        public static Random Randomizer = new();
        public static Pulsation HitPulsation;
        public static Pulsation HealPulsation;
        public static Pulsation StartGamePulsation;
        public static Player Player;
        public static SoundEffect EnemyShoot;
        public static float ElapsedSeconds;
        public const float BulletScale = 3.5f;
        public const float BlasterScale = 2.5f;
        public const float EnemyScale = 4f;
        public const float ExplosionScale = 4.5f;
        public const int PlayerShipSize = 80;
        public const int EnemySpawnOffset = 300;
        public const int PlayerBaseSpeed = 430;
        public const int GameOverLabelWidth = 550;
        public const int GameOverLabelHeight = 130;
        public const string SerializationPath = "userdata.json";
        public const float TimeToChengeDifficulty = 2;
        public const float MaxDifficult = 0.7f;
        public const float DeltaDifficultChange = 0.01f;
        public const float BonusScale = 3.5f;

        public static void Update(GameTime gameTime) 
            => ElapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}