using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MusicShopApp.Core.Contracts;
using MusicShopApp.Infrastructure.Data.Domain;
using MusicShopApp.Models.Brand;
using MusicShopApp.Models.Category;
using MusicShopApp.Models.Product;


namespace MusicShopApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller

    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._brandService = brandService;
        }

        [AllowAnonymous]
        public ActionResult Index(string searchStringCategoryName, string searchStringBrandName, string searchStringProductName)
        {
            List<ProductIndexVM> products = _productService.GetProducts(searchStringCategoryName, searchStringBrandName, searchStringProductName)
                .Select(product => new ProductIndexVM
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.BrandName,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.CategoryName,
                    Description = product.Description,  
                    Picture = product.Picture,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Discount = product.Discount

                }).ToList();
            return this.View(products);
        }

        // GET: ProductController/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Product item = _productService.GetPRoductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDetailsVM product = new ProductDetailsVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                BrandId = item.BrandId,
                BrandName = item.Brand.BrandName,
                CategoryId = item.CategoryId,
                Description = item.Description,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            var product = new ProductCreateVM();
            product.Brands = _brandService.GetBrands()
                .Select(x => new BrandPairVM()
                {
                    Id = x.Id,
                    Name = x.BrandName
                }).ToList();
            product.Categories = _categoryService.GetCategories()
                .Select(x => new CategoryPairVM()
                {
                    Id = x.Id,
                    Name = x.CategoryName
                }).ToList();
            return View(product);

        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ProductCreateVM product)
        {
            if (ModelState.IsValid)
            {
                var createdId = _productService.Create(product.ProductName, product.BrandId, product.CategoryId,
                    product.Picture, product.Quantity, product.Price, product.Discount, product.Description);
                if (createdId)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            return View();

        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = _productService.GetPRoductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ProductEditVM updatedProduct = new ProductEditVM()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                BrandId = product.BrandId,
                Description = product.Description,  
                CategoryId = product.CategoryId,
                Picture = product.Picture,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount
            };
            updatedProduct.Brands = _brandService.GetBrands()
                .Select(b => new BrandPairVM()
                {
                    Id = b.Id,
                    Name = b.BrandName
                }).ToList();

            updatedProduct.Categories = _categoryService.GetCategories()
                .Select(x => new CategoryPairVM()
                {
                    Id = x.Id,
                    Name = x.CategoryName
                }).ToList();
            return View(updatedProduct);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEditVM product)
        {
            if (ModelState.IsValid)
            {
                var updated = _productService.Update(id, product.ProductName,
                 product.BrandId, product.CategoryId,
                 product.Picture, product.Quantity,
                 product.Price, product.Discount, product.Description);
                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
            }
            return View(product);
        }
        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product item = _productService.GetPRoductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDeleteVM product = new ProductDeleteVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                BrandId = item.BrandId,
                BrandName = item.Brand.BrandName,
                Description = item.Description,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _productService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("Success");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
