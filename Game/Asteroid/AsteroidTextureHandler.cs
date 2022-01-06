using Raylib_CsLo;
using System;


namespace Asteroids.Game.Asteroid
{
    using static Asteroids.Game.Common.ImageLoaderHelper;
    using static Raylib_CsLo.Raylib;

    public static class AsteroidTextureHandler
    {
        private static Image _imLarge = LoadImageFromByteArray(Resource.AnimatedAsteroids);
        private static Image _imMedium = new Image();
        private static Image _imSmall = new Image();

        private static Texture LargeTexture = LoadTextureFromImage(_imLarge);
        private static Texture MediumTexture;
        private static Texture SmallTexture;

        // This function is very important and must be called in order for this to work.
        public unsafe static void Init()
		{
            var imOne = LoadImageFromByteArray(Resource.AnimatedAsteroids);
            var imTwo = LoadImageFromByteArray(Resource.AnimatedAsteroids);

            ImageResize(&imOne, _imLarge.width / 2, _imLarge.height / 2);
            ImageResize(&imTwo, 288, 40);
            _imMedium = ImageCopy(imOne);
            _imSmall = ImageCopy(imTwo);
            UnloadImage(imOne);
            UnloadImage(imTwo);

            MediumTexture = LoadTextureFromImage(_imMedium);
            SmallTexture = LoadTextureFromImage(_imSmall);
            UnloadImage(_imLarge);
            UnloadImage(_imMedium);
            UnloadImage(_imSmall);
		}

        public static Texture Texture(AsteroidEntitySize size)
		{
            switch (size)
            {
                case AsteroidEntitySize.BIG:
                    return LargeTexture;
                case AsteroidEntitySize.MEDIUM:
                    return MediumTexture;
                case AsteroidEntitySize.SMALL:
                    return SmallTexture;
                default:
                    throw new Exception("The Asteroid is not any of these sizes!");
            }
        }

        public static int FrameSizeX(AsteroidEntitySize size)
		{
            switch (size)
            {
                case AsteroidEntitySize.BIG:
                    return LargeTexture.width / 16;
                case AsteroidEntitySize.MEDIUM:
                    return MediumTexture.width / 16;
                case AsteroidEntitySize.SMALL:
                    return (int)((float)SmallTexture.width / 15.9);
                default:
                    throw new Exception("The Asteroid is not any of these sizes!");
            }
		}
        public static int FrameSizeY(AsteroidEntitySize size)
		{
            switch (size)
            {
                case AsteroidEntitySize.BIG:
                    return LargeTexture.height / 2;
                case AsteroidEntitySize.MEDIUM:
                    return MediumTexture.height / 2;
                case AsteroidEntitySize.SMALL:
                    return SmallTexture.height / 2;
                default:
                    throw new Exception("The Asteroid is not any of these sizes!");
            }
		}


        public static void UnloadAssets()
        {
            UnloadTexture(LargeTexture);
            UnloadTexture(MediumTexture);
            UnloadTexture(SmallTexture);
        }
    }
}
