using Dal.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
       
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {

            return View();
        }


        public async Task<IActionResult> Upsert(int? id)
        {
            // Product product = new Product();

            ProductVM productVM = new()
            {
                Product = new(),
                CategorySelectList = _unitOfWork.Category.GetAllInclude().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
            };

            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = await _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
                // update product
                return View(productVM);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductVM obj)
        {
         
            if (ModelState.IsValid)
            {
           
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }

                await _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        } 

        #region API CALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productList = await _unitOfWork.Product.GetAll(includeProperty: "Category");
            return Json(new { data = productList });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var obj = await _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
             
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }
        #endregion
    }
}
