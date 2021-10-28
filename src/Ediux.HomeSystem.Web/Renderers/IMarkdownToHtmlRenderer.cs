using System.Threading.Tasks;

namespace Ediux.HomeSystem.Web.Renderers
{
    public interface IMarkdownToHtmlRenderer
    {
        Task<string> RenderAsync(string rawMarkdown);
    }
}
