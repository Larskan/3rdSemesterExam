using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.FileIO.TemplatePDF
{
    public class TemplateLogoImage
    {
        public string FileName;
        public int Width;
        public int Height;

        //Rendering logo in upper left corner
        public TemplateLogoImage(string filename, int width, int height)
        {
            FileName = filename;
            Width = width;
            Height = height;
        }
    }
}
