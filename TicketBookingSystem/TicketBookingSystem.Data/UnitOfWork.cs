using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TicketBookingSystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        public void Dispose()
        {  
            _context.Dispose();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        // protected readonly DbContext _context;
        // public UnitOfWork(DbContext context) => _context = context;
        // public void Dispose() => _context?.Dispose();
        // public void Save() => _context?.SaveChanges();
    }
}
