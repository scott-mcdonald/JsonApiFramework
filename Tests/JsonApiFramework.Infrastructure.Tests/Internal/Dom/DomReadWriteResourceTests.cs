// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.TestAsserts.Internal.Dom;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomReadWriteResourceTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadWriteResourceTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Fact]
        public void TestDomReadWriteResourceCreateWithArticleResource()
        {
            // Arrange
            var serviceModel = ClrSampleData.ServiceModelWithBlogResourceTypes;

            var articleResourceType = serviceModel.GetResourceType<Article>();
            var articleResourceIdentity = articleResourceType.ResourceIdentity;
            var articleTitleAttribute = articleResourceType.GetClrAttribute(StaticReflection.GetMemberName<Article>(x => x.Title));

            var expected = ApiSampleData.ArticleResource;

            // Act
            var actual = DomReadWriteResource.Create(
                DomType.CreateFromResourceType(articleResourceType),
                DomId.CreateFromApiResourceIdentity(articleResourceType, expected),
                DomAttributes.Create(
                    DomAttribute.CreateFromApiResource(articleTitleAttribute, expected)),
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
                DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta));

            this.OutputDomTree(actual);

            // Assert
            DomReadWriteResourceAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestDomReadWriteResourceCreateWithArticleResourceWithResourceLinkage()
        {
            // Arrange
            var serviceModel = ClrSampleData.ServiceModelWithBlogResourceTypes;

            var articleResourceType = serviceModel.GetResourceType<Article>();
            var articleTitleAttribute = articleResourceType.GetClrAttribute(StaticReflection.GetMemberName<Article>(x => x.Title));

            var commentResourceType = serviceModel.GetResourceType<Comment>();
            var personResourceType = serviceModel.GetResourceType<Person>();

            var expectedArticle = ApiSampleData.ArticleResourceWithResourceLinkage;
            var expectedAuthor = ApiSampleData.PersonResource;
            var expectedComment1 = ApiSampleData.CommentResource1;
            var expectedComment2 = ApiSampleData.CommentResource2;

            // Act
            var actual = DomReadWriteResource.Create(
                DomType.CreateFromResourceType(articleResourceType),
                DomId.CreateFromApiResourceIdentity(articleResourceType, expectedArticle),
                DomAttributes.Create(
                    DomAttribute.CreateFromApiResource(articleTitleAttribute, expectedArticle)),
                DomReadWriteRelationships.Create(
                    DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                        DomReadWriteLinks.Create(
                            DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                            DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                        DomData.CreateFromResourceIdentifier(
                            DomReadWriteResourceIdentifier.Create(
                                DomType.CreateFromResourceType(personResourceType),
                                DomId.CreateFromApiResourceIdentity(personResourceType, expectedAuthor)))),
                    DomReadWriteRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                        DomReadWriteLinks.Create(
                            DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
                            DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToCommentsHRef))),
                        DomDataCollection.CreateFromResourceIdentifiers(
                            DomReadWriteResourceIdentifier.Create(
                                DomType.CreateFromResourceType(commentResourceType),
                                DomId.CreateFromApiResourceIdentity(commentResourceType, expectedComment1)),
                            DomReadWriteResourceIdentifier.Create(
                                DomType.CreateFromResourceType(commentResourceType),
                                DomId.CreateFromApiResourceIdentity(commentResourceType, expectedComment2))))),
                DomReadWriteLinks.Create(
                    DomReadWriteLink.Create(Keywords.Canonical, DomHRef.Create(ApiSampleData.ArticleHRef)),
                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleHRef))),
                DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta));

            this.OutputDomTree(actual);

            // Assert
            DomReadWriteResourceAssert.Equal(expectedArticle, actual);
        }

        [Fact]
        public void TestDomReadWriteResourceCreateWithBlogResource()
        {
            // Arrange
            var serviceModel = ClrSampleData.ServiceModelWithBlogResourceTypes;

            var blogResourceType = serviceModel.GetResourceType<Blog>();
            var blogResourceIdentity = blogResourceType.ResourceIdentity;
            var blogNameAttribute = blogResourceType.GetClrAttribute(StaticReflection.GetMemberName<Blog>(x => x.Name));

            var expectedBlog = ApiSampleData.BlogResource;

            // Act
            var actual = DomReadWriteResource.Create(
                DomType.CreateFromResourceType(blogResourceType),
                DomId.CreateFromApiResourceIdentity(blogResourceType, expectedBlog),
                DomAttributes.Create(
                    DomAttribute.CreateFromApiResource(blogNameAttribute, expectedBlog)),
                DomReadWriteRelationships.Create(
                    DomReadWriteRelationship.Create(ApiSampleData.BlogToArticlesRel,
                        DomReadWriteLinks.Create(
                            DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.BlogToRelationshipsToArticlesHRef)),
                            DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.BlogToArticlesHRef))))),
                DomReadWriteLinks.Create(
                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.BlogHRef))),
                DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta));

            this.OutputDomTree(actual);

            // Assert
            DomReadWriteResourceAssert.Equal(expectedBlog, actual);
        }

        [Fact]
        public void TestDomReadWriteResourceCreateWithCommentResource()
        {
            // Arrange
            var serviceModel = ClrSampleData.ServiceModelWithBlogResourceTypes;

            var commentResourceType = serviceModel.GetResourceType<Comment>();
            var commentResourceIdentity = commentResourceType.ResourceIdentity;
            var commentBodyAttribute = commentResourceType.GetClrAttribute(StaticReflection.GetMemberName<Comment>(x => x.Body));

            var expected = ApiSampleData.CommentResource;

            // Act
            var actual = DomReadWriteResource.Create(
                DomType.CreateFromResourceType(commentResourceType),
                DomId.CreateFromApiResourceIdentity(commentResourceType, expected),
                DomAttributes.Create(
                    DomAttribute.CreateFromApiResource(commentBodyAttribute, expected)),
                DomReadWriteRelationships.Create(
                    DomReadWriteRelationship.Create(ApiSampleData.CommentToAuthorRel,
                        DomReadWriteLinks.Create(
                            DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.CommentToRelationshipsToAuthorHRef)),
                            DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.CommentToAuthorHRef))))),
                DomReadWriteLinks.Create(
                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.CommentHRef))),
                DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta));

            this.OutputDomTree(actual);

            // Assert
            DomReadWriteResourceAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestDomReadWriteResourceCreateWithPersonResource()
        {
            // Arrange
            var serviceModel = ClrSampleData.ServiceModelWithBlogResourceTypes;

            var personResourceType = serviceModel.GetResourceType<Person>();
            var personResourceIdentity = personResourceType.ResourceIdentity;
            var personFirstNameAttribute = personResourceType.GetClrAttribute(StaticReflection.GetMemberName<Person>(x => x.FirstName));
            var personLastNameAttribute = personResourceType.GetClrAttribute(StaticReflection.GetMemberName<Person>(x => x.LastName));
            var personTwitterAttribute = personResourceType.GetClrAttribute(StaticReflection.GetMemberName<Person>(x => x.Twitter));

            var expected = ApiSampleData.PersonResource;

            // Act
            var actual = DomReadWriteResource.Create(
                DomType.CreateFromResourceType(personResourceType),
                DomId.CreateFromApiResourceIdentity(personResourceType, expected),
                DomAttributes.Create(
                    DomAttribute.CreateFromApiResource(personFirstNameAttribute, expected),
                    DomAttribute.CreateFromApiResource(personLastNameAttribute, expected),
                    DomAttribute.CreateFromApiResource(personTwitterAttribute, expected)),
                DomReadWriteRelationships.Create(
                    DomReadWriteRelationship.Create(ApiSampleData.PersonToCommentsRel,
                        DomReadWriteLinks.Create(
                            DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.PersonToRelationshipsToCommentsHRef)),
                            DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.PersonToCommentsHRef))))),
                DomReadWriteLinks.Create(
                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.PersonHRef))),
                DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta));

            this.OutputDomTree(actual);

            // Assert
            DomReadWriteResourceAssert.Equal(expected, actual);
        }
        #endregion
    }
}
