
namespace T031_MVCProject.Data
{
	public interface IWebApiExecutor
	{
		Task<T?> InvokeGet<T>(string relativeUrl);
	}
}