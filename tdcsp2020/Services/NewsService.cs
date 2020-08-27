using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tdcsp2020.Models;

namespace tdcsp2020.Services
{
    public class NewsService
    {
        public List<News> Load(int total, string category)
        {
            var news = new List<News>();

            var feed = FeedReader.ReadAsync("https://g1.globo.com/rss/g1/turismo-e-viagem/").Result;

            foreach (var item in feed.Items)
            {
                var feedItem = item.SpecificItem as CodeHollow.FeedReader.Feeds.MediaRssFeedItem;
                var media = feedItem.Media;
                var url = "";
                if (media.Any())
                    url = media.FirstOrDefault().Url;
                news.Add(new News() { Title = item.Title, Link = item.Link, Image = url });
            }

            return news;
        }
    }
}
