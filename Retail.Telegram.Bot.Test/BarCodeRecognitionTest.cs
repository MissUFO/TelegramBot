using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Retail.Telegram.Bot.BusinessLogic.Implementation;

namespace Retail.Telegram.Bot.Test
{
    [TestClass]
    public class BarCodeRecognitionTest
    {
        [TestMethod]
        public void GetBarCodeTest()
        {
            var fileName = "C:\\Work\\RetailTelegramBot\\Dev\\Retail.Telegram.Bot.Test\\Samples\\barcode.JPG";

            byte[] bytes = File.ReadAllBytes(fileName);
            Stream stream = new MemoryStream(bytes);

            BarCodeRecognition barcodeRecognition = new BarCodeRecognition();
            string barcode = barcodeRecognition.GetBarCode(stream);

            Assert.IsTrue(barcode!=string.Empty);
        }
    }
}
