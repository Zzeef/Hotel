using BLL.DTO;
using BLL.Interfaces;
using Hotel.WEB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.WEB.Controllers
{
    public class AdminController : Controller
    {
        readonly IRoomService roomService;
        readonly ICategoryService categoryService;
        readonly ISettlementService settlementService;

        public AdminController(IRoomService roomService, ICategoryService categoryService, ISettlementService settlementService)
        {
            this.roomService = roomService;
            this.categoryService = categoryService;
            this.settlementService = settlementService;
        }

        public IActionResult Rooms()
        {
            RoomCategoryModel roomCategory = new RoomCategoryModel()
            {
                Rooms = roomService.GetRooms(),
                Categories = categoryService.GetCategories()
            };

            return View(roomCategory);
        }

        public IActionResult CreateRoom()
        {
            RoomCreateModel roomCreate = new RoomCreateModel()
            {
                CategoriesList = categoryService.GetCategories()
            };

            return View(roomCreate);
        }

        [HttpPost]
        public IActionResult CreateRoom(RoomCreateModel roomCreateModel)
        {
            roomCreateModel.Room.Id = Guid.NewGuid();
            var result = roomService.AddRoom(roomCreateModel.Room);
            if (result.Succedeed) 
            {
                return RedirectToAction("Rooms");
            }

            ModelState.AddModelError("", "");
            ViewData["Message"] = result.Message;
            roomCreateModel.CategoriesList = categoryService.GetCategories();

            return View(roomCreateModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditRoom(Guid id)
        {
            RoomCreateModel roomCreate = new RoomCreateModel()
            {
                Room = await roomService.FindRoomByIdAsync(id),
                CategoriesList = categoryService.GetCategories()
            };

            return View(roomCreate);
        }

        [HttpPost]
        public IActionResult EditRoom(RoomCreateModel roomCreateModel)
        {
            var result = roomService.UpdateRoom(roomCreateModel.Room);
            if (result.Succedeed) 
            {
                return RedirectToAction("Rooms");
            }

            ModelState.AddModelError("", "");
            ViewData["Message"] = result.Message;
            roomCreateModel.CategoriesList = categoryService.GetCategories();

            return View(roomCreateModel);
        }

        [HttpPost]
        public IActionResult DeleteRoom(RoomDTO item)
        {
            var result = roomService.DeleteRoom(item.Id);
            if (result.Succedeed) 
            {
                return RedirectToAction("Rooms");
            }
            return RedirectToAction("Rooms");
        }

        public IActionResult Categories()
        {
            return View(categoryService.GetCategories());
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryDTO item)
        {
            var result = categoryService.AddCategory(item);
            if (result.Succedeed) 
            {
                return RedirectToAction("Categories");
            }

            ModelState.AddModelError("", "");
            ViewData["Message"] = result.Message;

            return View();         
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(Guid id)
        {
            return View(await categoryService.FindCategoryByIdAsync(id));
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryDTO item)
        {
            var result = categoryService.UpdateCategory(item);
            if (result.Succedeed) 
            {
                return RedirectToAction("Categories");
            }

            ModelState.AddModelError("", "");
            ViewData["Message"] = result.Message;

            return View();
        }

        [HttpPost]
        public IActionResult DeleteCategory(CategoryDTO item)
        {
            var result = categoryService.DeleteCategory(item.Id);
            if (result.Succedeed) 
            {
                return RedirectToAction("Categories");
            }
            return RedirectToAction("Categories");
        }

        public IActionResult BookingList()
        {
            return View();
        }
    }
}
