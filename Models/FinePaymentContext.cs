using Microsoft.EntityFrameworkCore;

namespace FinePaymentAPI.Models;

public class FinePaymentContext : DbContext
{
    public FinePaymentContext(DbContextOptions<FinePaymentContext> options)
        : base(options)
    {
    }

    public DbSet<FinePayment> FinePayments { get; set; }  = null!;
}