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
using JsonApiFramework.TestData.ClrResources.ComplexTypes;

namespace JsonApiFramework.TestData.ClrResources
{
    public static class ClrSampleData
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Api Collection Path Segments
        public const string DrawingCollectionPathSegment = "drawings";
        public const string OrderCollectionPathSegment = "orders";
        public const string OrderItemCollectionPathSegment = "order-items";
        public const string PaymentCollectionPathSegment = "payments";
        public const string PosSystemCollectionPathSegment = "pos-systems";
        public const string ProductCollectionPathSegment = "products";
        public const string StoreCollectionPathSegment = "stores";
        public const string StoreConfigurationCollectionPathSegment = "store-configurations";
        #endregion

        #region Api Types
        public const string DrawingType = DrawingCollectionPathSegment;
        public const string OrderType = OrderCollectionPathSegment;
        public const string OrderItemType = OrderItemCollectionPathSegment;
        public const string PaymentType = PaymentCollectionPathSegment;
        public const string PosSystemType = PosSystemCollectionPathSegment;
        public const string ProductType = ProductCollectionPathSegment;
        public const string StoreType = StoreCollectionPathSegment;
        public const string StoreConfigurationType = StoreConfigurationCollectionPathSegment;
        #endregion

        #region Clr Types

        // Resource Types
        public static readonly Type ArticleClrType = typeof(Article);
        public static readonly Type BlogClrType = typeof(Blog);
        public static readonly Type CommentClrType = typeof(Comment);
        public static readonly Type DrawingClrType = typeof(Drawing);
        public static readonly Type OrderClrType = typeof(Order);
        public static readonly Type OrderItemClrType = typeof(OrderItem);
        public static readonly Type PaymentClrType = typeof(Payment);
        public static readonly Type PersonClrType = typeof(Person);
        public static readonly Type PosSystemClrType = typeof(PosSystem);
        public static readonly Type ProductClrType = typeof(Product);
        public static readonly Type StoreClrType = typeof(Store);
        public static readonly Type StoreConfigurationClrType = typeof(StoreConfiguration);

        // Complex Types
        public static readonly Type CustomDataClrType = typeof(CustomData);
        public static readonly Type CustomPropertyClrType = typeof(CustomProperty);
        public static readonly Type LineClrType = typeof(Line);
        public static readonly Type MailingAddressClrType = typeof(MailingAddress);
        public static readonly Type PhoneNumberClrType = typeof(PhoneNumber);
        public static readonly Type PointClrType = typeof(Point);
        public static readonly Type PolygonClrType = typeof(Polygon);

        #endregion

        #region Hypermedia
        public static readonly IHypermediaInfo ArticleHypermediaInfo = new HypermediaInfo(ApiSampleData.ArticleCollectionPathSegment);
        public static readonly IHypermediaInfo BlogHypermediaInfo = new HypermediaInfo(ApiSampleData.BlogCollectionPathSegment);
        public static readonly IHypermediaInfo CommentHypermediaInfo = new HypermediaInfo(ApiSampleData.CommentCollectionPathSegment);
        public static readonly IHypermediaInfo DrawingHypermediaInfo = new HypermediaInfo(DrawingCollectionPathSegment);
        public static readonly IHypermediaInfo OrderHypermediaInfo = new HypermediaInfo(OrderCollectionPathSegment);
        public static readonly IHypermediaInfo OrderItemHypermediaInfo = new HypermediaInfo(OrderItemCollectionPathSegment);
        public static readonly IHypermediaInfo PaymentHypermediaInfo = new HypermediaInfo(PaymentCollectionPathSegment);
        public static readonly IHypermediaInfo PersonHypermediaInfo = new HypermediaInfo(ApiSampleData.PersonCollectionPathSegment);
        public static readonly IHypermediaInfo PosSystemHypermediaInfo = new HypermediaInfo(PosSystemCollectionPathSegment);
        public static readonly IHypermediaInfo ProductHypermediaInfo = new HypermediaInfo(ProductCollectionPathSegment);
        public static readonly IHypermediaInfo StoreHypermediaInfo = new HypermediaInfo(StoreCollectionPathSegment);
        public static readonly IHypermediaInfo StoreConfigurationHypermediaInfo = new HypermediaInfo(StoreConfigurationCollectionPathSegment);
        #endregion

        #region Resource Identity
        public static readonly IPropertyInfo ArticleIdInfo = new PropertyInfo(typeof(Article), StaticReflection.GetMemberName<Article>(x => x.Id), typeof(string));
        public static readonly IResourceIdentityInfo ArticleResourceIdentityInfo = new ResourceIdentityInfo(ApiSampleData.ArticleType, ArticleIdInfo);

        public static readonly IPropertyInfo BlogIdInfo = new PropertyInfo(typeof(Blog), StaticReflection.GetMemberName<Blog>(x => x.Id), typeof(string));
        public static readonly IResourceIdentityInfo BlogResourceIdentityInfo = new ResourceIdentityInfo(ApiSampleData.BlogType, BlogIdInfo);

        public static readonly IPropertyInfo CommentIdInfo = new PropertyInfo(typeof(Comment), StaticReflection.GetMemberName<Comment>(x => x.Id), typeof(string));
        public static readonly IResourceIdentityInfo CommentResourceIdentityInfo = new ResourceIdentityInfo(ApiSampleData.CommentType, CommentIdInfo);

        public static readonly IPropertyInfo DrawingIdInfo = new PropertyInfo(typeof(Drawing), StaticReflection.GetMemberName<Drawing>(x => x.Id), typeof(long));
        public static readonly IResourceIdentityInfo DrawingResourceIdentityInfo = new ResourceIdentityInfo(DrawingType, DrawingIdInfo);

