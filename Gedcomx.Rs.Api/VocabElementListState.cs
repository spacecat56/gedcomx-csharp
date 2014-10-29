﻿using Gedcomx.Model;
using Gx.Rs.Api.Util;
using JsonLD.Core;
using JsonLD.Util;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gx.Rs.Api
{
    public class VocabElementListState : GedcomxApplicationState<RDFDataset>
    {
        private RDFDataset model;
        private IEnumerable<RDFDataset.Quad> defaultQuads;
        private static JsonLdOptions options;

        static VocabElementListState()
        {
            options = new JsonLdOptions();
            options.useNamespaces = true;
        }


        protected internal VocabElementListState(IRestRequest request, IRestResponse response, IFilterableRestClient client, String accessToken, StateFactory stateFactory)
            : base(request, response, client, accessToken, stateFactory)
        {
        }

        public override String SelfRel
        {
            get
            {
                return Rel.DESCRIPTION;
            }
        }

        /// <summary>
        /// Clones the current state instance.
        /// </summary>
        /// <param name="request">The REST API request used to create this state instance.</param>
        /// <param name="response">The REST API response used to create this state instance.</param>
        /// <param name="client">The REST API client used to create this state instance.</param>
        /// <returns>A cloned instance of the current state instance.</returns>
        protected override GedcomxApplicationState<RDFDataset> Clone(IRestRequest request, IRestResponse response, IFilterableRestClient client)
        {
            return new VocabElementListState(request, response, client, this.CurrentAccessToken, this.stateFactory);
        }

        public VocabElementList GetVocabElementList()
        {
            var rootQuads = defaultQuads.GetSubjectQuads(this.Client.BuildUri(this.Request).ToString());

            // Create and populate the vocabulary element list
            VocabElementList vocabElementList = new VocabElementList();
            var identifierProperty = VocabConstants.DC_NAMESPACE + "identifier";
            if (rootQuads.HasPredicateQuad(identifierProperty))
            {
                vocabElementList.Id = rootQuads.GetPredicateQuad(VocabConstants.DC_NAMESPACE + "identifier").GetObject().GetValue();
            }
            vocabElementList.Uri = rootQuads.First().GetSubject().GetValue();
            vocabElementList.Title = rootQuads.GetPredicateQuad(VocabConstants.DC_NAMESPACE + "title").GetObject().GetValue();
            vocabElementList.Description = rootQuads.GetPredicateQuad(VocabConstants.DC_NAMESPACE + "description").GetObject().GetValue();

            // Populate the list of vocabulary elements within the vocabulary element list
            foreach (var element in defaultQuads.GetPredicateQuads(JSONLDConsts.RdfFirst))
            {
                var node = element.GetObject();
                var quads = defaultQuads.GetSubjectQuads(node.GetValue());

                vocabElementList.AddElement(MapToVocabElement(quads));
            }

            return vocabElementList;
        }

        protected override RDFDataset LoadEntity(IRestResponse response)
        {
            var token = JSONUtils.FromString(response.Content);
            model = (RDFDataset)JsonLdProcessor.ToRDF(token, options);
            defaultQuads = model.GetQuads("@default");

            return model;
        }

        protected override SupportsLinks MainDataElement
        {
            get
            {
                return null;
            }
        }

        /**
         * Map a RDF resource that represents a vocabulary element to a GedcomX vocabulary element
         *
         * @param resourceDescribingElement the RDF resource that represents a vocabulary element
         * @return a GedcomX vocabulary element that corresponds to the given RDF resource
         */
        private VocabElement MapToVocabElement(IEnumerable<RDFDataset.Quad> quads)
        {
            VocabElement vocabElement = new VocabElement();

            // Map required attributes into the VocabElement
            vocabElement.Id = quads.GetPredicateQuad(VocabConstants.DC_NAMESPACE + "identifier").GetObject().GetValue();
            vocabElement.Uri = quads.First().GetSubject().GetValue();

            var property = VocabConstants.RDFS_NAMESPACE + "subClassOf";
            if (quads.HasPredicateQuad(property))
            {
                vocabElement.Subclass = quads.GetPredicateQuad(property).GetObject().GetValue();
            }
            property = VocabConstants.DC_NAMESPACE + "type";
            if (quads.HasPredicateQuad(property))
            {
                vocabElement.Type = quads.GetPredicateQuad(property).GetObject().GetValue();
            }

            // Map the labels into the VocabElement
            var labels = quads.GetPredicateQuads(VocabConstants.RDFS_NAMESPACE + "label");
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    var node = label.GetObject();
                    vocabElement.AddLabel(node.GetValue(), node.GetLanguage().ToLower());
                }
            }

            // Map the descriptions into the VocabElement
            var descriptions = quads.GetPredicateQuads(VocabConstants.RDFS_NAMESPACE + "comment");
            if (descriptions != null)
            {
                foreach (var description in descriptions)
                {
                    var node = description.GetObject();
                    vocabElement.AddDescription(node.GetValue(), node.GetLanguage().ToLower());
                }
            }
            return vocabElement;
        }
    }
}
