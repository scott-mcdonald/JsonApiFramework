// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using Humanizer;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Internal;
using JsonApiFramework.TestData.ApiResources;

namespace JsonApiFramework.TestData.ClrResources
{
    public static class ClrSampleData
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ClrType
        public static readonly Type ArticleClrType = typeof(Article);
        public static readonly Type BlogClrType = typeof(Blog);
        public static readonly Type CommentClrType = typeof(Comment);
        public static readonly Type OrderClrType = typeof(Order);
        public static readonly Type OrderItemClrType = typeof(OrderItem);
        public static readonly Type PaymentClrType = typeof(Payment);
        public static readonly Type PersonClrType = typeof(Person);
        public static readonly Type PosSystemClrType = typeof(PosSystem);
        public static readonly Type ProductClrType = typeof(Product);
        public static readonly Type StoreClrType = typeof(Store);
        public static readonly Type StoreConfigurationClrType = typeof(StoreConfiguration);
        #endregion

        #region Collection Path Segments
        public const string OrderCollectionPathSegment = "orders";
        public const string OrderItemCollectionPathSegment = "order-items";
        public const string PaymentCollectionPathSegment = "payments";
        public const string PosSystemCollectionPathSegment = "pos-systems";
        public const string ProductCollectionPathSegment = "products";
        public const string StoreCollectionPathSegment = "stores";
        public const string StoreConfigurationCollectionPathSegment = "store-configurations";
        #endregion

        #region Types
        public const string OrderType = OrderCollectionPathSegment;
        public const string OrderItemType = OrderItemCollectionPathSegment;
        public const string PaymentType = PaymentCollectionPathSegment;
        public const string PosSystemType = PosSystemCollectionPathSegment;
        public const string ProductType = ProductCollectionPathSegment;
        public const string StoreType = StoreCollectionPathSegment;
        public const string StoreConfigurationType = StoreConfigurationCollectionPathSegment;
        #endregion

        #region Hypermedia
        public static readonly IHypermediaInfo ArticleHypermedia = new HypermediaInfo(ApiSampleData.ArticleCollectionPathSegment);
        public static readonly IHypermediaInfo BlogHypermedia = new HypermediaInfo(ApiSampleData.BlogCollectionPathSegment);
        public static readonly IHypermediaInfo CommentHypermedia = new HypermediaInfo(ApiSampleData.CommentCollectionPathSegment);
        public static readonly IHypermediaInfo OrderHypermedia = new HypermediaInfo(OrderCollectionPathSegment);
        public static readonly IHypermediaInfo OrderItemHypermedia = new HypermediaInfo(OrderItemCollectionPathSegment);
        public static readonly IHypermediaInfo PaymentHypermedia = new HypermediaInfo(PaymentCollectionPathSegment);
        public static readonly IHypermediaInfo PersonHypermedia = new HypermediaInfo(ApiSampleData.PersonCollectionPathSegment);
        public static readonly IHypermediaInfo PosSystemHypermedia = new HypermediaInfo(PosSystemCollectionPathSegment);
        public static readonly IHypermediaInfo ProductHypermedia = new HypermediaInfo(ProductCollectionPathSegment);
        public static readonly IHypermediaInfo StoreHypermedia = new HypermediaInfo(StoreCollectionPathSegment);
        public static readonly IHypermediaInfo StoreConfigurationHypermedia = new HypermediaInfo(StoreConfigurationCollectionPathSegment);
        #endregion

        #region ResourceIdentity
        public static readonly IPropertyInfo ArticleId = new PropertyInfo(StaticReflection.GetMemberName<Article>(x => x.Id), typeof(string));
        public static readonly IResourceIdentityInfo ArticleResourceIdentity = new ResourceIdentityInfo(ApiSampleData.ArticleType, ArticleId);

        public static readonly IPropertyInfo BlogId = new PropertyInfo(StaticReflection.GetMemberName<Blog>(x => x.Id), typeof(string));
        public static readonly IResourceIdentityInfo BlogResourceIdentity = new ResourceIdentityInfo(ApiSampleData.BlogType, BlogId);

