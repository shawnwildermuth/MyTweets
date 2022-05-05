namespace MyTweets.Models;

/// <summary>
/// A single Mention in the owning Tweet
/// </summary>
public class Mention
{
  /// <summary>
  /// Mention ID
  /// </summary>
  public Int64 id { get; set; }
  
  /// <summary>
  /// Name of the account Mentioned
  /// </summary>
  public string name { get; set; }
  
  /// <summary>
  /// Twitter handle of the account mentioned
  /// </summary>
  public string screenName { get; set; }
}

/// <summary>
/// Summary of a single Tweet
/// </summary>
public class TweetSummary
{
  /// <summary>
  /// Tweet ID
  /// </summary>
  public Int64 id { get; set; }
  /// <summary>
  /// The content of the Tweet
  /// </summary>
  public string fullText { get; set; }
  /// <summary>
  /// The Date of the Tweet
  /// </summary>
  public DateTime createdAt { get; set; }
  /// <summary>
  /// Number of times this tweet was retweeted
  /// </summary>
  public int retweetedCount { get; set; }
  /// <summary>
  /// Number of times this tweet was favorited/hearted
  /// </summary>
  public int favoritedCount { get; set; }
  /// <summary>
  /// The Tweet ID that this tweet is replying to.
  /// </summary>
  public Int64 replyId { get; set; }
  /// <summary>
  /// List of Mentions for this tweet.
  /// </summary>
  public List<Mention> Mentions { get; set; } = new List<Mention>();
}