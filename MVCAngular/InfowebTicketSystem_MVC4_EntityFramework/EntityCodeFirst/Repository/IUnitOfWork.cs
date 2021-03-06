﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityCodeFirst.Repository
{
    public interface  IUnitOfWork: IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void SaveChanges();
    }
}
