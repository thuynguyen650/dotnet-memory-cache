using CacheMemoryExample.Models;
using CacheMemoryExample.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CacheMemoryExample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployerController : ControllerBase
{
    private const string employeeListCacheKey = "employeeList";
    private readonly IDataRepository<Employee> _dataRepository;
    private IMemoryCache _cache;
    private ILogger<EmployerController> _logger;

    public EmployerController(IDataRepository<Employee> dataRepository,
        IMemoryCache cache,
        ILogger<EmployerController> logger)
    {
        _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        _cache = cache ?? throw new ArgumentNullException(nameof(dataRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        _logger.Log(LogLevel.Information, "Trying to fetch the list of employees from cache.");

        if (_cache.TryGetValue(employeeListCacheKey, out IEnumerable<Employee> employees))
        {
            _logger.Log(LogLevel.Information, "Employee list found in cache");
        } else
        {
            _logger.Log(LogLevel.Information, "Employee list not found in cache. Fetching from database.");

            employees = _dataRepository.GetAll();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1024);

            _cache.Set(employeeListCacheKey, employees, cacheEntryOptions);
        }

        return Ok(employees);
    }
}
