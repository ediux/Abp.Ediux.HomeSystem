using Ediux.HomeSystem.Web.Pages.CmsKit.Reactions;

using JetBrains.Annotations;

namespace Ediux.HomeSystem.Web.Pages.CmsKit
{
    public class CmsKitUiOptions
    {
        [NotNull]
        public ReactionIconDictionary ReactionIcons { get; }

        public CmsKitUiCommentOptions CommentsOptions { get; }

        public CmsKitUiOptions()
        {
            ReactionIcons = new ReactionIconDictionary();
            CommentsOptions = new CmsKitUiCommentOptions();
        }
    }
}
