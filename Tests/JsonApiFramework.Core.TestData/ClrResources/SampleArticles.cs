// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;
using JsonApiFramework.TestData.ApiResources;

namespace JsonApiFramework.TestData.ClrResources
{
    public static class SampleArticles
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly Article Article = new Article
            {
                Id = ApiSampleData.ArticleId,
                Title = "JSON API paints my bikeshed!",
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink},
                        {Keywords.Canonical, ApiSampleData.ArticleLink},
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Article ArticleWithResourceLinkage = new Article
            {
                Id = ApiSampleData.ArticleId,
                Title = "JSON API paints my bikeshed!",
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink},
                        {Keywords.Canonical, ApiSampleData.ArticleLink},
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Article Article1 = new Article
            {
                Id = ApiSampleData.ArticleId1,
                Title = "JSON API paints my bikeshed!",
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship1},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship1}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink1},
                        {Keywords.Canonical, ApiSampleData.ArticleLink1},
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Article ArticleWithResourceLinkage1 = new Article
            {
                Id = ApiSampleData.ArticleId1,
                Title = "JSON API paints my bikeshed!",
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship1},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship1}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink1},
                        {Keywords.Canonical, ApiSampleData.ArticleLink1},
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Article Article2 = new Article
            {
                Id = ApiSampleData.ArticleId2,
                Title = "JSON API paints my house!",
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship2},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship2}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink2},
                        {Keywords.Canonical, ApiSampleData.ArticleLink2},
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Article ArticleWithResourceLinkage2 = new Article
            {
                Id = ApiSampleData.ArticleId2,
                Title = "JSON API paints my house!",
                Relationships = new Relationships
                    {
                        {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship2},
                        {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship2}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.ArticleLink2},
                        {Keywords.Canonical, ApiSampleData.ArticleLink2},
                    },
                Meta = ApiSampleData.ResourceMeta
            };
        #endregion
    }
}