using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NorthwindAPI.Models;

namespace NorthwindAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        /// <summary>
        /// Get all the categories objects from the database.
        /// </summary>
        /// <returns>List of category object.</returns>
        [HttpGet]
        public HttpResponseMessage GetCategories()
        {
            //try
            //{
                using (NorthwindEntities northwindContext = new NorthwindEntities())
                {
                    List<ViewModels.Category> categories =
                        (from modelCat in northwindContext.Categories
                         select new ViewModels.Category()
                         {
                             CategoryID = modelCat.CategoryID,
                             CategoryName = modelCat.CategoryName,
                             Description = modelCat.Description,
                             Image = modelCat.Picture,
                         }).ToList();

                    return
                        Request.CreateResponse(HttpStatusCode.OK, categories);
                }
            //}
            //catch (Exception ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            //}
        }

        [HttpGet]
        public HttpResponseMessage GetCategory(int catId)
        {
            try
            {
                using (NorthwindEntities northwindContext = new NorthwindEntities())
                {
                    Category modelCat = northwindContext.Categories
                        .FirstOrDefault(cat => cat.CategoryID == catId);

                    ViewModels.Category category = new ViewModels.Category()
                    {
                        CategoryID = modelCat.CategoryID,
                        CategoryName = modelCat.CategoryName,
                        Description = modelCat.Description,
                        //Image = modelCat.Picture,
                    };

                    if (category == null)
                    {
                        return
                            Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            string.Format("Category with ID = {0}, not found", catId));
                    }
                    else
                    {
                        return
                            Request.CreateResponse(HttpStatusCode.OK, category);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
