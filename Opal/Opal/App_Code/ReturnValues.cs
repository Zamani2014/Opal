using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReturnValues
/// </summary>
public class ReturnValues
{
	public ReturnValues()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _body;
    private string _errorResult;
    private string _recipientNumber;
    private string _senderNumber;
    private string _date;

    public string body 
    {
        get { return _body; } 
        set{ _body = value; }
    }
    public string errorResult 
    {
        get { return _errorResult; }
        set { _errorResult = value; }
    }
    public string recipientNumber 
    {
        get { return _recipientNumber; }
        set { _recipientNumber = value; }
    }
    public string senderNumber 
    {
        get { return _senderNumber; }
        set { _senderNumber = value; }
    }
    public string date
    {
        get { return _date; }
        set { _date = value; }
    }
}