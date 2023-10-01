using NUnit.Framework;
using Ryujinx.Graphics.Gpu;
using Ryujinx.Graphics.Texture;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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
            _imageSrc = Path.Join(_rootDir, "TestImg.png");
            _imageDest = Path.Join(_rootDir, "TestImgD.png");
            
            GraphicsConfig.EnableTextureRecompression = false;
        }

        /// <summary>
        /// Just testing if the encoder/decoder is properly set up, compressing and decompressing the image
        /// </summary>
        /// Goals
        ///
        /// Implement a simple test and check if it outputs consistent data
        ///
        /// Test if the decoder is implemented correctly (mention findings if its not in the pull request)
        ///
        /// Test whether the test is actually able to catch real world bugs, this PR may be useful
        /// https://github.com/Ryujinx/Ryujinx/pull/4890
        /// 
        /// cache data
        ///
        /// push pr
        [Test]
        public void Test()
        {
            TestContext.Out.WriteLine($"Test dir {_imageSrc}");
            var fileData = File.ReadAllBytes(_imageSrc);

            //var encoded = BCnEncoder.EncodeBC7(fileData, 256, 256, 8, 1, 1);
            var decoded = BCnDecoder.DecodeBC7(fileData, 1024, 768, 1, 11, 1);

            var stream = new MemoryStream(decoded);
            var image = Image.FromStream(stream);
            //image.Save("ImgTestTest.png", ImageFormat.Png);
            // File.WriteAllBytes(_imageDest, fileData);
            File.WriteAllBytes(_imageDest, decoded);
        }
    }
}
