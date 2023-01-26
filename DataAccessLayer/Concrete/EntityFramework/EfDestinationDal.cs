﻿using EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using CoreLayer.DataAccessLayer.Concrete.EntityFramework;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfDestinationDal : EfEntityRepositoryBase<Destination, DatabaseContext>, IDestinationDal
    {

    }
}