using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Infrastuture.DataBase;

namespace StudentManagementSystem.Infrastuture.Repository;

public class GenricRepository<T>(AppDbContext dbContext) : IGenricRepository<T> where T : class
{
    //private readonly AppDbContext _dbContext = dbContext;
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();
    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        await Task.FromResult(_dbSet.Remove(entity));
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id).AsTask();
    }

    public async Task UpdateAsync(T entity)
    {
        await Task.FromResult(_dbSet.Update(entity));
    }
}
