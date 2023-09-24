using NUnit.Framework;
using System.IO;
using Ryujinx.Graphics.Texture;
using Ryujinx.Graphics.Texture.Encoders;

namespace Ryujinx.Tests.Texture
{
    public class BC7DecoderTests
    {
        private static string _rootDir;
        private static string _imageSrc;
        private static string _imageDest;

        [SetUp]
        public void Setup()
        {
            _rootDir = TestContext.CurrentContext.TestDirectory;
            _imageSrc = $@"{_rootDir}\TestImg.png";
            _imageDest = $@"{_rootDir}\TestImgD.png";
        }

        /// <summary>
        /// Just testing if the encoder/decoder is properly set up, compressing and decompressing the image
        /// </summary>
        [Test]
        public void Test()
        {
            TestContext.Out.WriteLine($"Test dir {_imageSrc}");
            var fileData = File.ReadAllBytes(_imageSrc);

            var encoded = BCnEncoder.EncodeBC7(fileData, 16, 16, 1, 1, 1);
            var decoded = BCnDecoder.DecodeBC7(encoded, 16, 16, 1, 1, 1);
            
            File.WriteAllBytes(_imageDest, decoded);
        }
    }
}
