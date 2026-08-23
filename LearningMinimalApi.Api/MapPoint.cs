using System.Reflection;

namespace LearningMinimalApi.Api;

public class MapPoint
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

  //  public static bool TryParse(string? value, out MapPoint? point)
  //  {
		//try
		//{
		//	var splitValue = value?.Split(',').Select(double.Parse).ToArray();
		//	point = new MapPoint
		//	{
		//		Latitude = splitValue?[0] ?? 0,
		//		Longitude = splitValue?[1] ?? 0
		//	};
		//	return true;
		//}
		//catch (Exception)
		//{
		//	point = default;
		//	return false;
		//}
  //  }

	public static async ValueTask<MapPoint?> BindAsync(HttpContext context, ParameterInfo parameterInfo)
	{
		var rawRequestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();

        try
        {
            var splitValue = rawRequestBody?.Split(',').Select(double.Parse).ToArray();
            return new MapPoint
            {
                Latitude = splitValue?[0] ?? 0,
                Longitude = splitValue?[1] ?? 0
            };
        }
        catch (Exception)
        {
			return null;
        }
    }
}
