namespace EgoCS
{
    using UnityEngine;
    using UnityEngine.Assertions;
    using System;
    using System.Collections.Generic;

    public static class ComponentUtils
    {
        public static Dictionary< Type, int > types { get; private set; }

        public static void Init()
        {
            types = new Dictionary< Type, int >();
            Init( types );
        }

        private static void Init( Dictionary< Type, int > allComponentTypes )
        {
            foreach( var assembly in AppDomain.CurrentDomain.GetAssemblies() )
            {
                foreach( var type in assembly.GetTypes() )
                {
                    if( !type.IsSubclassOf( typeof( Component ) ) )
                    {
                        continue;
                    }

                    if( type.IsAbstract )
                    {
                        continue;
                    }

                    if( type.IsGenericType && !type.IsConstructedGenericType )
                    {
                        continue;
                    }

                    allComponentTypes.Add( type, allComponentTypes.Count );
                }
            }
        }

        public static int GetCount()
        {
            return types.Count;
        }

        public static int Get< TComponent >()
            where TComponent : Component
        {
            return types[ typeof( TComponent ) ];
        }

        public static int Get( Type type )
        {
            Assert.IsTrue( type.IsSubclassOf( typeof( Component ) ), "Only get IDs of Component Types!" );
            return types[ type ];
        }
    }
}