// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework;
public static class InfrastructureErrorStrings
{
    #region Public Properties
    public static string DocumentBuildExceptionDetailBuildRelationshipWithCollectionOfObjects => "Unable to build '{0}' for a single json:api relationship[rel ={1}] with a collection of '{0}' objects.";
    public static string DocumentBuildExceptionDetailBuildResourceCollectionCountMismatch => "Unable to build '{0}' for a collection[count ={1}] of CLR resources or CLR resource identifiers [type={2}] with a collection [count={3}] of '{0}' objects, collection counts mismatch. Make sure the collection counts are the same.";
    public static string DocumentBuildExceptionDetailBuildResourceCollectionWithSingleObject => "Unable to build '{0}' for a collection of CLR resources or CLR resource identifiers [type={1}] with a single '{0}' object";
    public static string DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects => "Unable to build '{0}' for a single CLR resource or CLR resource identifier [type={1}] with a collection of '{0}' objects.";
    public static string DocumentBuildExceptionDetailBuildToManyRelationshipResourceLinkageCardinalityMismatch => "Unable to build CLR resource[type={0}] relationship[rel={1}] 'to-many' resource linkage with 'to-one' resource linkage.";
    public static string DocumentBuildExceptionDetailBuildToOneRelationshipResourceLinkageCardinalityMismatch => "Unable to build CLR resource[type={0}] relationship[rel={1}] 'to-one' resource linkage with 'to-many' resource linkage.";
    public static string DocumentBuildExceptionTitle => "Document Build Error";
    public static string DocumentContextExtensionValidationConfigurationError => "{0} was not configured for this document context.";
    public static string DocumentReadExceptionGetMultipleResourcesExistWithSameIdentity => "Unable to get the single CLR resource [type={0} id={1}] in the json:api document, json:api document has multiple resources with the same resource identity.";
    public static string DocumentReadExceptionGetMultipleResourcesExistWithSameType => "Unable to get the single CLR resource or resource identifier [type={0}] in the json:api document, json:api document has multiple resources or resource identifiers with the same resource type.";
    public static string DocumentReadExceptionGetToManyRelatedResourceCollectionWithToOneRelationship => "Unable to get the 'to-many' related CLR resource collection [type={0}] with a 'to-one' CLR relationship object. Call the get 'to-many' related CLR resource collection method with a 'to-many' CLR relationship object.";
    public static string DocumentReadExceptionGetToOneRelatedResourceWithToManyRelationship => "Unable to get the 'to-one' related CLR resource [type={0}] with a 'to-many' CLR relationship object. Call the get 'to-one' related CLR resource method with a 'to-one' CLR relationship object.";
    public static string DocumentReadExceptionTitle => "Document Read Error";
    public static string DocumentWriteExceptionTitle => "Document Write Error";
    public static string DocumentWriteToOneResourceLinkageMismatch => "The CLR resource[type={0} id={1}] has a 'to-one' relationship [rel={2}] with different 'to-one' resource linkage from user building [{3}] compared to user include [{4}].";
    public static string DomExceptionDetailNodeAlreadyContainsChildNode => "The DOM node[type={0}] already contains DOM child node [type={1}]. Only add the DOM child node [type={1}] once.";
    public static string DomExceptionDetailNodeAlreadyContainsRelBasedChildNode => "The DOM node[type={0}] already contains DOM child node [rel={1} type={2}]. Only add the DOM child node [rel={1} type={2}] once.";
    public static string DomExceptionDetailNodeGetMissingAttribute => "The DOM node[type={0}] attribute[name={1}] is missing. Can only get existing attributes.";
    public static string DomExceptionDetailNodeHasChildNodesThatMustNotCoexist => "The DOM node[type={0}] already contains DOM child node [type={1}], can not add DOM child node [type={2}] because these two DOM child nodes can not coexist as sibling DOM nodes. Would violate the JSON API specification.";
    public static string DomExceptionDetailNodeHasIncompatibleChildNodes => "The DOM node[type={0}] contains incompatible DOM child node [type={1}] and DOM child node [type={2}].";
    public static string DomExceptionDetailNodeHasNoServiceModel => "The DOM node[type={0}] has no access to a service model.";
    public static string DomExceptionDetailNodeHasUnexpectedChildNode => "The DOM node[type={0}] contains an unexpected DOM child node [type={1}].";
    public static string DomExceptionDetailNodeSetExistingAttribute => "The DOM node[type={0}] attribute[name={1}] already exists. Can only set an attribute one-time.";
    public static string DomExceptionTitle => "Internal DOM Error";
    public static string InternalErrorExceptionDetailMissingExtension => "{0} has missing '{1}' extension. Ensure extension is configured and added correctly to the {0} object.";
    public static string InternalErrorExceptionDetailUnknownEnumerationValue => "Unknown or unexpected {0} enumeration value of '{1}'.";
    public static string InternalErrorExceptionTitle => "Internal Error";
    public static string MetadataExceptionDetailMissingMetadata => "{0} has missing {1} metadata. Ensure metadata is configured correctly for the respective domain/schema.";
    public static string MetadataExceptionTitle => "Metadata Error";
    public static string ObjectDisposedExceptionDetailDocumentContext => "Cannot access a disposed object. A common cause of this error is disposing a document context that was resolved from dependency injection and then later trying to use the same document context instance elsewhere in your application. This may occur is you are calling Dispose() on the document context, or wrapping the document context in a using statement.If you are using dependency injection, you should let the dependency injection container take care of disposing document context instances.";
    #endregion
}