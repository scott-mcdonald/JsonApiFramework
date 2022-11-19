// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.Server;
public static class ServerErrorStrings
{
    #region Public Properties
    public static string DocumentBuildExceptionDetailBuildDocumentWithCollectionOfObjects => "Unable to build '{0}' for a single json:api document with a collection of '{0}' objects.";
    public static string DocumentBuildExceptionDetailBuildIncorrectResourceLinkage => "Unable to build json:api '{0}' relationship[rel ={1}], the '{0}' resource linkage was not built correctly in the json:api document.";
    public static string DocumentBuildExceptionDetailBuildLinkCollectionCountMismatch => "Unable to build '{0}' for a collection [count={ 1}] of json:api links[rel ={2}] with a collection [count={3}] of '{0}' objects, collection counts mismatch. Make sure the collection counts are the same.";
    public static string DocumentBuildExceptionDetailBuildLinkWithCollectionOfObjects => "Unable to build '{0}' for a single json:api link[rel ={1}] with a collection of '{0}' objects.";
    public static string DocumentBuildExceptionDetailBuildNonStandardDocumentLink => "Unable to build a non-standard json:api link[rel ={0}] for json:api document.Create a derived hypermedia assembler to create this non - standard json:api document link[rel ={ 0}].";
    public static string DocumentBuildExceptionDetailBuildNonStandardResourceLink => "Unable to build a non-standard json:api link[rel ={0}] for CLR resource[type ={ 1}] . Create a derived hypermedia assembler to create this non-standard json:api resource link [rel={0}].";
    public static string DocumentBuildExceptionDetailBuildRelationshipCollectionCountMismatch => "Unable to build '{0}' for a collection [count={1}] of json:api relationships[rel ={2}] with a collection [count={3}] of '{0}' objects, collection counts mismatch. Make sure the collection counts are the same.";
    public static string DocumentBuildExceptionDetailBuildRelationshipCollectionWithSingleObject => "Unable to build '{0}' for a collection of json:api relationship[rel ={1}] with a single '{0}' object.";
    public static string DocumentBuildExceptionDetailBuildResourceRelationshipCardinalityMismatch => "Unable to create json:api relationship[rel ={0}] from CLR resource [type ={ 1} to CLR resource [type={2}], relationship cardinality mismatch between built document [value={3}] and service model schema [value={4}].";
    public static string DomExceptionDetailReadWriteNodeHasUnexpectedReadOnlyChildNode => "The read - write DOM node[type ={0}] has unexpected read-only child DOM node [type={1}].";
    public static string InternalErrorExceptionDetailCollectionCountMismatch => "Unexpected collection count mismatch between {0} [count={1}] and { 2} [count={3}].";
    public static string InternalErrorExceptionDetailExceededHypermediaPathSizeLimit => "Unable to get a hypermedia assembler, exceeded internal size limit of '{0}' for hypermedia paths.";
    public static string InternalErrorExceptionDetailInvalidGetClrResourceTypeForHypermediaPath => "Unable to get the CLR resource type for  hypermedia path[type ={ 0}].";
    public static string InternalErrorExceptionDetailUnknownHypermediaAssembler => "Unable to initialize the hypermedia assembler registry, unknown hypermedia assembler [name={0}] parameter.";
    #endregion
}