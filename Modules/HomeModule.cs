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
            newMember.Save();
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
        model.Add("all-bands", Band.GetAll());
        return View["venue.cshtml", model];
      };
      Get["/bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Band selectedBand = Band.Find(parameters.id);
        model.Add("selected-band", selectedBand);
        model.Add("band-venues", selectedBand.GetVenues());
        model.Add("all-venues", Venue.GetAll());
        return View["band.cshtml", model];
      };
      Get["/venues/{id}/bands/new"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue selectedVenue = Venue.Find(parameters.id);
        model.Add("selected-venue", selectedVenue);
        model.Add("venue-bands", selectedVenue.GetBands());
        model.Add("all-bands", Band.GetAll());
        return View["add_band.cshtml", model];
      };
      Post["/venues/{id}/bands/new"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue selectedVenue = Venue.Find(parameters.id);
        Band selectedBand = Band.Find(Request.Form["band-select"]);
        selectedVenue.AddBand(selectedBand);
        model.Add("selected-venue", selectedVenue);
        model.Add("venue-bands", selectedVenue.GetBands());
        model.Add("all-bands", Band.GetAll());
        return View["venue.cshtml", model];
      };
      Get["/bands/{id}/venues/new"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Band selectedBand = Band.Find(parameters.id);
        model.Add("selected-band", selectedBand);
        model.Add("band-venues", selectedBand.GetVenues());
        model.Add("all-venues", Venue.GetAll());
        return View["add_venue.cshtml", model];
      };
      Post["/bands/{id}/venues/new"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Band selectedBand = Band.Find(parameters.id);
        Venue selectedVenue = Venue.Find(Request.Form["venue-select"]);
        selectedBand.AddVenue(selectedVenue);
        model.Add("selected-band", selectedBand);
        model.Add("band-venues", selectedBand.GetVenues());
        model.Add("all-venues", Venue.GetAll());
        return View["band.cshtml", model];
      };
      Delete["/bands/delete"] = _ => {
        Band.DeleteAll();
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Delete["/venues/delete"] = _ => {
        Venue.DeleteAll();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Get["/venues/{id}/edit"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue selectedVenue = Venue.Find(parameters.id);
        model.Add("selected-venue", selectedVenue);
        model.Add("edit", true);
        return View["venue_form.cshtml", model];
      };
      Post["/venues/{id}/edit"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue selectedVenue = Venue.Find(parameters.id);
        selectedVenue.Update(Request.Form["venue-name-edit"], Request.Form["venue-address-edit"]);
        model.Add("selected-venue", selectedVenue);
        model.Add("venue-bands", selectedVenue.GetBands());
        model.Add("all-bands", Band.GetAll());
        return View["venue.cshtml", model];
      };
      Get["/venues/{id}/bands/delete"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue selectedVenue = Venue.Find(parameters.id);
        model.Add("selected-venue", selectedVenue);
        model.Add("venue-bands", selectedVenue.GetBands());
        model.Add("all-bands", Band.GetAll());
        model.Add("list-edit", true);
        return View["venue.cshtml", model];
      };
      Delete["/venues/{id}/bands/delete"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue selectedVenue = Venue.Find(parameters.id);
        string bandIds = Request.Form["band"];
        if(bandIds != null)
        {
          string[] values = bandIds.Split(',');
          foreach(string id in values)
          {
            Band toDelete = Band.Find(int.Parse(id));
            toDelete.Delete();
          }
        }
        model.Add("selected-venue", selectedVenue);
        model.Add("venue-bands", selectedVenue.GetBands());
        model.Add("all-bands", Band.GetAll());
        return View["venue.cshtml", model];
      };
      //dont forget to edit delete methods
    }
  }
}
