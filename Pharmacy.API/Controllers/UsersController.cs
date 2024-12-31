using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Presentation.Controllers;

[Authorize(Roles = "Admin, Manager")]
public class UsersController : ApiBaseController
{
    private readonly IUserService _service;
    public UsersController(IUserService authService) => _service = authService;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        HandleResult(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) =>
        HandleResult(await _service.GetByIdAsync(id));


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RegisterDTO user) =>
        HandleResult(await _service.UpdateAsync(id, user));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
        HandleResult(await _service.DeleteAsync(id));
}