        public static readonly IPropertyInfo CommentId = new PropertyInfo(StaticReflection.GetMemberName<Comment>(x => x.Id), typeof(string));
        public static readonly IResourceIdentityInfo CommentResourceIdentity = new ResourceIdentityInfo(ApiSampleData.CommentType, CommentId);

        public static readonly IPropertyInfo OrderId = new PropertyInfo(StaticReflection.GetMemberName<Order>(x => x.OrderId), typeof(long));
        public static readonly IResourceIdentityInfo OrderResourceIdentity = new ResourceIdentityInfo(OrderType, OrderId);

        public static readonly IPropertyInfo OrderItemId = new PropertyInfo(StaticReflection.GetMemberName<OrderItem>(x => x.OrderItemId), typeof(long));
        public static readonly IResourceIdentityInfo OrderItemResourceIdentity = new ResourceIdentityInfo(OrderItemType, OrderItemId);

        public static readonly IPropertyInfo PaymentId = new PropertyInfo(StaticReflection.GetMemberName<Payment>(x => x.PaymentId), typeof(long));
        public static readonly IResourceIdentityInfo PaymentResourceIdentity = new ResourceIdentityInfo(PaymentType, PaymentId);

        public static readonly IPropertyInfo PersonId = new PropertyInfo(StaticReflection.GetMemberName<Person>(x => x.Id), typeof(string));
        public static readonly IResourceIdentityInfo PersonResourceIdentity = new ResourceIdentityInfo(ApiSampleData.PersonType, PersonId);

        public static readonly IPropertyInfo PosSystemId = new PropertyInfo(StaticReflection.GetMemberName<PosSystem>(x => x.PosSystemId), typeof(string));
        public static readonly IResourceIdentityInfo PosSystemResourceIdentity = new ResourceIdentityInfo(PosSystemType, PosSystemId);

        public static readonly IPropertyInfo ProductId = new PropertyInfo(StaticReflection.GetMemberName<Product>(x => x.ProductId), typeof(long));
        public static readonly IResourceIdentityInfo ProductResourceIdentity = new ResourceIdentityInfo(ProductType, ProductId);

        public static readonly IPropertyInfo StoreId = new PropertyInfo(StaticReflection.GetMemberName<Store>(x => x.StoreId), typeof(long));
        public static readonly IResourceIdentityInfo StoreResourceIdentity = new ResourceIdentityInfo(StoreType, StoreId);

        public static readonly IPropertyInfo StoreConfigurationId = new PropertyInfo(StaticReflection.GetMemberName<StoreConfiguration>(x => x.StoreConfigurationId), typeof(string));
        public static readonly IResourceIdentityInfo StoreConfigurationResourceIdentity = new ResourceIdentityInfo(StoreConfigurationType, StoreConfigurationId);
        #endregion

        #region Attributes
        // Article Attributes
        public static readonly IAttributeInfo ArticleTitleAttribute = new AttributeInfo(StaticReflection.GetMemberName<Article>(x => x.Title), typeof(string), ApiSampleData.ArticleTitlePropertyName);
        public static readonly IAttributeInfo[] ArticleAttributesCollection =
            {
                ArticleTitleAttribute
            };
        public static readonly IAttributesInfo ArticleAttributes = new AttributesInfo(ArticleAttributesCollection);

        // Blog Attributes
        public static readonly IAttributeInfo BlogNameAttribute = new AttributeInfo(StaticReflection.GetMemberName<Blog>(x => x.Name), typeof(string), ApiSampleData.BlogNamePropertyName);
        public static readonly IAttributeInfo[] BlogAttributesCollection =
            {
                BlogNameAttribute
            };
        public static readonly IAttributesInfo BlogAttributes = new AttributesInfo(BlogAttributesCollection);

