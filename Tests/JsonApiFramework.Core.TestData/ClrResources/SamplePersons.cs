// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;
using JsonApiFramework.TestData.ApiResources;

namespace JsonApiFramework.TestData.ClrResources
{
    public static class SamplePersons
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly Person Person = new Person
            {
                Id = ApiSampleData.PersonId,
                FirstName = "John",
                LastName = "Doe",
                Twitter = "johndoe24",
                Relationships = new Relationships
                    {
                        {ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelationship}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonLink}
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Person Person1 = new Person
            {
                Id = ApiSampleData.PersonId1,
                FirstName = "John",
                LastName = "Doe",
                Twitter = "johndoe24",
                Relationships = new Relationships
                    {
                        {ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelationship1}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonLink1}
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Person Person2 = new Person
            {
                Id = ApiSampleData.PersonId2,
                FirstName = "Jane",
                LastName = "Doe",
                Twitter = "janedoe42",
                Relationships = new Relationships
                    {
                        {ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelationship2}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonLink2}
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Person Person3 = new Person
            {
                Id = ApiSampleData.PersonId3,
                FirstName = "George",
                LastName = "Washington",
                Twitter = "georgewashington1",
                Relationships = new Relationships
                    {
                        {ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelationship3}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonLink3}
                    },
                Meta = ApiSampleData.ResourceMeta
            };

        public static readonly Person Person4 = new Person
            {
                Id = ApiSampleData.PersonId4,
                FirstName = "Thomas",
                LastName = "Jefferson",
                Twitter = "thomasjefferson2",
                Relationships = new Relationships
                    {
                        {ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelationship4}
                    },
                Links = new Links
                    {
                        {Keywords.Self, ApiSampleData.PersonLink4}
                    },
                Meta = ApiSampleData.ResourceMeta
            };
        #endregion
    }
}