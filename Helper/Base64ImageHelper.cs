using System;
using System.IO;
using Reisinger_Intelliface_1_0.Model;

namespace Reisinger_Intelliface_1_0.Helper
{

    public class Base64ImageHelper
    {
        public static string ToBase64ImageSingle(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageBytes);
        }

        public static string[] ToBase64Images(List<FaceImage> images)
        {


            List<string> convertedImages = new List<string>();

            foreach (FaceImage image in images)
            {
                convertedImages.Add(image.ImageData);
            }

            return convertedImages.ToArray();
        }


    }
}