        public static readonly IPropertyInfo OrderIdInfo = new PropertyInfo(typeof(Order), StaticReflection.GetMemberName<Order>(x => x.OrderId), typeof(long));
        public static readonly IResourceIdentityInfo OrderResourceIdentityInfo = new ResourceIdentityInfo(OrderType, OrderIdInfo);

        public static readonly IPropertyInfo OrderItemIdInfo = new PropertyInfo(typeof(OrderItem), StaticReflection.GetMemberName<OrderItem>(x => x.OrderItemId), typeof(long));
        public static readonly IResourceIdentityInfo OrderItemResourceIdentityInfo = new ResourceIdentityInfo(OrderItemType, OrderItemIdInfo);

        public static readonly IPropertyInfo PaymentIdInfo = new PropertyInfo(typeof(Payment), StaticReflection.GetMemberName<Payment>(x => x.PaymentId), typeof(long));
        public static readonly IResourceIdentityInfo PaymentResourceIdentityInfo = new ResourceIdentityInfo(PaymentType, PaymentIdInfo);

        public static readonly IPropertyInfo PersonIdInfo = new PropertyInfo(typeof(Person), StaticReflection.GetMemberName<Person>(x => x.Id), typeof(string));
        public static readonly IResourceIdentityInfo PersonResourceIdentityInfo = new ResourceIdentityInfo(ApiSampleData.PersonType, PersonIdInfo);

        public static readonly IPropertyInfo PosSystemIdInfo = new PropertyInfo(typeof(PosSystem), StaticReflection.GetMemberName<PosSystem>(x => x.PosSystemId), typeof(string));
        public static readonly IResourceIdentityInfo PosSystemResourceIdentityInfo = new ResourceIdentityInfo(PosSystemType, PosSystemIdInfo);

        public static readonly IPropertyInfo ProductIdInfo = new PropertyInfo(typeof(Product), StaticReflection.GetMemberName<Product>(x => x.ProductId), typeof(long));
        public static readonly IResourceIdentityInfo ProductResourceIdentityInfo = new ResourceIdentityInfo(ProductType, ProductIdInfo);

        public static readonly IPropertyInfo StoreIdInfo = new PropertyInfo(typeof(Store), StaticReflection.GetMemberName<Store>(x => x.StoreId), typeof(long));
        public static readonly IResourceIdentityInfo StoreResourceIdentityInfo = new ResourceIdentityInfo(StoreType, StoreIdInfo);

        public static readonly IPropertyInfo StoreConfigurationIdInfo = new PropertyInfo(typeof(StoreConfiguration), StaticReflection.GetMemberName<StoreConfiguration>(x => x.StoreConfigurationId), typeof(string));
        public static readonly IResourceIdentityInfo StoreConfigurationResourceIdentityInfo = new ResourceIdentityInfo(StoreConfigurationType, StoreConfigurationIdInfo);
        #endregion

        #region Resource Type Attributes
        // Article Attributes
        public static readonly IAttributeInfo ArticleTitleAttributeInfo = new AttributeInfo(typeof(Article), StaticReflection.GetMemberName<Article>(x => x.Title), typeof(string), ApiSampleData.ArticleTitlePropertyName, false);
        public static readonly IAttributeInfo[] ArticleAttributesInfoCollection =
            {
                ArticleTitleAttributeInfo
            };
        public static readonly IAttributesInfo ArticleAttributesInfo = new AttributesInfo(typeof(Article), ArticleAttributesInfoCollection);

        // Blog Attributes
        public static readonly IAttributeInfo BlogNameAttributeInfo = new AttributeInfo(typeof(Blog), StaticReflection.GetMemberName<Blog>(x => x.Name), typeof(string), ApiSampleData.BlogNamePropertyName, false);
        public static readonly IAttributeInfo[] BlogAttributesInfoCollection =
            {
                BlogNameAttributeInfo
            };
        public static readonly IAttributesInfo BlogAttributesInfo = new AttributesInfo(typeof(Blog), BlogAttributesInfoCollection);

        // Comment Attributes
        public static readonly IAttributeInfo CommentBodyAttributeInfo = new AttributeInfo(typeof(Comment), StaticReflection.GetMemberName<Comment>(x => x.Body), typeof(string), ApiSampleData.CommentBodyPropertyName, false);
        public static readonly IAttributeInfo[] CommentAttributesInfoCollection =
            {
                CommentBodyAttributeInfo
            };
        public static readonly IAttributesInfo CommentAttributesInfo = new AttributesInfo(typeof(Comment), CommentAttributesInfoCollection);

