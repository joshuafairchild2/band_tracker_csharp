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
      Get["/bands/new"] = _ => {
        return View["band_form.cshtml"];
      };
      Get["/bands/new/members"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("part2", true);
        model.Add("members", Request.Query["members-number"]);
        return View["band_form.cshtml", model];
      };
      Post["/bands/new"] = _ => {
        Band newBand = new Band(Request.Form["band-name"], Request.Form["members"]);
        newBand.Save();
        string membersValues = Request.Form["member-name"];
        if(membersValues != null)
        {
          string[] members = membersValues.Split(',');
          foreach(string member in members)
          {
            Member newMember = new Member(member, newBand.Id);
          }
        }
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Post["/venues/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"], Request.Form["venue-address"]);
        newVenue.Save();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Get["/venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue selectedVenue = Venue.Find(parameters.id);
        model.Add("selected-venue", selectedVenue);
        model.Add("venue-bands", selectedVenue.GetBands());
        return View["venue.cshtml", model];
      };
      Get["/bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Band selectedBand = Band.Find(parameters.id);
        model.Add("selected-band", selectedBand);
        model.Add("band-venues", selectedBand.GetVenues());
        return View["band.cshtml", model];
      };
    }
  }
}
