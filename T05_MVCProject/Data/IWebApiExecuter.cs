
namespace T05_MVCProject.Data
{
	public interface IWebApiExecuter
	{
		Task<T?> InvokePost<T>(string relativeUrl, T obj);
		Task<T?> InvokeGet<T>(string relativeUrl);
		Task InvokePut<T>(string relativeUrl, T obj);
		Task InvokeDelete(string relativeUrl);
	}
}