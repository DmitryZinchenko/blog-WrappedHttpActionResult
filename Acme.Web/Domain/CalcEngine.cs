namespace Acme.Web.Domain
{
	public class CalcEngine : ICalcEngine
	{
		public int Calc(int value)
		{
			var ten = 10;
			return ten % value;
		}
	}
}