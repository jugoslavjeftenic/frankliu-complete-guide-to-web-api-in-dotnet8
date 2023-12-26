
namespace T05_MVCProject.Data
{
	public interface IWebApiExecuter
	{
		Task<T?> InvokeGet<T>(string relativeUrl);
		Task<T?> InvokePost<T>(string relativeUrl, T obj);
	}
}