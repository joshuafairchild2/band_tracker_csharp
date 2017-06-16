using Nancy;
using Nancy.ViewEngines.Razor;
using System;
using System.Collections.Generic;
using BandTracker.Objects;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => View["index.cshtml"];
    }
  }
}
