using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Services
{
    public class ImageService
    {
        /// <summary>
        /// Gets the image as a byte array from the given <see cref="IFormFile"/> object.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>A task containing the image byte array
        /// and the image type string.</returns>
        public async Task<(byte[], string)> GetImageFromFormFile(IFormFile file)
        {
            using var stream = new System.IO.MemoryStream();
            await file.CopyToAsync(stream);

            return (stream.ToArray(), file.ContentType.Replace("image/", ""));
        }

        /// <summary>
        /// Downloads the image as a byte array from the given URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>A task containing the image byte array
        /// and the image type string.</returns>
        public async Task<(byte[], string)> GetImageFromUrl(string url)
        {
            using var httpClient = new System.Net.Http.HttpClient();

            return (await httpClient.GetByteArrayAsync(url), "jpeg");
        }

        /// <summary>
        /// Gets a Base64 data URI encoding of an image,
        /// which can be embedded into HTML files
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imageType"></param>
        /// <returns>A Base64 image data URI string.</returns>

        public string GetBase64Encoding(byte[] image, string imageType)
        {
            if (image == null || imageType == null)
                return null;

            var base64Image = Convert.ToBase64String(image);
            return $"data:image/{imageType};base64,{base64Image}";
        }
    }
}
