using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using BandTracker.Objects;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/bands"] = _ => {
        List<Band> AllBands = Band.GetAll();
        return View["bands.cshtml", AllBands];
      };
      Get["/venues"] = _ => {
        List<Venue> AllVenues = Venue.GetAll();
        return View["venues.cshtml", AllVenues];
      };
      Get["/bands/new"] = _ => {
        return View["band_form.cshtml"];
      };
      Post["/bands/new"] = _ => {
        Band newBand = new Band(Request.Form["band-name"], Request.Form["band-members"], Request.Form["band-genre"], Request.Form["band-info"]);
        newBand.Save();
        return View["success.cshtml"];
      };
      Get["/venues/new"] = _ => {
        return View["venue_form.cshtml"];
      };
      Post["/venues/new"] = _ => {
        Venue NewVenue = new Venue(Request.Form["venue-name"], Request.Form["venue-location"], Request.Form["venue-details"]);
        NewVenue.Save();
        return View["success.cshtml"];
      };
      Get["band/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band SelectedBand = Band.Find(parameters.id);
        List<Venue> VenueBand = SelectedBand.GetVenues();
        List<Venue> AllVenues = Venue.GetAll();
        model.Add("band", SelectedBand);
        model.Add("venueBand", VenueBand);
        model.Add("allVenues", AllVenues);
        return View["band.cshtml", model];
      };
      Post["band/success"] = _ => {
        Band band = Band.Find(Request.Form["band-id"]);
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        venue.AddBand(band);
        return View["success.cshtml"];
      };
      Get["venue/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue SelectedVenue = Venue.Find(parameters.id);
        List<Band> BandVenue = SelectedVenue.GetBands();
        List<Band> AllBands = Band.GetAll();
        model.Add("venue", SelectedVenue);
        model.Add("bandVenue", BandVenue);
        model.Add("allBands", AllBands);
        return View["venue.cshtml", model];
      };
      Post["venue/success"] = _ => {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        band.AddVenue(venue);
        return View["success.cshtml"];
      };
      Patch["/venue/update/instructions/{id}"] = parameters => {
        Venue CurrentVenue = Venue.Find(parameters.id);
        string newDetails = Request.Form["new-details"];
        CurrentVenue.UpdateDetails(newDetails);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Band> VenueBands = CurrentVenue.GetBands();
        List<Band> AllBands = Band.GetAll();
        model.Add("venue", CurrentVenue);
        model.Add("venueBands", VenueBands);
        model.Add("allBands", AllBands);
        return View["venue.cshtml", model];
      };
      Get["/band/delete/{id}"] = parameters => {
        Band CurrentBand = Band.Find(parameters.id);
        return View["band_delete.cshtml", CurrentBand];
      };
      Delete["/band/delete/{id}"] = parameters => {
        Band CurrentBand = Band.Find(parameters.id);
        CurrentBand.Delete();
        List<Band> AllBands = Band.GetAll();
        return View["bands.cshtml", AllBands];
      };
      Get["/venue/delete/{id}"] = parameters => {
        Venue CurrentVenue = Venue.Find(parameters.id);
        return View["venue_delete.cshtml", CurrentVenue];
      };
      Delete["/venue/delete/{id}"] = parameters => {
        Venue CurrentVenue = Venue.Find(parameters.id);
        CurrentVenue.Delete();
        List<Venue> AllVenues = Venue.GetAll();
        return View["venues.cshtml", AllVenues];
      };
      Get["venues/search"] = _ => {
        List<Venue> AllVenues = Venue.GetAll();
        return View["venues_search.cshtml", AllVenues];
      };
      Post["venues/search"] = _ => {
        List<Venue> SearchVenueLocation = Venue.SearchVenueLocation(Request.Form["venue-search-location"]);
        return View["venues_search.cshtml", SearchVenueLocation];
      };
    }
  }
}
