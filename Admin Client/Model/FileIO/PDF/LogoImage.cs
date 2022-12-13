using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.FileIO.PDF
{
    public class LogoImage
    {
        public string FileName;
        public int Width;
        public int Height;

        //Rendering logo in upper left corner
        public LogoImage(string filename, int width, int height)
        {
            FileName = filename;
            Width = width;
            Height = height;
        }
    }
}
