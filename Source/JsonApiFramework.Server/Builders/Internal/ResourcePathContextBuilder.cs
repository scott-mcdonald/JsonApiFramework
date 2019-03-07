// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Reflection;
using JsonApiFramework.Server.Hypermedia;
using JsonApiFramework.Server.Hypermedia.Internal;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Internal
{
    internal class ResourcePathContextBuilder<TParentBuilder> : IResourcePathContextBuilder<TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPathContextBuilder Implementation
        IPathContextBuilder IPathContextBuilder.AddPath(Action<IPathContextBuilder, object> action)
        {
            return this.AddPath(action);
        }

        IPathContextBuilder IPathContextBuilder.AddPath<TPath>(TPath clrResource, string rel, bool includePath)
        {
            return this.AddPath(clrResource, rel, includePath);
        }

        IPathContextBuilder IPathContextBuilder.AddPath<TPath, TResourceId>(TResourceId clrResourceId, string rel, bool includePath)
        {
            return this.AddPath<TPath, TResourceId>(clrResourceId, rel, includePath);
        }

        IPathContextBuilder IPathContextBuilder.AddPath(string pathSegment, bool includePath)
        {
            return this.AddPath(pathSegment, includePath);
        }
        #endregion

        #region IResourcePathContextBuilder<TParentBuilder> Implementation
        public IResourcePathContextBuilder<TParentBuilder> AddPath(Action<IPathContextBuilder, object> action)
        {
            Contract.Requires(action != null);

            _addPathActions = _addPathActions ?? new List<Action<IPathContextBuilder, object>>();
            _addPathActions.Add(action);
            return this;
        }

        public IResourcePathContextBuilder<TParentBuilder> AddPath<TPath>(TPath clrResource, string rel, bool includePath)
            where TPath : class
        {
            Contract.Requires(clrResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            if (includePath == false)
                return this;

            var serviceModel    = this.ServiceModel;
            var clrResourceType = typeof(TPath);
            var resourceType    = serviceModel.GetResourceType(clrResourceType);
            var apiResourceId   = resourceType.GetApiId(clrResource);

            return this.AddPath(resourceType, clrResourceType, apiResourceId, rel);
        }

        public IResourcePathContextBuilder<TParentBuilder> AddPath<TPath, TResourceId>(TResourceId clrResourceId, string rel, bool includePath)
            where TPath : class
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            if (includePath == false)
                return this;

            var clrResourceType = typeof(TPath);
            var resourceType    = this.ServiceModel.GetResourceType(clrResourceType);
            var apiResourceId   = TypeConverter.Convert<string>(clrResourceId);

            return this.AddPath(resourceType, clrResourceType, apiResourceId, rel);
        }

        public IResourcePathContextBuilder<TParentBuilder> AddPath(string pathSegment, bool includePath)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(pathSegment) == false);

            if (includePath == false)
                return this;

            this.EnsureBasePathsExist();

            AddNonHypermediaPath(_resourceCanonicalBasePath, pathSegment);
            AddNonHypermediaPath(_resourceSelfBasePath,      pathSegment);

            return this;
        }

        public TParentBuilder PathsEnd()
        {
            return this.ParentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ResourcePathContextBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, Type clrResourceType)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(clrResourceType != null);

            this.ParentBuilder   = parentBuilder;
            this.ServiceModel    = serviceModel;
            this.ClrResourceType = clrResourceType;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Methods
        internal IResourcePathContext CreateResourcePathContext(object clrResource)
        {
            if (clrResource == null)
                throw new ArgumentNullException(nameof(clrResource), $"CLR resource object can not be null.");

            if (_addPathActions != null)
            {
                foreach (var addPathAction in _addPathActions)
                {
                    addPathAction(this, clrResource);
                }
            }

            this.EnsureBasePathsExist();

            var clrResourceType = this.ClrResourceType;
            var resourceType    = this.ServiceModel.GetResourceType(clrResourceType);

            var resourceSelfPathParts = CreateResourcePathParts(clrResourceType, resourceType, _resourceSelfBasePath, _resourceSelfPreviousRelationship);
            var resourceSelfBasePath  = resourceSelfPathParts.Item1;
            var resourceSelfPathMode  = resourceSelfPathParts.Item2;

            var resourceCanonicalPathParts = CreateResourcePathParts(clrResourceType, resourceType, _resourceCanonicalBasePath, _resourceCanonicalPreviousRelationship);
            var resourceCanonicalBasePath  = resourceCanonicalPathParts.Item1;
            var resourceCanonicalPathMode  = resourceCanonicalPathParts.Item2;

            var resourcePathContext = new ResourcePathContext(resourceSelfBasePath, resourceSelfPathMode, resourceCanonicalBasePath, resourceCanonicalPathMode);
            return resourcePathContext;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder   { get; }
        private IServiceModel  ServiceModel    { get; }
        private Type           ClrResourceType { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static void AddNonHypermediaPath(ICollection<IHypermediaPath> resourceBasePath, string pathSegment)
        {
            Contract.Requires(resourceBasePath != null);
            Contract.Requires(String.IsNullOrWhiteSpace(pathSegment) == false);

            // If the last hypermedia path is already a NonResourceHypermediaPath
            // object then simply add the new path segment to this existing NonResourceHypermediaPath
            var lastResourceBasePath = resourceBasePath.LastOrDefault();
            if (lastResourceBasePath != null && lastResourceBasePath.IsNonResourcePath())
            {
                var lastNonResourceHypermediaPath = (NonResourceHypermediaPath)lastResourceBasePath;
                lastNonResourceHypermediaPath.AddPathSegment(pathSegment);
            }
            else
            {
                var nonResourceHypermediaPath = new NonResourceHypermediaPath(pathSegment);
                resourceBasePath.Add(nonResourceHypermediaPath);
            }
        }

        private IResourcePathContextBuilder<TParentBuilder> AddPath(IResourceType resourceType, Type clrResourceType, string apiId, string rel)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiId) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.EnsureBasePathsExist();

            var nextRelationship = resourceType.GetRelationshipInfo(rel);

            var nextRelationshipCanonicalRelPathMode = nextRelationship.ToCanonicalRelPathMode;
            if (nextRelationshipCanonicalRelPathMode == RelationshipCanonicalRelPathMode.DropPreviousPathSegments)
            {
                _resourceCanonicalBasePath.Clear();
                _resourceCanonicalPathMode             = default(ResourcePathMode);
                _resourceCanonicalPreviousRelationship = null;
            }
            else
            {
                AddPath(resourceType, clrResourceType, apiId, nextRelationship, ref _resourceCanonicalBasePath, ref _resourceCanonicalPathMode, ref _resourceCanonicalPreviousRelationship);
            }

            AddPath(resourceType, clrResourceType, apiId, nextRelationship, ref _resourceSelfBasePath, ref _resourceSelfPathMode, ref _resourceSelfPreviousRelationship);

            return this;
        }

        private static void AddPath(IResourceType             resourceType,
                                    Type                      clrResourceType,
                                    string                    apiId,
                                    IRelationshipInfo         nextRelationship,
                                    ref List<IHypermediaPath> resourceBasePath,
                                    ref ResourcePathMode      resourcePathMode,
                                    ref IRelationshipInfo     resourcePreviousRelationship)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiId) == false);
            Contract.Requires(nextRelationship != null);
            Contract.Requires(resourceBasePath != null);

            if (resourcePreviousRelationship == null)
            {
                var apiCollectionPathSegment = resourceType.HypermediaInfo.ApiCollectionPathSegment;

                var hypermediaPath = default(IHypermediaPath);
                if (!resourceType.IsSingleton())
                {
                    hypermediaPath   = new ResourceHypermediaPath(clrResourceType, apiCollectionPathSegment, apiId);
                    resourcePathMode = ResourcePathMode.IncludeApiId;
                }
                else
                {
                    hypermediaPath   = new SingletonHypermediaPath(clrResourceType, apiCollectionPathSegment);
                    resourcePathMode = ResourcePathMode.IgnoreApiId;
                }

                resourceBasePath.Add(hypermediaPath);
                resourcePreviousRelationship = nextRelationship;
                return;
            }

            var previousRelationshipCardinality = resourcePreviousRelationship.ToCardinality;
            var apiRelationshipRelPathSegment   = resourcePreviousRelationship.ApiRelPathSegment;
            switch (previousRelationshipCardinality)
            {
                case RelationshipCardinality.ToOne:
                {
                    var toOneResourceHypermediaPath = new ToOneResourceHypermediaPath(clrResourceType, apiRelationshipRelPathSegment);

                    resourceBasePath.Add(toOneResourceHypermediaPath);
                    resourcePathMode = ResourcePathMode.IgnoreApiId;
                }
                    break;

                case RelationshipCardinality.ToMany:
                {
                    var toManyResourceHypermediaPath = new ToManyResourceHypermediaPath(clrResourceType, apiRelationshipRelPathSegment, apiId);

                    resourceBasePath.Add(toManyResourceHypermediaPath);
                    resourcePathMode = ResourcePathMode.IncludeApiId;
                }
                    break;

                default:
                {
                    var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                           .FormatWith(typeof(RelationshipCardinality).Name, previousRelationshipCardinality);
                    throw new InternalErrorException(detail);
                }
            }

            resourcePreviousRelationship = nextRelationship;
        }

        private static Tuple<ICollection<IHypermediaPath>, ResourcePathMode> CreateResourcePathParts(Type clrResourceType, IResourceType resourceType, ICollection<IHypermediaPath> resourceBasePath, IRelationshipInfo resourcePreviousRelationship)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(resourceType != null);

            ResourcePathMode resourcePathMode;

            if (resourcePreviousRelationship == null)
            {
                var apiCollectionPathSegment = resourceType.HypermediaInfo.ApiCollectionPathSegment;

                var hypermediaPath = default(IHypermediaPath);
                if (!resourceType.IsSingleton())
                {
                    hypermediaPath   = new ResourceCollectionHypermediaPath(clrResourceType, apiCollectionPathSegment);
                    resourcePathMode = ResourcePathMode.IncludeApiId;
                }
                else
                {
                    hypermediaPath   = new SingletonHypermediaPath(clrResourceType, apiCollectionPathSegment);
                    resourcePathMode = ResourcePathMode.IgnoreApiId;
                }

                resourceBasePath.Add(hypermediaPath);
            }
            else
            {
                var previousRelationshipCardinality = resourcePreviousRelationship.ToCardinality;
                var apiRelationshipRelPathSegment   = resourcePreviousRelationship.ApiRelPathSegment;
                switch (previousRelationshipCardinality)
                {
                    case RelationshipCardinality.ToOne:
                    {
                        var toOneResourceHypermediaPath = new ToOneResourceHypermediaPath(clrResourceType, apiRelationshipRelPathSegment);
                        resourceBasePath.Add(toOneResourceHypermediaPath);
                        resourcePathMode = ResourcePathMode.IgnoreApiId;
                    }
                        break;

                    case RelationshipCardinality.ToMany:
                    {
                        var toManyResourceHypermediaPath = new ToManyResourceCollectionHypermediaPath(clrResourceType, apiRelationshipRelPathSegment);
                        resourceBasePath.Add(toManyResourceHypermediaPath);
                        resourcePathMode = ResourcePathMode.IncludeApiId;
                    }
                        break;

                    default:
                    {
                        var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                               .FormatWith(typeof(RelationshipCardinality).Name, previousRelationshipCardinality);
                        throw new InternalErrorException(detail);
                    }
                }
            }

            var resourcePathParts = new Tuple<ICollection<IHypermediaPath>, ResourcePathMode>(resourceBasePath, resourcePathMode);
            return resourcePathParts;
        }

        private void EnsureBasePathsExist()
        {
            _resourceCanonicalBasePath = _resourceCanonicalBasePath ?? new List<IHypermediaPath>();
            _resourceSelfBasePath      = _resourceSelfBasePath ?? new List<IHypermediaPath>();
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private List<Action<IPathContextBuilder, object>> _addPathActions;

        private List<IHypermediaPath> _resourceCanonicalBasePath;
        private ResourcePathMode      _resourceCanonicalPathMode;
        private IRelationshipInfo     _resourceCanonicalPreviousRelationship;

        private List<IHypermediaPath> _resourceSelfBasePath;
        private ResourcePathMode      _resourceSelfPathMode;
        private IRelationshipInfo     _resourceSelfPreviousRelationship;
        #endregion
    }
}
