using BigO.Core.Interfaces;
using BigO.Services.Analysis;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Services
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddBigOServices(this IServiceCollection services)
		{
			services.AddScoped<ILexicalAnalyzer, LexicalAnalyzer>();
			return services;
		}
	}
}