        // Comment Attributes
        public static readonly IAttributeInfo CommentBodyAttribute = new AttributeInfo(StaticReflection.GetMemberName<Comment>(x => x.Body), typeof(string), ApiSampleData.CommentBodyPropertyName);
        public static readonly IAttributeInfo[] CommentAttributesCollection =
            {
                CommentBodyAttribute
            };
        public static readonly IAttributesInfo CommentAttributes = new AttributesInfo(CommentAttributesCollection);

        // Order Attributes
        public static readonly IAttributeInfo OrderTotalPriceAttribute = new AttributeInfo(StaticReflection.GetMemberName<Order>(x => x.TotalPrice), typeof(decimal), StaticReflection.GetMemberName<Order>(x => x.TotalPrice).Underscore().Dasherize());
        public static readonly IAttributeInfo[] OrderAttributesCollection =
            {
                OrderTotalPriceAttribute
            };
        public static readonly IAttributesInfo OrderAttributes = new AttributesInfo(OrderAttributesCollection);

        // OrderItem Attributes
        public static readonly IAttributeInfo OrderItemProductNameAttribute = new AttributeInfo(StaticReflection.GetMemberName<OrderItem>(x => x.ProductName), typeof(string), StaticReflection.GetMemberName<OrderItem>(x => x.ProductName).Underscore().Dasherize());
        public static readonly IAttributeInfo OrderItemQuantityAttribute = new AttributeInfo(StaticReflection.GetMemberName<OrderItem>(x => x.Quantity), typeof(decimal), StaticReflection.GetMemberName<OrderItem>(x => x.Quantity).Underscore().Dasherize());
        public static readonly IAttributeInfo OrderItemUnitPriceAttribute = new AttributeInfo(StaticReflection.GetMemberName<OrderItem>(x => x.UnitPrice), typeof(decimal), StaticReflection.GetMemberName<OrderItem>(x => x.UnitPrice).Underscore().Dasherize());
        public static readonly IAttributeInfo[] OrderItemAttributesCollection =
            {
                OrderItemProductNameAttribute,
                OrderItemQuantityAttribute,
                OrderItemUnitPriceAttribute
            };
        public static readonly IAttributesInfo OrderItemAttributes = new AttributesInfo(OrderItemAttributesCollection);

        // Payment Attributes
        public static readonly IAttributeInfo PaymentAmountAttribute = new AttributeInfo(StaticReflection.GetMemberName<Payment>(x => x.Amount), typeof(decimal), StaticReflection.GetMemberName<Payment>(x => x.Amount).Underscore().Dasherize());
        public static readonly IAttributeInfo[] PaymentAttributesCollection =
            {
                PaymentAmountAttribute
            };
        public static readonly IAttributesInfo PaymentAttributes = new AttributesInfo(PaymentAttributesCollection);

        // Person Attributes
        public static readonly IAttributeInfo PersonFirstNameAttribute = new AttributeInfo(StaticReflection.GetMemberName<Person>(x => x.FirstName), typeof(string), ApiSampleData.PersonFirstNamePropertyName);
        public static readonly IAttributeInfo PersonLastNameAttribute = new AttributeInfo(StaticReflection.GetMemberName<Person>(x => x.LastName), typeof(string), ApiSampleData.PersonLastNamePropertyName);
        public static readonly IAttributeInfo PersonTwitterAttribute = new AttributeInfo(StaticReflection.GetMemberName<Person>(x => x.Twitter), typeof(string), ApiSampleData.PersonTwitterPropertyName);
        public static readonly IAttributeInfo[] PersonAttributesCollection =
            {
                PersonFirstNameAttribute,
                PersonLastNameAttribute,
                PersonTwitterAttribute
            };
        public static readonly IAttributesInfo PersonAttributes = new AttributesInfo(PersonAttributesCollection);

