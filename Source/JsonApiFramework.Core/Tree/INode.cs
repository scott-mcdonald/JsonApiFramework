using System.Collections.Generic;

namespace JsonApiFramework.Tree
{
    /// <summary>
    /// Abstracts queryable (non-mutating) access to a node within a 1-N tree.
    /// </summary>
    public interface INode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the name of this node.</summary>
        string Name { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Returns a collection of the attributes for this node.</summary>
        IEnumerable<NodeAttribute> Attributes();

        /// <summary>
        /// Returns a collection of the direct child nodes for this node, in document order.
        /// </summary>
        IEnumerable<INode> Nodes();

        /// <summary>
        /// Returns a collection starting with this node followed by the direct child nodes for this node, in document order.
        /// </summary>
        IEnumerable<INode> NodesIncludeSelf();

        /// <summary>
        /// Returns a collection of the descendant nodes for this node, in document order.
        /// </summary>
        IEnumerable<INode> DescendantNodes();

        /// <summary>
        /// Returns a collection starting with this node followed by the descendant nodes for this node, in document order.
        /// </summary>
        IEnumerable<INode> DescendantNodesIncludeSelf();

        /// <summary>
        /// Create a string representation of this 1-N object tree.
        /// </summary>
        string ToTreeString();
        #endregion
    }
}