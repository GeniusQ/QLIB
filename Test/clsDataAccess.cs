using System;
using System.Data;
using NUnit.Framework;
using NUnit;

namespace CommandClassLibrary.Test
{
	/// <summary>
	/// clsDataAccess µƒ≤‚ ‘¿‡
	/// </summary>
	[TestFixture]
  public class clsDataAccess
	{
    private Data.clsDataAccess _da = null;
    string str = "server=.;uid=ktvDB;pwd=ktvDB;database=ktvDB;";
    
		public clsDataAccess()
		{
      _da =  new CommandClassLibrary.Data.clsDataAccess(str);
		}

    [Test]
    public void GetDataTest()
    {
      Assert.AreEqual("aaa","aaa");
    }

    [Test]
    public void IsSame()
    {
      Data.clsDataAccess da = new CommandClassLibrary.Data.clsDataAccess(str);
      Assert.AreSame(da,_da);
    }
	}
}