        // PosSystem Attributes
        public static readonly IAttributeInfo PosSystemNameAttribute = new AttributeInfo(StaticReflection.GetMemberName<PosSystem>(x => x.PosSystemName), typeof(string), StaticReflection.GetMemberName<PosSystem>(x => x.PosSystemName).Underscore().Dasherize());
        public static readonly IAttributeInfo[] PosSystemAttributesCollection =
            {
                PosSystemNameAttribute
            };
        public static readonly IAttributesInfo PosSystemAttributes = new AttributesInfo(PosSystemAttributesCollection);

        // Product Attributes
        public static readonly IAttributeInfo ProductNameAttribute = new AttributeInfo(StaticReflection.GetMemberName<Product>(x => x.Name), typeof(string), StaticReflection.GetMemberName<Product>(x => x.Name).Underscore().Dasherize());
        public static readonly IAttributeInfo ProductUnitPriceAttribute = new AttributeInfo(StaticReflection.GetMemberName<Product>(x => x.UnitPrice), typeof(decimal), StaticReflection.GetMemberName<Product>(x => x.UnitPrice).Underscore().Dasherize());
        public static readonly IAttributeInfo[] ProductAttributesCollection =
            {
                ProductNameAttribute,
                ProductUnitPriceAttribute
            };
        public static readonly IAttributesInfo ProductAttributes = new AttributesInfo(ProductAttributesCollection);

        // Store Attributes
        public static readonly IAttributeInfo StoreNameAttribute = new AttributeInfo(StaticReflection.GetMemberName<Store>(x => x.StoreName), typeof(string), StaticReflection.GetMemberName<Store>(x => x.StoreName).Underscore().Dasherize());
        public static readonly IAttributeInfo StoreAddressAttribute = new AttributeInfo(StaticReflection.GetMemberName<Store>(x => x.Address), typeof(string), StaticReflection.GetMemberName<Store>(x => x.Address).Underscore().Dasherize());
        public static readonly IAttributeInfo StoreCityAttribute = new AttributeInfo(StaticReflection.GetMemberName<Store>(x => x.City), typeof(string), StaticReflection.GetMemberName<Store>(x => x.City).Underscore().Dasherize());
        public static readonly IAttributeInfo StoreStateAttribute = new AttributeInfo(StaticReflection.GetMemberName<Store>(x => x.State), typeof(string), StaticReflection.GetMemberName<Store>(x => x.State).Underscore().Dasherize());
        public static readonly IAttributeInfo StoreZipCodeAttribute = new AttributeInfo(StaticReflection.GetMemberName<Store>(x => x.ZipCode), typeof(string), StaticReflection.GetMemberName<Store>(x => x.ZipCode).Underscore().Dasherize());
        public static readonly IAttributeInfo[] StoreAttributesCollection =
            {
                StoreNameAttribute,
                StoreAddressAttribute,
                StoreCityAttribute,
                StoreStateAttribute,
                StoreZipCodeAttribute
            };
        public static readonly IAttributesInfo StoreAttributes = new AttributesInfo(StoreAttributesCollection);

        // StoreConfiguration Attributes
        public static readonly IAttributeInfo StoreConfigurationIsLiveAttribute = new AttributeInfo(StaticReflection.GetMemberName<StoreConfiguration>(x => x.IsLive), typeof(bool), StaticReflection.GetMemberName<StoreConfiguration>(x => x.IsLive).Underscore().Dasherize());
        public static readonly IAttributeInfo StoreConfigurationMailingAddressAttribute = new AttributeInfo(StaticReflection.GetMemberName<StoreConfiguration>(x => x.MailingAddress), typeof(MailingAddress), StaticReflection.GetMemberName<StoreConfiguration>(x => x.MailingAddress).Underscore().Dasherize());
        public static readonly IAttributeInfo StoreConfigurationPhoneNumbersAttribute = new AttributeInfo(StaticReflection.GetMemberName<StoreConfiguration>(x => x.PhoneNumbers), typeof(List<PhoneNumber>), StaticReflection.GetMemberName<StoreConfiguration>(x => x.PhoneNumbers).Underscore().Dasherize());
        public static readonly IAttributeInfo[] StoreConfigurationAttributesCollection =
            {
                StoreConfigurationIsLiveAttribute,
                StoreConfigurationMailingAddressAttribute,
                StoreConfigurationPhoneNumbersAttribute
            };
        public static readonly IAttributesInfo StoreConfigurationAttributes = new AttributesInfo(StoreConfigurationAttributesCollection);
        #endregion

