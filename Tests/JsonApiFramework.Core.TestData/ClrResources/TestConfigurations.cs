// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.


using JsonApiFramework.Conventions;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Configuration;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources.ComplexTypes;

namespace JsonApiFramework.TestData.ClrResources
{
    public static class TestConfigurations
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Conventions
        public static IConventions CreateConventions()
        {
            var conventionsBuilder = new ConventionsBuilder();

            conventionsBuilder.ApiAttributeNamingConventions()
                              .AddStandardMemberNamingConvention();

            conventionsBuilder.ApiTypeNamingConventions()
                              .AddPluralNamingConvention()
                              .AddStandardMemberNamingConvention();

            conventionsBuilder.ComplexTypeConventions()
                              .AddPropertyDiscoveryConvention();

            conventionsBuilder.ResourceTypeConventions()
                              .AddPropertyDiscoveryConvention();

            var conventions = conventionsBuilder.Create();
            return conventions;
        }
        #endregion

        #region Configurations With Null Conventions
        public class ArticleConfigurationWithNullConventions : ResourceTypeBuilder<Article>
        {
            public ArticleConfigurationWithNullConventions()
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

        public class BlogConfigurationWithNullConventions : ResourceTypeBuilder<Blog>
        {
            public BlogConfigurationWithNullConventions()
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

        public class CommentConfigurationWithNullConventions : ResourceTypeBuilder<Comment>
        {
            public CommentConfigurationWithNullConventions()
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

        public class HomeConfigurationWithNullConventions : ResourceTypeBuilder<Home>
        {
            public HomeConfigurationWithNullConventions()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ClrSampleData.HomeCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity()
                    .SetApiType(ClrSampleData.HomeType);

                // Attributes
                this.Attribute(x => x.Message)
                    .SetApiPropertyName("message");

                // Links
                this.Links(x => x.Links);
                this.Link(Keywords.Self);
            }
        }

        public class SearchConfigurationWithNullConventions : ResourceTypeBuilder<Search>
        {
            public SearchConfigurationWithNullConventions()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ClrSampleData.SearchCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity()
                    .SetApiType(ClrSampleData.SearchType);

                // Attributes
                this.Attribute(x => x.Criteria)
                    .SetApiPropertyName("criteria");

                // Links
                this.Links(x => x.Links);
                this.Link(Keywords.Self);
            }
        }

        public class MailingAddressConfigurationWithNullConventions : ComplexTypeBuilder<MailingAddress>
        {
            public MailingAddressConfigurationWithNullConventions()
            {
                // Attributes
                this.Attribute(x => x.Address)
                    .SetApiPropertyName("address");

                this.Attribute(x => x.City)
                    .SetApiPropertyName("city");

                this.Attribute(x => x.State)
                    .SetApiPropertyName("state");

                this.Attribute(x => x.ZipCode)
                    .SetApiPropertyName("zip-code");
            }
        }

        public class MailingAddressConfigurationIgnoreAllButAddressWithNullConventions : ComplexTypeBuilder<MailingAddress>
        {
            public MailingAddressConfigurationIgnoreAllButAddressWithNullConventions()
            {
                // Attributes
                this.Attribute(x => x.Address)
                    .SetApiPropertyName("address");

                this.Attribute(x => x.City)
                    .SetApiPropertyName("city");

                this.Attribute(x => x.State)
                    .SetApiPropertyName("state");

                this.Attribute(x => x.ZipCode)
                    .SetApiPropertyName("zip-code");

                // Ignores
                this.Attribute(x => x.City)
                    .Ignore();

                this.Attribute(x => x.State)
                    .Ignore();

                this.Attribute(x => x.ZipCode)
                    .Ignore();
            }
        }

        public class OrderConfigurationWithNullConventions : ResourceTypeBuilder<Order>
        {
            public OrderConfigurationWithNullConventions()
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

        public class OrderItemConfigurationWithNullConventions : ResourceTypeBuilder<OrderItem>
        {
            public OrderItemConfigurationWithNullConventions()
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

        public class PaymentConfigurationWithNullConventions : ResourceTypeBuilder<Payment>
        {
            public PaymentConfigurationWithNullConventions()
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

        public class PersonConfigurationWithNullConventions : ResourceTypeBuilder<Person>
        {
            public PersonConfigurationWithNullConventions()
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

        public class PersonConfigurationIgnoreFirstAndLastNamesWithNullConventions : ResourceTypeBuilder<Person>
        {
            public PersonConfigurationIgnoreFirstAndLastNamesWithNullConventions()
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

                // Ignores
                this.Attribute(x => x.FirstName)
                    .Ignore();

                this.Attribute(x => x.LastName)
                    .Ignore();

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

        public class PhoneNumberConfigurationWithNullConventions : ComplexTypeBuilder<PhoneNumber>
        {
            public PhoneNumberConfigurationWithNullConventions()
            {
                // Attributes
                this.Attribute(x => x.AreaCode)
                    .SetApiPropertyName("area-code");

                this.Attribute(x => x.Number)
                    .SetApiPropertyName("number");
            }
        }

        public class PosSystemConfigurationWithNullConventions : ResourceTypeBuilder<PosSystem>
        {
            public PosSystemConfigurationWithNullConventions()
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

                this.Attribute(x => x.EndOfLifeDate)
                    .SetApiPropertyName("end-of-life-date");

