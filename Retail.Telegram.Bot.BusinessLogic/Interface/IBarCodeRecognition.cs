using System.IO;

namespace Retail.Telegram.Bot.BusinessLogic.Interface
{
    /// <summary>
    /// Basic interface for barcode recognition
    /// </summary>
    public interface IBarCodeRecognition
    {
        string GetBarCode(Stream photo);

        string[] GetBarCodes(Stream photo);
    }
}
