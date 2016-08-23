// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;
using JsonApiFramework.TestData.ApiResources;

namespace JsonApiFramework.TestData.ClrResources
{
    public static class SampleComments
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly Comment Comment = new Comment
            {
                Id = ApiSampleData.CommentId,
                Body = "I disagree completely.",
                Relationships = new Relationships
                    {
                        {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship}

                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentLink}
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Comment Comment1 = new Comment
            {
                Id = ApiSampleData.CommentId1,
                Body = "I disagree completely.",
                Relationships = new Relationships
                    {
                        {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1}

                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentLink1}
                    },
                Meta = ApiSampleData.ResourceMeta1
            };

        public static readonly Comment Comment2 = new Comment
            {
                Id = ApiSampleData.CommentId2,
                Body = "I agree completely.",
                Relationships = new Relationships
                    {
                        {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2}

                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentLink2}
                    },
                Meta = ApiSampleData.ResourceMeta2
            };

        public static readonly Comment Comment3 = new Comment
            {
                Id = ApiSampleData.CommentId3,
                Body = "Is 42 the answer?",
                Relationships = new Relationships
                    {
                        {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship3}

                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentLink3}
                    },
                Meta = ApiSampleData.ResourceMeta3
            };

        public static readonly Comment Comment4 = new Comment
            {
                Id = ApiSampleData.CommentId4,
                Body = "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.",
                Relationships = new Relationships
                    {
                        {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship4}

                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.CommentLink4}
                    },
                Meta = ApiSampleData.ResourceMeta4
            };
        #endregion
    }
}