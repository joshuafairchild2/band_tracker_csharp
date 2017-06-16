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
      Get["/venues"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Get["/bands"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Get["/venues/new"] = _ => {
        return View["venue_form.cshtml"];
      };
      // Get["/bands/new"] = _ => {
      //   return View["band_form.cshtml"];
      // };
      Post["/venues/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"], Request.Form["venue-address"]);
        newVenue.Save();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
    }
  }
}
