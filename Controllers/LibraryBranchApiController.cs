using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryBranchApiController : ControllerBase
    {
        private readonly LibraryBranchService _libraryBranchService;

        public LibraryBranchApiController(LibraryBranchService libraryBranchService)
        {
            _libraryBranchService = libraryBranchService;
        }

        /// <summary>
        /// 获取所有分支
        /// </summary>
        /// <returns>所有分支列表</returns>
        [HttpGet]
        public IActionResult GetAllLibraryBranches()
        {
            var branches = _libraryBranchService.GetAllLibraryBranches();
            return Ok(branches);
        }

        /// <summary>
        /// 根据 ID 获取分支信息
        /// </summary>
        /// <param name="id">分支的唯一标识</param>
        /// <returns>指定 ID 的分支信息</returns>
        [HttpGet("{id}")]
        public IActionResult GetLibraryBranchById(int id)
        {
            var branch = _libraryBranchService.GetLibraryBranchById(id);
            if (branch == null)
            {
                return NotFound(new { Message = $"Library branch with ID {id} not found." });
            }
            return Ok(branch);
        }

        /// <summary>
        /// 创建新分支
        /// </summary>
        /// <param name="branch">分支数据</param>
        /// <returns>创建成功的分支信息</returns>
        [HttpPost]
        public IActionResult CreateLibraryBranch([FromBody] LibraryBranch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _libraryBranchService.CreateLibraryBranch(branch);
            return CreatedAtAction(nameof(GetLibraryBranchById), new { id = branch.LibraryBranchId }, branch);
        }

        /// <summary>
        /// 更新分支信息
        /// </summary>
        /// <param name="id">分支的唯一标识</param>
        /// <param name="branch">更新后的分支数据</param>
        /// <returns>无内容表示更新成功</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateLibraryBranch(int id, [FromBody] LibraryBranch branch)
        {
            if (id != branch.LibraryBranchId)
            {
                return BadRequest(new { Message = "ID mismatch in request body and URL." });
            }

            var existingBranch = _libraryBranchService.GetLibraryBranchById(id);
            if (existingBranch == null)
            {
                return NotFound(new { Message = $"Library branch with ID {id} not found." });
            }

            _libraryBranchService.UpdateLibraryBranch(branch);
            return NoContent();
        }

        /// <summary>
        /// 删除分支
        /// </summary>
        /// <param name="id">分支的唯一标识</param>
        /// <returns>无内容表示删除成功</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteLibraryBranch(int id)
        {
            var branch = _libraryBranchService.GetLibraryBranchById(id);
            if (branch == null)
            {
                return NotFound(new { Message = $"Library branch with ID {id} not found." });
            }

            _libraryBranchService.DeleteLibraryBranch(id);
            return NoContent();
        }
    }
}