        #region Relationship Names
        public const string OrderToOrderItemsRel = "line-items";
        public const string OrderToPaymentsRel = "payments";
        public const string OrderToStoreRel = "store";

        public const string OrderItemToOrderRel = "order";
        public const string OrderItemToProductRel = "product";

        public const string PaymentToOrderRel = "order";

        public const string PosSystemToStoreConfigurationsRel = "store-configurations";

        public const string StoreToStoreConfigurationRel = "store-configuration";
        public const string StoreToStoreConfigurationToPosSystemRel = "pos-system";
        #endregion

        #region Relationship Path Segments
        public const string OrderToOrderItemsRelPathSegment = OrderToOrderItemsRel;
        public const string OrderToPaymentsRelPathSegment = OrderToPaymentsRel;
        public const string OrderToStoreRelPathSegment = OrderToStoreRel;

        public const string OrderItemToOrderRelPathSegment = OrderItemToOrderRel;
        public const string OrderItemToProductRelPathSegment = OrderItemToProductRel;

        public const string PaymentToOrderRelPathSegment = PaymentToOrderRel;

        public const string PosSystemToStoreConfigurationsRelPathSegment = PosSystemToStoreConfigurationsRel;

        public const string StoreToStoreConfigurationRelPathSegment = "configuration";
        public const string StoreToStoreConfigurationToPosSystemRelPathSegment = "pos";
        #endregion

