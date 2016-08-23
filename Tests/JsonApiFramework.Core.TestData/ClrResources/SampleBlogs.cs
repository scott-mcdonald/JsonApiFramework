// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;
using JsonApiFramework.TestData.ApiResources;

namespace JsonApiFramework.TestData.ClrResources
{
    public static class SampleBlogs
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly Blog Blog = new Blog
            {
                Id = ApiSampleData.BlogId,
                Name = "JSON API",
                Relationships = new Relationships
                    {
                        {ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesRelationship}

                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogLink}
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Blog Blog1 = new Blog
            {
                Id = ApiSampleData.BlogId1,
                Name = "JSON API",
                Relationships = new Relationships
                    {
                        {ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesRelationship1}

                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogLink1}
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Blog Blog2 = new Blog
            {
                Id = ApiSampleData.BlogId2,
                Name = "JSON API",
                Relationships = new Relationships
                    {
                        {ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesRelationship2}

                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.BlogLink2}
                    },
                Meta = ApiSampleData.ResourceMeta
            };
        #endregion
    }
}