using System.ComponentModel;
using Microsoft.OpenApi.Models;
using MyTweets.Data;
using MyTweets.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TweetRepo>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();

builder.Services.AddSwaggerGen(setup =>
{
  var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"MyTweets.xml"));
  setup.IncludeXmlComments(path);

  setup.SwaggerDoc("v1", new OpenApiInfo()
  {
    Description = "My Tweets from downloaded archive as of 4/27/2022",
    Title = "My Tweets",
    Version = "v1"
  });
});

var app = builder.Build();

app.UseCors(cfg =>
{
  cfg.WithMethods("GET");
  cfg.AllowAnyHeader();
  cfg.AllowAnyOrigin();
});

app.UseSwagger();


app.MapGet("/random",
  (TweetRepo repo) => repo.GetRandomTweet())
    .Produces(200)
    .Produces<TweetSummary>();

app.MapGet("/dates/{date:DateTime}",
  (TweetRepo repo, DateTime date) => repo.GetTweetsForDate(date))
    .Produces(200)
    .Produces<IEnumerable<TweetSummary>>();

app.MapGet("/retweets/{minimum:int}", 
  (TweetRepo repo, int minimum) => repo.GetByRetweets(minimum))
    .Produces(200)
    .Produces<IEnumerable<TweetSummary>>();

app.MapGet("/favorited/{minimum:int}",
  (TweetRepo repo, int minimum) => repo.GetByFavorited(minimum))
    .Produces(200)
    .Produces<IEnumerable<TweetSummary>>();

app.MapGet("/mentioned/{twitterHandle}",
  (TweetRepo repo, string twitterHandle) => repo.GetByMentioned(twitterHandle))
    .Produces(200)
    .Produces<IEnumerable<TweetSummary>>();

app.UseSwaggerUI(c =>
{
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Tweets");
  c.RoutePrefix = string.Empty;
});


app.Run();
