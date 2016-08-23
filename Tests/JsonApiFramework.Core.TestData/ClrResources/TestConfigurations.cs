// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Configuration;
using JsonApiFramework.ServiceModel.Conventions;
using JsonApiFramework.TestData.ApiResources;

namespace JsonApiFramework.TestData.ClrResources
{
    public static class TestConfigurations
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Convention Sets
        public static ConventionSet CreateConventionSet()
        {
            var conventionSetBuilder = new ConventionSetBuilder();

            conventionSetBuilder.ApiAttributeNamingConventions()
                                .AddStandardMemberNamingConvention();

            conventionSetBuilder.ApiTypeNamingConventions()
                                .AddPluralNamingConvention()
                                .AddStandardMemberNamingConvention();

            conventionSetBuilder.ResourceTypeConventions()
                                .AddPropertyDiscoveryConvention();

            var conventionSet = conventionSetBuilder.Create();
            return conventionSet;
        }
        #endregion

        #region Configurations With Null ConventionSet
        public class ArticleConfigurationWithNullConventionSet : ResourceTypeBuilder<Article>
        {
            public ArticleConfigurationWithNullConventionSet()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ApiSampleData.ArticleCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity(x => x.Id)
                    .SetApiType(ApiSampleData.ArticleType);

                // Attributes
                this.Attribute(x => x.Title)
                    .SetApiPropertyName(ApiSampleData.ArticleTitlePropertyName);

                // Relationships
                this.Relationships(x => x.Relationships);

                this.ToOneRelationship<Person>(ApiSampleData.ArticleToAuthorRel)
                    .SetApiRelPathSegment(ApiSampleData.ArticleToAuthorRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                this.ToManyRelationship<Comment>(ApiSampleData.ArticleToCommentsRel)
                    .SetApiRelPathSegment(ApiSampleData.ArticleToCommentsRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Links(x => x.Links);
                this.Link(Keywords.Canonical);
                this.Link(Keywords.Self);

                // Meta
                this.Meta(x => x.Meta);
            }
        }

        public class BlogConfigurationWithNullConventionSet : ResourceTypeBuilder<Blog>
        {
            public BlogConfigurationWithNullConventionSet()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ApiSampleData.BlogCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity(x => x.Id)
                    .SetApiType(ApiSampleData.BlogType);

                // Attributes
                this.Attribute(x => x.Name)
                    .SetApiPropertyName(ApiSampleData.BlogNamePropertyName);

                // Relationships
                this.Relationships(x => x.Relationships);

                this.ToManyRelationship<Article>(ApiSampleData.BlogToArticlesRel)
                    .SetApiRelPathSegment(ApiSampleData.BlogToArticlesRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Links(x => x.Links);
                this.Link(Keywords.Self);

                // Meta
                this.Meta(x => x.Meta);
            }
        }

        public class CommentConfigurationWithNullConventionSet : ResourceTypeBuilder<Comment>
        {
            public CommentConfigurationWithNullConventionSet()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ApiSampleData.CommentCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity(x => x.Id)
                    .SetApiType(ApiSampleData.CommentType);

                // Attributes
                this.Attribute(x => x.Body)
                    .SetApiPropertyName(ApiSampleData.CommentBodyPropertyName);

                // Relationships
                this.Relationships(x => x.Relationships);

                this.ToOneRelationship<Person>(ApiSampleData.CommentToAuthorRel)
                    .SetApiRelPathSegment(ApiSampleData.CommentToAuthorRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Links(x => x.Links);
                this.Link(Keywords.Self);

                // Meta
                this.Meta(x => x.Meta);
            }
        }

        public class OrderConfigurationWithNullConventionSet : ResourceTypeBuilder<Order>
        {
            public OrderConfigurationWithNullConventionSet()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ClrSampleData.OrderCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity(x => x.OrderId)
                    .SetApiType(ClrSampleData.OrderType);

                // Attributes
                this.Attribute(x => x.TotalPrice)
                    .SetApiPropertyName("total-price");

                // Relationships
                this.ToManyRelationship<OrderItem>(ClrSampleData.OrderToOrderItemsRel)
                    .SetApiRelPathSegment(ClrSampleData.OrderToOrderItemsRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);

