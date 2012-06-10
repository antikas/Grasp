﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using Grasp.Hypermedia.Http.Media;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public class RootController : ApiController
	{
		public static readonly ResourceUriPart Route = ResourceUriPart.Root;

		private readonly IRelationshipRepository _relationshipRepository;
		private readonly IMediaTypeRepository _mediaTypeRepository;

		public RootController(IRelationshipRepository relationshipRepository, IMediaTypeRepository mediaTypeRepository)
		{
			Contract.Requires(relationshipRepository != null);
			Contract.Requires(mediaTypeRepository != null);

			_relationshipRepository = relationshipRepository;
			_mediaTypeRepository = mediaTypeRepository;
		}

		public RootResource Get()
		{
			var api = new RootResource();

			api.SetValue(RootResource.DescriptionField, "This is my API");
			api.SetValue(RootResource.MediaTypesField, new Many<MediaType>(_mediaTypeRepository.GetAll()));
			api.SetValue(RootResource.RelationshipsField, new Many<Relationship>(_relationshipRepository.GetAll()));

			return api;
		}
	}
}