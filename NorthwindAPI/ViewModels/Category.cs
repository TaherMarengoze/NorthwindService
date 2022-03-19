using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace NorthwindAPI.ViewModels
{
    public class Category
    {
        private byte[] _image;

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Image
        {
            get => _image; set
            {
                _image = Offset78(value);
            }
        }

        private byte[] Offset78(byte[] source)
        {
            byte[] offsetObject;

            using (var ms = new MemoryStream())
            {
                int offset = 78;
                ms.Write(source, offset, source.Length - offset);
                Bitmap bmp = new Bitmap(ms);
                using (MemoryStream jpegms = new MemoryStream())
                {
                    bmp.Save(jpegms, ImageFormat.Jpeg);
                    offsetObject = jpegms.ToArray();
                }
            }
            return offsetObject;
        }
    }
}