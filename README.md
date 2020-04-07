# prnt.sc Web Scraper

## Introduction

[prnt.sc](https://prnt.sc/) is a screengrabbing/screenshot website where users photos are automatically uploaded to their website. This project is a web scraper that makes navigating random photos easier. This applications has no "essential use", but it was something fun that I made in a few hours.

## Code Samples

The first part of this code uses [HtmlAgilityPack](https://html-agility-pack.net/) to spoof our application to appear as a web browser. It then downloads the html data and parses it for the source file of the image.
```cs
wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows; Windows NT 5.1; rv:1.9.2.4) Gecko/20100611 Firefox/3.6.4");
string html = wc.DownloadString(url);
HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
doc.LoadHtml(html);
HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"screenshot-image\"]");
```
The image source is then displayed into a picturebox. 
```cs
var request = WebRequest.Create(node.Attributes["src"].Value);
using (var response = request.GetResponse())
using (var stream = response.GetResponseStream())
{
    pictureBox1.Image = Bitmap.FromStream(stream);
}
```

## Installation

You can build the application yourself, or download and run the release [here](https://github.com/HeathHowren/prnt.sc-Web-Scraper/blob/master/PrintScreenApp/bin/Release/PrintScreenApp.exe). 
<br />

![screenshot](https://raw.githubusercontent.com/HeathHowren/prnt.sc-Web-Scraper/master/images/Capture.PNG?token=AMNS37YDVKJHVTI4UT6FDGS6RSZQ6)
