
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using AutoMapper;
using LeaveManagementSystem.Web.Services;

public class LeaveTypesController(ILeaveTypeService leaveTypeService) : Controller
{
   
    private const string NameExistValidationMessage = "This leave type already exist in the database";

  private readonly ILeaveTypeService _leaveTypesService = leaveTypeService;

    // GET: LEAVETYPES
    public async Task<IActionResult> Index()    
    {
     
        var viewData = await _leaveTypesService.GetAll();
        return View(viewData);
    }

    // GET: LEAVETYPES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _leaveTypesService.GetDetails<LeaveTypeReadOnlyVM>(id.Value);
        if (leavetype == null)
        {
            return NotFound();
        }

        return View(leavetype);
    }

    // GET: LEAVETYPES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LEAVETYPES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveTypeCreateVM leavetypeCreate)
    {
        if (await _leaveTypesService.CheckIfLeaveTypeNameExists(leavetypeCreate.Name))
        {
            ModelState.AddModelError(nameof(leavetypeCreate.Name), NameExistValidationMessage);
        }
        if (ModelState.IsValid)
        {
            var leaveType =  _leaveTypesService.Create(leavetypeCreate);
            return RedirectToAction(nameof(Index));
        }
        return View(leavetypeCreate);
    }

   

    // GET: LEAVETYPES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _leaveTypesService.GetDetails <LeaveTypeEditVM>(id.Value);

        if (leavetype == null)
        {
            return NotFound();
        }
    
        return View(leavetype);
    }

    // POST: LEAVETYPES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,NumberOfDays")] LeaveTypeEditVM leavetypeEdit)
    {
        if (id != leavetypeEdit.Id)
        {
            return NotFound();
        }

        if (await _leaveTypesService.CheckIfLeaveTypeNameExistsForEdit(leavetypeEdit))
        {
            ModelState.AddModelError(nameof(leavetypeEdit.Name), NameExistValidationMessage);
        }
        if (ModelState.IsValid)
        {
            try
            {
                 await _leaveTypesService.Edit(leavetypeEdit);
                    
               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_leaveTypesService.LeaveTypeExists(leavetypeEdit.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(leavetypeEdit);
    }

    // GET: LEAVETYPES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _leaveTypesService.GetDetails<LeaveTypeReadOnlyVM>(id.Value);
          
        if (leavetype == null)
        {
            return NotFound();
        }
     
        return View(leavetype);
    }

    // POST: LEAVETYPES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {

        if (id != null)
        {

        await _leaveTypesService.Delete(id.Value);
        }



        return RedirectToAction(nameof(Index));
    }

 
}
