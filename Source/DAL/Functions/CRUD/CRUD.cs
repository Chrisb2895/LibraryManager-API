﻿using DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Functions.CRUD
{
    internal class CRUD : ICRUD
    {
        public CRUD()
        {
        }

        public async Task<T> Create<T>(T objectForDB) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    await context.AddAsync<T>(objectForDB);
                    await context.SaveChangesAsync();
                    return objectForDB;
                }
            }
            catch 
            {
                throw;
            }
        }

        public async Task<T> Read<T>(long entityID) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    T result = await context.FindAsync<T>(entityID);
                    return result;
                }
            }
            catch 
            {
                throw;
            }
        }

        public async Task<List<T>> ReadAll<T>() where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    List<T> result = await context.Set<T>().ToListAsync();
                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> Update<T>(T entityToUpdate, long entityID) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    var objectFound = await context.FindAsync<T>(entityID);
                    if(objectFound != null)
                    {
                        context.Entry(objectFound).CurrentValues.SetValues(entityToUpdate);
                        await context.SaveChangesAsync();
                    }
                    return objectFound;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete<T>(long entityID) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    var objectFound = await context.FindAsync<T>(entityID);
                    if (objectFound != null)
                    {
                        context.Remove(objectFound);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }


    }
}
