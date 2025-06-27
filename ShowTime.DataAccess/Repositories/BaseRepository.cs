﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShowTime.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ShowTimeDbContext _context;
        public BaseRepository(ShowTimeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding entity: {ex.Message}", ex);
            }
        }
        public virtual async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FindAsync(id);
                if (entity == null)
                {
                    throw new Exception($"Entity with ID {id} not found.");
                }
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting entity with ID {id}: {ex.Message}", ex);
            }
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all entities.", ex);
            }
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving entity with ID {id}: {ex.Message}", ex);
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating entity: {ex.Message}", ex);
            }
        }
    }
}
