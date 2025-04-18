using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePagedResult<T>
                    (IGenericRepository<T> repo, ISpecification<T> specParams,
                    int pageIndex, int pageSize) where T : BaseEntity
    {
        var items = await repo.ListAsync(specParams);
        var count = await repo.CountAsync(specParams);

        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);
        return Ok(pagination);

    }
}