        // Drawing Attributes
        public static readonly IAttributeInfo DrawingNameAttributeInfo = new AttributeInfo(typeof(Drawing), StaticReflection.GetMemberName<Drawing>(x => x.Name), typeof(string), StaticReflection.GetMemberName<Drawing>(x => x.Name).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo DrawingLinesAttributeInfo = new AttributeInfo(typeof(Drawing), StaticReflection.GetMemberName<Drawing>(x => x.Lines), typeof(List<Line>), StaticReflection.GetMemberName<Drawing>(x => x.Lines).Underscore().Dasherize(), true);
        public static readonly IAttributeInfo DrawingPolygonsAttributeInfo = new AttributeInfo(typeof(Drawing), StaticReflection.GetMemberName<Drawing>(x => x.Polygons), typeof(List<Polygon>), StaticReflection.GetMemberName<Drawing>(x => x.Polygons).Underscore().Dasherize(), true);
        public static readonly IAttributeInfo DrawingCustomDataAttributeInfo = new AttributeInfo(typeof(Drawing), StaticReflection.GetMemberName<Drawing>(x => x.CustomData), typeof(CustomData), StaticReflection.GetMemberName<Drawing>(x => x.CustomData).Underscore().Dasherize(), true);

        public static readonly IAttributeInfo[] DrawingAttributesInfoCollection =
            {
                DrawingNameAttributeInfo,
                DrawingLinesAttributeInfo,
                DrawingPolygonsAttributeInfo,
                DrawingCustomDataAttributeInfo
            };
        public static readonly IAttributesInfo DrawingAttributesInfo = new AttributesInfo(typeof(Drawing), DrawingAttributesInfoCollection);

        // Order Attributes
        public static readonly IAttributeInfo OrderTotalPriceAttributeInfo = new AttributeInfo(typeof(Order), StaticReflection.GetMemberName<Order>(x => x.TotalPrice), typeof(decimal), StaticReflection.GetMemberName<Order>(x => x.TotalPrice).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo[] OrderAttributesInfoCollection =
            {
                OrderTotalPriceAttributeInfo
            };
        public static readonly IAttributesInfo OrderAttributesInfo = new AttributesInfo(typeof(Order), OrderAttributesInfoCollection);

        // OrderItem Attributes
        public static readonly IAttributeInfo OrderItemProductNameAttributeInfo = new AttributeInfo(typeof(OrderItem), StaticReflection.GetMemberName<OrderItem>(x => x.ProductName), typeof(string), StaticReflection.GetMemberName<OrderItem>(x => x.ProductName).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo OrderItemQuantityAttributeInfo = new AttributeInfo(typeof(OrderItem), StaticReflection.GetMemberName<OrderItem>(x => x.Quantity), typeof(decimal), StaticReflection.GetMemberName<OrderItem>(x => x.Quantity).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo OrderItemUnitPriceAttributeInfo = new AttributeInfo(typeof(OrderItem), StaticReflection.GetMemberName<OrderItem>(x => x.UnitPrice), typeof(decimal), StaticReflection.GetMemberName<OrderItem>(x => x.UnitPrice).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo[] OrderItemAttributesInfoCollection =
            {
                OrderItemProductNameAttributeInfo,
                OrderItemQuantityAttributeInfo,
                OrderItemUnitPriceAttributeInfo
            };
        public static readonly IAttributesInfo OrderItemAttributesInfo = new AttributesInfo(typeof(OrderItem), OrderItemAttributesInfoCollection);

        // Payment Attributes
        public static readonly IAttributeInfo PaymentAmountAttributeInfo = new AttributeInfo(typeof(Payment), StaticReflection.GetMemberName<Payment>(x => x.Amount), typeof(decimal), StaticReflection.GetMemberName<Payment>(x => x.Amount).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo[] PaymentAttributesInfoCollection =
            {
                PaymentAmountAttributeInfo
            };
        public static readonly IAttributesInfo PaymentAttributesInfo = new AttributesInfo(typeof(Payment), PaymentAttributesInfoCollection);

        // Person Attributes
        public static readonly IAttributeInfo PersonFirstNameAttributeInfo = new AttributeInfo(typeof(Person), StaticReflection.GetMemberName<Person>(x => x.FirstName), typeof(string), ApiSampleData.PersonFirstNamePropertyName, false);
        public static readonly IAttributeInfo PersonLastNameAttributeInfo = new AttributeInfo(typeof(Person), StaticReflection.GetMemberName<Person>(x => x.LastName), typeof(string), ApiSampleData.PersonLastNamePropertyName, false);
        public static readonly IAttributeInfo PersonTwitterAttributeInfo = new AttributeInfo(typeof(Person), StaticReflection.GetMemberName<Person>(x => x.Twitter), typeof(string), ApiSampleData.PersonTwitterPropertyName, false);
        public static readonly IAttributeInfo[] PersonAttributesInfoCollection =
            {
                PersonFirstNameAttributeInfo,
                PersonLastNameAttributeInfo,
                PersonTwitterAttributeInfo
            };
        public static readonly IAttributesInfo PersonAttributesInfo = new AttributesInfo(typeof(Person), PersonAttributesInfoCollection);

        // PosSystem Attributes
        public static readonly IAttributeInfo PosSystemNameAttributeInfo = new AttributeInfo(typeof(PosSystem), StaticReflection.GetMemberName<PosSystem>(x => x.PosSystemName), typeof(string), StaticReflection.GetMemberName<PosSystem>(x => x.PosSystemName).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo PosSystemEndOfLifeDateAttributeInfo = new AttributeInfo(typeof(PosSystem), StaticReflection.GetMemberName<PosSystem>(x => x.EndOfLifeDate), typeof(DateTime?), StaticReflection.GetMemberName<PosSystem>(x => x.EndOfLifeDate).Underscore().Dasherize(), false);

        public static readonly IAttributeInfo[] PosSystemAttributesInfoCollection =
            {
                PosSystemNameAttributeInfo,
                PosSystemEndOfLifeDateAttributeInfo
            };
        public static readonly IAttributesInfo PosSystemAttributesInfo = new AttributesInfo(typeof(PosSystem), PosSystemAttributesInfoCollection);

        // Product Attributes
        public static readonly IAttributeInfo ProductNameAttributeInfo = new AttributeInfo(typeof(Product), StaticReflection.GetMemberName<Product>(x => x.Name), typeof(string), StaticReflection.GetMemberName<Product>(x => x.Name).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo ProductUnitPriceAttributeInfo = new AttributeInfo(typeof(Product), StaticReflection.GetMemberName<Product>(x => x.UnitPrice), typeof(decimal), StaticReflection.GetMemberName<Product>(x => x.UnitPrice).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo[] ProductAttributesInfoCollection =
            {
                ProductNameAttributeInfo,
                ProductUnitPriceAttributeInfo
            };
        public static readonly IAttributesInfo ProductAttributesInfo = new AttributesInfo(typeof(Product), ProductAttributesInfoCollection);

        // Store Attributes
        public static readonly IAttributeInfo StoreNameAttributeInfo = new AttributeInfo(typeof(Store), StaticReflection.GetMemberName<Store>(x => x.StoreName), typeof(string), StaticReflection.GetMemberName<Store>(x => x.StoreName).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo StoreAddressAttributeInfo = new AttributeInfo(typeof(Store), StaticReflection.GetMemberName<Store>(x => x.Address), typeof(string), StaticReflection.GetMemberName<Store>(x => x.Address).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo StoreCityAttributeInfo = new AttributeInfo(typeof(Store), StaticReflection.GetMemberName<Store>(x => x.City), typeof(string), StaticReflection.GetMemberName<Store>(x => x.City).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo StoreStateAttributeInfo = new AttributeInfo(typeof(Store), StaticReflection.GetMemberName<Store>(x => x.State), typeof(string), StaticReflection.GetMemberName<Store>(x => x.State).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo StoreZipCodeAttributeInfo = new AttributeInfo(typeof(Store), StaticReflection.GetMemberName<Store>(x => x.ZipCode), typeof(string), StaticReflection.GetMemberName<Store>(x => x.ZipCode).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo[] StoreAttributesInfoCollection =
            {
                StoreNameAttributeInfo,
                StoreAddressAttributeInfo,
                StoreCityAttributeInfo,
                StoreStateAttributeInfo,
                StoreZipCodeAttributeInfo
            };
        public static readonly IAttributesInfo StoreAttributesInfo = new AttributesInfo(typeof(Store), StoreAttributesInfoCollection);

        // StoreConfiguration Attributes
        public static readonly IAttributeInfo StoreConfigurationIsLiveAttributeInfo = new AttributeInfo(typeof(StoreConfiguration), StaticReflection.GetMemberName<StoreConfiguration>(x => x.IsLive), typeof(bool), StaticReflection.GetMemberName<StoreConfiguration>(x => x.IsLive).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo StoreConfigurationMailingAddressAttributeInfo = new AttributeInfo(typeof(StoreConfiguration), StaticReflection.GetMemberName<StoreConfiguration>(x => x.MailingAddress), typeof(MailingAddress), StaticReflection.GetMemberName<StoreConfiguration>(x => x.MailingAddress).Underscore().Dasherize(), true);
        public static readonly IAttributeInfo StoreConfigurationPhoneNumbersAttributeInfo = new AttributeInfo(typeof(StoreConfiguration), StaticReflection.GetMemberName<StoreConfiguration>(x => x.PhoneNumbers), typeof(List<PhoneNumber>), StaticReflection.GetMemberName<StoreConfiguration>(x => x.PhoneNumbers).Underscore().Dasherize(), true);
        public static readonly IAttributeInfo[] StoreConfigurationAttributesInfoCollection =
            {
                StoreConfigurationIsLiveAttributeInfo,
                StoreConfigurationMailingAddressAttributeInfo,
                StoreConfigurationPhoneNumbersAttributeInfo
            };
        public static readonly IAttributesInfo StoreConfigurationAttributesInfo = new AttributesInfo(typeof(StoreConfiguration), StoreConfigurationAttributesInfoCollection);
        #endregion

        #region Complex Type Attributes

        // CustomData Attributes
        public static readonly IAttributeInfo CustomDataCollectionAttributeInfo = new AttributeInfo(typeof(CustomData), StaticReflection.GetMemberName<CustomData>(x => x.Collection), typeof(List<CustomProperty>), StaticReflection.GetMemberName<CustomData>(x => x.Collection).Underscore().Dasherize(), true);
        public static readonly IAttributeInfo[] CustomDataAttributesInfoCollection =
            {
                CustomDataCollectionAttributeInfo
            };
        public static readonly IAttributesInfo CustomDataAttributesInfo = new AttributesInfo(typeof(CustomData), CustomDataAttributesInfoCollection);

        // CustomProperty Attributes
        public static readonly IAttributeInfo CustomPropertyNameAttributeInfo = new AttributeInfo(typeof(CustomProperty), StaticReflection.GetMemberName<CustomProperty>(x => x.Name), typeof(string), StaticReflection.GetMemberName<CustomProperty>(x => x.Name).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo CustomPropertyValueAttributeInfo = new AttributeInfo(typeof(CustomProperty), StaticReflection.GetMemberName<CustomProperty>(x => x.Value), typeof(string), StaticReflection.GetMemberName<CustomProperty>(x => x.Value).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo[] CustomPropertyAttributesInfoCollection =
            {
                CustomPropertyNameAttributeInfo,
                CustomPropertyValueAttributeInfo
            };
        public static readonly IAttributesInfo CustomPropertyAttributesInfo = new AttributesInfo(typeof(CustomProperty), CustomPropertyAttributesInfoCollection);

        // Line Attributes
        public static readonly IAttributeInfo LinePoint1AttributeInfo = new AttributeInfo(typeof(Line), StaticReflection.GetMemberName<Line>(x => x.Point1), typeof(Point), StaticReflection.GetMemberName<Line>(x => x.Point1).Underscore().Dasherize(), true);
        public static readonly IAttributeInfo LinePoint2AttributeInfo = new AttributeInfo(typeof(Line), StaticReflection.GetMemberName<Line>(x => x.Point2), typeof(Point), StaticReflection.GetMemberName<Line>(x => x.Point2).Underscore().Dasherize(), true);
        public static readonly IAttributeInfo LineCustomDataAttributeInfo = new AttributeInfo(typeof(Line), StaticReflection.GetMemberName<Line>(x => x.CustomData), typeof(CustomData), StaticReflection.GetMemberName<Line>(x => x.CustomData).Underscore().Dasherize(), true);
        public static readonly IAttributeInfo[] LineAttributesInfoCollection =
            {
                LinePoint1AttributeInfo,
                LinePoint2AttributeInfo,
                LineCustomDataAttributeInfo
            };
        public static readonly IAttributesInfo LineAttributesInfo = new AttributesInfo(typeof(Line), LineAttributesInfoCollection);

        // MailingAddress Attributes
        public static readonly IAttributeInfo MailingAddressAddressAttributeInfo = new AttributeInfo(typeof(MailingAddress), StaticReflection.GetMemberName<MailingAddress>(x => x.Address), typeof(string), StaticReflection.GetMemberName<MailingAddress>(x => x.Address).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo MailingAddressCityAttributeInfo = new AttributeInfo(typeof(MailingAddress), StaticReflection.GetMemberName<MailingAddress>(x => x.City), typeof(string), StaticReflection.GetMemberName<MailingAddress>(x => x.City).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo MailingAddressStateAttributeInfo = new AttributeInfo(typeof(MailingAddress), StaticReflection.GetMemberName<MailingAddress>(x => x.State), typeof(string), StaticReflection.GetMemberName<MailingAddress>(x => x.State).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo MailingAddressZipCodeAttributeInfo = new AttributeInfo(typeof(MailingAddress), StaticReflection.GetMemberName<MailingAddress>(x => x.ZipCode), typeof(string), StaticReflection.GetMemberName<MailingAddress>(x => x.ZipCode).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo[] MailingAddressAttributesInfoCollection =
            {
                MailingAddressAddressAttributeInfo,
                MailingAddressCityAttributeInfo,
                MailingAddressStateAttributeInfo,
                MailingAddressZipCodeAttributeInfo
            };
        public static readonly IAttributesInfo MailingAddressAttributesInfo = new AttributesInfo(typeof(MailingAddress), MailingAddressAttributesInfoCollection);

        // PhoneNumber Attributes
        public static readonly IAttributeInfo PhoneNumberAreaCodeAttributeInfo = new AttributeInfo(typeof(PhoneNumber), StaticReflection.GetMemberName<PhoneNumber>(x => x.AreaCode), typeof(string), StaticReflection.GetMemberName<PhoneNumber>(x => x.AreaCode).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo PhoneNumberNumberAttributeInfo = new AttributeInfo(typeof(PhoneNumber), StaticReflection.GetMemberName<PhoneNumber>(x => x.Number), typeof(string), StaticReflection.GetMemberName<PhoneNumber>(x => x.Number).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo[] PhoneNumberAttributesInfoCollection =
            {
                PhoneNumberAreaCodeAttributeInfo,
                PhoneNumberNumberAttributeInfo
            };
        public static readonly IAttributesInfo PhoneNumberAttributesInfo = new AttributesInfo(typeof(PhoneNumber), PhoneNumberAttributesInfoCollection);

        // Point Attributes
        public static readonly IAttributeInfo PointXAttributeInfo = new AttributeInfo(typeof(Point), StaticReflection.GetMemberName<Point>(x => x.X), typeof(int), StaticReflection.GetMemberName<Point>(x => x.X).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo PointYAttributeInfo = new AttributeInfo(typeof(Point), StaticReflection.GetMemberName<Point>(x => x.Y), typeof(int), StaticReflection.GetMemberName<Point>(x => x.Y).Underscore().Dasherize(), false);
        public static readonly IAttributeInfo PointCustomDataAttributeInfo = new AttributeInfo(typeof(Point), StaticReflection.GetMemberName<Point>(x => x.CustomData), typeof(CustomData), StaticReflection.GetMemberName<Point>(x => x.CustomData).Underscore().Dasherize(), true);
        public static readonly IAttributeInfo[] PointAttributesInfoCollection =
            {
                PointXAttributeInfo,
                PointYAttributeInfo,
                PointCustomDataAttributeInfo
            };
        public static readonly IAttributesInfo PointAttributesInfo = new AttributesInfo(typeof(Point), PointAttributesInfoCollection);

        // Polygon Attributes
        public static readonly IAttributeInfo PolygonPointsAttributeInfo = new AttributeInfo(typeof(Polygon), StaticReflection.GetMemberName<Polygon>(x => x.Points), typeof(List<Point>), StaticReflection.GetMemberName<Polygon>(x => x.Points).Underscore().Dasherize(), true);
        public static readonly IAttributeInfo PolygonCustomDataAttributeInfo = new AttributeInfo(typeof(Polygon), StaticReflection.GetMemberName<Polygon>(x => x.CustomData), typeof(CustomData), StaticReflection.GetMemberName<Polygon>(x => x.CustomData).Underscore().Dasherize(), true);
        public static readonly IAttributeInfo[] PolygonAttributesInfoCollection =
            {
                PolygonPointsAttributeInfo,
                PolygonCustomDataAttributeInfo
            };
        public static readonly IAttributesInfo PolygonAttributesInfo = new AttributesInfo(typeof(Polygon), PolygonAttributesInfoCollection);
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
        public static readonly IRelationshipInfo ArticleToAuthorRelationshipInfo = new RelationshipInfo(ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelPathSegment, typeof(Person), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo ArticleToCommentsRelationshipInfo = new RelationshipInfo(ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelPathSegment, typeof(Comment), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] ArticleRelationshipsInfoCollection = 
            {
                ArticleToAuthorRelationshipInfo,
                ArticleToCommentsRelationshipInfo
            };
        public static readonly IRelationshipsInfo ArticleRelationshipsInfo = new RelationshipsInfo(typeof(Article), StaticReflection.GetMemberName<Article>(x => x.Relationships), ArticleRelationshipsInfoCollection);

        // Blog Relationships
        public static readonly IRelationshipInfo BlogToArticlesRelationshipInfo = new RelationshipInfo(ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesRelPathSegment, typeof(Article), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] BlogRelationshipsInfoCollection = 
            {
                BlogToArticlesRelationshipInfo
            };
        public static readonly IRelationshipsInfo BlogRelationshipsInfo = new RelationshipsInfo(typeof(Blog), StaticReflection.GetMemberName<Blog>(x => x.Relationships), BlogRelationshipsInfoCollection);

        // Comment Relationships
        public static readonly IRelationshipInfo CommentToAuthorRelationshipInfo = new RelationshipInfo(ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelPathSegment, typeof(Person), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] CommentRelationshipsInfoCollection = 
            {
                CommentToAuthorRelationshipInfo
            };
        public static readonly IRelationshipsInfo CommentRelationshipsInfo = new RelationshipsInfo(typeof(Comment), StaticReflection.GetMemberName<Comment>(x => x.Relationships), CommentRelationshipsInfoCollection);

        // Drawing Relationships
        public static readonly IRelationshipInfo[] DrawingRelationshipsInfoCollection = 
            {
            };
        public static readonly IRelationshipsInfo DrawingRelationshipsInfo = new RelationshipsInfo(DrawingRelationshipsInfoCollection);

        // Order Relationships
        public static readonly IRelationshipInfo OrderToOrderItemsRelationshipInfo = new RelationshipInfo(OrderToOrderItemsRel, OrderToOrderItemsRelPathSegment, typeof(OrderItem), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);
        public static readonly IRelationshipInfo OrderToPaymentsRelationshipInfo = new RelationshipInfo(OrderToPaymentsRel, OrderToPaymentsRelPathSegment, typeof(Payment), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo OrderToStoreRelationshipInfo = new RelationshipInfo(OrderToStoreRel, OrderToStoreRelPathSegment, typeof(Store), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] OrderRelationshipsInfoCollection = 
            {
                OrderToOrderItemsRelationshipInfo,
                OrderToPaymentsRelationshipInfo,
                OrderToStoreRelationshipInfo
            };
        public static readonly IRelationshipsInfo OrderRelationshipsInfo = new RelationshipsInfo(OrderRelationshipsInfoCollection);

        // OrderItem Relationships
        public static readonly IRelationshipInfo OrderItemToOrderRelationshipInfo = new RelationshipInfo(OrderItemToOrderRel, OrderItemToOrderRelPathSegment, typeof(Order), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo OrderItemToProductRelationshipInfo = new RelationshipInfo(OrderItemToProductRel, OrderItemToProductRelPathSegment, typeof(Product), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] OrderItemRelationshipsInfoCollection = 
            {
                OrderItemToOrderRelationshipInfo,
                OrderItemToProductRelationshipInfo
            };
        public static readonly IRelationshipsInfo OrderItemRelationshipsInfo = new RelationshipsInfo(OrderItemRelationshipsInfoCollection);

        // Payment Relationships
        public static readonly IRelationshipInfo PaymentToOrderRelationshipInfo = new RelationshipInfo(PaymentToOrderRel, PaymentToOrderRelPathSegment, typeof(Order), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] PaymentRelationshipsInfoCollection = 
            {
                PaymentToOrderRelationshipInfo
            };
        public static readonly IRelationshipsInfo PaymentRelationshipsInfo = new RelationshipsInfo(PaymentRelationshipsInfoCollection);

        // Person Relationships
        public static readonly IRelationshipInfo PersonToCommentsRelationshipInfo = new RelationshipInfo(ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelPathSegment, typeof(Comment), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] PersonRelationshipsInfoCollection = 
            {
                PersonToCommentsRelationshipInfo
            };
        public static readonly IRelationshipsInfo PersonRelationshipsInfo = new RelationshipsInfo(typeof(Person), StaticReflection.GetMemberName<Person>(x => x.Relationships), PersonRelationshipsInfoCollection);

        // PosSystem Relationships
        public static readonly IRelationshipInfo PosSystemToStoreConfigurationsRelationshipInfo = new RelationshipInfo(PosSystemToStoreConfigurationsRel, PosSystemToStoreConfigurationsRelPathSegment, typeof(StoreConfiguration), RelationshipCardinality.ToMany, RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);

        public static readonly IRelationshipInfo[] PosSystemRelationshipsInfoCollection = 
            {
                PosSystemToStoreConfigurationsRelationshipInfo
            };
        public static readonly IRelationshipsInfo PosSystemRelationshipsInfo = new RelationshipsInfo(PosSystemRelationshipsInfoCollection);

        // Product Relationships
        public static readonly IRelationshipInfo[] ProductRelationshipsInfoCollection = 
            {
            };
        public static readonly IRelationshipsInfo ProductRelationshipsInfo = new RelationshipsInfo(ProductRelationshipsInfoCollection);

        // Store Relationships
        public static readonly IRelationshipInfo StoreToStoreConfigurationRelationshipInfo = new RelationshipInfo(StoreToStoreConfigurationRel, StoreToStoreConfigurationRelPathSegment, typeof(StoreConfiguration), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.KeepPreviousPathSegments);
        public static readonly IRelationshipInfo[] StoreRelationshipsInfoCollection = 
            {
                StoreToStoreConfigurationRelationshipInfo
            };
        public static readonly IRelationshipsInfo StoreRelationshipsInfo = new RelationshipsInfo(StoreRelationshipsInfoCollection);

        // StoreConfiguration Relationships
        public static readonly IRelationshipInfo StoreToStoreConfigurationToPosSystemRelationshipInfo = new RelationshipInfo(StoreToStoreConfigurationToPosSystemRel, StoreToStoreConfigurationToPosSystemRelPathSegment, typeof(PosSystem), RelationshipCardinality.ToOne, RelationshipCanonicalRelPathMode.DropPreviousPathSegments);
        public static readonly IRelationshipInfo[] StoreConfigurationRelationshipsInfoCollection = 
            {
                StoreToStoreConfigurationToPosSystemRelationshipInfo
            };
        public static readonly IRelationshipsInfo StoreConfigurationRelationshipsInfo = new RelationshipsInfo(StoreConfigurationRelationshipsInfoCollection);
        #endregion

        #region Links
        // Article Links
        public static readonly ILinkInfo ArticleCanonicalLinkInfo = new LinkInfo(Keywords.Canonical);
        public static readonly ILinkInfo ArticleSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] ArticleLinksInfoCollection = 
            {
                ArticleCanonicalLinkInfo,
                ArticleSelfLinkInfo
            };
        public static readonly ILinksInfo ArticleLinksInfo = new LinksInfo(typeof(Article), StaticReflection.GetMemberName<Article>(x => x.Links), ArticleLinksInfoCollection);

        // Blog Links
        public static readonly ILinkInfo BlogSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] BlogLinksInfoCollection = 
            {
                BlogSelfLinkInfo
            };
        public static readonly ILinksInfo BlogLinksInfo = new LinksInfo(typeof(Blog), StaticReflection.GetMemberName<Blog>(x => x.Links), BlogLinksInfoCollection);

        // Comment Links
        public static readonly ILinkInfo CommentSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] CommentLinksInfoCollection = 
            {
                CommentSelfLinkInfo
            };
        public static readonly ILinksInfo CommentLinksInfo = new LinksInfo(typeof(Comment), StaticReflection.GetMemberName<Comment>(x => x.Links), CommentLinksInfoCollection);

        // Drawing Links
        public static readonly ILinkInfo DrawingSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] DrawingLinksInfoCollection = 
            {
                DrawingSelfLinkInfo
            };
        public static readonly ILinksInfo DrawingLinksInfo = new LinksInfo(DrawingLinksInfoCollection);

        // Order Links
        public static readonly ILinkInfo OrderSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] OrderLinksInfoCollection = 
            {
                OrderSelfLinkInfo
            };
        public static readonly ILinksInfo OrderLinksInfo = new LinksInfo(OrderLinksInfoCollection);

        // OrderItem Links
        public static readonly ILinkInfo OrderItemSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] OrderItemLinksInfoCollection = 
            {
                OrderItemSelfLinkInfo
            };
        public static readonly ILinksInfo OrderItemLinksInfo = new LinksInfo(OrderItemLinksInfoCollection);

        // Payment Links
        public static readonly ILinkInfo PaymentSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] PaymentLinksInfoCollection = 
            {
                PaymentSelfLinkInfo
            };
        public static readonly ILinksInfo PaymentLinksInfo = new LinksInfo(PaymentLinksInfoCollection);

        // Person Links
        public static readonly ILinkInfo PersonSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] PersonLinksInfoCollection = 
            {
                PersonSelfLinkInfo
            };
        public static readonly ILinksInfo PersonLinksInfo = new LinksInfo(typeof(Person), StaticReflection.GetMemberName<Person>(x => x.Links), PersonLinksInfoCollection);

        // PosSystem Links
        public static readonly ILinkInfo PosSystemSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] PosSystemLinksInfoCollection = 
            {
                PosSystemSelfLinkInfo
            };
        public static readonly ILinksInfo PosSystemLinksInfo = new LinksInfo(PosSystemLinksInfoCollection);

        // Product Links
        public static readonly ILinkInfo ProductSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] ProductLinksInfoCollection = 
            {
                ProductSelfLinkInfo
            };
        public static readonly ILinksInfo ProductLinksInfo = new LinksInfo(ProductLinksInfoCollection);

        // Store Links
        public static readonly ILinkInfo StoreSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] StoreLinksInfoCollection = 
            {
                StoreSelfLinkInfo
            };
        public static readonly ILinksInfo StoreLinksInfo = new LinksInfo(StoreLinksInfoCollection);

        // StoreConfiguration Links
        public static readonly ILinkInfo StoreConfigurationSelfLinkInfo = new LinkInfo(Keywords.Self);
        public static readonly ILinkInfo[] StoreConfigurationLinksInfoCollection = 
            {
                StoreConfigurationSelfLinkInfo
            };
        public static readonly ILinksInfo StoreConfigurationLinksInfo = new LinksInfo(StoreConfigurationLinksInfoCollection);
        #endregion

        #region Meta
        public static readonly IMetaInfo ArticleMetaInfo = new MetaInfo(typeof(Article), StaticReflection.GetMemberName<Article>(x => x.Meta));
        public static readonly IMetaInfo BlogMetaInfo = new MetaInfo(typeof(Blog), StaticReflection.GetMemberName<Blog>(x => x.Meta));
        public static readonly IMetaInfo CommentMetaInfo = new MetaInfo(typeof(Comment), StaticReflection.GetMemberName<Comment>(x => x.Meta));
        public static readonly IMetaInfo PersonMetaInfo = new MetaInfo(typeof(Person), StaticReflection.GetMemberName<Person>(x => x.Meta));
        #endregion

        #region ComplexTypes
        public static readonly IComplexType CustomDataComplexType = new ComplexType(CustomDataClrType, CustomDataAttributesInfo);
        public static readonly IComplexType CustomPropertyComplexType = new ComplexType(CustomPropertyClrType, CustomPropertyAttributesInfo);
        public static readonly IComplexType LineComplexType = new ComplexType(LineClrType, LineAttributesInfo);
        public static readonly IComplexType MailingAddressComplexType = new ComplexType(MailingAddressClrType, MailingAddressAttributesInfo);
        public static readonly IComplexType PhoneNumberComplexType = new ComplexType(PhoneNumberClrType, PhoneNumberAttributesInfo);
        public static readonly IComplexType PointComplexType = new ComplexType(PointClrType, PointAttributesInfo);
        public static readonly IComplexType PolygonComplexType = new ComplexType(PolygonClrType, PolygonAttributesInfo);
        #endregion

        #region ResourceTypes
        public static readonly IResourceType ArticleResourceType = new ResourceType(ArticleClrType, ArticleHypermediaInfo, ArticleResourceIdentityInfo, ArticleAttributesInfo, ArticleRelationshipsInfo, ArticleLinksInfo, ArticleMetaInfo);
        public static readonly IResourceType BlogResourceType = new ResourceType(BlogClrType, BlogHypermediaInfo, BlogResourceIdentityInfo, BlogAttributesInfo, BlogRelationshipsInfo, BlogLinksInfo, BlogMetaInfo);
        public static readonly IResourceType CommentResourceType = new ResourceType(CommentClrType, CommentHypermediaInfo, CommentResourceIdentityInfo, CommentAttributesInfo, CommentRelationshipsInfo, CommentLinksInfo, CommentMetaInfo);
        public static readonly IResourceType DrawingResourceType = new ResourceType(DrawingClrType, DrawingHypermediaInfo, DrawingResourceIdentityInfo, DrawingAttributesInfo, DrawingRelationshipsInfo, DrawingLinksInfo, null);
        public static readonly IResourceType OrderResourceType = new ResourceType(OrderClrType, OrderHypermediaInfo, OrderResourceIdentityInfo, OrderAttributesInfo, OrderRelationshipsInfo, OrderLinksInfo, null);
        public static readonly IResourceType OrderItemResourceType = new ResourceType(OrderItemClrType, OrderItemHypermediaInfo, OrderItemResourceIdentityInfo, OrderItemAttributesInfo, OrderItemRelationshipsInfo, OrderItemLinksInfo, null);
        public static readonly IResourceType PaymentResourceType = new ResourceType(PaymentClrType, PaymentHypermediaInfo, PaymentResourceIdentityInfo, PaymentAttributesInfo, PaymentRelationshipsInfo, PaymentLinksInfo, null);
        public static readonly IResourceType PersonResourceType = new ResourceType(PersonClrType, PersonHypermediaInfo, PersonResourceIdentityInfo, PersonAttributesInfo, PersonRelationshipsInfo, PersonLinksInfo, PersonMetaInfo);
        public static readonly IResourceType PosSystemResourceType = new ResourceType(PosSystemClrType, PosSystemHypermediaInfo, PosSystemResourceIdentityInfo, PosSystemAttributesInfo, PosSystemRelationshipsInfo, PosSystemLinksInfo, null);
        public static readonly IResourceType ProductResourceType = new ResourceType(ProductClrType, ProductHypermediaInfo, ProductResourceIdentityInfo, ProductAttributesInfo, ProductRelationshipsInfo, ProductLinksInfo, null);
        public static readonly IResourceType StoreResourceType = new ResourceType(StoreClrType, StoreHypermediaInfo, StoreResourceIdentityInfo, StoreAttributesInfo, StoreRelationshipsInfo, StoreLinksInfo, null);
        public static readonly IResourceType StoreConfigurationResourceType = new ResourceType(StoreConfigurationClrType, StoreConfigurationHypermediaInfo, StoreConfigurationResourceIdentityInfo, StoreConfigurationAttributesInfo, StoreConfigurationRelationshipsInfo, StoreConfigurationLinksInfo, null);
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
        /// 
        /// This service model also has complex types defined.
        /// </remarks>
        public static readonly IServiceModel ServiceModelWithOrderResourceTypes =
            new JsonApiFramework.ServiceModel.Internal.ServiceModel(
                new List<IComplexType>
                    {
                        ClrSampleData.MailingAddressComplexType,
                        ClrSampleData.PhoneNumberComplexType
                    },
                new List<IResourceType>
                    {
                        ClrSampleData.OrderResourceType,
                        ClrSampleData.OrderItemResourceType,
                        ClrSampleData.PaymentResourceType,
                        ClrSampleData.PosSystemResourceType,
                        ClrSampleData.ProductResourceType,
                        ClrSampleData.StoreResourceType,
                        ClrSampleData.StoreConfigurationResourceType
                    });

        /// <summary>
        /// Drawing with many nested complex types service model.
        /// </summary>
        /// <remarks>
        /// This service model is designed to test complex types.
        /// </remarks>
        public static readonly IServiceModel ServiceModelWithDrawingResourceTypes =
            new JsonApiFramework.ServiceModel.Internal.ServiceModel(
                new List<IComplexType>
                    {
                        ClrSampleData.CustomDataComplexType,
                        ClrSampleData.CustomPropertyComplexType,
                        ClrSampleData.LineComplexType,
                        ClrSampleData.PointComplexType,
                        ClrSampleData.PolygonComplexType,
                    },
                new List<IResourceType>
                    {
                        ClrSampleData.DrawingResourceType
                    });
        #endregion
    }
}