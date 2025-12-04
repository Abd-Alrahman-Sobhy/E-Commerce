using ECommerce.Context;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Services.Implementation;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
	private readonly AppDbContext _context;
	private readonly DbSet<T> _dbSet;

	public GenericRepository(AppDbContext context)
	{
		_dbSet = context.Set<T>();
		_context = context;
	}

	public async Task<T> AddAsync(T entity)
	{
		await _dbSet.AddAsync(entity);
		await _context.SaveChangesAsync();
		return entity;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var entity = await _dbSet.FindAsync(id);
		if (entity == null) return false;

		_dbSet.Remove(entity);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
	{
		return await _dbSet.Where(predicate).ToListAsync();
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task<T?> GetByIdAsync(int id)
	{
		return await _dbSet.FindAsync(id);
	}

	public async Task<T> UpdateAsync(T entity)
	{
		_dbSet.Update(entity);
		await _context.SaveChangesAsync();
		return entity;
	}

	public async Task<bool> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}
}
