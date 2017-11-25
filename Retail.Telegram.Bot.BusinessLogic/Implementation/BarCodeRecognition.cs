using System.IO;
using System.Linq;
using Retail.Telegram.Bot.BusinessLogic.Interface;

namespace Retail.Telegram.Bot.BusinessLogic.Implementation
{
    /// <summary>
    /// Barcode reader using Spire.BarCode
    /// </summary>
    public class BarCodeRecognition : IBarCodeRecognition
    {
        /// <summary>
        /// Get barcode from photo image
        /// </summary>
        public string GetBarCode(Stream photo)
        {
            string[] barcodes = GetBarCodes(photo);
            if (barcodes != null && barcodes.Length > 0)
            {
                string val = barcodes.FirstOrDefault();
                if (val != null && val.Contains("Spire.Barcode"))
                    return string.Empty;
                else
                    return val;
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Get barcodes from photo image
        /// </summary>
        public string[] GetBarCodes(Stream photo)
        {
            return Spire.Barcode.BarcodeScanner.Scan(photo);
        }
    }
}
