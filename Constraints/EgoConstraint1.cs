using UnityEngine;
using System.Collections.Generic;
using System;

public class EgoConstraint<C1> : EgoConstraint
    where C1 : Component
{
    public EgoConstraint()
    {
        _mask[ComponentIDs.Get( typeof( C1 ) )] = true;
        _mask[ComponentIDs.Get( typeof( EgoComponent ) )] = true;

        // Attach built-in Event Handlers
        //EgoEvents<AddedGameObject>.AddHandler( Handle );
        //EgoEvents<AddedComponent<C1>>.AddHandler( Handle );
        //EgoEvents<DestroyedComponent<C1>>.AddHandler( Handle );
    }

    /// <summary>
    /// Create a Bundle for the given egoComponent outright
    /// Assumes the EgoComponent has the required components
    /// </summary>
    /// <param name="egoComponent"></param>
    protected override EgoBundle CreateBundle( EgoComponent egoComponent )
    {
        return new EgoBundle<C1>( egoComponent.GetComponent<C1>() );
    }

    public delegate void ForEachGameObjectDelegate( EgoComponent egoComponent, C1 component1 );

    public void ForEachGameObject( ForEachGameObjectDelegate callback )
    {
        var lookup = GetLookup( rootBundles );
        foreach( var kvp in lookup )
        {
            currentEgoComponent = kvp.Key;
            var bundle = kvp.Value as EgoBundle<C1>;
            callback( currentEgoComponent, bundle.component1 );
        }
    }
}
