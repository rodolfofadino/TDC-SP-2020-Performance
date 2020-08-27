using CodeHollow.FeedReader;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tdcsp2020.Models;

namespace tdcsp2020.Services
{
    public class NewsService
    {
        private IMemoryCache _memoryCache;
        public NewsService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        //public List<News> Load(int total, string category)
        public async Task<List<News>> LoadAsync(int total, string category)
        {
            var news = new List<News>();
            var key = $"key{total}_{category}";

            if (!_memoryCache.TryGetValue(key, out news))
            {
                news = new List<News>();

                var feed = await FeedReader.ReadAsync("https://g1.globo.com/rss/g1/turismo-e-viagem/");

                foreach (var item in feed.Items)
                {
                    var feedItem = item.SpecificItem as CodeHollow.FeedReader.Feeds.MediaRssFeedItem;
                    var media = feedItem.Media;
                    var url = "";
                    if (media.Any())
                        url = media.FirstOrDefault().Url;
                    news.Add(new News() { Title = item.Title, Link = item.Link, Image = url });
                }

                _memoryCache.Set(key, news, DateTime.Now.AddMinutes(5));
            }

            return news;
        }
    }
}
