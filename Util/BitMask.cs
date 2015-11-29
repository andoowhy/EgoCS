using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class BitMask : object
{
	private uint[] _bytes;
    private static int _byteWidth = 32;

	public BitMask( int size )
	{
        _bytes = new uint[ ( ( size - 1 ) / _byteWidth ) + 1 ];
	}

    public BitMask( BitMask mask )
    {
        _bytes = new uint[mask._bytes.Length];
        for( var i = 0; i < mask._bytes.Length; i++ )
        {
            _bytes[i] = mask._bytes[i];
        }
    }

    private static bool _isEqual( BitMask m1, BitMask m2 )
    {
        // BitMasks need to be the same size
        if( m1._bytes.Length != m2._bytes.Length ) return false;

        // Compare all elements in each BitMask's _byte array
        for( int i = 0; i < m1._bytes.Length; i++ )
        {
            if( m1._bytes[i] != m2._bytes[i] )
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals( object obj )
    {
        // If parameter is null return false.
        if( null == obj ) return false;

        // If parameter cannot be cast to BitMask return false.
        BitMask m = obj as BitMask;
        if( null == (object)m ) return false;

        return _isEqual( this, m );
    }

    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;
            hash = hash * 23 + _bytes.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==( BitMask m1, BitMask m2 )
    {
        return _isEqual( m1, m2 );
    }

    public static bool operator !=( BitMask m1, BitMask m2 )
    {
        return !( _isEqual( m1, m2 ) );
    }

    public BitMask And( BitMask mask )
    {
        Debug.Assert( _bytes.Length == mask._bytes.Length, "BitMasks must be the same size" );
        for( var i = 0; i < mask._bytes.Length; i++ )
        {
            _bytes[i] = _bytes[i] & mask._bytes[i];
        }
        return this;
    }

    public BitMask Or( BitMask mask )
    {
        Debug.Assert( _bytes.Length == mask._bytes.Length, "BitMasks must be the same size" );
        for( var i = 0; i < mask._bytes.Length; i++ )
        {
            _bytes[i] = _bytes[i] | mask._bytes[i];
        }
        return this;
    }

    public BitMask Xor( BitMask mask )
    {
        Debug.Assert( _bytes.Length == mask._bytes.Length, "BitMasks must be the same size" );
        for( var i = 0; i < mask._bytes.Length; i++ )
        {
            _bytes[i] = _bytes[i] ^ mask._bytes[i];
        }
        return this;
    }

    public BitMask Not()
    {
        for( var i = 0; i < _bytes.Length; i++ )
        {
            _bytes[i] = ~_bytes[i];
        }
        return this;
    }

    public void SetAll( bool b )
    {
        var num = b ? uint.MaxValue : 0u;
        for( var i = 0; i < _bytes.Length; i++ )
        {
            _bytes[i] = num;
        }
    }

    public bool this[ int index ]
    {
        get
        {
            uint bytesIndex = (uint)( index / _byteWidth );
            int bitIndex = index % _byteWidth;
            uint b = ( _bytes[bytesIndex] >> bitIndex ) & 1u;
            return Convert.ToBoolean( b );
        }
        set
        {
            uint bytesIndex = (uint)( index / _byteWidth );
            int bitIndex = index % _byteWidth;
            uint b =  1u << (int)bitIndex ;
            if( value )
            {
                _bytes[bytesIndex] = _bytes[bytesIndex] | b;
            }
            else
            {
                _bytes[bytesIndex] = _bytes[bytesIndex] & ( ~b );
            }
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder( _bytes.Length * (int)_byteWidth );
        for( int i = _bytes.Length - 1; i >= 0; i-- )
        {
            sb.Append( Convert.ToString( _bytes[i], 2 ).PadLeft( (int)_byteWidth, '0' ) );
        }
        return sb.ToString();
    }

}
