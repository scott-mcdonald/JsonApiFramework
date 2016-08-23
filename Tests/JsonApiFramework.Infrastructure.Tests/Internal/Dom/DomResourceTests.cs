// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.TestAsserts.ClrResources;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    using DomNode = Node<DomNodeType>;
    using IAttributeInfo = JsonApiFramework.ServiceModel.IAttributeInfo;

    public class DomResourceTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomResourceTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomResourceTestData")]
        internal void TestIDomResourceAbstraction(string name, Resource expectedApiResource, object expectedClrResource, IDomResource actual)
        {
            // Arrange

            // Act
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            this.OutputDomTree((DomNode)actual);
            this.Output.WriteLine(String.Empty);

            this.Output.WriteLine("Expected Resource");
            this.OutputJson(expectedApiResource);

            var actualApiResource = actual.ApiResource;
            var actualClrResource = actual.ClrResource;

            this.Output.WriteLine(String.Empty);

            this.Output.WriteLine("Actual Resource");
            this.OutputJson(actualApiResource);

            // Assert
            ResourceAssert.Equal(expectedApiResource, actualApiResource);
            ClrResourceAssert.Equal(expectedClrResource, actualClrResource);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        private static readonly IServiceModel ServiceModel = ClrSampleData.ServiceModelWithBlogResourceTypes;

        private static readonly IResourceType ArticleResourceType = ServiceModel.GetResourceType<Article>();
        private static readonly IResourceIdentityInfo ArticleResourceIdentity = ArticleResourceType.ResourceIdentity;
        private static readonly IAttributeInfo ArticleTitleAttribute = ArticleResourceType.GetClrAttribute(StaticReflection.GetMemberName<Article>(x => x.Title));

        private static readonly IResourceType BlogResourceType = ServiceModel.GetResourceType<Blog>();
        private static readonly IResourceIdentityInfo BlogResourceIdentity = BlogResourceType.ResourceIdentity;
        private static readonly IAttributeInfo BlogNameAttribute = BlogResourceType.GetClrAttribute(StaticReflection.GetMemberName<Blog>(x => x.Name));

        private static readonly IResourceType CommentResourceType = ServiceModel.GetResourceType<Comment>();
        private static readonly IResourceIdentityInfo CommentResourceIdentity = CommentResourceType.ResourceIdentity;
        private static readonly IAttributeInfo CommentBodyAttribute = CommentResourceType.GetClrAttribute(StaticReflection.GetMemberName<Comment>(x => x.Body));

        private static readonly IResourceType PersonResourceType = ServiceModel.GetResourceType<Person>();
        private static readonly IResourceIdentityInfo PersonResourceIdentity = PersonResourceType.ResourceIdentity;
        private static readonly IAttributeInfo PersonFirstNameAttribute = PersonResourceType.GetClrAttribute(StaticReflection.GetMemberName<Person>(x => x.FirstName));
        private static readonly IAttributeInfo PersonLastNameAttribute = PersonResourceType.GetClrAttribute(StaticReflection.GetMemberName<Person>(x => x.LastName));
        private static readonly IAttributeInfo PersonTwitterAttribute = PersonResourceType.GetClrAttribute(StaticReflection.GetMemberName<Person>(x => x.Twitter));

        public static readonly IEnumerable<object[]> DomResourceTestData = new[]
            {
                new object[]
                    {
                        "WithDomReadOnlyResourceAndArticleResource",
                        ApiSampleData.ArticleResource, SampleArticles.Article,
                        DomReadOnlyResource.Create(ApiSampleData.ArticleResource, SampleArticles.Article)
                    },
                new object[]
                    {
                        "WithDomReadOnlyResourceAndArticleResourceWithResourceLinkage",
                        ApiSampleData.ArticleResourceWithResourceLinkage, SampleArticles.ArticleWithResourceLinkage,
                        DomReadOnlyResource.Create(ApiSampleData.ArticleResourceWithResourceLinkage, SampleArticles.ArticleWithResourceLinkage)
                    },
                new object[]
                    {
                        "WithDomReadOnlyResourceAndBlogResource",
                        ApiSampleData.BlogResource, SampleBlogs.Blog,
                        DomReadOnlyResource.Create(ApiSampleData.BlogResource, SampleBlogs.Blog)
                    },
                new object[]
                    {
                        "WithDomReadOnlyResourceAndCommentResource",
                        ApiSampleData.CommentResource, SampleComments.Comment,
                        DomReadOnlyResource.Create(ApiSampleData.CommentResource, SampleComments.Comment)
                    },
                new object[]
                    {
                        "WithDomReadOnlyResourceAndPersonResource",
                        ApiSampleData.PersonResource, SamplePersons.Person,
                        DomReadOnlyResource.Create(ApiSampleData.PersonResource, SamplePersons.Person)
                    },
                new object[]
                    {
                        "WithDomReadWriteResourceAndArticleResource",
                        ApiSampleData.ArticleResource, SampleArticles.Article,
                        DomReadWriteResource.Create(ServiceModel,
                            DomType.CreateFromResourceType(ArticleResourceType),
                            DomId.CreateFromApiResourceIdentity(ArticleResourceType, ApiSampleData.ArticleResource),
                            DomAttributes.Create(
                                DomAttribute.CreateFromApiResource(ArticleTitleAttribute, ApiSampleData.ArticleResource)),
                            DomReadWriteRelationships.Create(
                                DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                                    DomReadWriteLinks.Create(
                                        DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                        DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef)))),
                                DomReadWriteRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                                    DomReadWriteLinks.Create(
                                        DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
                                        DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToCommentsHRef))))),
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Canonical, DomHRef.Create(ApiSampleData.ArticleHRef)),
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleHRef))),
                            DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta))
                    },
                new object[]
                    {
                        "WithDomReadWriteResourceAndArticleResourceWithResourceLinkage",
                        ApiSampleData.ArticleResourceWithResourceLinkage, SampleArticles.ArticleWithResourceLinkage,
                        DomReadWriteResource.Create(ServiceModel,
                            DomType.CreateFromResourceType(ArticleResourceType),
                            DomId.CreateFromApiResourceIdentity(ArticleResourceType, ApiSampleData.ArticleResource),
                            DomAttributes.Create(
                                DomAttribute.CreateFromApiResource(ArticleTitleAttribute, ApiSampleData.ArticleResource)),
                            DomReadWriteRelationships.Create(
                                DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                                    DomReadWriteLinks.Create(
                                        DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                        DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                                    DomData.CreateFromResourceIdentifier(
                                        DomReadWriteResourceIdentifier.Create(
                                            DomType.CreateFromResourceType(PersonResourceType),
                                            DomId.CreateFromApiResourceIdentity(PersonResourceType, ApiSampleData.PersonResource)))),
                                DomReadWriteRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                                    DomReadWriteLinks.Create(
                                        DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
                                        DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToCommentsHRef))),
                                    DomDataCollection.CreateFromResourceIdentifiers(
                                        DomReadWriteResourceIdentifier.Create(
                                            DomType.CreateFromResourceType(CommentResourceType),
                                            DomId.CreateFromApiResourceIdentity(CommentResourceType, ApiSampleData.CommentResource1)),
                                        DomReadWriteResourceIdentifier.Create(
                                            DomType.CreateFromResourceType(CommentResourceType),
                                            DomId.CreateFromApiResourceIdentity(CommentResourceType, ApiSampleData.CommentResource2))))),
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Canonical, DomHRef.Create(ApiSampleData.ArticleHRef)),
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleHRef))),
                            DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta))
                    },
                new object[]
                    {
                        "WithDomReadWriteResourceAndBlogResource",
                        ApiSampleData.BlogResource, SampleBlogs.Blog,
                        DomReadWriteResource.Create(ServiceModel,
                            DomType.CreateFromResourceType(BlogResourceType),
                            DomId.CreateFromApiResourceIdentity(BlogResourceType, ApiSampleData.BlogResource),
                            DomAttributes.Create(
                                DomAttribute.CreateFromApiResource(BlogNameAttribute, ApiSampleData.BlogResource)),
                            DomReadWriteRelationships.Create(
                                DomReadWriteRelationship.Create(ApiSampleData.BlogToArticlesRel,
                                    DomReadWriteLinks.Create(
                                        DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.BlogToRelationshipsToArticlesHRef)),
                                        DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.BlogToArticlesHRef))))),
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.BlogHRef))),
                            DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta))
                    },
                new object[]
                    {
                        "WithDomReadWriteResourceAndCommentResource",
                        ApiSampleData.CommentResource, SampleComments.Comment,
                        DomReadWriteResource.Create(ServiceModel,
                            DomType.CreateFromResourceType(CommentResourceType),
                            DomId.CreateFromApiResourceIdentity(CommentResourceType, ApiSampleData.CommentResource),
                            DomAttributes.Create(
                                DomAttribute.CreateFromApiResource(CommentBodyAttribute, ApiSampleData.CommentResource)),
                            DomReadWriteRelationships.Create(
                                DomReadWriteRelationship.Create(ApiSampleData.CommentToAuthorRel,
                                    DomReadWriteLinks.Create(
                                        DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.CommentToRelationshipsToAuthorHRef)),
                                        DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.CommentToAuthorHRef))))),
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.CommentHRef))),
                            DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta))
                    },
                new object[]
                    {
                        "WithDomReadWriteResourceAndPersonResource",
                        ApiSampleData.PersonResource, SamplePersons.Person,
                        DomReadWriteResource.Create(ServiceModel,
                            DomType.CreateFromResourceType(PersonResourceType),
                            DomId.CreateFromApiResourceIdentity(PersonResourceType, ApiSampleData.PersonResource),
                            DomAttributes.Create(
                                DomAttribute.CreateFromApiResource(PersonFirstNameAttribute, ApiSampleData.PersonResource),
                                DomAttribute.CreateFromApiResource(PersonLastNameAttribute, ApiSampleData.PersonResource),
                                DomAttribute.CreateFromApiResource(PersonTwitterAttribute, ApiSampleData.PersonResource)),
                            DomReadWriteRelationships.Create(
                                DomReadWriteRelationship.Create(ApiSampleData.PersonToCommentsRel,
                                    DomReadWriteLinks.Create(
                                        DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.PersonToRelationshipsToCommentsHRef)),
                                        DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.PersonToCommentsHRef))))),
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.PersonHRef))),
                            DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta))
                    },
            };
        #endregion
    }
}