                this.ToManyRelationship<Payment>(ClrSampleData.OrderToPaymentsRel)
                    .SetApiRelPathSegment(ClrSampleData.OrderToPaymentsRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                this.ToOneRelationship<Store>(ClrSampleData.OrderToStoreRel)
                    .SetApiRelPathSegment(ClrSampleData.OrderToStoreRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class OrderItemConfigurationWithNullConventionSet : ResourceTypeBuilder<OrderItem>
        {
            public OrderItemConfigurationWithNullConventionSet()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ClrSampleData.OrderItemCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity(x => x.OrderItemId)
                    .SetApiType(ClrSampleData.OrderItemType);

                // Attributes
                this.Attribute(x => x.ProductName)
                    .SetApiPropertyName("product-name");

                this.Attribute(x => x.Quantity)
                    .SetApiPropertyName("quantity");

                this.Attribute(x => x.UnitPrice)
                    .SetApiPropertyName("unit-price");

                // Relationships
                this.ToOneRelationship<Order>(ClrSampleData.OrderItemToOrderRel)
                    .SetApiRelPathSegment(ClrSampleData.OrderItemToOrderRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                this.ToOneRelationship<Product>(ClrSampleData.OrderItemToProductRel)
                    .SetApiRelPathSegment(ClrSampleData.OrderItemToProductRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class PaymentConfigurationWithNullConventionSet : ResourceTypeBuilder<Payment>
        {
            public PaymentConfigurationWithNullConventionSet()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ClrSampleData.PaymentCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity(x => x.PaymentId)
                    .SetApiType(ClrSampleData.PaymentType);

                // Attributes
                this.Attribute(x => x.Amount)
                    .SetApiPropertyName("amount");

                // Relationships
                this.ToOneRelationship<Order>(ClrSampleData.PaymentToOrderRel)
                    .SetApiRelPathSegment(ClrSampleData.PaymentToOrderRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class PersonConfigurationWithNullConventionSet : ResourceTypeBuilder<Person>
        {
            public PersonConfigurationWithNullConventionSet()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ApiSampleData.PersonCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity(x => x.Id)
                    .SetApiType(ApiSampleData.PersonType);

                // Attributes
                this.Attribute(x => x.FirstName)
                    .SetApiPropertyName(ApiSampleData.PersonFirstNamePropertyName);

                this.Attribute(x => x.LastName)
                    .SetApiPropertyName(ApiSampleData.PersonLastNamePropertyName);

                this.Attribute(x => x.Twitter)
                    .SetApiPropertyName(ApiSampleData.PersonTwitterPropertyName);

                // Relationships
                this.Relationships(x => x.Relationships);

                this.ToManyRelationship<Comment>(ApiSampleData.PersonToCommentsRel)
                    .SetApiRelPathSegment(ApiSampleData.PersonToCommentsRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Links(x => x.Links);
                this.Link(Keywords.Self);

                // Meta
                this.Meta(x => x.Meta);
            }
        }

        public class PosSystemConfigurationWithNullConventionSet : ResourceTypeBuilder<PosSystem>
        {
            public PosSystemConfigurationWithNullConventionSet()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ClrSampleData.PosSystemCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity(x => x.PosSystemId)
                    .SetApiType(ClrSampleData.PosSystemType);

                // Attributes
                this.Attribute(x => x.PosSystemName)
                    .SetApiPropertyName("pos-system-name");

                // Relationships
                this.ToManyRelationship<StoreConfiguration>(ClrSampleData.PosSystemToStoreConfigurationsRel)
                    .SetApiRelPathSegment(ClrSampleData.PosSystemToStoreConfigurationsRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class ProductConfigurationWithNullConventionSet : ResourceTypeBuilder<Product>
        {
            public ProductConfigurationWithNullConventionSet()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ClrSampleData.ProductCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity(x => x.ProductId)
                    .SetApiType(ClrSampleData.ProductType);

                // Attributes
                this.Attribute(x => x.Name)
                    .SetApiPropertyName("name");

                this.Attribute(x => x.UnitPrice)
                    .SetApiPropertyName("unit-price");

                // Relationships

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class StoreConfigurationConfigurationWithNullConventionSet : ResourceTypeBuilder<StoreConfiguration>
        {
            public StoreConfigurationConfigurationWithNullConventionSet()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ClrSampleData.StoreConfigurationCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity(x => x.StoreConfigurationId)
                    .SetApiType(ClrSampleData.StoreConfigurationType);

                // Attributes
                this.Attribute(x => x.IsLive)
                    .SetApiPropertyName("is-live");

                this.Attribute(x => x.MailingAddress)
                    .SetApiPropertyName("mailing-address");

                this.Attribute(x => x.PhoneNumbers)
                    .SetApiPropertyName("phone-numbers");

                // Relationships
                this.ToOneRelationship<PosSystem>(ClrSampleData.StoreToStoreConfigurationToPosSystemRel)
                    .SetApiRelPathSegment(ClrSampleData.StoreToStoreConfigurationToPosSystemRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class StoreConfigurationWithNullConventionSet : ResourceTypeBuilder<Store>
        {
            public StoreConfigurationWithNullConventionSet()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ClrSampleData.StoreCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity(x => x.StoreId)
                    .SetApiType(ClrSampleData.StoreType);

                // Attributes
                this.Attribute(x => x.StoreName)
                    .SetApiPropertyName("store-name");

                this.Attribute(x => x.Address)
                    .SetApiPropertyName("address");

                this.Attribute(x => x.City)
                    .SetApiPropertyName("city");

                this.Attribute(x => x.State)
                    .SetApiPropertyName("state");

                this.Attribute(x => x.ZipCode)
                    .SetApiPropertyName("zip-code");

                // Relationships
                this.ToOneRelationship<StoreConfiguration>(ClrSampleData.StoreToStoreConfigurationRel)
                    .SetApiRelPathSegment(ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);

                // Links
                this.Link(Keywords.Self);
            }
        }
        #endregion

        #region Configurations With ConventionSet
        public class ArticleConfigurationWithConventionSet : ResourceTypeBuilder<Article>
        {
            public ArticleConfigurationWithConventionSet()
            {
                // Relationships
                this.ToOneRelationship<Person>(ApiSampleData.ArticleToAuthorRel);

                this.ToManyRelationship<Comment>(ApiSampleData.ArticleToCommentsRel);

                // Links
                this.Link(Keywords.Canonical);
                this.Link(Keywords.Self);
            }
        }

        public class BlogConfigurationWithConventionSet : ResourceTypeBuilder<Blog>
        {
            public BlogConfigurationWithConventionSet()
            {
                // Relationships
                this.ToManyRelationship<Article>(ApiSampleData.BlogToArticlesRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class CommentConfigurationWithConventionSet : ResourceTypeBuilder<Comment>
        {
            public CommentConfigurationWithConventionSet()
            {
                // Relationships
                this.ToOneRelationship<Person>(ApiSampleData.CommentToAuthorRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class OrderConfigurationWithConventionSet : ResourceTypeBuilder<Order>
        {
            public OrderConfigurationWithConventionSet()
            {
                // Relationships
                this.ToManyRelationship<OrderItem>(ClrSampleData.OrderToOrderItemsRel)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);

                this.ToManyRelationship<Payment>(ClrSampleData.OrderToPaymentsRel);

                this.ToOneRelationship<Store>(ClrSampleData.OrderToStoreRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class OrderItemConfigurationWithConventionSet : ResourceTypeBuilder<OrderItem>
        {
            public OrderItemConfigurationWithConventionSet()
            {
                // Relationships
                this.ToOneRelationship<Order>(ClrSampleData.OrderItemToOrderRel);

                this.ToOneRelationship<Product>(ClrSampleData.OrderItemToProductRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class PaymentConfigurationWithConventionSet : ResourceTypeBuilder<Payment>
        {
            public PaymentConfigurationWithConventionSet()
            {
                // Relationships
                this.ToOneRelationship<Order>(ClrSampleData.PaymentToOrderRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class PersonConfigurationWithConventionSet : ResourceTypeBuilder<Person>
        {
            public PersonConfigurationWithConventionSet()
            {
                // Relationships
                this.ToManyRelationship<Comment>(ApiSampleData.PersonToCommentsRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class PosSystemConfigurationWithConventionSet : ResourceTypeBuilder<PosSystem>
        {
            public PosSystemConfigurationWithConventionSet()
            {
                // Relationships
                this.ToManyRelationship<StoreConfiguration>(ClrSampleData.PosSystemToStoreConfigurationsRel)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class ProductConfigurationWithConventionSet : ResourceTypeBuilder<Product>
        {
            public ProductConfigurationWithConventionSet()
            {
                // Links
                this.Link(Keywords.Self);
            }
        }

        public class StoreConfigurationConfigurationWithConventionSet : ResourceTypeBuilder<StoreConfiguration>
        {
            public StoreConfigurationConfigurationWithConventionSet()
            {
                // Relationships
                this.ToOneRelationship<PosSystem>(ClrSampleData.StoreToStoreConfigurationToPosSystemRel)
                    .SetApiRelPathSegment(ClrSampleData.StoreToStoreConfigurationToPosSystemRelPathSegment);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class StoreConfigurationWithConventionSet : ResourceTypeBuilder<Store>
        {
            public StoreConfigurationWithConventionSet()
            {
                // Relationships
                this.ToOneRelationship<StoreConfiguration>(ClrSampleData.StoreToStoreConfigurationRel)
                    .SetApiRelPathSegment(ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);

                // Links
                this.Link(Keywords.Self);
            }
        }
        #endregion
    }
}