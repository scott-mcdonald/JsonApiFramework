// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Http;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Tests.JsonApi
{
    public static class JsonApiSampleData
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
            Authors = new[] { "John Doe", "Jane Doe" }
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

        #region HRefs
        public static readonly IUrlBuilderConfiguration UrlBuilderConfiguration = new UrlBuilderConfiguration(Uri.UriSchemeHttps, "api.example.com");

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
    }
}
