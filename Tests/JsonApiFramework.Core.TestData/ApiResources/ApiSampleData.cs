// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net;

using JsonApiFramework.Http;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.TestData.ApiResources
{
    using Attribute = JsonApiFramework.JsonApi.ApiProperty;
    using Attributes = JsonApiFramework.JsonApi.ApiObject;

    public static class ApiSampleData
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Collection Path Segments
        public const string ArticleCollectionPathSegment = "articles";
        public const string BlogCollectionPathSegment = "blogs";
        public const string CommentCollectionPathSegment = "comments";
        public const string PersonCollectionPathSegment = "people";
        #endregion

        #region Types
        public const string ArticleType = ArticleCollectionPathSegment;
        public const string BlogType = BlogCollectionPathSegment;
        public const string CommentType = CommentCollectionPathSegment;
        public const string PersonType = PersonCollectionPathSegment;
        #endregion

        #region Ids
        public const string ArticleId = "24";
        public const string ArticleId1 = "24";
        public const string ArticleId2 = "42";

        public const string BlogId = "24";
        public const string BlogId1 = "24";
        public const string BlogId2 = "42";

        public const string CommentId = "24";
        public const string CommentId1 = "24";
        public const string CommentId2 = "42";
        public const string CommentId3 = "68";
        public const string CommentId4 = "86";

        public const string PersonId = "24";
        public const string PersonId1 = "24";
        public const string PersonId2 = "42";
        public const string PersonId3 = "100";
        public const string PersonId4 = "101";

        public const string ErrorId = "85319674-1da4-4f1c-8af4-b09e17755ab2";
        public const string ErrorId1 = "a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2";
        public const string ErrorId2 = "192ca96c-1b36-4721-aa12-110ee9c43958";
        #endregion

        #region Meta
        public static readonly Meta DocumentMeta = Meta.Create(new DocumentMeta
            {
                IsPublic = true,
                Version = 2.1m,
                Copyright = "Copyright 2015 Example Corporation.",
                Authors = new[] {"John Doe", "Jane Doe"}
            });

        public static readonly Meta ErrorMeta = Meta.Create(new ErrorMeta
            {
                StackTrace = "Foo.Method1 line 42\nFoo.Method2 line 24\nBar.Method1 line 86\nBar.Method2 line 68"
            });

        public static readonly Meta JsonApiVersionMeta = Meta.Create(new JsonApiVersionMeta
            {
                Website = "http://jsonapi.org"
            });

        public static readonly Meta LinkMeta = Meta.Create(new LinkMeta
            {
                IsPublic = true,
                Version = "2.0"
            });

        public static readonly Meta RelationshipMeta = Meta.Create(new RelationshipMeta
            {
                CascadeDelete = true
            });

        public static readonly Meta ResourceMeta = Meta.Create(new ResourceMeta { Version = "2.0" });

        public static readonly Meta ResourceMeta1 = Meta.Create(new ResourceMeta { Version = "2.0" });
        public static readonly Meta ResourceMeta2 = Meta.Create(new ResourceMeta { Version = "2.1" });
        public static readonly Meta ResourceMeta3 = Meta.Create(new ResourceMeta { Version = "2.2" });
        public static readonly Meta ResourceMeta4 = Meta.Create(new ResourceMeta { Version = "2.3" });
        #endregion

        #region ResourceIdentifiers
        public static readonly ResourceIdentifier BlogResourceIdentifier = new ResourceIdentifier(BlogType, BlogId);
        public static readonly ResourceIdentifier BlogResourceIdentifier1 = new ResourceIdentifier(BlogType, BlogId1);
        public static readonly ResourceIdentifier BlogResourceIdentifier2 = new ResourceIdentifier(BlogType, BlogId2);

        public static readonly ResourceIdentifier CommentResourceIdentifier = new ResourceIdentifier(CommentType, CommentId);
        public static readonly ResourceIdentifier CommentResourceIdentifier1 = new ResourceIdentifier(CommentType, CommentId1);
        public static readonly ResourceIdentifier CommentResourceIdentifier2 = new ResourceIdentifier(CommentType, CommentId2);
        public static readonly ResourceIdentifier CommentResourceIdentifier3 = new ResourceIdentifier(CommentType, CommentId3);
        public static readonly ResourceIdentifier CommentResourceIdentifier4 = new ResourceIdentifier(CommentType, CommentId4);
        public static readonly List<ResourceIdentifier> CommentResourceIdentifiers = new List<ResourceIdentifier>
            {
                CommentResourceIdentifier1,
                CommentResourceIdentifier2
            };

        public static readonly List<ResourceIdentifier> EmptyResourceIdentifiers = new List<ResourceIdentifier>();

        public static readonly ResourceIdentifier PersonResourceIdentifier = new ResourceIdentifier(PersonType, PersonId);
        public static readonly ResourceIdentifier PersonResourceIdentifier1 = new ResourceIdentifier(PersonType, PersonId1);
        public static readonly ResourceIdentifier PersonResourceIdentifier2 = new ResourceIdentifier(PersonType, PersonId2);
        public static readonly ResourceIdentifier PersonResourceIdentifier3 = new ResourceIdentifier(PersonType, PersonId3);
        public static readonly ResourceIdentifier PersonResourceIdentifier4 = new ResourceIdentifier(PersonType, PersonId4);

        public static readonly ResourceIdentifier ArticleResourceIdentifier = new ResourceIdentifier(ArticleType, ArticleId);
        public static readonly ResourceIdentifier ArticleResourceIdentifier1 = new ResourceIdentifier(ArticleType, ArticleId1);
        public static readonly ResourceIdentifier ArticleResourceIdentifier2 = new ResourceIdentifier(ArticleType, ArticleId2);
        public static readonly List<ResourceIdentifier> ArticleResourceIdentifiers = new List<ResourceIdentifier>
            {
                ArticleResourceIdentifier1,
                ArticleResourceIdentifier2
            };

        public static readonly List<ResourceIdentifier> ArticleToCommentResourceIdentifiers1 = new List<ResourceIdentifier>
            {
                CommentResourceIdentifier1,
                CommentResourceIdentifier2
            };

        public static readonly List<ResourceIdentifier> ArticleToCommentResourceIdentifiers2 = new List<ResourceIdentifier>
            {
                CommentResourceIdentifier3,
                CommentResourceIdentifier4
            };

        public static readonly ResourceIdentifier ArticleResourceIdentifierWithMeta = new ResourceIdentifier(ArticleType, ArticleId, ResourceMeta);
        public static readonly ResourceIdentifier BlogResourceIdentifierWithMeta = new ResourceIdentifier(BlogType, BlogId, ResourceMeta);
        public static readonly ResourceIdentifier CommentResourceIdentifierWithMeta = new ResourceIdentifier(CommentType, CommentId, ResourceMeta);
        public static readonly ResourceIdentifier CommentResourceIdentifierWithMeta1 = new ResourceIdentifier(CommentType, CommentId1, ResourceMeta);
        public static readonly ResourceIdentifier CommentResourceIdentifierWithMeta2 = new ResourceIdentifier(CommentType, CommentId2, ResourceMeta);
        public static readonly ResourceIdentifier CommentResourceIdentifierWithMeta3 = new ResourceIdentifier(CommentType, CommentId3, ResourceMeta);
        public static readonly ResourceIdentifier CommentResourceIdentifierWithMeta4 = new ResourceIdentifier(CommentType, CommentId4, ResourceMeta);
        public static readonly ResourceIdentifier PersonResourceIdentifierWithMeta = new ResourceIdentifier(PersonType, PersonId, ResourceMeta);
        #endregion

        #region Attributes
        // Article
        public const string ArticleTitlePropertyName = "title";

        // Blog
        public const string BlogNamePropertyName = "name";

        // Comment
        public const string CommentBodyPropertyName = "body";

        // Person
        public const string PersonFirstNamePropertyName = "first-name";
        public const string PersonLastNamePropertyName = "last-name";
        public const string PersonTwitterPropertyName = "twitter";
        #endregion

        #region HRefs
        public static readonly UrlBuilderConfiguration UrlBuilderConfiguration = new UrlBuilderConfiguration
            {
                Scheme = Uri.UriSchemeHttp,
                Host = "api.example.com"
            };

        public static readonly string ArticleCollectionHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Build();
        public static readonly string BlogCollectionHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(BlogCollectionPathSegment).Build();
        public static readonly string CommentCollectionHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Build();
        public static readonly string PersonCollectionHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Build();

        public static readonly string ArticleHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId).Build();
        public static readonly string ArticleHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId1).Build();
        public static readonly string ArticleHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId2).Build();

        public static readonly string ArticleToRelationshipsToAuthorHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId).Path(Keywords.Relationships).Path(ArticleToAuthorRel).Build();
        public static readonly string ArticleToAuthorHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId).Path(ArticleToAuthorRel).Build();

        public static readonly string ArticleToRelationshipsToAuthorHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId1).Path(Keywords.Relationships).Path(ArticleToAuthorRel).Build();
        public static readonly string ArticleToAuthorHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId1).Path(ArticleToAuthorRel).Build();

        public static readonly string ArticleToRelationshipsToAuthorHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId2).Path(Keywords.Relationships).Path(ArticleToAuthorRel).Build();
        public static readonly string ArticleToAuthorHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId2).Path(ArticleToAuthorRel).Build();

        public static readonly string ArticleToRelationshipsToBlogHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId).Path(Keywords.Relationships).Path(ArticleToBlogRel).Build();
        public static readonly string ArticleToBlogHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId).Path(ArticleToBlogRel).Build();

        public static readonly string ArticleToRelationshipsToCommentsHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId).Path(Keywords.Relationships).Path(CommentCollectionPathSegment).Build();
        public static readonly string ArticleToCommentsHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId).Path(CommentCollectionPathSegment).Build();

        public static readonly string ArticleToRelationshipsToCommentsHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId1).Path(Keywords.Relationships).Path(CommentCollectionPathSegment).Build();
        public static readonly string ArticleToCommentsHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId1).Path(CommentCollectionPathSegment).Build();

        public static readonly string ArticleToRelationshipsToCommentsHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId2).Path(Keywords.Relationships).Path(CommentCollectionPathSegment).Build();
        public static readonly string ArticleToCommentsHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(ArticleCollectionPathSegment).Path(ArticleId2).Path(CommentCollectionPathSegment).Build();

        public static readonly string BlogHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(BlogCollectionPathSegment).Path(BlogId).Build();
        public static readonly string BlogHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(BlogCollectionPathSegment).Path(BlogId1).Build();
        public static readonly string BlogHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(BlogCollectionPathSegment).Path(BlogId2).Build();

        public static readonly string BlogToRelationshipsToArticlesHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(BlogCollectionPathSegment).Path(BlogId).Path(Keywords.Relationships).Path(ArticleCollectionPathSegment).Build();
        public static readonly string BlogToArticlesHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(BlogCollectionPathSegment).Path(BlogId).Path(ArticleCollectionPathSegment).Build();

        public static readonly string BlogToRelationshipsToArticlesHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(BlogCollectionPathSegment).Path(BlogId1).Path(Keywords.Relationships).Path(ArticleCollectionPathSegment).Build();
        public static readonly string BlogToArticlesHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(BlogCollectionPathSegment).Path(BlogId1).Path(ArticleCollectionPathSegment).Build();

        public static readonly string BlogToRelationshipsToArticlesHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(BlogCollectionPathSegment).Path(BlogId2).Path(Keywords.Relationships).Path(ArticleCollectionPathSegment).Build();
        public static readonly string BlogToArticlesHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(BlogCollectionPathSegment).Path(BlogId2).Path(ArticleCollectionPathSegment).Build();

        public static readonly string CommentHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId).Build();
        public static readonly string CommentHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId1).Build();
        public static readonly string CommentHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId2).Build();
        public static readonly string CommentHRef3 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId3).Build();
        public static readonly string CommentHRef4 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId4).Build();

        public static readonly string CommentToRelationshipsToAuthorHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId).Path(Keywords.Relationships).Path(CommentToAuthorRel).Build();
        public static readonly string CommentToAuthorHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId).Path(CommentToAuthorRel).Build();

        public static readonly string CommentToRelationshipsToAuthorHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId1).Path(Keywords.Relationships).Path(CommentToAuthorRel).Build();
        public static readonly string CommentToAuthorHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId1).Path(CommentToAuthorRel).Build();

        public static readonly string CommentToRelationshipsToAuthorHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId2).Path(Keywords.Relationships).Path(CommentToAuthorRel).Build();
        public static readonly string CommentToAuthorHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId2).Path(CommentToAuthorRel).Build();

        public static readonly string CommentToRelationshipsToAuthorHRef3 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId3).Path(Keywords.Relationships).Path(CommentToAuthorRel).Build();
        public static readonly string CommentToAuthorHRef3 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId3).Path(CommentToAuthorRel).Build();

        public static readonly string CommentToRelationshipsToAuthorHRef4 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId4).Path(Keywords.Relationships).Path(CommentToAuthorRel).Build();
        public static readonly string CommentToAuthorHRef4 = UrlBuilder.Create(UrlBuilderConfiguration).Path(CommentCollectionPathSegment).Path(CommentId4).Path(CommentToAuthorRel).Build();

        public static readonly string PersonHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId).Build();
        public static readonly string PersonHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId1).Build();
        public static readonly string PersonHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId2).Build();
        public static readonly string PersonHRef3 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId3).Build();
        public static readonly string PersonHRef4 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId4).Build();

        public static readonly string PersonToRelationshipsToCommentsHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId).Path(Keywords.Relationships).Path(CommentCollectionPathSegment).Build();
        public static readonly string PersonToCommentsHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId).Path(CommentCollectionPathSegment).Build();

        public static readonly string PersonToRelationshipsToCommentsHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId1).Path(Keywords.Relationships).Path(CommentCollectionPathSegment).Build();
        public static readonly string PersonToCommentsHRef1 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId1).Path(CommentCollectionPathSegment).Build();

        public static readonly string PersonToRelationshipsToCommentsHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId2).Path(Keywords.Relationships).Path(CommentCollectionPathSegment).Build();
        public static readonly string PersonToCommentsHRef2 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId2).Path(CommentCollectionPathSegment).Build();

        public static readonly string PersonToRelationshipsToCommentsHRef3 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId3).Path(Keywords.Relationships).Path(CommentCollectionPathSegment).Build();
        public static readonly string PersonToCommentsHRef3 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId3).Path(CommentCollectionPathSegment).Build();

        public static readonly string PersonToRelationshipsToCommentsHRef4 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId4).Path(Keywords.Relationships).Path(CommentCollectionPathSegment).Build();
        public static readonly string PersonToCommentsHRef4 = UrlBuilder.Create(UrlBuilderConfiguration).Path(PersonCollectionPathSegment).Path(PersonId4).Path(CommentCollectionPathSegment).Build();
        #endregion

        #region Links
        public static readonly Link ArticleCollectionLink = new Link(ArticleCollectionHRef);
        public static readonly Link BlogCollectionLink = new Link(BlogCollectionHRef);
        public static readonly Link CommentCollectionLink = new Link(CommentCollectionHRef);
        public static readonly Link PersonCollectionLink = new Link(PersonCollectionHRef);

        public static readonly Link ArticleCollectionLinkWithMeta = new Link(ArticleCollectionHRef, LinkMeta);

        public static readonly Link ArticleLink = new Link(ArticleHRef);
        public static readonly Link ArticleLink1 = new Link(ArticleHRef1);
        public static readonly Link ArticleLink2 = new Link(ArticleHRef2);

        public static readonly Link ArticleLinkWithMeta = new Link(ArticleHRef, LinkMeta);
        public static readonly Link ArticleLinkWithMeta1 = new Link(ArticleHRef1, LinkMeta);
        public static readonly Link ArticleLinkWithMeta2 = new Link(ArticleHRef2, LinkMeta);

        public static readonly Link ArticleToRelationshipsToAuthorLink = new Link(ArticleToRelationshipsToAuthorHRef);
        public static readonly Link ArticleToAuthorLink = new Link(ArticleToAuthorHRef);

        public static readonly Link ArticleToRelationshipsToAuthorLink1 = new Link(ArticleToRelationshipsToAuthorHRef1);
        public static readonly Link ArticleToAuthorLink1 = new Link(ArticleToAuthorHRef1);

        public static readonly Link ArticleToRelationshipsToAuthorLink2 = new Link(ArticleToRelationshipsToAuthorHRef2);
        public static readonly Link ArticleToAuthorLink2 = new Link(ArticleToAuthorHRef2);

        public static readonly Link ArticleToRelationshipsToBlogLink = new Link(ArticleToRelationshipsToBlogHRef);
        public static readonly Link ArticleToBlogLink = new Link(ArticleToBlogHRef);

        public static readonly Link ArticleToRelationshipsToCommentsLink = new Link(ArticleToRelationshipsToCommentsHRef);
        public static readonly Link ArticleToCommentsLink = new Link(ArticleToCommentsHRef);

        public static readonly Link ArticleToRelationshipsToCommentsLink1 = new Link(ArticleToRelationshipsToCommentsHRef1);
        public static readonly Link ArticleToCommentsLink1 = new Link(ArticleToCommentsHRef1);

        public static readonly Link ArticleToRelationshipsToCommentsLink2 = new Link(ArticleToRelationshipsToCommentsHRef2);
        public static readonly Link ArticleToCommentsLink2 = new Link(ArticleToCommentsHRef2);

        public static readonly Link BlogLink = new Link(BlogHRef);
        public static readonly Link BlogLink1 = new Link(BlogHRef1);
        public static readonly Link BlogLink2 = new Link(BlogHRef2);

        public static readonly Link BlogToRelationshipsToArticlesLink = new Link(BlogToRelationshipsToArticlesHRef);
        public static readonly Link BlogToArticlesLink = new Link(BlogToArticlesHRef);

        public static readonly Link BlogToRelationshipsToArticlesLink1 = new Link(BlogToRelationshipsToArticlesHRef1);
        public static readonly Link BlogToArticlesLink1 = new Link(BlogToArticlesHRef1);

        public static readonly Link BlogToRelationshipsToArticlesLink2 = new Link(BlogToRelationshipsToArticlesHRef2);
        public static readonly Link BlogToArticlesLink2 = new Link(BlogToArticlesHRef2);

        public static readonly Link CommentLink = new Link(CommentHRef);
        public static readonly Link CommentLink1 = new Link(CommentHRef1);
        public static readonly Link CommentLink2 = new Link(CommentHRef2);
        public static readonly Link CommentLink3 = new Link(CommentHRef3);
        public static readonly Link CommentLink4 = new Link(CommentHRef4);

        public static readonly Link CommentToRelationshipsToAuthorLink = new Link(CommentToRelationshipsToAuthorHRef);
        public static readonly Link CommentToAuthorLink = new Link(CommentToAuthorHRef);

        public static readonly Link CommentToRelationshipsToAuthorLink1 = new Link(CommentToRelationshipsToAuthorHRef1);
        public static readonly Link CommentToAuthorLink1 = new Link(CommentToAuthorHRef1);

        public static readonly Link CommentToRelationshipsToAuthorLink2 = new Link(CommentToRelationshipsToAuthorHRef2);
        public static readonly Link CommentToAuthorLink2 = new Link(CommentToAuthorHRef2);

        public static readonly Link CommentToRelationshipsToAuthorLink3 = new Link(CommentToRelationshipsToAuthorHRef3);
        public static readonly Link CommentToAuthorLink3 = new Link(CommentToAuthorHRef3);

        public static readonly Link CommentToRelationshipsToAuthorLink4 = new Link(CommentToRelationshipsToAuthorHRef4);
        public static readonly Link CommentToAuthorLink4 = new Link(CommentToAuthorHRef4);

        public static readonly Link PersonLink = new Link(PersonHRef);
        public static readonly Link PersonLink1 = new Link(PersonHRef1);
        public static readonly Link PersonLink2 = new Link(PersonHRef2);
        public static readonly Link PersonLink3 = new Link(PersonHRef3);
        public static readonly Link PersonLink4 = new Link(PersonHRef4);

        public static readonly Link PersonToRelationshipsToCommentsLink = new Link(PersonToRelationshipsToCommentsHRef);
        public static readonly Link PersonToCommentsLink = new Link(PersonToCommentsHRef);

        public static readonly Link PersonToRelationshipsToCommentsLink1 = new Link(PersonToRelationshipsToCommentsHRef1);
        public static readonly Link PersonToCommentsLink1 = new Link(PersonToCommentsHRef1);

        public static readonly Link PersonToRelationshipsToCommentsLink2 = new Link(PersonToRelationshipsToCommentsHRef2);
        public static readonly Link PersonToCommentsLink2 = new Link(PersonToCommentsHRef2);

        public static readonly Link PersonToRelationshipsToCommentsLink3 = new Link(PersonToRelationshipsToCommentsHRef3);
        public static readonly Link PersonToCommentsLink3 = new Link(PersonToCommentsHRef3);

        public static readonly Link PersonToRelationshipsToCommentsLink4 = new Link(PersonToRelationshipsToCommentsHRef4);
        public static readonly Link PersonToCommentsLink4 = new Link(PersonToCommentsHRef4);
        #endregion

        #region Relationship Names
        public const string ArticleToAuthorRel = "author";
        public const string ArticleToBlogRel = "blog";
        public const string ArticleToCommentsRel = "comments";
        public const string BlogToArticlesRel = "articles";
        public const string CommentToAuthorRel = "author";
        public const string PersonToCommentsRel = "comments";
        #endregion

        #region Relationship Path Segments
        public const string ArticleToAuthorRelPathSegment = ArticleToAuthorRel;
        public const string ArticleToBlogRelPathSegment = ArticleToBlogRel;
        public const string ArticleToCommentsRelPathSegment = ArticleToCommentsRel;
        public const string BlogToArticlesRelPathSegment = BlogToArticlesRel;
        public const string CommentToAuthorRelPathSegment = CommentToAuthorRel;
        public const string PersonToCommentsRelPathSegment = PersonToCommentsRel;
        #endregion

        #region Relationships
        public static readonly Relationship ArticleToAuthorRelationship = new Relationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                    }
        };

        public static readonly Relationship ArticleToAuthorRelationship1 = new Relationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink1},
                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink1}
                    }
        };

        public static readonly Relationship ArticleToAuthorRelationship2 = new Relationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink2},
                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink2}
                    }
        };

        public static readonly Relationship ArticleToCommentsRelationship = new Relationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink},
                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink}
                    }
        };

        public static readonly Relationship ArticleToCommentsRelationship1 = new Relationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink1},
                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink1}
                    }
        };

        public static readonly Relationship ArticleToCommentsRelationship2 = new Relationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink2},
                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink2}
                    }
        };

        public static readonly Relationship ArticleToAuthorToOneRelationship = new ToOneRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                    },
            Data = PersonResourceIdentifier
        };

        public static readonly Relationship ArticleToAuthorToOneRelationshipNull = new ToOneRelationship
        {
            Links = new Links
            {
                {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
            },
            Data = default(ResourceIdentifier)
        };

        public static readonly Relationship ArticleToAuthorToOneRelationship1 = new ToOneRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink1},
                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink1}
                    },
            Data = PersonResourceIdentifier1
        };

        public static readonly Relationship ArticleToAuthorToOneRelationshipNull1 = new ToOneRelationship
        {
            Links = new Links
            {
                {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink1},
                {Keywords.Related, ApiSampleData.ArticleToAuthorLink1}
            },
            Data = default(ResourceIdentifier)
        };

        public static readonly Relationship ArticleToAuthorToOneRelationship2 = new ToOneRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink2},
                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink2}
                    },
            Data = PersonResourceIdentifier2
        };

        public static readonly Relationship ArticleToAuthorToOneRelationshipNull2 = new ToOneRelationship
        {
            Links = new Links
            {
                {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink2},
                {Keywords.Related, ApiSampleData.ArticleToAuthorLink2}
            },
            Data = default(ResourceIdentifier)
        };

        public static readonly Relationship ArticleToBlogToOneRelationship = new ToOneRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToBlogLink},
                        {Keywords.Related, ApiSampleData.ArticleToBlogLink}
                    },
            Data = BlogResourceIdentifier
        };

        public static readonly Relationship ArticleToCommentsToManyRelationship = new ToManyRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink},
                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink}
                    },
            Data = CommentResourceIdentifiers
        };

        public static readonly Relationship ArticleToCommentsToManyRelationshipEmpty = new ToManyRelationship
        {
            Links = new Links
            {
                {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink},
                {Keywords.Related, ApiSampleData.ArticleToCommentsLink}
            },
            Data = new List<ResourceIdentifier>()
        };

        public static readonly Relationship ArticleToCommentsToManyRelationship1 = new ToManyRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink1},
                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink1}
                    },
            Data = ArticleToCommentResourceIdentifiers1
        };

        public static readonly Relationship ArticleToCommentsToManyRelationshipEmpty1 = new ToManyRelationship
        {
            Links = new Links
            {
                {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink1},
                {Keywords.Related, ApiSampleData.ArticleToCommentsLink1}
            },
            Data = new List<ResourceIdentifier>()
        };

        public static readonly Relationship ArticleToCommentsToManyRelationship2 = new ToManyRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink2},
                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink2}
                    },
            Data = ArticleToCommentResourceIdentifiers2
        };

        public static readonly Relationship ArticleToCommentsToManyRelationshipEmpty2 = new ToManyRelationship
        {
            Links = new Links
            {
                {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink2},
                {Keywords.Related, ApiSampleData.ArticleToCommentsLink2}
            },
            Data = new List<ResourceIdentifier>()
        };

        public static readonly Relationship BlogToArticlesRelationship = new Relationship
            {
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogToRelationshipsToArticlesLink},
                        {Keywords.Related, ApiSampleData.BlogToArticlesLink}
                    }
            };

        public static readonly Relationship BlogToArticlesRelationship1 = new Relationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogToRelationshipsToArticlesLink1},
                        {Keywords.Related, ApiSampleData.BlogToArticlesLink1}
                    }
        };

        public static readonly Relationship BlogToArticlesRelationship2 = new Relationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogToRelationshipsToArticlesLink2},
                        {Keywords.Related, ApiSampleData.BlogToArticlesLink2}
                    }
        };

        public static readonly Relationship BlogToArticlesToManyRelationship = new ToManyRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogToRelationshipsToArticlesLink},
                        {Keywords.Related, ApiSampleData.BlogToArticlesLink}
                    },
            Data = ArticleResourceIdentifiers
        };

        public static readonly Relationship BlogToArticlesToManyRelationship1 = new ToManyRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogToRelationshipsToArticlesLink1},
                        {Keywords.Related, ApiSampleData.BlogToArticlesLink1}
                    },
            Data = ArticleResourceIdentifiers
        };

        public static readonly Relationship BlogToArticlesToManyRelationship2 = new ToManyRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogToRelationshipsToArticlesLink2},
                        {Keywords.Related, ApiSampleData.BlogToArticlesLink2}
                    },
            Data = ArticleResourceIdentifiers
        };

        public static readonly Relationship CommentToAuthorRelationship = new Relationship
            {
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentToRelationshipsToAuthorLink},
                        {Keywords.Related, ApiSampleData.CommentToAuthorLink}
                    }
            };

        public static readonly Relationship CommentToAuthorRelationship1 = new Relationship
            {
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentToRelationshipsToAuthorLink1},
                        {Keywords.Related, ApiSampleData.CommentToAuthorLink1}
                    }
            };

        public static readonly Relationship CommentToAuthorRelationship2 = new Relationship
            {
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentToRelationshipsToAuthorLink2},
                        {Keywords.Related, ApiSampleData.CommentToAuthorLink2}
                    }
            };

        public static readonly Relationship CommentToAuthorRelationship3 = new Relationship
            {
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentToRelationshipsToAuthorLink3},
                        {Keywords.Related, ApiSampleData.CommentToAuthorLink3}
                    }
            };

        public static readonly Relationship CommentToAuthorRelationship4 = new Relationship
            {
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentToRelationshipsToAuthorLink4},
                        {Keywords.Related, ApiSampleData.CommentToAuthorLink4}
                    }
            };

        public static readonly Relationship CommentToAuthorToOneRelationship = new ToOneRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentToRelationshipsToAuthorLink},
                        {Keywords.Related, ApiSampleData.CommentToAuthorLink}
                    },
            Data = PersonResourceIdentifier
        };

        public static readonly Relationship CommentToAuthorToOneRelationship1 = new ToOneRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentToRelationshipsToAuthorLink1},
                        {Keywords.Related, ApiSampleData.CommentToAuthorLink1}
                    },
            Data = PersonResourceIdentifier1
        };

        public static readonly Relationship CommentToAuthorToOneRelationship2 = new ToOneRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentToRelationshipsToAuthorLink2},
                        {Keywords.Related, ApiSampleData.CommentToAuthorLink2}
                    },
            Data = PersonResourceIdentifier2
        };

        public static readonly Relationship CommentToAuthorToOneRelationship3 = new ToOneRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentToRelationshipsToAuthorLink3},
                        {Keywords.Related, ApiSampleData.CommentToAuthorLink3}
                    },
            Data = PersonResourceIdentifier3
        };

        public static readonly Relationship CommentToAuthorToOneRelationship4 = new ToOneRelationship
        {
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentToRelationshipsToAuthorLink4},
                        {Keywords.Related, ApiSampleData.CommentToAuthorLink4}
                    },
            Data = PersonResourceIdentifier4
        };

        public static readonly Relationship PersonToCommentsRelationship = new Relationship
            {
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonToRelationshipsToCommentsLink},
                        {Keywords.Related, ApiSampleData.PersonToCommentsLink}
                    }
            };

        public static readonly Relationship PersonToCommentsRelationship1 = new Relationship
            {
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonToRelationshipsToCommentsLink1},
                        {Keywords.Related, ApiSampleData.PersonToCommentsLink1}
                    }
            };

        public static readonly Relationship PersonToCommentsRelationship2 = new Relationship
            {
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonToRelationshipsToCommentsLink2},
                        {Keywords.Related, ApiSampleData.PersonToCommentsLink2}
                    }
            };

        public static readonly Relationship PersonToCommentsRelationship3 = new Relationship
            {
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonToRelationshipsToCommentsLink3},
                        {Keywords.Related, ApiSampleData.PersonToCommentsLink3}
                    }
            };

        public static readonly Relationship PersonToCommentsRelationship4 = new Relationship
            {
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonToRelationshipsToCommentsLink4},
                        {Keywords.Related, ApiSampleData.PersonToCommentsLink4}
                    }
            };
        #endregion

        #region Resources
        public static readonly Resource ArticleResource = new Resource
            {
                Type = ArticleType,
                Id = ArticleId,
                Attributes = new Attributes(Attribute.Create("title", "JSON API paints my bikeshed!")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink},
                        {Keywords.Canonical, ApiSampleData.ArticleLink}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource ArticleResourceWithNoAttributes = new Resource
            {
                Type       = ArticleType,
                Id         = ArticleId,
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink},
                        {Keywords.Canonical, ApiSampleData.ArticleLink}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource ArticleResource1 = new Resource
            {
                Type = ArticleType,
                Id = ArticleId1,
                Attributes = new Attributes(Attribute.Create("title", "JSON API paints my bikeshed!")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship1},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship1}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink1},
                        {Keywords.Canonical, ApiSampleData.ArticleLink1}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource ArticleResource2 = new Resource
            {
                Type = ArticleType,
                Id = ArticleId2,
                Attributes = new Attributes(Attribute.Create("title", "JSON API paints my house!")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship2},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship2}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink2},
                        {Keywords.Canonical, ApiSampleData.ArticleLink2}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource ArticleResourceWithResourceLinkage = new Resource
            {
                Type = ArticleType,
                Id = ArticleId,
                Attributes = new Attributes(Attribute.Create("title", "JSON API paints my bikeshed!")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink},
                        {Keywords.Canonical, ApiSampleData.ArticleLink}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource ArticleResourceWithNullAndEmptyResourceLinkage = new Resource
        {
            Type = ArticleType,
            Id = ArticleId,
            Attributes = new Attributes(Attribute.Create("title", "JSON API paints my bikeshed!")),
            Relationships = new Relationships
            {
                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationshipNull},
                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationshipEmpty}
            },
            Links = new Links
            {
                {Keywords.Self, ApiSampleData.ArticleLink},
                {Keywords.Canonical, ApiSampleData.ArticleLink}
            },
            Meta = ResourceMeta
        };

        public static readonly Resource ArticleResourceWithResourceLinkage1 = new Resource
            {
                Type = ArticleType,
                Id = ArticleId1,
                Attributes = new Attributes(Attribute.Create("title", "JSON API paints my bikeshed!")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship1},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship1}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink1},
                        {Keywords.Canonical, ApiSampleData.ArticleLink1}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource ArticleResourceWithNullAndEmptyResourceLinkage1 = new Resource
        {
            Type = ArticleType,
            Id = ArticleId1,
            Attributes = new Attributes(Attribute.Create("title", "JSON API paints my bikeshed!")),
            Relationships = new Relationships
            {
                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationshipNull1},
                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationshipEmpty1}
            },
            Links = new Links
            {
                {Keywords.Self, ApiSampleData.ArticleLink1},
                {Keywords.Canonical, ApiSampleData.ArticleLink1}
            },
            Meta = ResourceMeta
        };

        public static readonly Resource ArticleResourceWithResourceLinkage2 = new Resource
            {
                Type = ArticleType,
                Id = ArticleId2,
                Attributes = new Attributes(Attribute.Create("title", "JSON API paints my house!")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship2},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship2}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink2},
                        {Keywords.Canonical, ApiSampleData.ArticleLink2}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource ArticleResourceWithNullAndEmptyResourceLinkage2 = new Resource
        {
            Type = ArticleType,
            Id = ArticleId2,
            Attributes = new Attributes(Attribute.Create("title", "JSON API paints my house!")),
            Relationships = new Relationships
            {
                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationshipNull2},
                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationshipEmpty2}
            },
            Links = new Links
            {
                {Keywords.Self, ApiSampleData.ArticleLink2},
                {Keywords.Canonical, ApiSampleData.ArticleLink2}
            },
            Meta = ResourceMeta
        };

        public static readonly Resource ArticleResourceWithIdentityOnly = new Resource
            {
                Type = ArticleType,
                Id = ArticleId
            };

        public static readonly Resource ArticleResourceWithJsonDataTypesAttributes = new Resource
            {
                Type = ArticleType,
                Id = ArticleId,
                Attributes = new Attributes(
                    Attribute.Create("title", "JSON API paints my house!"),
                    Attribute.Create("is-online", true),
                    Attribute.Create("version", (decimal)1.42),
                    Attribute.Create("line-count", 1024),
                    Attribute.Create("tags", new[] { "json", "json:api", "bikeshed", "REST" }),
                    Attribute.Create("audit", new AuditAttributes
                            {
                                Created = new AuditAttributeItem
                                    {
                                        Date = new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)),
                                        By = "John Doe",
                                    },
                                Modified = new AuditAttributeItem
                                    {
                                        Date =
                                            new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) +
                                            new TimeSpan(42, 0, 42, 42),
                                        By = "Jane Doe",
                                    },
                                ModifiedHistory = new []
                                    {
                                        new AuditAttributeItem
                                            {
                                                Date =
                                                    new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) +
                                                    new TimeSpan(1, 0, 10, 20),
                                                By = "Jane Doe",
                                            },
                                        new AuditAttributeItem
                                            {
                                                Date =
                                                    new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) +
                                                    new TimeSpan(2, 0, 15, 59),
                                                By = "Jane Doe",
                                            },
                                        new AuditAttributeItem
                                            {
                                                Date =
                                                    new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) +
                                                    new TimeSpan(3, 0, 24, 24),
                                                By = "Jane Doe",
                                            },
                                        new AuditAttributeItem
                                            {
                                                Date =
                                                    new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) +
                                                    new TimeSpan(42, 0, 42, 42),
                                                By = "Jane Doe",
                                            }
                                    },
                                Deleted = new AuditAttributeItem
                                    {
                                        Date =
                                            new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) +
                                            new TimeSpan(1000, 0, 0, 0),
                                        By = "Jane Doe",
                                    }
                            })
                    ),
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource BlogResource = new Resource
        {
            Type = BlogType,
            Id = BlogId,
            Attributes = new Attributes(Attribute.Create("name", "JSON API")),
            Relationships = new Relationships
                    {
                        {ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesRelationship}
                    },
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogLink}
                    },
            Meta = ResourceMeta
        };

        public static readonly Resource BlogResource1 = new Resource
            {
                Type = BlogType,
                Id = BlogId1,
                Attributes = new Attributes(Attribute.Create("name", "JSON API")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesRelationship1}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogLink1}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource BlogResource2 = new Resource
        {
            Type = BlogType,
            Id = BlogId2,
            Attributes = new Attributes(Attribute.Create("name", "JSON API")),
            Relationships = new Relationships
                    {
                        {ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesRelationship2}
                    },
            Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogLink2}
                    },
            Meta = ResourceMeta
        };

        public static readonly Resource CommentResource = new Resource
            {
                Type = CommentType,
                Id = CommentId,
                Attributes = new Attributes(Attribute.Create("body", "I disagree completely.")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentLink}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource CommentResource1 = new Resource
            {
                Type = CommentType,
                Id = CommentId1,
                Attributes = new Attributes(Attribute.Create("body", "I disagree completely.")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentLink1}
                    },
                Meta = ResourceMeta1
            };

        public static readonly Resource CommentResource2 = new Resource
            {
                Type = CommentType,
                Id = CommentId2,
                Attributes = new Attributes(Attribute.Create("body", "I agree completely.")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentLink2}
                    },
                Meta = ResourceMeta2
            };

        public static readonly Resource CommentResource3 = new Resource
            {
                Type = CommentType,
                Id = CommentId3,
                Attributes = new Attributes(Attribute.Create("body", "Is 42 the answer?")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship3}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentLink3}
                    },
                Meta = ResourceMeta3
            };

        public static readonly Resource CommentResource4 = new Resource
            {
                Type = CommentType,
                Id = CommentId4,
                Attributes = new Attributes(Attribute.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship4}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentLink4}
                    },
                Meta = ResourceMeta4
            };

        public static readonly Resource CommentResourceWithResourceLinkage = new Resource
            {
                Type = CommentType,
                Id = CommentId,
                Attributes = new Attributes(Attribute.Create("body", "I disagree completely.")),
                Relationships = new Relationships
                        {
                            {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorToOneRelationship}
                        },
                Links = new Links
                        {
                            {Keywords.Self, ApiSampleData.CommentLink}
                        },
                Meta = ResourceMeta
            };

        public static readonly Resource CommentResourceWithResourceLinkage1 = new Resource
            {
                Type = CommentType,
                Id = CommentId1,
                Attributes = new Attributes(Attribute.Create("body", "I disagree completely.")),
                Relationships = new Relationships
                        {
                            {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorToOneRelationship1}
                        },
                Links = new Links
                        {
                            {Keywords.Self, ApiSampleData.CommentLink1}
                        },
                Meta = ResourceMeta1
            };

        public static readonly Resource CommentResourceWithResourceLinkage2 = new Resource
            {
                Type = CommentType,
                Id = CommentId2,
                Attributes = new Attributes(Attribute.Create("body", "I agree completely.")),
                Relationships = new Relationships
                        {
                            {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorToOneRelationship2}
                        },
                Links = new Links
                        {
                            {Keywords.Self, ApiSampleData.CommentLink2}
                        },
                Meta = ResourceMeta2
            };

        public static readonly Resource CommentResourceWithResourceLinkage3 = new Resource
            {
                Type = CommentType,
                Id = CommentId3,
                Attributes = new Attributes(Attribute.Create("body", "Is 42 the answer?")),
                Relationships = new Relationships
                        {
                            {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorToOneRelationship3}
                        },
                Links = new Links
                        {
                            {Keywords.Self, ApiSampleData.CommentLink3}
                        },
                Meta = ResourceMeta3
            };

        public static readonly Resource CommentResourceWithResourceLinkage4 = new Resource
            {
                Type = CommentType,
                Id = CommentId4,
                Attributes = new Attributes(Attribute.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")),
                Relationships = new Relationships
                        {
                            {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorToOneRelationship4}
                        },
                Links = new Links
                        {
                            {Keywords.Self, ApiSampleData.CommentLink4}
                        },
                Meta = ResourceMeta4
            };

        public static readonly Resource PersonResource = new Resource
            {
                Type = PersonType,
                Id = PersonId,
                Attributes = new Attributes(
                    Attribute.Create("first-name", "John"),
                    Attribute.Create("last-name", "Doe"),
                    Attribute.Create("twitter", "johndoe24")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelationship}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonLink}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource PersonResource1 = new Resource
            {
                Type = PersonType,
                Id = PersonId1,
                Attributes = new Attributes(
                    Attribute.Create("first-name", "John"),
                    Attribute.Create("last-name", "Doe"),
                    Attribute.Create("twitter", "johndoe24")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelationship1}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonLink1}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource PersonResource2 = new Resource
            {
                Type = PersonType,
                Id = PersonId2,
                Attributes = new Attributes(
                    Attribute.Create("first-name", "Jane"),
                    Attribute.Create("last-name", "Doe"),
                    Attribute.Create("twitter", "janedoe42")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelationship2}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonLink2}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource PersonResource3 = new Resource
            {
                Type = PersonType,
                Id = PersonId3,
                Attributes = new Attributes(
                    Attribute.Create("first-name", "George"),
                    Attribute.Create("last-name", "Washington"),
                    Attribute.Create("twitter", "georgewashington1")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelationship3}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonLink3}
                    },
                Meta = ResourceMeta
            };

        public static readonly Resource PersonResource4 = new Resource
            {
                Type = PersonType,
                Id = PersonId4,
                Attributes = new Attributes(
                    Attribute.Create("first-name", "Thomas"),
                    Attribute.Create("last-name", "Jefferson"),
                    Attribute.Create("twitter", "thomasjefferson2")),
                Relationships = new Relationships
                    {
                        {ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelationship4}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonLink4}
                    },
                Meta = ResourceMeta
            };
        #endregion

        #region Errors
        [JsonObject(MemberSerialization.OptIn)]
        public class ErrorSource : JsonObject
        {
            // ReSharper disable UnusedAutoPropertyAccessor.Global
            [JsonProperty(Keywords.Pointer)] public string Pointer { get; set; }
            [JsonProperty(Keywords.Parameter)] public string Parameter { get; set; }
            // ReSharper restore UnusedAutoPropertyAccessor.Global
        }

        public static readonly Error Error = new Error
            {
                Id = ErrorId,
                Status = Convert.ToString((int)HttpStatusCode.NotFound),
                Code = Convert.ToString(HttpStatusCode.NotFound),
                Title = "Resource Not Found in Database",
                Detail = String.Format("Unable to find article [id={0}] in the database.", ArticleId),
                Links = new Links
                    {
                        About = ArticleLink
                    },
                Meta = ErrorMeta
            };

        public static readonly Error Error1 = new Error
            {
                Id = ErrorId1,
                Status = Convert.ToString((int)HttpStatusCode.BadRequest),
                Code = Convert.ToString(HttpStatusCode.BadRequest),
                Title = "Unknown Parameter",
                Detail = "Unknown field [name=Foo] used in URL query.",
                Source = JObject.FromObject(new ErrorSource
                    {
                        Pointer = "/data",
                        Parameter = "Foo"
                    }),
                Meta = ErrorMeta
            };

        public static readonly Error Error2 = new Error
            {
                Id = ErrorId2,
                Status = Convert.ToString((int)HttpStatusCode.InternalServerError),
                Code = Convert.ToString(HttpStatusCode.InternalServerError),
                Title = "Uncaught Exception",
                Detail = String.Format("SqlException was thrown on update of the Article [ArticleId={0}] entity in the database.", ArticleId),
                Source = JObject.FromObject(new ErrorSource
                    {
                        Pointer = "/data/attributes/title"
                    }),
                Links = new Links
                    {
                        About = ArticleLink
                    },
                Meta = ErrorMeta
            };
        #endregion

        #region JsonApi
        public static readonly JsonApiVersion JsonApiVersion = new JsonApiVersion
            {
                Version = JsonApiVersion.Version10String
            };

        public static readonly JsonApiVersion JsonApiVersionAndMeta = new JsonApiVersion
            {
                Version = JsonApiVersion.Version10String,
                Meta = JsonApiVersionMeta
            };
        #endregion
    }
}