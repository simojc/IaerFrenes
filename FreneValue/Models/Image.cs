using System;

using System.Web.Mvc;
using System.IO;

using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FreneValue.Models
{
    public class Image
    {
        [Key]
        public int id { get; set; }
        public String nom { get; set; }
        public String alt { get; set; }
        public Byte[] image { get; set; }
        public String typ_cont { get; set; }
        public string util { get; set; }
    }

    public class arbre_image
    {
        [Key]
        public int id { get; set; }
        public int arbre_id { get; set; }
        public int img_id { get; set; }
        public string util { get; set; }
    }

    public class ess_image
    {
        [Key]
        public int id { get; set; }
        public int ess_id { get; set; }
        public int? id_dessn_feuil { get; set; }        
        public int? id_dessn_brh { get; set; }
        public int? id_dessn_abr { get; set; }
        public string util { get; set; }
    }

    public class ImageResult : ActionResult
    {
        public String ContentType { get; set; }
        public byte[] ImageBytes { get; set; }
        public String SourceFilename { get; set; }

        //This is used for times where you have a physical location
        public ImageResult(String sourceFilename, String contentType)
        {
            SourceFilename = sourceFilename;
            ContentType = contentType;
        }

        //This is used for when you have the actual image in byte form
        //  which is more important for this post.
        public ImageResult(byte[] sourceStream, String contentType)
        {
            ImageBytes = sourceStream;
            ContentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = ContentType;

            //Check to see if this is done from bytes or physical location
            //  If you're really paranoid you could set a true/false flag in
            //  the constructor.
            if (ImageBytes != null)
            {
                var stream = new MemoryStream(ImageBytes);
                stream.WriteTo(response.OutputStream);
                stream.Dispose();
            }
            else
            {
                response.TransmitFile(SourceFilename);
            }
        }
    }

    /// important
    /// // tiliser cette balise pour appeler laction et afficher la photo:
    /// //  /* <img src="/Photo/ShowPhoto/1" alt="" />*/

}

