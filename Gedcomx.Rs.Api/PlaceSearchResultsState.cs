﻿using Gx.Atom;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gx.Rs.Api.Util;
using Gx.Links;

namespace Gx.Rs.Api
{
    public class PlaceSearchResultsState : GedcomxApplicationState<Feed>
    {
        internal PlaceSearchResultsState(IRestRequest request, IRestResponse response, IFilterableRestClient client, String accessToken, StateFactory stateFactory)
            : base(request, response, client, accessToken, stateFactory)
        {
        }

        /// <summary>
        /// Clones the current state instance.
        /// </summary>
        /// <param name="request">The REST API request used to create this state instance.</param>
        /// <param name="response">The REST API response used to create this state instance.</param>
        /// <param name="client">The REST API client used to create this state instance.</param>
        /// <returns>A cloned instance of the current state instance.</returns>
        protected override GedcomxApplicationState<Feed> Clone(IRestRequest request, IRestResponse response, IFilterableRestClient client)
        {
            return new PlaceSearchResultsState(request, response, client, this.CurrentAccessToken, this.stateFactory);
        }

        public Feed Results
        {
            get
            {
                return Entity;
            }
        }

        public PlaceDescriptionState ReadPlaceDescription(Entry place, params StateTransitionOption[] options)
        {
            Link link = place.GetLink(Rel.DESCRIPTION);
            link = link == null ? place.GetLink(Rel.SELF) : link;
            if (link == null || link.Href == null)
            {
                return null;
            }

            IRestRequest request = CreateAuthenticatedGedcomxRequest().Build(link.Href, Method.GET);
            return this.stateFactory.NewPlaceDescriptionState(request, Invoke(request, options), this.Client, this.CurrentAccessToken);
        }
    }
}
