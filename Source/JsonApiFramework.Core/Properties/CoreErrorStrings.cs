// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework;

internal static class CoreErrorStrings
{
    #region Public Properties
    public static string DocumentDoesNotContainDataMemberAsResourceCollectionDetail => "Unable to read or write the \"data\" member, not a document that contains a \"data\" member as an array of resources.";
    public static string DocumentDoesNotContainDataMemberAsResourceDetail => "Unable to read or write the \"data\" member, not a document that contains a \"data\" member as a single resource.";
    public static string DocumentDoesNotContainDataMemberAsResourceIdentifierCollectionDetail => "Unable to read or write the \"data\" member, not a document that contains a \"data\" member as an array of resource identifiers.";
    public static string DocumentDoesNotContainDataMemberAsResourceIdentifierDetail => "Unable to read or write the \"data\" member, not a document that contains a \"data\" member as a single resource identifier.";
    public static string DocumentDoesNotContainDataMemberDetail => "Unable to read or write the \"data\" member, not a document that contains a \"data\" member.";
    public static string DocumentDoesNotContainErrorsMemberDetail => "Unable to read or write the \"errors\" member, not a document that contains an \"errors\" member.";
    public static string DocumentDoesNotContainIncludedMemberDetail => "Unable to read or write the \"included\" member, not a document that contains an \"included\" member.";
    public static string DocumentNotErrorsDocumentTitle => "Not an Errors Document";
    public static string DocumentNotResourceCollectionDocumentTitle => "Not a Resource Collection Document";
    public static string DocumentNotResourceDocumentTitle => "Not a Resource Document";
    public static string DocumentNotResourceIdentifierCollectionDocumentTitle => "Not a Resource Identifier Collection Document";
    public static string DocumentNotResourceIdentifierDocumentTitle => "Not a Resource Identifier Document";
    public static string DocumentNotResourceOrientedDocumentTitle => "Not a Resource Oriented Document";
    public static string DocumentNotResourceOrResourceCollectionDocumentTitle => "Not a Resource Document or Resource Collection Document";
    public static string JsonApiDocumentCanNotContainBothMembersDetail => "Document can not contain both \"{0}\" and \"{1}\" members.";
    public static string JsonApiDocumentContainsIllegalMemberMemberShouldBeAnArrayDetail => "Document contains an illegal \"{0}\" member. Member should be an array.";
    public static string JsonApiDocumentContainsIllegalMemberMemberShouldBeNullAnObjectOrAnArrayDetail => "Document contains an illegal \"{0}\" member. Member should be null, an object, or an array.";
    public static string JsonApiDocumentMustContainHetergenousCollectionOrResourcesOrResourceIdentifiersDetail => "Document contains a hetergenous collection of Resources and ResourceIdentifiers, must be a homogenous collection of either Resources or ResourceIdentifiers.";
    public static string JsonApiErrorTitle => "JsonApi Error";
    public static string JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail => "Invalid JSON \"{0}\" for either a Resource or ResourceIdentifier.";
    public static string JsonTextContainsInvalidJsonTitle => "Invalid JSON";
    public static string LinksLinkNotFoundDetail => "Could not find link for the rel = {0}.";
    public static string LinksLinkNotFoundTitle => "Link Not Found";
    public static string RelationshipDoesNotContainDataMemberAsResourceIdentifierCollectionDetail => "Unable to read or write the \"data\" member, not a relationship that contains a \"data\" member as an array of resource identifiers.";
    public static string RelationshipDoesNotContainDataMemberAsResourceIdentifierDetail => "Unable to read or write the \"data\" member, not a relationship that contains a \"data\" member as a single resource identifier.";
    public static string RelationshipNotToManyRelatioshipTitle => "Not a ToManyRelationship";
    public static string RelationshipNotToOneRelatioshipTitle => "Not a ToOneRelationship";
    public static string RelationshipsRelationshipNotFoundDetail => "Could not find relationship for the rel = {0}.";
    public static string RelationshipsRelationshipNotFoundTitle => "Relationship Not Found";
    public static string ServiceModelExceptionDetailMissingMetadata => "{0} has missing {1} metadata. Ensure metadata is configured correctly for the respective domain/schema.";
    public static string ServiceModelExceptionTitle => "ServiceModel Error";
    #endregion
}
