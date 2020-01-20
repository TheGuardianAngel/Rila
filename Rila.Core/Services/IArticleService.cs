using Rila.Core.ViewModels;
using System.Collections.Generic;
using System.Web;
using Umbraco.Core.Models.PublishedContent;

namespace Rila.Core.Services
{
    public interface IArticleService
    {
        IEnumerable<IPublishedContent> GetRiversList(IPublishedContent siteRoot);
        IEnumerable<IPublishedContent> GetLakesList(IPublishedContent siteRoot);
        IEnumerable<IPublishedContent> GetLatestArticles(IPublishedContent siteRoot);
        ArticleResultSet GetLatestArticles(IPublishedContent currentContentItem, HttpRequestBase request);
        ArticleResultSet GetLakes(IPublishedContent currentContentItem, HttpRequestBase request);
        ArticleResultSet GetRivers(IPublishedContent currentContentItem, HttpRequestBase request);
    }
}
