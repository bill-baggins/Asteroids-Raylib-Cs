using System;
using System.IO;
using System.Collections.Generic;
using Asteroids.Common;
using Asteroids.State;

using Raylib_CsLo;

namespace Asteroids.Game.ArchivedIdeas
{
    using static Raylib_CsLo.Raylib;
    public static class Lazy<Ideas>
    {
        public static string GameOverText = "Surprise!";

        private static byte[] Initialize()
        {
            try
            {
                List<byte> thing = new List<byte>();
                foreach (string str in File.ReadAllLines(@"cursed.txt")[0].Split(' '))
                {
                    try
                    {
                        thing.Add(Convert.ToByte(str));
                    }
                    catch (FormatException)
                    {
                        break;
                    }
                }
                return thing.ToArray();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No secrets will be revealed today...");
                return Array.Empty<byte>();
            }
            
        }

        public unsafe static Texture Resized()
        {
            var im = ResourceLoaderHelper.LoadImageFromByteArray(Initialize());
            ImageResize(&im, Settings.ScreenWidth, Settings.ScreenHeight);
            var tex = LoadTextureFromImage(im);
            UnloadImage(im);
            return tex;
        }
    }
}
