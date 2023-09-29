using System;

[Serializable()]
public class BFCException : System.Exception
{
    public BFCException() : base() { }
    public BFCException(int err) : base("BFC_ERROR_" + err) { }
    public BFCException(string message) : base(message) { }
    public BFCException(string message, Exception inner) : base(message, inner) { }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client. 
    protected BFCException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) { }
}
