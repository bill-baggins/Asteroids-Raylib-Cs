using System;
using System.IO;
using Raylib_CsLo;

namespace Asteroids.Common
{
    using static Raylib_CsLo.Raylib;
    public static class ResourceLoaderHelper
    {
        public static unsafe Image LoadImageFromByteArray(byte[] imageData)
        {
            Image im;
            fixed (byte* pBuffer = &imageData[0])
            {
                im = LoadImageFromMemory(".png", pBuffer, imageData.Length);
            }
            return im;
        }

        public static unsafe Sound LoadSoundFromMemoryStream(UnmanagedMemoryStream stream)
		{
            Wave wave;
            var managedStream = new MemoryStream();
            stream.CopyTo(managedStream);
            byte[] streamInfo = managedStream.ToArray();
            fixed (byte* pStreamBuffer = &streamInfo[0])
			{
                wave = LoadWaveFromMemory(".wav", pStreamBuffer, streamInfo.Length);
            }
            Sound sound = LoadSoundFromWave(wave);
            return sound;
		}
    }
}
