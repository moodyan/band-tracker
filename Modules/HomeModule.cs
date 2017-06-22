
using System;
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
        Band selectedBand = Band.Find(parameters.id);
        List<Venue> bandVenues = selectedBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        List<Show> bandShows = selectedBand.GetShows();
        model.Add("band", selectedBand);
        model.Add("bandVenues", bandVenues);
        model.Add("bandShows", bandShows);
        model.Add("allVenues", allVenues);
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
        Venue selectedVenue = Venue.Find(parameters.id);
        List<Band> venueBands = selectedVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        List<Show> venueShows = selectedVenue.GetShows();
        model.Add("venue", selectedVenue);
        model.Add("venueBands", venueBands);
        model.Add("venueShows", venueShows);
        model.Add("allBands", allBands);
        return View["venue.cshtml", model];
      };
      Post["venue/success"] = _ => {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        band.AddVenue(venue);
        return View["success.cshtml"];
      };
      Patch["/venue/update/details/{id}"] = parameters => {
        Venue currentVenue = Venue.Find(parameters.id);
        string newDetails = Request.Form["new-details"];
        currentVenue.UpdateDetails(newDetails);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Band> venueBands = currentVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        List<Show> venueShows = currentVenue.GetShows();
        model.Add("venue", currentVenue);
        model.Add("venueBands", venueBands);
        model.Add("venueShows", venueShows);
        model.Add("allBands", allBands);
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
      Post["band/venue/success"] = _ => {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        band.AddVenue(venue);
        return View["success.cshtml"];
      };
      Post["venue/band/success"] = _ => {
        Band band = Band.Find(Request.Form["band-id"]);
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        venue.AddBand(band);
        return View["success.cshtml"];
      };
      Get["venues/search"] = _ => {
        List<Venue> AllVenues = Venue.GetAll();
        return View["venues_search.cshtml", AllVenues];
      };
      Post["venues/search"] = _ => {
        List<Venue> SearchVenueLocation = Venue.SearchVenueLocation(Request.Form["venue-search-location"]);
        return View["venues_search.cshtml", SearchVenueLocation];
      };
      Get["band/show/delete/{id}"]= parameters =>{
        Show SelectedShow = Show.Find(parameters.id);
        return View["show_band_delete.cshtml", SelectedShow];
      };
      // Delete["band/show/delete/{id}"]= parameters =>{
      //   Show SelectedShow = Show.Find(parameters.id);
      //   SelectedShow.DeleteShow();
      //   return View["success.cshtml"];
      // };
      // Delete["venue/show/delete/{id}"]= parameters =>{
      //   Show SelectedShow = Show.Find(parameters.id);
      //   SelectedShow.DeleteShow();
      //   return View["success.cshtml"];
      // };
      Get["/shows/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Band> allBands = Band.GetAll();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("allBands", allBands);
        model.Add("allVenues", allVenues);
        return View["show_form.cshtml", model];
      };
      Post["/shows/new"] = _ => {
        Show NewShow = new Show(Request.Form["show-location"], Request.Form["show-date"], Request.Form["band-id"],  Request.Form["venue-id"]);
        NewShow.Save();
        return View["success.cshtml"];
      };
    }
  }
}