        #region Relationships
        // Article Relationships
        public static readonly IRelationshipInfo ArticleToAuthorRelationship = new RelationshipInfo(ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelPathSegment, typeof(Person), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo ArticleToCommentsRelationship = new RelationshipInfo(ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelPathSegment, typeof(Comment), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] ArticleRelationshipsCollection = 
            {
                ArticleToAuthorRelationship,
                ArticleToCommentsRelationship
            };
        public static readonly IRelationshipsInfo ArticleRelationships = new RelationshipsInfo(StaticReflection.GetMemberName<Article>(x => x.Relationships), ArticleRelationshipsCollection);

        // Blog Relationships
        public static readonly IRelationshipInfo BlogToArticlesRelationship = new RelationshipInfo(ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesRelPathSegment, typeof(Article), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] BlogRelationshipsCollection = 
            {
                BlogToArticlesRelationship
            };
        public static readonly IRelationshipsInfo BlogRelationships = new RelationshipsInfo(StaticReflection.GetMemberName<Blog>(x => x.Relationships), BlogRelationshipsCollection);

        // Comment Relationships
        public static readonly IRelationshipInfo CommentToAuthorRelationship = new RelationshipInfo(ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelPathSegment, typeof(Person), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] CommentRelationshipsCollection = 
            {
                CommentToAuthorRelationship
            };
        public static readonly IRelationshipsInfo CommentRelationships = new RelationshipsInfo(StaticReflection.GetMemberName<Comment>(x => x.Relationships), CommentRelationshipsCollection);

        // Order Relationships
        public static readonly IRelationshipInfo OrderToOrderItemsRelationship = new RelationshipInfo(OrderToOrderItemsRel, OrderToOrderItemsRelPathSegment, typeof(OrderItem), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);
        public static readonly IRelationshipInfo OrderToPaymentsRelationship = new RelationshipInfo(OrderToPaymentsRel, OrderToPaymentsRelPathSegment, typeof(Payment), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo OrderToStoreRelationship = new RelationshipInfo(OrderToStoreRel, OrderToStoreRelPathSegment, typeof(Store), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] OrderRelationshipsCollection = 
            {
                OrderToOrderItemsRelationship,
                OrderToPaymentsRelationship,
                OrderToStoreRelationship
            };
        public static readonly IRelationshipsInfo OrderRelationships = new RelationshipsInfo(OrderRelationshipsCollection);

        // OrderItem Relationships
        public static readonly IRelationshipInfo OrderItemToOrderRelationship = new RelationshipInfo(OrderItemToOrderRel, OrderItemToOrderRelPathSegment, typeof(Order), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo OrderItemToProductRelationship = new RelationshipInfo(OrderItemToProductRel, OrderItemToProductRelPathSegment, typeof(Product), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] OrderItemRelationshipsCollection = 
            {
                OrderItemToOrderRelationship,
                OrderItemToProductRelationship
            };
        public static readonly IRelationshipsInfo OrderItemRelationships = new RelationshipsInfo(OrderItemRelationshipsCollection);

        // Payment Relationships
        public static readonly IRelationshipInfo PaymentToOrderRelationship = new RelationshipInfo(PaymentToOrderRel, PaymentToOrderRelPathSegment, typeof(Order), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] PaymentRelationshipsCollection = 
            {
                PaymentToOrderRelationship
            };
        public static readonly IRelationshipsInfo PaymentRelationships = new RelationshipsInfo(PaymentRelationshipsCollection);

        // Person Relationships
        public static readonly IRelationshipInfo PersonToCommentsRelationship = new RelationshipInfo(ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelPathSegment, typeof(Comment), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] PersonRelationshipsCollection = 
            {
                PersonToCommentsRelationship
            };
        public static readonly IRelationshipsInfo PersonRelationships = new RelationshipsInfo(StaticReflection.GetMemberName<Person>(x => x.Relationships), PersonRelationshipsCollection);

        // PosSystem Relationships
        public static readonly IRelationshipInfo PosSystemToStoreConfigurationsRelationship = new RelationshipInfo(PosSystemToStoreConfigurationsRel, PosSystemToStoreConfigurationsRelPathSegment, typeof(StoreConfiguration), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);

        public static readonly IRelationshipInfo[] PosSystemRelationshipsCollection = 
            {
                PosSystemToStoreConfigurationsRelationship
            };
        public static readonly IRelationshipsInfo PosSystemRelationships = new RelationshipsInfo(PosSystemRelationshipsCollection);

        // Product Relationships
        public static readonly IRelationshipInfo[] ProductRelationshipsCollection = 
            {
            };
        public static readonly IRelationshipsInfo ProductRelationships = new RelationshipsInfo(ProductRelationshipsCollection);

        // Store Relationships
        public static readonly IRelationshipInfo StoreToStoreConfigurationRelationship = new RelationshipInfo(StoreToStoreConfigurationRel, StoreToStoreConfigurationRelPathSegment, typeof(StoreConfiguration), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);
        public static readonly IRelationshipInfo[] StoreRelationshipsCollection = 
            {
                StoreToStoreConfigurationRelationship
            };
        public static readonly IRelationshipsInfo StoreRelationships = new RelationshipsInfo(StoreRelationshipsCollection);

        // StoreConfiguration Relationships
        public static readonly IRelationshipInfo StoreToStoreConfigurationToPosSystemRelationship = new RelationshipInfo(StoreToStoreConfigurationToPosSystemRel, StoreToStoreConfigurationToPosSystemRelPathSegment, typeof(PosSystem), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] StoreConfigurationRelationshipsCollection = 
            {
                StoreToStoreConfigurationToPosSystemRelationship
            };
        public static readonly IRelationshipsInfo StoreConfigurationRelationships = new RelationshipsInfo(StoreConfigurationRelationshipsCollection);
        #endregion

        #region Links
        // Article Links
        public static readonly ILinkInfo ArticleCanonicalLink = new LinkInfo(Keywords.Canonical);
        public static readonly ILinkInfo ArticleSelfLink = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] ArticleLinksCollection = 
            {
                ArticleCanonicalLink,
                ArticleSelfLink
            };
        public static readonly ILinksInfo ArticleLinks = new LinksInfo(StaticReflection.GetMemberName<Article>(x => x.Links), ArticleLinksCollection);

        // Blog Links
        public static readonly ILinkInfo BlogSelfLink = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] BlogLinksCollection = 
            {
                BlogSelfLink
            };
        public static readonly ILinksInfo BlogLinks = new LinksInfo(StaticReflection.GetMemberName<Blog>(x => x.Links), BlogLinksCollection);

        // Comment Links
        public static readonly ILinkInfo CommentSelfLink = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] CommentLinksCollection = 
            {
                CommentSelfLink
            };
        public static readonly ILinksInfo CommentLinks = new LinksInfo(StaticReflection.GetMemberName<Comment>(x => x.Links), CommentLinksCollection);

