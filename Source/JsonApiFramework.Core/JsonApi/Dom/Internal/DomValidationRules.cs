// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal class DomValidationRules : IDomValidationRules
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomValidationRules(
            IEnumerable<IDomValidationRule> domDocumentValidationRules,
            IEnumerable<IDomValidationRule> domResourceValidationRules,
            IEnumerable<IDomValidationRule> domRelationshipValidationRules,
            IEnumerable<IDomValidationRule> domLinkValidationRules)
        {
            var domDocumentValidationRulesDictionary = CreateDomValidationRulesDictionary(domDocumentValidationRules);
            var domResourceValidationRulesDictionary = CreateDomValidationRulesDictionary(domResourceValidationRules);
            var domRelationshipValidationRulesDictionary = CreateDomValidationRulesDictionary(domRelationshipValidationRules);
            var domLinkValidationRulesDictionary = CreateDomValidationRulesDictionary(domLinkValidationRules);

            this.DomDocumentValidationRulesDictionary = domDocumentValidationRulesDictionary;
            this.DomResourceValidationRulesDictionary = domResourceValidationRulesDictionary;
            this.DomRelationshipValidationRulesDictionary = domRelationshipValidationRulesDictionary;
            this.DomLinkValidationRulesDictionary = domLinkValidationRulesDictionary;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDomValidationRules Implementation
        public IEnumerable<IDomValidationRule<TContext>> DomDocumentValidationRules<TContext>()
        {
            var domDocumentValidationRules = GetDomValidationRules<TContext>(this.DomDocumentValidationRulesDictionary);
            return domDocumentValidationRules;
        }

        public IEnumerable<IDomValidationRule<TContext>> DomResourceValidationRules<TContext>()
        {
            var domResourceValidationRules = GetDomValidationRules<TContext>(this.DomResourceValidationRulesDictionary);
            return domResourceValidationRules;
        }

        public IEnumerable<IDomValidationRule<TContext>> DomRelationshipValidationRules<TContext>()
        {
            var domRelationshipValidationRules = GetDomValidationRules<TContext>(this.DomRelationshipValidationRulesDictionary);
            return domRelationshipValidationRules;
        }

        public IEnumerable<IDomValidationRule<TContext>> DomLinkValidationRules<TContext>()
        {
            var domLinkValidationRules = GetDomValidationRules<TContext>(this.DomLinkValidationRulesDictionary);
            return domLinkValidationRules;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IReadOnlyDictionary<Type, IEnumerable<IDomValidationRule>> DomDocumentValidationRulesDictionary { get; }
        private IReadOnlyDictionary<Type, IEnumerable<IDomValidationRule>> DomResourceValidationRulesDictionary { get; }
        private IReadOnlyDictionary<Type, IEnumerable<IDomValidationRule>> DomRelationshipValidationRulesDictionary { get; }
        private IReadOnlyDictionary<Type, IEnumerable<IDomValidationRule>> DomLinkValidationRulesDictionary { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static IEnumerable<IDomValidationRule<TContext>> GetDomValidationRules<TContext>(IReadOnlyDictionary<Type, IEnumerable<IDomValidationRule>> domValidationRulesDictionary)
        {
            Contract.Requires(domValidationRulesDictionary != null);
            
            var contextType = typeof(TContext);
            return domValidationRulesDictionary.TryGetValue(contextType, out var domValidationRules)
    ? domValidationRules.Cast<IDomValidationRule<TContext>>()
    : Enumerable.Empty<IDomValidationRule<TContext>>();
        }

        private static Dictionary<Type, IEnumerable<IDomValidationRule>> CreateDomValidationRulesDictionary(IEnumerable<IDomValidationRule> domValidationRules)
        {
            var domValidationRulesDictionary = new Dictionary<Type, IEnumerable<IDomValidationRule>>();
            foreach (var domValidationRulesGroup in domValidationRules.EmptyIfNull().GroupBy(x => x.ContextType))
            {
                var contextType = domValidationRulesGroup.Key;
                var domValidationRulesCollection = domValidationRulesGroup.ToList();
                domValidationRulesDictionary.Add(contextType, domValidationRulesCollection);
            }
            return domValidationRulesDictionary;
        }
        #endregion
    }
}