using System.Collections.Generic;

namespace EgoCS
{
    public abstract class Constraint
    {
        protected BitMask mask;

        public ParentConstraint parentConstraint = null;
        public Constraint childConstraint = null;

        public EgoComponent currentEgoComponent;

        public Dictionary< EgoComponent, Bundle > rootBundles = new Dictionary< EgoComponent, Bundle >();
        public Dictionary< EgoComponent, Dictionary< EgoComponent, Bundle > > childBundles = new Dictionary< EgoComponent, Dictionary< EgoComponent, Bundle > >();

        public void CreateMask( BitMaskPool bitMaskPool )
        {
            mask = bitMaskPool.Get();

            childConstraint?.CreateMask( bitMaskPool );
        }

        public abstract void InitMask();

        protected bool CanUpdate( EgoComponent egoComponent, BitMaskPool bitMaskPool )
        {
            var comparisonMask = bitMaskPool.Get();
            comparisonMask.Set( egoComponent.mask ).And( mask );

            var result = comparisonMask == mask;
            bitMaskPool.Return( comparisonMask );
            return result;
        }

        /// <summary>
        /// Create a Bundle for the given egoComponent outright
        /// Assumes the EgoComponent has the required components
        /// </summary>
        /// <param name="egoComponent"></param>
        protected abstract Bundle CreateBundle( EgoComponent egoComponent );

        public abstract void CreateConstraintCallbacks( EgoCS egoCS );

        /// <summary>
        /// Try to create Bundles for the given EgoComponent, and all of its children (recursively)
        /// </summary>
        /// <param name="egoComponent"></param>
        public void CreateBundles( EgoComponent egoComponent, BitMaskPool bitMaskPool )
        {
            if( egoComponent == null ) { return; }

            // Only Create Bundles from the youngest Constraint
            if( childConstraint != null )
            {
                childConstraint.CreateBundles( egoComponent, bitMaskPool );
            }
            else
            {
                // Recurse to All Children EgoComponents
                {
                    var egoTransform = egoComponent.transform;
                    var childCount = egoComponent.transform.childCount;
                    for( var i = 0; i < childCount; i++ )
                    {
                        CreateBundles( egoTransform.GetChild( i ).GetComponent< EgoComponent >(), bitMaskPool );
                    }
                }

                // Setup Constraint & EgoComponent Ancestries
                // Early exit if the given EgoComponent and ancestors can't satisfy all constraints
                var tuples = new List< ( Constraint, EgoComponent ) >();
                {
                    var currentConstraint = this;
                    var currentEgoComponent = egoComponent;

                    while( currentConstraint != null )
                    {
                        if( currentEgoComponent == null || !currentConstraint.CanUpdate( currentEgoComponent, bitMaskPool ) ) { return; }

                        tuples.Add( ( currentConstraint, currentEgoComponent ) );
                        currentConstraint = currentConstraint.parentConstraint;
                        currentEgoComponent = currentEgoComponent.parent;
                    }
                }

                var endIndex = tuples.Count - 1;
                var topConstraint = tuples[ endIndex ].Item1;
                var topEgoComponent = tuples[ endIndex ].Item2;

                topConstraint.rootBundles[ topEgoComponent ] = topConstraint.CreateBundle( topEgoComponent );

                for( var i = endIndex; i > 0; i-- )
                {
                    var currentChildConstraint = tuples[ i - 1 ].Item1;
                    var currentChildEgoComponent = tuples[ i - 1 ].Item2;
                    var currentParentConstraint = tuples[ i ].Item1;
                    var currentParentEgoComponent = tuples[ i ].Item2;

                    if( !currentParentConstraint.childBundles.ContainsKey( currentParentEgoComponent ) )
                    {
                        currentParentConstraint.childBundles[ currentParentEgoComponent ] = new Dictionary< EgoComponent, Bundle >();
                    }

                    currentParentConstraint.childBundles[ currentParentEgoComponent ][ currentChildEgoComponent ] = currentChildConstraint.CreateBundle( currentChildEgoComponent );
                }
            }
        }

        public void RemoveBundles( EgoComponent egoComponent )
        {
            RemoveChildBundles( this, egoComponent );
            RemoveParentBundles( this, egoComponent );
            rootBundles.Remove( egoComponent );
        }

        public void SetParent( EgoComponent newParent, EgoComponent child, bool worldPositionStays, BitMaskPool bitMaskPool )
        {
            if( child.parent == newParent ) { return; }

            var currentParent = child.parent;

            if( currentParent != null )
            {
                RemoveBundles( currentParent );
            }

            if( newParent != null )
            {
                child.transform.SetParent( newParent.transform, worldPositionStays );
            }
            else
            {
                child.transform.SetParent( null, worldPositionStays );
            }

            CreateBundles( child, bitMaskPool );
        }

        void RemoveChildBundles( Constraint constraint, EgoComponent egoComponent )
        {
            if( constraint.childBundles.ContainsKey( egoComponent ) )
            {
                if( constraint.childConstraint != null )
                {
                    var lookup = constraint.childBundles[ egoComponent ];
                    foreach( var childEgoComponent in lookup.Keys )
                    {
                        RemoveChildBundles( constraint.childConstraint, childEgoComponent );
                    }

                    lookup.Clear();
                }
                else
                {
                    constraint.childBundles.Remove( egoComponent );
                }
            }
        }

        void RemoveParentBundles( Constraint childConstraint, EgoComponent childEgoComponent )
        {
            var parentConstraint = childConstraint.parentConstraint;
            var parentEgoComponent = childEgoComponent.parent;

            if( parentConstraint != null && parentEgoComponent != null && parentConstraint.childBundles.ContainsKey( parentEgoComponent ) )
            {
                parentConstraint.childBundles[ parentEgoComponent ].Remove( childEgoComponent );
                if( parentConstraint.childBundles[ parentEgoComponent ].Count <= 0 )
                {
                    parentConstraint.childBundles.Remove( parentEgoComponent );
                }

                RemoveParentBundles( parentConstraint, parentEgoComponent );
            }
        }

        /// <summary>
        /// Get the lookup based on the parent constraint's current bundle (if applicable)
        /// </summary>
        /// <typeparam name="B"></typeparam>
        /// <param name="defaultLookup"></param>
        public Dictionary< EgoComponent, B > GetLookup< B >( Dictionary< EgoComponent, B > defaultLookup )
            where B : Bundle
        {
            if( parentConstraint != null && parentConstraint.currentEgoComponent != null )
            {
                return parentConstraint.childBundles[ parentConstraint.currentEgoComponent ] as Dictionary< EgoComponent, B >;
            }
            else
            {
                return defaultLookup;
            }
        }
    }
}