                // Relationships
                this.ToManyRelationship<StoreConfiguration>(ClrSampleData.PosSystemToStoreConfigurationsRel)
                    .SetApiRelPathSegment(ClrSampleData.PosSystemToStoreConfigurationsRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class ProductConfigurationWithNullConventions : ResourceTypeBuilder<Product>
        {
            public ProductConfigurationWithNullConventions()
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

        public class StoreConfigurationConfigurationWithNullConventions : ResourceTypeBuilder<StoreConfiguration>
        {
            public StoreConfigurationConfigurationWithNullConventions()
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

        public class StoreConfigurationWithNullConventions : ResourceTypeBuilder<Store>
        {
            public StoreConfigurationWithNullConventions()
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

        #region Configurations With Conventions
        public class ArticleConfigurationWithConventions : ResourceTypeBuilder<Article>
        {
            public ArticleConfigurationWithConventions()
            {
                // Relationships
                this.ToOneRelationship<Person>(ApiSampleData.ArticleToAuthorRel);

                this.ToManyRelationship<Comment>(ApiSampleData.ArticleToCommentsRel);

                // Links
                this.Link(Keywords.Canonical);
                this.Link(Keywords.Self);
            }
        }

        public class BlogConfigurationWithConventions : ResourceTypeBuilder<Blog>
        {
            public BlogConfigurationWithConventions()
            {
                // Relationships
                this.ToManyRelationship<Article>(ApiSampleData.BlogToArticlesRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class CommentConfigurationWithConventions : ResourceTypeBuilder<Comment>
        {
            public CommentConfigurationWithConventions()
            {
                // Relationships
                this.ToOneRelationship<Person>(ApiSampleData.CommentToAuthorRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class HomeConfigurationWithConventions : ResourceTypeBuilder<Home>
        {
            public HomeConfigurationWithConventions()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ClrSampleData.HomeCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity()
                    .SetApiType(ClrSampleData.HomeType);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class SearchConfigurationWithConventions : ResourceTypeBuilder<Search>
        {
            public SearchConfigurationWithConventions()
            {
                // Hypermedia
                this.Hypermedia()
                    .SetApiCollectionPathSegment(ClrSampleData.SearchCollectionPathSegment);

                // ResourceIdentity
                this.ResourceIdentity()
                    .SetApiType(ClrSampleData.SearchType);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class MailingAddressConfigurationWithConventions : ComplexTypeBuilder<MailingAddress>
        {
            public MailingAddressConfigurationWithConventions()
            { }
        }

        public class MailingAddressConfigurationIgnoreAllButAddressWithConventions : ComplexTypeBuilder<MailingAddress>
        {
            public MailingAddressConfigurationIgnoreAllButAddressWithConventions()
            {
                // Ignores
                this.Attribute(x => x.City)
                    .Ignore();

                this.Attribute(x => x.State)
                    .Ignore();

                this.Attribute(x => x.ZipCode)
                    .Ignore();
            }
        }

        public class OrderConfigurationWithConventions : ResourceTypeBuilder<Order>
        {
            public OrderConfigurationWithConventions()
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

        public class OrderItemConfigurationWithConventions : ResourceTypeBuilder<OrderItem>
        {
            public OrderItemConfigurationWithConventions()
            {
                // Relationships
                this.ToOneRelationship<Order>(ClrSampleData.OrderItemToOrderRel);

                this.ToOneRelationship<Product>(ClrSampleData.OrderItemToProductRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class PaymentConfigurationWithConventions : ResourceTypeBuilder<Payment>
        {
            public PaymentConfigurationWithConventions()
            {
                // Relationships
                this.ToOneRelationship<Order>(ClrSampleData.PaymentToOrderRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class PersonConfigurationWithConventions : ResourceTypeBuilder<Person>
        {
            public PersonConfigurationWithConventions()
            {
                // Relationships
                this.ToManyRelationship<Comment>(ApiSampleData.PersonToCommentsRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class PersonConfigurationIgnoreFirstAndLastNamesWithConventions : ResourceTypeBuilder<Person>
        {
            public PersonConfigurationIgnoreFirstAndLastNamesWithConventions()
            {
                // Ignores
                this.Attribute(x => x.FirstName)
                    .Ignore();

                this.Attribute(x => x.LastName)
                    .Ignore();

                // Relationships
                this.ToManyRelationship<Comment>(ApiSampleData.PersonToCommentsRel);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class PhoneNumberConfigurationWithConventions : ComplexTypeBuilder<PhoneNumber>
        {
            public PhoneNumberConfigurationWithConventions()
            { }
        }

        public class PosSystemConfigurationWithConventions : ResourceTypeBuilder<PosSystem>
        {
            public PosSystemConfigurationWithConventions()
            {
                // Relationships
                this.ToManyRelationship<StoreConfiguration>(ClrSampleData.PosSystemToStoreConfigurationsRel)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class ProductConfigurationWithConventions : ResourceTypeBuilder<Product>
        {
            public ProductConfigurationWithConventions()
            {
                // Links
                this.Link(Keywords.Self);
            }
        }

        public class StoreConfigurationConfigurationWithConventions : ResourceTypeBuilder<StoreConfiguration>
        {
            public StoreConfigurationConfigurationWithConventions()
            {
                // Relationships
                this.ToOneRelationship<PosSystem>(ClrSampleData.StoreToStoreConfigurationToPosSystemRel)
                    .SetApiRelPathSegment(ClrSampleData.StoreToStoreConfigurationToPosSystemRelPathSegment);

                // Links
                this.Link(Keywords.Self);
            }
        }

        public class StoreConfigurationWithConventions : ResourceTypeBuilder<Store>
        {
            public StoreConfigurationWithConventions()
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