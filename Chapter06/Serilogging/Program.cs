﻿using Serilog;
using Serilog.Core;
using Serilogging.Models;

// create a new logger that will write to the console and to
// a text file, one-file-per-day, named with the date.
using Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Assign the new logger to the static entry point for logging.
Log.Logger = log;
Log.Information("The global logger has been configured.");

// Log some example entries of differing severity.
Log.Warning("Danger, Serilog, danger!");
Log.Error("This is an error!");
Log.Fatal("Fatal problem!");
ProductPageView pageView = new ProductPageView()
{
    PageTitle = "Chai",
    SiteSection = "Beverages",
    ProductId = 1
};
Log.Information("{@PageView} occurred at {Viewed}", pageView, DateTimeOffset.UtcNow);

// For a log with a buffer, like a text file logger, you
// must flush before ending the app.
Log.CloseAndFlush();