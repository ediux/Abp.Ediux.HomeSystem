using Ediux.HomeSystem.SystemManagement;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace Ediux.HomeSystem.Controllers
{
    public class TextImageController : HomeSystemController
    {
        private readonly IFileStoreAppService _fileStoreAppService;

        public TextImageController(IFileStoreAppService fileStoreAppService)
        {
            _fileStoreAppService = fileStoreAppService;
        }

        private SKBitmap saveBitmap;
        private SKImageInfo info;
        private SKCanvas canvas;

        [HttpGet]
        public async Task<IActionResult> GetAsync(string id, string text, int width, int height, string color)
        {
            info = new SKImageInfo(width, height);
            saveBitmap = new SKBitmap(info);

            float fontSize = (width - 10) / text.Length;

            using (canvas = new SKCanvas(saveBitmap))
            {
                canvas.Clear(SKColors.White);

                // draw centered text, stroked
                using (var paint = new SKPaint())
                {
                    paint.TextSize = fontSize;
                    paint.IsAntialias = true;
                    if (color.IsNullOrWhiteSpace())
                    {
                        paint.Color = SKColors.Black;
                    }
                    else
                    {
                        paint.Color = new SKColor(uint.Parse(color));
                    }
                    paint.IsStroke = true;
                    paint.StrokeWidth = 3;
                    paint.TextAlign = SKTextAlign.Center;

                    canvas.DrawText(text, info.Width / 2f, info.Height / 2f, paint);
                }

                using (MemoryStream memStream = new MemoryStream())
                {
                    using (SKManagedWStream wstream = new SKManagedWStream(memStream))
                    {
                        saveBitmap.Encode(wstream, SKEncodedImageFormat.Png, 100);
                        byte[] data = memStream.ToArray();

                        // Check the data array for content!
                        return await Task.FromResult(File(data, "Image/png", id ?? text + ".png"));
                        //bool success = await LazyServiceProvider.get DependencyService.Get<IPhotoLibrary>().SavePhotoAsync(data, folder, filename);

                        // Check return value for success!
                    }
                }
            }
        }
    }
}
