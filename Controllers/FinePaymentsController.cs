using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinePaymentAPI.Models;

namespace FinePaymentAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinePaymentsController : ControllerBase
{
    private readonly FinePaymentContext _context;

    public FinePaymentsController(FinePaymentContext context)
    {
        _context = context;
    }

    // GET: api/FinePayments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinePayment>>> GetFinePayments()
    {
        List<FinePayment> finePayments;
        try
        {
            finePayments = await (from fp in _context.FinePayments select fp).ToListAsync();
        }
        catch (Exception exception)
        {
            return NotFound();
        }
        return finePayments;
    }

    // GET: api/FinePayments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<FinePayment>> GetFinePayment(long id)
    {
        FinePayment finePayment;
        try
        {
            finePayment = await (from fp in _context.FinePayments where fp.Id == id select fp).FirstAsync<FinePayment>();
        }
        catch (Exception exception)
        {
            return NotFound();
        }
        return finePayment;
    }

    // PUT: api/FinePayments/{id}
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFinePayment(long id, FinePayment finePayment)
    {
        FinePayment newFinePayment;
        try{
            newFinePayment = await (from fp in _context.FinePayments where fp.Id == id select fp).FirstAsync<FinePayment>();
            
            newFinePayment.PaymentCompleted = finePayment.PaymentCompleted;
            newFinePayment.PaymentDate = finePayment.PaymentDate;
            newFinePayment.PaymentReference = finePayment.PaymentReference;

            _context.Entry(newFinePayment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        //catch (DbUpdateConcurrencyException) when (!FinePaymentExists(id))
        catch (Exception exception)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/FinePayments
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<FinePayment>> PostFinePayment(FinePayment finePayment)
    {
        var newfinePayment = new FinePayment
        {
            CaseReference = finePayment.CaseReference,
            OnlineAccountReference = finePayment.OnlineAccountReference,
            Amount = finePayment.Amount,
            PaymentDueDate = finePayment.PaymentDueDate,
            PaymentCompleted = false
        };

        _context.FinePayments.Add(newfinePayment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFinePayment), new { id = finePayment.Id }, finePayment);
    }

    // DELETE: api/FinePayments/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFinePayment(long id)
    {
        var finePayment = await (
            from fp
            in _context.FinePayments
            where fp.Id == id
            select fp
        ).FirstAsync<FinePayment>();

        _context.FinePayments.Remove(finePayment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

        // GET: api/FinePayments/search
    [HttpGet("search")]
    public async Task<ActionResult<FinePayment>> SearchFinePayment(string? caseRef, string? onlineAccountRef)
    {
        if (caseRef == null || onlineAccountRef == null)
        {
            return NotFound();
        }
        
        var finePayment = await (
            from fp
            in _context.FinePayments
            where fp.CaseReference == caseRef && fp.OnlineAccountReference == onlineAccountRef
            select fp
        ).FirstAsync<FinePayment>();

        if (finePayment == null) return NotFound();
        return finePayment;
    }
}