        // Order Links
        public static readonly ILinkInfo OrderSelfLink = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] OrderLinksCollection = 
            {
                OrderSelfLink
            };
        public static readonly ILinksInfo OrderLinks = new LinksInfo(OrderLinksCollection);

        // OrderItem Links
        public static readonly ILinkInfo OrderItemSelfLink = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] OrderItemLinksCollection = 
            {
                OrderItemSelfLink
            };
        public static readonly ILinksInfo OrderItemLinks = new LinksInfo(OrderItemLinksCollection);

        // Payment Links
        public static readonly ILinkInfo PaymentSelfLink = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] PaymentLinksCollection = 
            {
                PaymentSelfLink
            };
        public static readonly ILinksInfo PaymentLinks = new LinksInfo(PaymentLinksCollection);

        // Person Links
        public static readonly ILinkInfo PersonSelfLink = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] PersonLinksCollection = 
            {
                PersonSelfLink
            };
        public static readonly ILinksInfo PersonLinks = new LinksInfo(StaticReflection.GetMemberName<Person>(x => x.Links), PersonLinksCollection);

        // PosSystem Links
        public static readonly ILinkInfo PosSystemSelfLink = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] PosSystemLinksCollection = 
            {
                PosSystemSelfLink
            };
        public static readonly ILinksInfo PosSystemLinks = new LinksInfo(PosSystemLinksCollection);

        // Product Links
        public static readonly ILinkInfo ProductSelfLink = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] ProductLinksCollection = 
            {
                ProductSelfLink
            };
        public static readonly ILinksInfo ProductLinks = new LinksInfo(ProductLinksCollection);

        // Store Links
        public static readonly ILinkInfo StoreSelfLink = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] StoreLinksCollection = 
            {
                StoreSelfLink
            };
        public static readonly ILinksInfo StoreLinks = new LinksInfo(StoreLinksCollection);

        // StoreConfiguration Links
        public static readonly ILinkInfo StoreConfigurationSelfLink = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] StoreConfigurationLinksCollection = 
            {
                StoreConfigurationSelfLink
            };
        public static readonly ILinksInfo StoreConfigurationLinks = new LinksInfo(StoreConfigurationLinksCollection);
        #endregion

        #region Meta
        public static readonly IMetaInfo ArticleMeta = new MetaInfo(StaticReflection.GetMemberName<Article>(x => x.Meta));
        public static readonly IMetaInfo BlogMeta = new MetaInfo(StaticReflection.GetMemberName<Blog>(x => x.Meta));
        public static readonly IMetaInfo CommentMeta = new MetaInfo(StaticReflection.GetMemberName<Comment>(x => x.Meta));
        public static readonly IMetaInfo PersonMeta = new MetaInfo(StaticReflection.GetMemberName<Person>(x => x.Meta));
        #endregion

        #region ResourceTypes
        public static readonly IResourceType ArticleResourceType = new ResourceType(ArticleClrType, ArticleHypermedia, ArticleResourceIdentity, ArticleAttributes, ArticleRelationships, ArticleLinks, ArticleMeta);
        public static readonly IResourceType BlogResourceType = new ResourceType(BlogClrType, BlogHypermedia, BlogResourceIdentity, BlogAttributes, BlogRelationships, BlogLinks, BlogMeta);
        public static readonly IResourceType CommentResourceType = new ResourceType(CommentClrType, CommentHypermedia, CommentResourceIdentity, CommentAttributes, CommentRelationships, CommentLinks, CommentMeta);
        public static readonly IResourceType OrderResourceType = new ResourceType(OrderClrType, OrderHypermedia, OrderResourceIdentity, OrderAttributes, OrderRelationships, OrderLinks, null);
        public static readonly IResourceType OrderItemResourceType = new ResourceType(OrderItemClrType, OrderItemHypermedia, OrderItemResourceIdentity, OrderItemAttributes, OrderItemRelationships, OrderItemLinks, null);
        public static readonly IResourceType PaymentResourceType = new ResourceType(PaymentClrType, PaymentHypermedia, PaymentResourceIdentity, PaymentAttributes, PaymentRelationships, PaymentLinks, null);
        public static readonly IResourceType PersonResourceType = new ResourceType(PersonClrType, PersonHypermedia, PersonResourceIdentity, PersonAttributes, PersonRelationships, PersonLinks, PersonMeta);
        public static readonly IResourceType PosSystemResourceType = new ResourceType(PosSystemClrType, PosSystemHypermedia, PosSystemResourceIdentity, PosSystemAttributes, PosSystemRelationships, PosSystemLinks, null);
        public static readonly IResourceType ProductResourceType = new ResourceType(ProductClrType, ProductHypermedia, ProductResourceIdentity, ProductAttributes, ProductRelationships, ProductLinks, null);
        public static readonly IResourceType StoreResourceType = new ResourceType(StoreClrType, StoreHypermedia, StoreResourceIdentity, StoreAttributes, StoreRelationships, StoreLinks, null);
        public static readonly IResourceType StoreConfigurationResourceType = new ResourceType(StoreConfigurationClrType, StoreConfigurationHypermedia, StoreConfigurationResourceIdentity, StoreConfigurationAttributes, StoreConfigurationRelationships, StoreConfigurationLinks, null);
        #endregion

        #region ServiceModels
        public static readonly IServiceModel ServiceModelEmpty = JsonApiFramework.ServiceModel.Internal.ServiceModel.Empty;

        public static readonly IServiceModel ServiceModelWithOnlyArticleResourceType =
            new JsonApiFramework.ServiceModel.Internal.ServiceModel(new List<IResourceType>
                {
                    ClrSampleData.ArticleResourceType
                });

        /// <summary>
        /// Blog and related resources service model.
        /// </summary>
        /// <remarks>
        /// This service model has CLR resources that have Relationships, Links, and Meta CLR properties defined.
        /// </remarks>
        public static readonly IServiceModel ServiceModelWithBlogResourceTypes =
            new JsonApiFramework.ServiceModel.Internal.ServiceModel(new List<IResourceType>
                {
                    ClrSampleData.ArticleResourceType,
                    ClrSampleData.BlogResourceType,
                    ClrSampleData.CommentResourceType,
                    ClrSampleData.PersonResourceType
                });

        /// <summary>
        /// Order and related resources service model.
        /// </summary>
        /// <remarks>
        /// This service model has CLR resources that do not have Relationships, Links, and Meta CLR properties defined.
        /// </remarks>
        public static readonly IServiceModel ServiceModelWithOrderResourceTypes =
            new JsonApiFramework.ServiceModel.Internal.ServiceModel(new List<IResourceType>
                {
                    ClrSampleData.OrderResourceType,
                    ClrSampleData.OrderItemResourceType,
                    ClrSampleData.PaymentResourceType,
                    ClrSampleData.PosSystemResourceType,
                    ClrSampleData.ProductResourceType,
                    ClrSampleData.StoreResourceType,
                    ClrSampleData.StoreConfigurationResourceType
                });
        #endregion
    }
}