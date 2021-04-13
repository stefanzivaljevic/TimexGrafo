using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using SimpleMvcSitemap.StyleSheets;

namespace TimexGrafo.Controllers
{
    public class SitemapController : Controller
    {
        [Route("/sitemap.xml")]
        public IActionResult Index()
        {
            List<SitemapNode> nodes = new List<SitemapNode>
        {
            new SitemapNode(Url.Action("Index","Home")){
                    ChangeFrequency = ChangeFrequency.Weekly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 1M
                },
            new SitemapNode(Url.Action("Shop","Home")){
                    ChangeFrequency = ChangeFrequency.Weekly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.8M
                },
            new SitemapNode(Url.Action("Proizvodnja","Home")){
                    ChangeFrequency = ChangeFrequency.Monthly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.6M
                },
            new SitemapNode(Url.Action("Onama","Home")){
                    ChangeFrequency = ChangeFrequency.Monthly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.6M
                },
            new SitemapNode(Url.Action("Index","Kontakt")){
                    ChangeFrequency = ChangeFrequency.Yearly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.3M
                },
            new SitemapNode(Url.Action("Error","Home")){
                    ChangeFrequency = ChangeFrequency.Yearly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.3M
                }
            //other nodes
        };

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}