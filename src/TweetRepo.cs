using System.Text.Json;
using MyTweets.Models;

namespace MyTweets.Data;

public class TweetRepo
{
  List<TweetSummary> _tweets = null;  

  public TweetSummary GetRandomTweet()
  {
    var item = Random.Shared.Next(0, Tweets.Count() - 1);
    return Tweets.ElementAt(item);
  }

  public IEnumerable<TweetSummary> GetTweetsForDate(DateTime selectedDate)
  {
    return Tweets.Where(t => t.createdAt.Date == selectedDate.Date).ToList();
  }

  public IEnumerable<TweetSummary> GetByRetweets(int minimum)
  {
    return Tweets.Where(t => t.retweetedCount >= minimum)
                 .OrderByDescending(t => t.retweetedCount)
                 .ToList();
  }

  public IEnumerable<TweetSummary>  GetByFavorited(int minimum)
  {
    return Tweets.Where(t => t.favoritedCount >= minimum)
                 .OrderByDescending(t => t.favoritedCount)
                 .ToList();
  }

  public IEnumerable<TweetSummary> GetByMentioned(string twitterHandle)
  {
    var handle = twitterHandle.ToLowerInvariant().Replace("@", "");
    return Tweets.Where(t => t.Mentions.Any(m => m.screenName.ToLowerInvariant() == handle))
                 .OrderByDescending(t => t.favoritedCount)
                 .ToList();
  }

  protected IEnumerable<TweetSummary> Tweets 
  {
    get
    {
      if (_tweets is null)
      {
        var json = File.ReadAllText("tweetSummaries.json");
        _tweets = JsonSerializer.Deserialize<List<TweetSummary>>(json);
      }

      return _tweets;
    }
  }
}
