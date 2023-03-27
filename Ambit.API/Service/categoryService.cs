using Microsoft.Extensions.Options;
using Navrang.Billing.AppCore.Common;
using Navrang.Billing.AppCore.EntityModels;
using Navrang.Billing.AppCore.Models;
using Ambit.API.Helpers;
using System;
using System.Collections.Generic;

namespace Navrang.Services
{
	public class categoryService : ICategoryservice
	{
          private readonly AppSettings _appSettings;
          private readonly IRepoSupervisor _repoSupervisor;
          public categoryService(IOptions<AppSettings> appSettings, IRepoSupervisor repoSupervisor)
          {
               _appSettings = appSettings.Value;
               _repoSupervisor = repoSupervisor;
          }

          public IEnumerable<CategoryEntityModel> GetAllCategories()
          {
               return _repoSupervisor.Category.GetAllCategories();
          }

          public CategoryEntityModel GetCategoryByID(int Id)
          {
               return _repoSupervisor.Category.GetCategoryById(Id);
          }

          public bool AddNewCategory(CategoryEntityModel CategoryEntityModel)
          {
			try
			{
                   
                    if (_repoSupervisor.Category.AddNewCategory(CategoryEntityModel))
                    {
                         _repoSupervisor.Complete();
                         return true;
                    }
               }
			catch (Exception ex)
			{
                    var error = ex.InnerException;
			}
			
               return false;
          }
          public bool UpdateCategory(CategoryEntityModel CategoryEntityModel)
          {
               if (_repoSupervisor.Category.UpdateCategory(CategoryEntityModel))
               {
                    _repoSupervisor.Complete();
                    return true;
               }
               return false;
          }

          public bool DeleteCategory(long id)
          {
               if (_repoSupervisor.Category.DeleteCategory(id))
               {
                    _repoSupervisor.Complete();
                    return true;
               }
               return false;
          }

          public bool ActiveInactiveCategory(long id, bool status)
          {
               if (_repoSupervisor.Category.ActiveInactiveCategory(id,status))
               {
                    _repoSupervisor.Complete();
                    return true;
               }
               return false;
          }

          public IEnumerable<CategoryEntityModel> GetCategoriesByKey(string key)
          {
               return _repoSupervisor.Category.GetCategoriesByKey(key);
          }
     }
}
