using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GetFee
/// </summary>
public class GetFee
{
	public GetFee()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static int GetFarsiSMSFee(long Credit)
    {
        if (Credit == 0)
        {
            return 0;
        }
        else if (500000 > Credit && Credit > 0)
        {
            return 130;
        }
        else if (500000 <= Credit && Credit < 1500000)
        {
            return 130;
        }
        else if (1500000 <= Credit && Credit < 4000000)
        {
            return 140;
        }
        else if (4000000 <= Credit && Credit < 15000000)
        {
            return 129;
        }
        else if (15000000 <= Credit && Credit < 30000000)
        {
            return 120;
        }
        else if (30000000 <= Credit && Credit < 65000000)
        {
            return 112;
        }
        else if (65000000 <= Credit)
        {
            return 105;
        }

        return 0;
    }
    public static int GetEnglishSMSFee(long Credit)
    {
        if (Credit == 0)
        {
            return 0;
        }
        else if (500000 > Credit && Credit > 0)
        {
            return 255;
        }
        else if (500000 <= Credit && Credit < 1500000)
        {
            return 255;
        }
        else if (1500000 <= Credit && Credit < 4000000)
        {
            return 265;
        }
        else if (4000000 <= Credit && Credit < 15000000)
        {
            return 242;
        }
        else if (15000000 <= Credit && Credit < 30000000)
        {
            return 233;
        }
        else if (30000000 <= Credit && Credit < 65000000)
        {
            return 225;
        }
        else if (65000000 <= Credit)
        {
            return 220;
        }

        return 0;
    }
}