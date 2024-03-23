
using Microsoft.AspNetCore.Http;
//using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using NReco.VideoConverter;
using System.Diagnostics;
using System;
using System.IO;
namespace FireDetection.Backend.Domain.Helpers.Media
{
    public static class VideoConverterHandler
    {

        public static string SaveAviFile(IFormFile aviFile)
        {
            // Check if the file is not null
            if (aviFile == null || aviFile.Length == 0)
                throw new ArgumentException("AVI file is empty or null.");

            // Generate a unique file name for the AVI file
            string fileName = $"{Guid.NewGuid()}.avi";
            string filePath = Path.Combine(@"C:\Code\code\FACS-Backend-API\FireDetection.Grpc.System\FireDetection.Backend.API\VideoCache\", fileName);

            // Save the AVI file to the specified directory
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                aviFile.CopyTo(fileStream);
            }
            Convert(filePath);
            return filePath;
        }


        public static void Convert(string inputFilePath)
        {
            string recordFolderPath = "C:\\Code\\code\\FACS-Backend-API\\FireDetection.Grpc.System\\FireDetection.Backend.API\\VideoCache\\";
            try
            {
                string outputFilePath = ConvertAviToMp4(inputFilePath);

                // Move the converted MP4 file to the record folder
                string mp4FileName = Path.GetFileName(outputFilePath);
                string destinationFilePath = Path.Combine(recordFolderPath, mp4FileName);
                File.Move(outputFilePath, destinationFilePath);

                Console.WriteLine($"AVI file '{inputFilePath}' converted to MP4 and saved to '{destinationFilePath}'.");

                // Use the MP4 file as needed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting AVI file '{inputFilePath}' to MP4: {ex.Message}");
            }

        }


        static string ConvertAviToMp4(string inputFilePath)
        {
            string outputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), inputFilePath.Replace(".avi", ".mp4"));

            var ffMpeg = new FFMpegConverter();
            ffMpeg.ConvertMedia(inputFilePath, outputFilePath, Format.mp4);

            uploadLocalToFirebase(outputFilePath);
            return outputFilePath;
        }



        static async void uploadLocalToFirebase(string inputFilePath)
        {
           // string recordFolderPath = $"C:\\Code\\code\\FACS-Backend-API\\FireDetection.Grpc.System\\FireDetection.Backend.API\\VideoCache\\{inputFilePath}";

            using (var stream = File.OpenRead(inputFilePath))
            {
                await StorageHandlers.UploadFileStream(stream, "video", inputFilePath);
            }
        }
    }
}
