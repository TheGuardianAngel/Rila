using Rila.Core.Helpers;
using Rila.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Rila.Core.Services
{
    public class ArticleService : IArticleService
    {
        public IEnumerable<IPublishedContent> GetLakesList(IPublishedContent siteRoot)
        {
            return siteRoot.Descendants().Where(x => x.ContentType.Alias == "riverList");
        }

        public IEnumerable<IPublishedContent> GetRiversList(IPublishedContent siteRoot)
        {
            return siteRoot.Descendants().Where(x => x.ContentType.Alias == "lakeList");
        }

        public IEnumerable<IPublishedContent> GetLatestArticles(IPublishedContent siteRoot)
        {
            var articlesAll = siteRoot.Descendants().Where(x => x.ContentType.Alias == "article" && x.IsVisible()).OrderByDescending(y => y.Value<DateTime>("articleDate"));
            return articlesAll;
        }
        
        public ArticleResultSet GetLatestArticles(IPublishedContent currentContentItem, HttpRequestBase request)
        {
            var siteRoot = currentContentItem.Root();

            var articlesAll = siteRoot.Descendants().Where(x => x.ContentType.Alias == "article" && x.IsVisible()).OrderByDescending(y => y.Value<DateTime>("articleDate"));

            var pageNumber = QueryStringHelper.GetIntFromQueryString(request, "page", 1);
            var pageSize = QueryStringHelper.GetIntFromQueryString(request, "size", 6);

            var pageOfArticles = articlesAll.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var totalArticlesCount = articlesAll.Count();

            var articlePageCount = totalArticlesCount > 0 ? Math.Ceiling((double)totalArticlesCount / pageSize) : 1;

            ArticleResultSet resultSet = new ArticleResultSet() 
            { 
                PageCount = articlePageCount, 
                PageNumber = pageNumber, 
                PageSize = pageSize, 
                Results = pageOfArticles
            };

            return resultSet;
        }
        public ArticleResultSet GetRivers(IPublishedContent currentContentItem, HttpRequestBase request)
        {
            var siteRoot = currentContentItem.Root();

            var rivers = GetRiversList(siteRoot);

            var pageNumber = QueryStringHelper.GetIntFromQueryString(request, "page", 1);
            var pageSize = QueryStringHelper.GetIntFromQueryString(request, "size", 6);

            var pageOfArticlesRivers = rivers.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var totalRiversCount = rivers.Count();

            var riversPageCount = totalRiversCount > 0 ? Math.Ceiling((double)totalRiversCount / pageSize) : 1;

            ArticleResultSet resultSet = new ArticleResultSet() 
            {
                PageCount = riversPageCount, 
                PageNumber = pageNumber, 
                PageSize = pageSize, 
                Results = pageOfArticlesRivers 
            };
            return resultSet;
        }
        public ArticleResultSet GetLakes(IPublishedContent currentContentItem, HttpRequestBase request)
        {
            var siteRoot = currentContentItem.Root();

            var lakes = GetLakesList(siteRoot);

            var pageNumber = QueryStringHelper.GetIntFromQueryString(request, "page", 1);
            var pageSize = QueryStringHelper.GetIntFromQueryString(request, "size", 6);

            var pageOfArticlesLakes = lakes.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var totalLakesCount = lakes.Count();

            var lakesPageCount = totalLakesCount > 0 ? Math.Ceiling((double)totalLakesCount / pageSize) : 1;

            ArticleResultSet resultSet = new ArticleResultSet() 
            { 
                PageCount = lakesPageCount, 
                PageNumber = pageNumber, 
                PageSize = pageSize, 
                Results = pageOfArticlesLakes
            };

            return resultSet;
        }
    }
}
